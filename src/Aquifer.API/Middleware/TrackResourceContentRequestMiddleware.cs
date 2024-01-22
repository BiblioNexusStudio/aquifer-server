using Aquifer.Functions.Messages;
using Azure.Storage.Queues;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Aquifer.API.Middleware;

public class TrackResourceContentRequestMiddleware
{
    private readonly RequestDelegate _next;
    private readonly QueueClient _jobQueueClient;
    private readonly Regex _getResourceRegex = new Regex("^/resources/(\\d+)/content$");
    private readonly Regex _getBatchResourcesRegex = new Regex("^/resources/batch/content/text$");

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
        else if (_getBatchResourcesRegex.IsMatch(path))
        {
            var ids = context.Request.Query["ids[]"].Where(s => !string.IsNullOrEmpty(s)).Select(int.Parse!).ToList();
            await SendTrackResourceContentRequestMessage(ids, context);
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