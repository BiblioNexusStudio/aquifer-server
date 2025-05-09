using System.Data;
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
    private const string TrackResourceContentRequestMessageSubscriberFunctionName = "TrackResourceContentRequestMessageSubscriber";

    [Function(TrackResourceContentRequestMessageSubscriberFunctionName)]
    public async Task RunAsync(
        [QueueTrigger(Queues.TrackResourceContentRequest)]
        QueueMessage queueMessage,
        CancellationToken ct)
    {
        await queueMessage.ProcessAsync<TrackResourceContentRequestMessage, TrackResourceContentRequestMessageSubscriber>(
            logger,
            TrackResourceContentRequestMessageSubscriberFunctionName,
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
                    Created = queueMessage.InsertedOn?.UtcDateTime ?? DateTime.UtcNow,
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

            // Use a serializable transaction with UPDLOCK on SELECT to exclusive lock on row reads (including empty range).
            // This locks other threads from SELECTing a null value in the DB for the given IP and thus prevents duplicate calls to the
            // IP service for the same IP address at the same time.
            // Using a transaction requires explicitly defining an execution strategy so that EF knows exactly what to replay in the event
            // of a transient failure.
            await dbContext.Database
                .CreateExecutionStrategy()
                .ExecuteAsync(async () =>
                {
                    await using var transaction = await dbContext.Database.BeginTransactionAsync(IsolationLevel.Serializable, ct);

                    var ipDataRecord = await dbContext.Database
                        .SqlQuery<IpAddressDataEntity>(
                            $"""
                            SELECT
                                IpAddress,
                                City,
                                Region,
                                Country,
                                Created
                            FROM IpAddressData WITH (UPDLOCK)
                            WHERE IpAddress = {trackingMetadata.IpAddress}
                            """)
                        .SingleOrDefaultAsync(ct);

                    if (ipDataRecord is null)
                    {
                        var ipData = await ipAddressClient.LookupIpAddressAsync(trackingMetadata.IpAddress, ct);

                        var newRecord = new IpAddressDataEntity
                        {
                            IpAddress = trackingMetadata.IpAddress,
                            City = ipData.City,
                            Region = ipData.Region,
                            Country = ipData.Country,
                        };

                        await dbContext.IpAddressData.AddAsync(newRecord, ct);
                        await dbContext.SaveChangesAsync(ct);
                        await transaction.CommitAsync(ct);
                    }
                });
        }
        catch (Exception e)
        {
            logger.LogError(e, "IP Address Lookup failed");
        }
    }
}