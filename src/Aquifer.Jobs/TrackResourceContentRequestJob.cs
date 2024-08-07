using System.Text.Json;
using Aquifer.Common.Jobs.Messages;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Aquifer.Jobs;

public class TrackResourceContentRequestJob(ILogger<TrackResourceContentRequestJob> _logger, AquiferDbContext _dbContext)
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
                _logger.LogError("Failed to deserialize the message: {MessageText}", message.MessageText);
                return;
            }

            foreach (var resourceContentId in trackingMetadata.ResourceContentIds)
            {
                await _dbContext.ResourceContentRequests.AddAsync(new ResourceContentRequestEntity
                    {
                        ResourceContentId = resourceContentId,
                        IpAddress = trackingMetadata.IpAddress,
                        SubscriptionName = trackingMetadata.SubscriptionName,
                        EndpointId = trackingMetadata.EndpointId,
                        Source = trackingMetadata.Source,
                        UserId = trackingMetadata.UserId,
                        Created = message.InsertedOn?.UtcDateTime ?? DateTime.UtcNow
                    },
                    ct);
            }

            await _dbContext.SaveChangesAsync(ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the message: {MessageText}", message.MessageText);
            throw;
        }
    }
}