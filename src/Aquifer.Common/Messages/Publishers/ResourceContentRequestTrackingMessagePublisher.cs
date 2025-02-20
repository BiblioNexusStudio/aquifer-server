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

public class ResourceContentRequestTrackingMessagePublisher(IQueueClientFactory _queueClientFactory)
    : IResourceContentRequestTrackingMessagePublisher
{
    public async Task PublishTrackResourceContentRequestMessageAsync(
        TrackResourceContentRequestMessage message,
        CancellationToken ct)
    {
        var queueClient = await _queueClientFactory.GetQueueClientAsync(Queues.TrackResourceContentRequest, ct);
        await queueClient.SendMessageAsync(message, cancellationToken: ct);
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
        httpContext.Items.TryGetValue(Constants.HttpContextItemCachedApiKey, out var maybeCachedApiKey);

        var message = new TrackResourceContentRequestMessage
        {
            Source = string.IsNullOrEmpty(sourceHeader) ? source : sourceHeader,
            IpAddress = GetClientIp(httpContext),
            SubscriptionName = maybeCachedApiKey is CachedApiKey cachedApiKey ? cachedApiKey.ApiKey : null,
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