using System.Text.RegularExpressions;
using Aquifer.API.Services;

namespace Aquifer.API.Middleware;

public class TrackResourceContentRequestMiddleware(
    RequestDelegate _next,
    ITrackResourceContentRequestService _trackerService,
    ILogger<TrackResourceContentRequestMiddleware> _logger)
{
    private static readonly Regex ResourcePathRegex = new("^/resources/(\\d+)/content$");
    private static readonly Regex BatchResourcesPathRegex = new("^/resources/batch/content/text$");

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        try
        {
            if (context.Response.StatusCode == 200)
            {
                var path = context.Request.Path.Value ?? "";

                if (ResourcePathRegex.IsMatch(path))
                {
                    var resourceId = int.Parse(ResourcePathRegex.Match(path).Groups[1].Value);
                    await _trackerService.TrackResourceContentRequest([resourceId], context.Request.HttpContext);
                }
                else if (BatchResourcesPathRegex.IsMatch(path))
                {
                    var ids = context.Request.Query["ids"].Where(s => !string.IsNullOrEmpty(s)).Select(int.Parse!).ToList();
                    await _trackerService.TrackResourceContentRequest(ids, context.Request.HttpContext);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to track resource content request");
        }
    }
}