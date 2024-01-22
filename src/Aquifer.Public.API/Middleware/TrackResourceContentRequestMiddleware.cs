﻿using System.Text.Json;
using System.Text.RegularExpressions;
using Aquifer.Functions.Messages;
using Azure.Storage.Queues;

namespace Aquifer.Public.API.Middleware;

public class TrackResourceContentRequestMiddleware
{
    private readonly RequestDelegate _next;
    private readonly QueueClient _jobQueueClient;
    private static readonly Regex _getResourceRegex = new Regex("^/resources/(\\d+)$");

    public TrackResourceContentRequestMiddleware(RequestDelegate next, QueueClient jobQueueClient)
    {
        _next = next;
        _jobQueueClient = jobQueueClient;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        var path = context.Request.Path.Value ?? "";

        if (_getResourceRegex.IsMatch(path))
        {
            var resourceId = int.Parse(_getResourceRegex.Match(path).Groups[1].Value);
            await SendTrackResourceContentRequestMessage(new List<int> { resourceId }, context);
        }
    }

    private async Task SendTrackResourceContentRequestMessage(List<int> resourceIds, HttpContext context)
    {
        var ipAddress = context.Request.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        var message = new TrackResourceContentRequestMessage
        {
            ResourceContentIds = resourceIds,
            IpAddress = ipAddress
        };
        var serializedMessage = JsonSerializer.Serialize(message);
        await _jobQueueClient.SendMessageAsync(serializedMessage);
    }
}