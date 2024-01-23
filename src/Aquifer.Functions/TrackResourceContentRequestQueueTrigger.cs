using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Aquifer.Functions.Messages;
using Aquifer.Data;
using Aquifer.Data.Entities;

namespace Aquifer.Functions;

public class TrackResourceContentRequestQueueTrigger(ILogger<TrackResourceContentRequestQueueTrigger> _logger, AquiferDbContext _dbContext)
{

    [Function(nameof(TrackResourceContentRequestQueueTrigger))]
    public async Task Run([QueueTrigger("%JobQueues:TrackResourceContentRequestQueue%", Connection = "AzureWebJobsStorage")]
        QueueMessage message, CancellationToken stoppingToken)
    {
        try
        {
            var trackingMetadata = JsonSerializer.Deserialize<TrackResourceContentRequestMessage>(message.MessageText);
            if (trackingMetadata == null)
            {
                _logger.LogError("Failed to deserialize message");
                return;
            }

            foreach (var resourceContentId in trackingMetadata.ResourceContentIds)
            {
                _dbContext.ResourceContentRequests.Add(new ResourceContentRequestEntity
                {
                    ResourceContentId = resourceContentId,
                    IpAddress = trackingMetadata.IpAddress,
                    Created = message.InsertedOn?.UtcDateTime ?? DateTime.UtcNow
                });
            }
            await _dbContext.SaveChangesAsync(stoppingToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the message");
        }
    }
}
