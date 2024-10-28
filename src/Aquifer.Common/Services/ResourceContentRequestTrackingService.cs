using Aquifer.Common.Jobs;
using Aquifer.Common.Jobs.Messages;
using Microsoft.AspNetCore.Http;

namespace Aquifer.Common.Services;

public interface IResourceContentRequestTrackingService
{
    Task TrackAsync(HttpContext httpContext, int resourceContentId, string endpointId, string? source = null, CancellationToken cancellationToken = default);
    Task TrackAsync(HttpContext httpContext, List<int> resourceContentIds, string endpointId, string? source = null, CancellationToken cancellationToken = default);
}

public class ResourceContentRequestTrackingService(IQueueClientFactory _queueClientFactory)
    : IResourceContentRequestTrackingService
{
    public async Task TrackAsync(HttpContext httpContext, int resourceContentId, string endpointId, string? source = null, CancellationToken cancellationToken = default)
    {
        await TrackAsync(httpContext, [resourceContentId], endpointId, source, cancellationToken);
    }

    public async Task TrackAsync(HttpContext httpContext, List<int> resourceContentIds, string endpointId, string? source = null, CancellationToken cancellationToken = default)
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
            SubscriptionName = httpContext.Request.Headers["bn-subscription-name"],
            EndpointId = endpointId,
            UserId = httpContext.Request.Headers["bn-user-id"],
            ResourceContentIds = resourceContentIds
        };

        var queueClient = await _queueClientFactory.GetQueueClientAsync(Queues.TrackResourceContentRequest, cancellationToken);
        await queueClient.SendMessageAsync(message, cancellationToken: cancellationToken);
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