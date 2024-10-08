using Aquifer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Aquifer.Common.Services.Caching;

public interface ICachingLanguageService
{
    Task<string?> GetLanguageCodeAsync(int languageId, CancellationToken ct);
    Task<IReadOnlyDictionary<int, string>> GetLanguageCodeByIdMapAsync(CancellationToken ct);
    Task<int?> GetLanguageIdAsync(string languageCode, CancellationToken ct);
    Task<IReadOnlyDictionary<string, int>> GetLanguageIdByCodeMapAsync(CancellationToken ct);
}

public sealed class CachingLanguageService(AquiferDbContext _dbContext, IMemoryCache _memoryCache) : ICachingLanguageService
{
    private const string LanguageDictionariesCacheKey = $"{nameof(CachingLanguageService)}:LanguageDictionaries";
    private static readonly TimeSpan s_cacheLifetime = TimeSpan.FromMinutes(30);

    public async Task<string?> GetLanguageCodeAsync(int languageId, CancellationToken ct)
    {
        return (await GetLanguageCodeByIdMapAsync(ct))
            .TryGetValue(languageId, out var languageCode)
                ? languageCode
                : null;
    }

    public async Task<IReadOnlyDictionary<int, string>> GetLanguageCodeByIdMapAsync(CancellationToken ct)
    {
        return (await GetDictionariesFromCacheAsync(ct))
            .LanguageCodeByIdMap;
    }

    public async Task<int?> GetLanguageIdAsync(string languageCode, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(languageCode);

        return (await GetLanguageIdByCodeMapAsync(ct))
            .TryGetValue(languageCode, out var languageId)
                ? languageId
                : null;
    }

    public async Task<IReadOnlyDictionary<string, int>> GetLanguageIdByCodeMapAsync(CancellationToken ct)
    {
        return (await GetDictionariesFromCacheAsync(ct))
            .LanguageIdByCodeMap;
    }

    private async Task<(IReadOnlyDictionary<int, string> LanguageCodeByIdMap, IReadOnlyDictionary<string, int> LanguageIdByCodeMap)>
        GetDictionariesFromCacheAsync(CancellationToken ct)
    {
        return await _memoryCache.GetOrCreateAsync(
            LanguageDictionariesCacheKey,
            async cacheEntry =>
            {
                cacheEntry.SlidingExpiration = s_cacheLifetime;

                var LanguageCodeById = await _dbContext.Languages
                    .ToDictionaryAsync(li => li.Id, li => li.ISO6393Code, ct);

                return
                    (LanguageCodeById,
                    LanguageIdByCode: LanguageCodeById.ToDictionary(li => li.Value, li => li.Key, StringComparer.OrdinalIgnoreCase));
            });
    }
}