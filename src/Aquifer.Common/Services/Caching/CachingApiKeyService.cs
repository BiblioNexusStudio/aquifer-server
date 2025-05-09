using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Aquifer.Common.Services.Caching;

public interface ICachingApiKeyService
{
    ApiKey CurrentApiKey { get; set; }
    Task<ApiKey?> GetApiKeyAsync(string apiKey, CancellationToken ct);
    Task<ApiKey?> GetApiKeyAsync(string apiKey, ApiKeyScope scope, CancellationToken ct);
    Task<bool> IsValidApiKeyAsync(string apiKey, ApiKeyScope scope, CancellationToken ct);
}

public class CachingApiKeyService(AquiferDbContext _dbContext, IMemoryCache _memoryCache, IHttpContextAccessor _httpContextAccessor)
    : ICachingApiKeyService
{
    private const string ApiKeysCacheKey = $"{nameof(CachingApiKeyService)}:ApiKeys";
    private static readonly TimeSpan s_cacheLifetime = TimeSpan.FromMinutes(10);

    public ApiKey CurrentApiKey
    {
        get => _httpContextAccessor.HttpContext?.Items[Constants.HttpContextItemCachedApiKey] as ApiKey ??
            throw new InvalidOperationException("API Key should be saved on HTTP Context!");
        set => _httpContextAccessor.HttpContext?.Items.Add(Constants.HttpContextItemCachedApiKey, value);
    }

    public async Task<ApiKey?> GetApiKeyAsync(string apiKey, CancellationToken ct)
    {
        return (await GetApiKeysFromCacheAsync(ct)).SingleOrDefault(x => x.Key == apiKey);
    }

    public async Task<ApiKey?> GetApiKeyAsync(string apiKey, ApiKeyScope scope, CancellationToken ct)
    {
        return (await GetApiKeysFromCacheAsync(ct)).SingleOrDefault(x => x.Key == apiKey && x.HasScope(scope));
    }

    public async Task<bool> IsValidApiKeyAsync(string apiKey, ApiKeyScope scope, CancellationToken ct)
    {
        var scopedApiKey = await GetApiKeyAsync(apiKey, scope, ct);
        return scopedApiKey is not null;
    }

    private async Task<List<ApiKey>> GetApiKeysFromCacheAsync(CancellationToken ct)
    {
        return await _memoryCache.GetOrCreateAsync(
                ApiKeysCacheKey,
                async cacheEntry =>
                {
                    cacheEntry.AbsoluteExpirationRelativeToNow = s_cacheLifetime;

                    return await _dbContext.ApiKeys.Select(x => new ApiKey(x.Id, x.ApiKey, x.Scope, x.Organization)).ToListAsync(ct);
                }) ??
            throw new InvalidOperationException($"{ApiKeysCacheKey} unexpectedly had a null value cached.");
    }
}

public sealed class ApiKey(int _id, string _key, ApiKeyScope _scope, string? _organization)
{
    public int Id { get; } = _id;
    public string Key { get; } = _key;
    public ApiKeyScope Scope { get; } = _scope;
    public string? Organization { get; } = _organization;

    public bool HasScope(ApiKeyScope scope)
    {
        return scope is ApiKeyScope.Admin or ApiKeyScope.None
            ? Scope == scope
            : Scope == scope || Scope == ApiKeyScope.All || Scope == ApiKeyScope.Admin;
    }
}