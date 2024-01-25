﻿using System.Text.Json;
using Aquifer.Common.Jobs.Messages;
using Aquifer.Common.Services;
using Aquifer.Public.API.Configuration;
using Azure.Storage.Queues;
using Microsoft.Extensions.Options;

namespace Aquifer.Public.API.Services;

public interface ITrackResourceContentRequestService
{
    Task TrackResourceContentRequest(List<int> resourceIds, HttpContext httpContext);
}

public class TrackResourceContentRequestService : ITrackResourceContentRequestService
{
    private readonly QueueClient _client;

    public TrackResourceContentRequestService(IOptions<ConfigurationOptions> options,
        IAzureClientService azureClientService)
    {
        var clientOptions = new QueueClientOptions { MessageEncoding = QueueMessageEncoding.Base64 };
        if (options.Value.ConnectionStrings.AzureStorageAccount.StartsWith("http"))
        {
            _client = new QueueClient(
                new Uri(
                    $"{options.Value.ConnectionStrings.AzureStorageAccount}/{options.Value.JobQueues.TrackResourceContentRequestQueue}"),
                azureClientService.GetCredential(),
                clientOptions);
        }
        else
        {
            _client = new QueueClient(options.Value.ConnectionStrings.AzureStorageAccount,
                options.Value.JobQueues.TrackResourceContentRequestQueue,
                clientOptions);
        }

        _client.CreateIfNotExists();
    }

    public async Task TrackResourceContentRequest(List<int> resourceIds, HttpContext httpContext)
    {
        var message = new TrackResourceContentRequestMessage
        {
            ResourceContentIds = resourceIds,
            IpAddress = GetClientIp(httpContext)
        };
        var serializedMessage = JsonSerializer.Serialize(message);
        await _client.SendMessageAsync(serializedMessage);
    }

    private string GetClientIp(HttpContext httpContext)
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