using System.Data;
using System.Text.Json;
using Aquifer.Common.Clients.Http.IpAddressLookup;
using Aquifer.Common.Jobs.Messages;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Aquifer.Jobs;

public class TrackResourceContentRequestJob(
    ILogger<TrackResourceContentRequestJob> logger,
    AquiferDbContext dbContext,
    IIpAddressLookupHttpClient ipAddressClient)
{
    [Function(nameof(TrackResourceContentRequestJob))]
    public async Task Run(
        [QueueTrigger("%JobQueues:TrackResourceContentRequestQueue%", Connection = "AzureWebJobsStorage")] QueueMessage message,
        CancellationToken ct)
    {
        try
        {
            var trackingMetadata = JsonSerializer.Deserialize<TrackResourceContentRequestMessage>(message.MessageText);
            if (trackingMetadata == null)
            {
                logger.LogError("Failed to deserialize the message: {MessageText}", message.MessageText);
                return;
            }

            await LookupIpAddressAsync(trackingMetadata, ct);
            await dbContext.ResourceContentRequests
                .AddRangeAsync(trackingMetadata.ResourceContentIds.Select(x =>
                    new ResourceContentRequestEntity
                    {
                        ResourceContentId = x,
                        IpAddress = trackingMetadata.IpAddress,
                        SubscriptionName = trackingMetadata.SubscriptionName,
                        EndpointId = trackingMetadata.EndpointId,
                        Source = trackingMetadata.Source,
                        UserId = trackingMetadata.UserId,
                        Created = message.InsertedOn?.UtcDateTime ?? DateTime.UtcNow
                    }),
                ct);

            await dbContext.SaveChangesAsync(ct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while processing the message: {MessageText}", message.MessageText);
            throw;
        }
    }

    private async Task LookupIpAddressAsync(TrackResourceContentRequestMessage trackingMetadata, CancellationToken ct)
    {
        try
        {
            if (trackingMetadata.IpAddress.Length < 7)
            {
                return;
            }

            using (var transaction = await dbContext.Database.BeginTransactionAsync(IsolationLevel.Serializable, ct))
            {
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

                        await dbContext.IpAddressData.AddAsync(newRecord, ct);
                        await dbContext.SaveChangesAsync(ct);
                        await transaction.CommitAsync(ct);
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