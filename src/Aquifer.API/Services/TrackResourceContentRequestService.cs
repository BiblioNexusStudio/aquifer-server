using Aquifer.API.Configuration;
using Aquifer.Common.Jobs.Messages;
using Azure.Identity;
using Azure.Storage.Queues;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Aquifer.API.Services;

public interface ITrackResourceContentRequestService
{
    Task TrackResourceContentRequest(List<int> resourceIds, HttpContext httpContext);
}

public class TrackResourceContentRequestService : ITrackResourceContentRequestService
{
    private readonly QueueClient _client;

    public TrackResourceContentRequestService(IOptions<ConfigurationOptions> options)
    {
        var clientOptions = new QueueClientOptions { MessageEncoding = QueueMessageEncoding.Base64 };
        if (options.Value.ConnectionStrings.AzureStorageAccount.StartsWith("http"))
        {
            _client = new QueueClient(
                    new Uri($"{options.Value.ConnectionStrings.AzureStorageAccount}/{options.Value.JobQueues.TrackResourceContentRequestQueue}"),
                    new DefaultAzureCredential(),
                    clientOptions);
        }
        else
        {
            _client = new QueueClient(
                    options.Value.ConnectionStrings.AzureStorageAccount,
                    options.Value.JobQueues.TrackResourceContentRequestQueue,
                    clientOptions);
        }
        _client.CreateIfNotExists();
    }

    public async Task TrackResourceContentRequest(List<int> resourceIds, HttpContext httpContext)
    {
        var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        var message = new TrackResourceContentRequestMessage
        {
            ResourceContentIds = resourceIds,
            IpAddress = ipAddress
        };
        var serializedMessage = JsonSerializer.Serialize(message);
        await _client.SendMessageAsync(serializedMessage);
    }
}