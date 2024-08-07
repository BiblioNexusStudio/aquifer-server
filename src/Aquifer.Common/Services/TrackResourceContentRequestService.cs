using System.Text.Json;
using Aquifer.Common.Jobs.Messages;
using Azure.Storage.Queues;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Aquifer.Common.Services;

public interface ITrackResourceContentRequestService
{
    Task TrackAsync(HttpContext httpContext, string endpointId, int resourceContentId);
    Task TrackAsync(HttpContext httpContext, string endpointId, List<int> resourceContentIds);
}

public class TrackResourceContentRequestService : ITrackResourceContentRequestService
{
    private readonly QueueClient _client;

    public TrackResourceContentRequestService(IConfiguration configuration, IAzureClientService azureClientService)
    {
        var clientOptions = new QueueClientOptions { MessageEncoding = QueueMessageEncoding.Base64 };
        var (connectionString, queue) = GetClientConfigurations(configuration);

        _client = connectionString.StartsWith("http")
            ? new QueueClient(new Uri($"{connectionString}/{queue}"), azureClientService.GetCredential(), clientOptions)
            : new QueueClient(connectionString, queue, clientOptions);

        _client.CreateIfNotExists();
    }

    public async Task TrackAsync(HttpContext httpContext, string endpointId, int resourceContentId)
    {
        await TrackAsync(httpContext, endpointId, [resourceContentId]);
    }

    public async Task TrackAsync(HttpContext httpContext, string endpointId, List<int> resourceContentIds)
    {
        var message = new TrackResourceContentRequestMessage
        {
            Source = httpContext.Request.Headers["bn-source"],
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