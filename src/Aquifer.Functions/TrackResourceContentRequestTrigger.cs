using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Aquifer.Functions
{
    public class TrackResourceContentRequestTrigger
    {
        private readonly ILogger<TrackResourceContentRequestTrigger> _logger;

        public TrackResourceContentRequestTrigger(ILogger<TrackResourceContentRequestTrigger> logger)
        {
            _logger = logger;
        }

        [Function(nameof(TrackResourceContentRequestTrigger))]
        public void Run([QueueTrigger("myqueue-items", Connection = "")] QueueMessage message)
        {
            try
            {
                foreach (var resourceContentId in resourceContentIds)
                {
                    dbContext.ResourceContentRequests.Add(new ResourceContentRequestEntity
                    {
                        ResourceContentId = resourceContentId,
                        IpAddress = trackingMetadata.IpAddress
                    });
                }
                await dbContext.SaveChangesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.ToString()}");
                _telemetry.TrackException(ex);
            }
        }
    }
}
