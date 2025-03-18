using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Aquifer.Common.Services.Caching;

public interface ICachingApiKeyService
{
    Task<bool> IsValidApiKeyAsync(string apiKey, ApiKeyScope scope, CancellationToken ct);
    Task<CachedApiKey?> GetCachedApiKeyAsync(string apiKey, CancellationToken ct);
    Task<CachedApiKey?> GetCachedApiKeyAsync(string apiKey, ApiKeyScope scope, CancellationToken ct);
}

public class CachingApiKeyService(AquiferDbContext _dbContext, IMemoryCache _memoryCache) : ICachingApiKeyService
{
    private const string ApiKeysCacheKey = $"{nameof(CachingApiKeyService)}:ApiKeys";
    private static readonly TimeSpan s_cacheLifetime = TimeSpan.FromMinutes(10);

    public async Task<CachedApiKey?> GetCachedApiKeyAsync(string apiKey, CancellationToken ct)
    {
        return (await GetApiKeysFromCacheAsync(ct)).SingleOrDefault(x => x.ApiKey == apiKey);
    }

    public async Task<CachedApiKey?> GetCachedApiKeyAsync(string apiKey, ApiKeyScope scope, CancellationToken ct)
    {
        return (await GetApiKeysFromCacheAsync(ct)).SingleOrDefault(
            x => (x.Scope == scope || x.Scope == ApiKeyScope.All) && x.ApiKey == apiKey);
    }

    public async Task<bool> IsValidApiKeyAsync(string apiKey, ApiKeyScope scope, CancellationToken ct)
    {
        var scopedApiKey = await GetCachedApiKeyAsync(apiKey, scope, ct);
        return scopedApiKey is not null;
    }

    private async Task<List<CachedApiKey>> GetApiKeysFromCacheAsync(CancellationToken ct)
    {
        return await _memoryCache.GetOrCreateAsync(
                ApiKeysCacheKey,
                async cacheEntry =>
                {
                    cacheEntry.AbsoluteExpirationRelativeToNow = s_cacheLifetime;

                    return await _dbContext.ApiKeys.Select(x => new CachedApiKey(x.Id, x.ApiKey, x.Scope, x.Organization)).ToListAsync(ct);
                }) ??
            throw new InvalidOperationException($"{ApiKeysCacheKey} unexpectedly had a null value cached.");
    }
}

public record CachedApiKey(int Id, string ApiKey, ApiKeyScope Scope, string? Organization);