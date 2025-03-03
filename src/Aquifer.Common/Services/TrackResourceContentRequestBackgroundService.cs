using System.Threading.Channels;
using Aquifer.Common.Messages;
using Aquifer.Common.Messages.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Aquifer.Common.Services;

public sealed class TrackResourceContentRequestBackgroundService(
    Channel<TrackResourceContentRequestMessage> _channel,
    IQueueClientFactory _queueClientFactory,
    ILogger<TrackResourceContentRequestBackgroundService> _logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (await _channel.Reader.WaitToReadAsync(stoppingToken))
            {
                try
                {
                    var message = await _channel.Reader.ReadAsync(stoppingToken);
                    await PublishTrackResourceContentRequestMessageCoreAsync(message, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Failed to publish a {nameof(TrackResourceContentRequestMessage)}. Gracefully skipping.");
                }
            }
        }
        catch (OperationCanceledException ocex)
        {
            _logger.LogWarning(ocex, $"The {nameof(TrackResourceContentRequestBackgroundService)} has been cancelled.");
        }
    }

    private async Task PublishTrackResourceContentRequestMessageCoreAsync(
        TrackResourceContentRequestMessage message,
        CancellationToken ct)
    {
        var queueClient = await _queueClientFactory.GetQueueClientAsync(Queues.TrackResourceContentRequest, ct);
        await queueClient.SendMessageAsync(message, cancellationToken: ct);
    }
}