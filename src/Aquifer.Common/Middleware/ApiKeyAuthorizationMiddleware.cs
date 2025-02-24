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
        var apiKeyToValidate = context.Request.Headers[ApiKeyHeaderName].FirstOrDefault() ??
            context.Request.Query[ApiKeyHeaderName].FirstOrDefault();
        // This should never happen in practice because the WAF prevents requests without the "api-key" header
        if (string.IsNullOrEmpty(apiKeyToValidate))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync($"API key is required in the {ApiKeyHeaderName} header");

            return;
        }

        var scopedApiKey = await cachingApiKeyService.GetCachedApiKeyAsync(apiKeyToValidate, _options.Value.Scope, context.RequestAborted);

        if (scopedApiKey is null)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid API key");

            return;
        }

        context.Items.Add(Constants.HttpContextItemCachedApiKey, scopedApiKey);

        await _next(context);
    }
}

public class ApiKeyAuthorizationMiddlewareOptions
{
    public ApiKeyScope Scope { get; set; }
}