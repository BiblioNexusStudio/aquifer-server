using System.Collections.ObjectModel;
using Aquifer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Aquifer.Common.Services.Caching;

public interface ICachingVersificationService
{
    Task<ReadOnlyDictionary<int, int>> GetBaseVerseIdByBibleVerseIdMapAsync(int bibleId, CancellationToken cancellationToken);

    Task<ReadOnlyDictionary<int, int>> GetBibleVerseIdByBaseVerseIdMapAsync(int bibleId, CancellationToken cancellationToken);

    Task<ReadOnlySet<int>> GetExclusionsByBibleIdAsync(int bibleId, CancellationToken cancellationToken);
}

public class CachingVersificationService(AquiferDbContext _dbContext, IMemoryCache _memoryCache) : ICachingVersificationService
{
    private const string BaseVerseIdByBibleVerseIdMapsCacheKey =
        $"{nameof(CachingVersificationService)}:{nameof(BaseVerseIdByBibleVerseIdMapsCacheKey)}";

    private const string BibleVerseIdByBaseVerseIdMapsCacheKey =
        $"{nameof(CachingVersificationService)}:{nameof(BibleVerseIdByBaseVerseIdMapsCacheKey)}";
    private const string ExclusionDictionariesCacheKey = $"{nameof(CachingVersificationService)}:ExclusionDictionaries";
    private static readonly TimeSpan s_cacheLifetime = TimeSpan.FromMinutes(30);

    public async Task<ReadOnlyDictionary<int, int>>
        GetBaseVerseIdByBibleVerseIdMapAsync(int bibleId, CancellationToken cancellationToken)
    {
        var dictionaries = await GetBaseVerseIdByBibleVerseIdMapsAsync(cancellationToken);

        var versificationDictionary = dictionaries?.GetValueOrDefault(bibleId) ??
                                      new Dictionary<int, int>()
                                          .AsReadOnly();
        return versificationDictionary;
    }

    public async Task<ReadOnlyDictionary<int, int>> GetBibleVerseIdByBaseVerseIdMapAsync(int bibleId, CancellationToken cancellationToken)
    {
        var dictionaries = await GetBibleVerseIdByBaseVerseIdMapsAsync(cancellationToken);

        var versificationDictionary = dictionaries?.GetValueOrDefault(bibleId) ??
                                      new Dictionary<int, int>()
                                          .AsReadOnly();
        return versificationDictionary;
    }

    public async Task<ReadOnlySet<int>> GetExclusionsByBibleIdAsync(int bibleId, CancellationToken cancellationToken)
    {
        var dictionaries = await GetExclusionListsFromCacheAsync(cancellationToken);
        var exclusions = dictionaries?.GetValueOrDefault(bibleId) ?? new ReadOnlySet<int>(new HashSet<int>());

        return exclusions;
    }

    private async Task<ReadOnlyDictionary<int, ReadOnlyDictionary<int, int>>?>
        GetBaseVerseIdByBibleVerseIdMapsAsync(CancellationToken ct)
    {
        return await _memoryCache.GetOrCreateAsync(BaseVerseIdByBibleVerseIdMapsCacheKey,
            async cacheEntry =>
            {
                cacheEntry.SlidingExpiration = s_cacheLifetime;

                var versifications = await _dbContext.VersificationMappings
                    .GroupBy(x => x.BibleId)
                    .ToDictionaryAsync(
                        x => x.Key,
                        x => x.ToDictionary(g => g.BibleVerseId, g
                            => g.BaseVerseId).AsReadOnly(), ct);

                return versifications.AsReadOnly();
            });
    }

    private async Task<ReadOnlyDictionary<int, ReadOnlyDictionary<int, int>>?> GetBibleVerseIdByBaseVerseIdMapsAsync(CancellationToken ct)
    {
        return await _memoryCache.GetOrCreateAsync(BibleVerseIdByBaseVerseIdMapsCacheKey,
            async cacheEntry =>
            {
                cacheEntry.SlidingExpiration = s_cacheLifetime;

                var baseVerseByBibleIdDictionaries = await GetBaseVerseIdByBibleVerseIdMapsAsync(ct);

                var inverted = baseVerseByBibleIdDictionaries?.ToDictionary(x => x.Key, x
                    => x.Value.ToDictionary(y => y.Value, y => y.Key).AsReadOnly());

                return inverted?.AsReadOnly();
            });
    }

    private async Task<ReadOnlyDictionary<int, ReadOnlySet<int>>?> GetExclusionListsFromCacheAsync(CancellationToken ct)
    {
        return await _memoryCache.GetOrCreateAsync(ExclusionDictionariesCacheKey,
            async cacheEntry =>
            {
                cacheEntry.SlidingExpiration = s_cacheLifetime;

                var exclusions = await _dbContext.VersificationExclusions.GroupBy(x => x.BibleId)
                    .ToDictionaryAsync(x => x.Key,
                        x => new ReadOnlySet<int>(x.Select(v => v.BibleVerseId).ToHashSet()), ct);

                return exclusions.AsReadOnly();
            });
    }
}