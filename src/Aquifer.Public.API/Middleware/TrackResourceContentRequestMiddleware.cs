using System.Text.RegularExpressions;
using Aquifer.Public.API.Services;

namespace Aquifer.Public.API.Middleware;

public class TrackResourceContentRequestMiddleware(
    RequestDelegate _next,
    ITrackResourceContentRequestService _trackerService,
    ILogger<TrackResourceContentRequestMiddleware> _logger)
{
    private static readonly Regex ResourcePathRegex = new("^/resources/(\\d+)$");

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        try
        {
            if (context.Response.StatusCode == 200)
            {
                string path = context.Request.Path.Value ?? "";

                if (ResourcePathRegex.IsMatch(path))
                {
                    int resourceId = int.Parse(ResourcePathRegex.Match(path).Groups[1].Value);
                    await _trackerService.TrackResourceContentRequest([resourceId], context.Request.HttpContext);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to track resource content request");
        }
    }
}