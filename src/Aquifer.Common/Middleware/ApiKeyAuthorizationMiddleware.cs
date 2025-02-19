using Aquifer.Common.Services.Caching;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace Aquifer.Common.Middleware;

public class ApiKeyAuthorizationMiddleware(RequestDelegate _next)
{
    private const string ApiKeyHeaderName = "api-key";

    public async Task InvokeAsync(HttpContext context, ICachingApiKeyService cachingApiKeyService)
    {
        if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKeyToValidate) ||
            apiKeyToValidate.Count == 0 ||
            string.IsNullOrEmpty(apiKeyToValidate[0]))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync($"API key is required in the {ApiKeyHeaderName} header");

            return;
        }

        var isKeyValid = await cachingApiKeyService.IsValidApiKeyAsync(apiKeyToValidate[0]!, ApiKeyScope.PublicApi, CancellationToken.None);

        if (!isKeyValid)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid API key");

            return;
        }

        await _next(context);
    }
}