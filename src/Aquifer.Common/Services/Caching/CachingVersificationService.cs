using System.Collections.ObjectModel;
using Aquifer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Aquifer.Common.Services.Caching;

public interface ICachingVersificationService
{
    Task<ReadOnlyDictionary<int, int>>
        GetVersificationsByBibleIdAsync(
        int bibleId, CancellationToken cancellationToken);

    Task<IReadOnlyList<int>>
        GetExclusionsByBibleIdAsync(int bibleId, CancellationToken cancellationToken);
}

public class CachingVersificationService(AquiferDbContext _dbContext, IMemoryCache _memoryCache) : ICachingVersificationService
{
    private const string VersificationDictionariesCacheKey = $"{nameof(CachingVersificationService)}:VersificationDictionaries";
    private const string ExclusionDictionariesCacheKey = $"{nameof(CachingVersificationService)}:ExclusionDictionaries";
    private static readonly TimeSpan s_cacheLifetime = TimeSpan.FromMinutes(30);

    public async Task<ReadOnlyDictionary<int, int>>
        GetVersificationsByBibleIdAsync(int bibleId, CancellationToken cancellationToken)
    {
        var dictionaries = await GetVersificationDictionariesFromCacheAsync(cancellationToken);

        var versificationDictionary = dictionaries?.GetValueOrDefault(bibleId) ??
                                      new Dictionary<int, int>()
                                          .AsReadOnly();
        return versificationDictionary;
    }

    public async Task<IReadOnlyList<int>> GetExclusionsByBibleIdAsync(int bibleId, CancellationToken cancellationToken)
    {
        var dictionaries = await GetExclusionDictionariesFromCacheAsync(cancellationToken);
        var exclusionsDictionary = dictionaries?.GetValueOrDefault(bibleId) ??
        [
        ];

        return exclusionsDictionary;
    }

    private async Task<ReadOnlyDictionary<int,
            ReadOnlyDictionary<int, int>>?>
        GetVersificationDictionariesFromCacheAsync(CancellationToken ct)
    {
        return await _memoryCache.GetOrCreateAsync(VersificationDictionariesCacheKey,
            async cacheEntry =>
            {
                cacheEntry.SlidingExpiration = s_cacheLifetime;

                var versifications = await _dbContext.VersificationMappings.GroupBy(x => x.BibleId)
                    .ToDictionaryAsync(x => x.Key,
                        x => x.ToDictionary(g => g.BibleVerseId, g => g.BaseVerseId)
                            .AsReadOnly(), ct);

                return versifications.AsReadOnly();
            });
    }

    private async Task<ReadOnlyDictionary<int, List<int>>?> GetExclusionDictionariesFromCacheAsync(CancellationToken ct)
    {
        return await _memoryCache.GetOrCreateAsync(ExclusionDictionariesCacheKey,
            async cacheEntry =>
            {
                cacheEntry.SlidingExpiration = s_cacheLifetime;

                var exclusions = await _dbContext.VersificationExclusions.GroupBy(x => x.BibleId)
                    .ToDictionaryAsync(x => x.Key,
                        x => x.Select(v => v.BibleVerseId).ToList(), ct);

                return exclusions.AsReadOnly();
            });
    }
}