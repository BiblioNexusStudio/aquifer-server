using Aquifer.Common.Clients.Http.IpAddressLookup;
using Aquifer.Common.Messages;
using Aquifer.Common.Messages.Models;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Aquifer.Jobs.Subscribers;

public class TrackResourceContentRequestMessageSubscriber(
    ILogger<TrackResourceContentRequestMessageSubscriber> logger,
    AquiferDbContext dbContext,
    IIpAddressLookupHttpClient ipAddressClient)
{
    [Function(nameof(TrackResourceContentRequestMessageSubscriber))]
    public async Task Run(
        [QueueTrigger(Queues.TrackResourceContentRequest)] QueueMessage queueMessage,
        CancellationToken ct)
    {
        await queueMessage.ProcessAsync<TrackResourceContentRequestMessage, TrackResourceContentRequestMessageSubscriber>(
            logger,
            nameof(TrackResourceContentRequestMessageSubscriber),
            ProcessAsync,
            ct);
    }

    private async Task ProcessAsync(QueueMessage queueMessage, TrackResourceContentRequestMessage message, CancellationToken ct)
    {
        await LookupIpAddressAsync(message, ct);
        await dbContext.ResourceContentRequests.AddRangeAsync(
            message.ResourceContentIds
                .Select(x => new ResourceContentRequestEntity
                {
                    ResourceContentId = x,
                    IpAddress = message.IpAddress,
                    SubscriptionName = message.SubscriptionName,
                    EndpointId = message.EndpointId,
                    Source = message.Source,
                    UserId = message.UserId,
                    Created = queueMessage.InsertedOn?.UtcDateTime ?? DateTime.UtcNow
                }),
            ct);

        await dbContext.SaveChangesAsync(ct);

        logger.LogInformation(
            "Resource tracking successful for Endpoint ID {EndpointId} for IP Address {IpAddress}.",
            message.EndpointId,
            message.IpAddress);
    }

    private async Task LookupIpAddressAsync(TrackResourceContentRequestMessage trackingMetadata, CancellationToken ct)
    {
        try
        {
            if (trackingMetadata.IpAddress.Length < 7)
            {
                return;
            }

            var ipDataRecord = await dbContext.IpAddressData
                .AsTracking()
                .SingleOrDefaultAsync(x => x.IpAddress == trackingMetadata.IpAddress, ct);
            if (ipDataRecord is null)
            {
                var ipData = await ipAddressClient.LookupIpAddressAsync(trackingMetadata.IpAddress, ct);
                if (ipData.City is not null && ipData.Country is not null && ipData.Region is not null)
                {
                    var newRecord = new IpAddressData
                    {
                        IpAddress = trackingMetadata.IpAddress,
                        City = ipData.City,
                        Region = ipData.Region,
                        Country = ipData.Country
                    };

                    try
                    {
                        await dbContext.IpAddressData.AddAsync(newRecord, ct);
                        await dbContext.SaveChangesAsync(ct);
                    }
                    catch (DbUpdateException)
                    {
                        // Don't care about this since it's a concurrency issue. Need to remove it though or the next Save
                        // will pick it up and blow up.
                        dbContext.IpAddressData.Remove(newRecord);
                    }
                }
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, "IP Address Lookup failed");
        }
    }
}