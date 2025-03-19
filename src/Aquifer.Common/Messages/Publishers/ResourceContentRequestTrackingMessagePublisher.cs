using System.Threading.Channels;
using Aquifer.Common.Messages.Models;
using Aquifer.Common.Services.Caching;
using Microsoft.AspNetCore.Http;

namespace Aquifer.Common.Messages.Publishers;

public interface IResourceContentRequestTrackingMessagePublisher
{
    Task PublishTrackResourceContentRequestMessageAsync(
        TrackResourceContentRequestMessage message,
        CancellationToken cancellationToken);

    Task PublishTrackResourceContentRequestMessageAsync(
        HttpContext httpContext,
        int resourceContentId,
        string endpointId,
        string? source,
        CancellationToken cancellationToken);

    Task PublishTrackResourceContentRequestMessageAsync(
        HttpContext httpContext,
        List<int> resourceContentIds,
        string endpointId,
        string? source,
        CancellationToken cancellationToken);
}

/// <summary>
/// This class uses channels to write messages to a background service that will then publish the messages to a queue.
/// This allows the caller to publish messages without waiting for the distributed queue request/response, but it is unreliable because
/// if the API unexpectedly ends/crashes then the queue publish action may not be sent. Therefore, this strategy should only be used for
/// queues where it is acceptable to drop messages on rare occasions.
/// </summary>
public class ResourceContentRequestTrackingMessagePublisher(
    Channel<TrackResourceContentRequestMessage> _channel,
    ICachingApiKeyService _cachingApiKeyService)
    : IResourceContentRequestTrackingMessagePublisher
{
    public async Task PublishTrackResourceContentRequestMessageAsync(
        TrackResourceContentRequestMessage message,
        CancellationToken ct)
    {
        await _channel.Writer.WriteAsync(message, ct);
    }

    public async Task PublishTrackResourceContentRequestMessageAsync(
        HttpContext httpContext,
        int resourceContentId,
        string endpointId,
        string? source,
        CancellationToken cancellationToken)
    {
        await PublishTrackResourceContentRequestMessageAsync(
            httpContext,
            [resourceContentId],
            endpointId,
            source,
            cancellationToken);
    }

    public async Task PublishTrackResourceContentRequestMessageAsync(
        HttpContext httpContext,
        List<int> resourceContentIds,
        string endpointId,
        string? source,
        CancellationToken cancellationToken)
    {
        if (httpContext.Response.StatusCode >= 400)
        {
            return;
        }

        var sourceHeader = httpContext.Request.Headers["bn-source"].ToString();

        var message = new TrackResourceContentRequestMessage
        {
            Source = string.IsNullOrEmpty(sourceHeader) ? source : sourceHeader,
            IpAddress = GetClientIp(httpContext),
            SubscriptionName = _cachingApiKeyService.CurrentApiKey.Organization,
            EndpointId = endpointId,
            UserId = httpContext.Request.Headers["bn-user-id"],
            ResourceContentIds = resourceContentIds
        };

        await PublishTrackResourceContentRequestMessageAsync(message, cancellationToken);
    }

    private static string GetClientIp(HttpContext httpContext)
    {
        if (httpContext.Request.Headers.TryGetValue("X-Forwarded-For", out var forwardedFor))
        {
            var ip = forwardedFor.FirstOrDefault()?.Split(',').FirstOrDefault()?.Trim();
            if (!string.IsNullOrEmpty(ip))
            {
                return ip;
            }
        }

        return httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
    }
}