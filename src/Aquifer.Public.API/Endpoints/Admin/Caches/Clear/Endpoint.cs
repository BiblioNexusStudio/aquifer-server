using Aquifer.Common.Services.Caching;
using Aquifer.Data.Entities;
using Aquifer.Public.API.Helpers;
using Aquifer.Public.API.OpenApi;
using FastEndpoints;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Caching.Memory;

namespace Aquifer.Public.API.Endpoints.Admin.Caches.Clear;

public class Endpoint(ICachingApiKeyService _cachingApiKeyService, IMemoryCache _memoryCache, IOutputCacheStore _outputCacheStore)
    : Endpoint<Request>
{
    public override void Configure()
    {
        Post("/admin/caches/clear");
        Tags(EndpointHelpers.EndpointTags.ExcludeFromSwaggerDocument);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var apiKey = _cachingApiKeyService.CurrentApiKey;
        if (!apiKey.HasScope(ApiKeyScope.Admin))
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        if (request.ShouldClearMemoryCache)
        {
            // The IMemoryCache interface is normally injected into constructors, but it doesn't support clearing the entire cache.
            if (_memoryCache is not MemoryCache memoryCache)
            {
                throw new InvalidOperationException($"The {nameof(IMemoryCache)} implementation is not {nameof(MemoryCache)}.");
            }

            memoryCache.Clear();
        }

        if (request.ShouldClearOutputCache)
        {
            // if any other output cache tags are added then they should also be cleared here
            await _outputCacheStore.EvictByTagAsync(EndpointHelpers.AnonymousOutputCacheTag, ct);
            await _outputCacheStore.EvictByTagAsync(ClientGenerationSettings.ClientGenerationOutputCacheTag, ct);
        }

        await SendNoContentAsync(ct);
    }
}