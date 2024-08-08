using System.Text.Json;
using Aquifer.Common.Jobs.Messages;
using Azure.Storage.Queues;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Aquifer.Common.Services;

public interface IResourceContentRequestTrackingService
{
    Task TrackAsync(HttpContext httpContext, int resourceContentId, string endpointId, string? source = null);
    Task TrackAsync(HttpContext httpContext, List<int> resourceContentIds, string endpointId, string? source = null);
}

public class ResourceContentRequestTrackingService : IResourceContentRequestTrackingService
{
    private readonly QueueClient _client;

    public ResourceContentRequestTrackingService(IConfiguration configuration, IAzureClientService azureClientService)
    {
        var clientOptions = new QueueClientOptions { MessageEncoding = QueueMessageEncoding.Base64 };
        var (connectionString, queue) = GetClientConfigurations(configuration);

        _client = connectionString.StartsWith("http")
            ? new QueueClient(new Uri($"{connectionString}/{queue}"), azureClientService.GetCredential(), clientOptions)
            : new QueueClient(connectionString, queue, clientOptions);

        _client.CreateIfNotExists();
    }

    public async Task TrackAsync(HttpContext httpContext, int resourceContentId, string endpointId, string? source = null)
    {
        await TrackAsync(httpContext, [resourceContentId], endpointId, source);
    }

    public async Task TrackAsync(HttpContext httpContext, List<int> resourceContentIds, string endpointId, string? source = null)
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

        var serializedMessage = JsonSerializer.Serialize(message);
        await _client.SendMessageAsync(serializedMessage);
    }

    private static (string connectionString, string queue) GetClientConfigurations(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("AzureStorageAccount");
        var queue = configuration.GetSection("JobQueues").GetValue<string>("TrackResourceContentRequestQueue");

        ArgumentException.ThrowIfNullOrEmpty(connectionString);
        ArgumentException.ThrowIfNullOrEmpty(queue);

        return (connectionString, queue);
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