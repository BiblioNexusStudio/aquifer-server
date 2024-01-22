using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Aquifer.Functions.Messages;
using Aquifer.Data;
using Aquifer.Data.Entities;

namespace Aquifer.Functions
{
    public class TrackResourceContentRequestTrigger
    {
        private readonly ILogger<TrackResourceContentRequestTrigger> _logger;
        private readonly AquiferDbContext _dbContext;

        public TrackResourceContentRequestTrigger(ILogger<TrackResourceContentRequestTrigger> logger, AquiferDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [Function(nameof(TrackResourceContentRequestTrigger))]
        public async Task Run([QueueTrigger("myqueue-items", Connection = "")] QueueMessage message, CancellationToken stoppingToken)
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
                        IpAddress = trackingMetadata.IpAddress
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
}
