using System.Collections.ObjectModel;
using Aquifer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Aquifer.Common.Services.Caching;

public interface ICachingVersificationService
{
    Task<ReadOnlyDictionary<(int bibleVerseId, char? bibleVersePart), (int baseVerseId, char? baseVersePart)>>
        GetVersificationsByBibleIdAsync(
        int bibleId, CancellationToken cancellationToken);
}

public class CachingVersificationService(AquiferDbContext _dbContext, IMemoryCache _memoryCache) : ICachingVersificationService
{
    private const string VersificationDictionariesCacheKey = $"{nameof(CachingVersificationService)}:VersificationDictionaries";
    private static readonly TimeSpan s_cacheLifetime = TimeSpan.FromMinutes(30);

    public async Task<ReadOnlyDictionary<(int bibleVerseId, char? bibleVersePart), (int baseVerseId, char? baseVersePart)>>
        GetVersificationsByBibleIdAsync(int bibleId, CancellationToken cancellationToken)
    {
        var dictionaries = await GetDictionariesFromCacheAsync(cancellationToken);

        var versificationDictionary = dictionaries?.GetValueOrDefault(bibleId) ?? throw new KeyNotFoundException();
        return versificationDictionary;
    }

    private async Task<ReadOnlyDictionary<int,
            ReadOnlyDictionary<(int bibleVerseId, char? bibleVersePart), (int baseVerseId, char? baseVersePart)>>?>
        GetDictionariesFromCacheAsync(CancellationToken ct)
    {
        return await _memoryCache.GetOrCreateAsync(VersificationDictionariesCacheKey,
            async cacheEntry =>
            {
                cacheEntry.SlidingExpiration = s_cacheLifetime;

                var versifications = await _dbContext.VersificationMappings.GroupBy(x => x.BibleId)
                    .ToDictionaryAsync(x => x.Key,
                        x => x.ToDictionary(g => (g.BibleVerseId, g.BibleVerseIdPart), g => (g.BaseVerseId, g.BaseVerseIdPart))
                            .AsReadOnly(), ct);

                return versifications.AsReadOnly();
            });
    }
}