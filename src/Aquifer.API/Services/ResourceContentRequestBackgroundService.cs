using Aquifer.Data;
using System.Collections.Concurrent;
using Aquifer.Data.Entities;
using Microsoft.ApplicationInsights;

namespace Aquifer.API.Services;

public class ResourceContentRequestBackgroundService(IServiceScopeFactory _serviceScopeFactory, TelemetryClient _telemetry) : BackgroundService
{
    private readonly ConcurrentQueue<TrackingMetadata[]> _resourceContentIdsQueue = new ConcurrentQueue<TrackingMetadata[]>();

    public void TrackResourceContentRequestsInBackground(int[] resourceContentIds, string ipAddress)
    {
        _resourceContentIdsQueue.Enqueue([new TrackingMetadata(resourceContentIds, ipAddress)]);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            while (_resourceContentIdsQueue.TryDequeue(out var trackingMetadatas))
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AquiferDbContext>();

                try
                {
                    foreach (var trackingMetadata in trackingMetadatas)
                    {
                        foreach (var resourceContentId in trackingMetadata.ResourceContentIds)
                        {
                            dbContext.ResourceContentRequests.Add(new ResourceContentRequestEntity
                            {
                                ResourceContentId = resourceContentId,
                                IpAddress = trackingMetadata.IpAddress
                            });
                        }
                    }

                    await dbContext.SaveChangesAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.ToString()}");
                    _telemetry.TrackException(ex);
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
        }
    }

    private record TrackingMetadata(int[] ResourceContentIds, string IpAddress) { }
}