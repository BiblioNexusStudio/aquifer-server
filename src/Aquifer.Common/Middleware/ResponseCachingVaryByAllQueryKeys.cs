using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCaching;

namespace Aquifer.Common.Middleware;

public static class ResponseCachingVaryByAllQueryKeysExtensions
{
    public static IApplicationBuilder UseResponseCachingVaryByAllQueryKeys(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ResponseCachingVaryByAllQueryKeysMiddleware>();
    }
}

public class ResponseCachingVaryByAllQueryKeysMiddleware(RequestDelegate _next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        context.Features.Set<IResponseCachingFeature>(new ResponseCachingFeature
        {
            VaryByQueryKeys = ["*"]
        });

        await _next(context);
    }
}