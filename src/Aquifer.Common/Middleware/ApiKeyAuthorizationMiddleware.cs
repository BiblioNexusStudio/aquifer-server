using Aquifer.Common.Services.Caching;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Aquifer.Common.Middleware;

public class ApiKeyAuthorizationMiddleware(RequestDelegate _next, IOptions<ApiKeyAuthorizationMiddlewareOptions> _options)
{
    private const string ApiKeyHeaderName = "api-key";

    public async Task InvokeAsync(HttpContext context, ICachingApiKeyService cachingApiKeyService)
    {
        if (_options.Value.ExemptRoutes.Any(x => context.Request.Path.StartsWithSegments(x)))
        {
            await _next(context);
            return;
        }

        var apiKeyToValidate = context.Request.Headers[ApiKeyHeaderName].FirstOrDefault() ??
            context.Request.Query[ApiKeyHeaderName].FirstOrDefault();
        // This should never happen in practice because the WAF prevents requests without the "api-key" header
        if (string.IsNullOrEmpty(apiKeyToValidate))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync($"API key is required in the {ApiKeyHeaderName} header");

            return;
        }

        var scopedApiKey = await cachingApiKeyService.GetApiKeyAsync(apiKeyToValidate, _options.Value.Scope, context.RequestAborted);

        if (scopedApiKey is null)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid API key");

            return;
        }

        cachingApiKeyService.CurrentApiKey = scopedApiKey;

        await _next(context);
    }
}

public class ApiKeyAuthorizationMiddlewareOptions
{
    public ApiKeyScope Scope { get; set; }

    /// <summary>
    /// Routes that will not require an API key. This uses simple <c>StartsWith</c> logic.
    /// </summary>
    /// <example>
    /// This would prevent any routes that start with "/marketing" from requiring an API key:
    /// <code>
    /// /marketing
    /// </code>
    /// </example>
    public IReadOnlyList<string> ExemptRoutes { get; set; } = [];
}