using System.Text.RegularExpressions;

namespace Aquifer.API.Middleware;

public class TrackResourceRequestMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Regex _getResourceRegex = new Regex("^/resources/(\\d+)/content$");
    private readonly Regex _getBatchResourcesRegex = new Regex("^/resources/batch/content/text$");

    public TrackResourceRequestMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        var path = context.Request.Path.Value ?? "";

        if (_getResourceRegex.IsMatch(path))
        {
            var resourceId = int.Parse(_getResourceRegex.Match(path).Groups[1].Value);
            var ipAddress = context.Request.HttpContext.Connection.RemoteIpAddress;
            // send request to queue
        }
        else if (_getBatchResourcesRegex.IsMatch(path))
        {
            var ids = context.Request.Query["ids[]"].Where(s => !string.IsNullOrEmpty(s)).Select(int.Parse!).ToArray();
            var ipAddress = context.Request.HttpContext.Connection.RemoteIpAddress;
            // send request to queue
        }
    }
}