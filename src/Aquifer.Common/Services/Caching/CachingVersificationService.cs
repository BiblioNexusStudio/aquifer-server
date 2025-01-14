using System.Collections.ObjectModel;
using Aquifer.Data;
using Aquifer.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Aquifer.Common.Services.Caching;

public interface ICachingVersificationService
{
    Task<ReadOnlyDictionary<int, string>> GetBaseVerseIdWithOptionalPartByBibleVerseIdMapAsync(int bibleId, CancellationToken cancellationToken);

    Task<ReadOnlyDictionary<string, int>> GetBibleVerseIdByBaseVerseIdWithOptionalPartMapAsync(int bibleId, CancellationToken cancellationToken);

    Task<ReadOnlySet<int>> GetExcludedVerseIdsAsync(int bibleId, CancellationToken cancellationToken);

    Task<ReadOnlyDictionary<BookId, (int MaxChapterNumber, ReadOnlyDictionary<int, int> MaxVerseNumberByChapterNumberMap)>>
        GetMaxChapterNumberAndVerseNumbersByBookIdMapAsync(int bibleId, CancellationToken cancellationToken);
}

public sealed class CachingVersificationService(AquiferDbContext _dbContext, IMemoryCache _memoryCache) : ICachingVersificationService
{
    public const int EngVersificationSchemeBibleId = 0;

    private const string BaseVerseIdByBibleVerseIdMapsCacheKey =
        $"{nameof(CachingVersificationService)}:{nameof(BaseVerseIdByBibleVerseIdMapsCacheKey)}";

    private const string BibleVerseIdByBaseVerseIdMapsCacheKey =
        $"{nameof(CachingVersificationService)}:{nameof(BibleVerseIdByBaseVerseIdMapsCacheKey)}";

    private const string ExcludedVerseIdsByBibleIdMapCacheKey =
        $"{nameof(CachingVersificationService)}:{nameof(ExcludedVerseIdsByBibleIdMapCacheKey)}";

    private const string MaxVerseNumberByChapterNumberMapByBookIdMapCacheKey =
        $"{nameof(CachingVersificationService)}:{nameof(MaxVerseNumberByChapterNumberMapByBookIdMapCacheKey)}";

    private static readonly TimeSpan s_cacheLifetime = TimeSpan.FromMinutes(120);

    public async Task<ReadOnlyDictionary<int, string>>
        GetBaseVerseIdWithOptionalPartByBibleVerseIdMapAsync(int bibleId, CancellationToken cancellationToken)
    {
        return (await GetBaseVerseIdWithOptionalPartByBibleVerseIdMapByBibleIdMapAsync(cancellationToken))
            .GetValueOrDefault(bibleId)
            ?? new Dictionary<int, string>().AsReadOnly();
    }

    public async Task<ReadOnlyDictionary<string, int>> GetBibleVerseIdByBaseVerseIdWithOptionalPartMapAsync(int bibleId, CancellationToken cancellationToken)
    {
        return (await GetBibleVerseIdByBaseVerseIdWithOptionalPartMapByBibleIdMapAsync(cancellationToken))
            .GetValueOrDefault(bibleId)
            ?? new Dictionary<string, int>().AsReadOnly();
    }

    public async Task<ReadOnlyDictionary<BookId, (int MaxChapterNumber, ReadOnlyDictionary<int, int> MaxVerseNumberByChapterNumberMap)>>
        GetMaxChapterNumberAndVerseNumbersByBookIdMapAsync(int bibleId, CancellationToken cancellationToken)
    {
        // Cache each Bible's data independently in order to have cheaper SQL queries for each Bible.
        var cacheKey = $"{MaxVerseNumberByChapterNumberMapByBookIdMapCacheKey}:{bibleId}";

        // Special case: The "eng" (common superset of most English Bibles) versification scheme's max verses are saved in the
        // BookChapters DB table because we use that scheme for Resource verse references (Bible ID 0 has no BibleTexts data).
        if (bibleId == EngVersificationSchemeBibleId)
        {
            return await _memoryCache.GetOrCreateAsync(
                cacheKey,
                async cacheEntry =>
                {
                    cacheEntry.SlidingExpiration = s_cacheLifetime;

                    return (await _dbContext.BookChapters
                            .Select(bc => new
                            {
                                bc.BookId,
                                bc.Number,
                                bc.MaxVerseNumber,
                            })
                            .ToListAsync(cancellationToken))
                        .GroupBy(x => x.BookId)
                        .ToDictionary(
                            grp => grp.Key,
                            grp =>
                            (
                                MaxChapterNumber: grp.Max(x => x.Number),
                                MaxVerseNumberByChapterNumberMap: grp
                                    .ToDictionary(
                                        x => x.Number,
                                        x => x.MaxVerseNumber)
                                    .AsReadOnly()
                            ))
                        .AsReadOnly();
                })
                ?? throw new InvalidOperationException($"\"{cacheKey}\" unexpectedly had a null value cached.");
        }

        // For regular Bibles (not the "eng" versification scheme), get the max verse numbers from the BibleTexts table.
        return await _memoryCache.GetOrCreateAsync(
            cacheKey,
            async cacheEntry =>
            {
                cacheEntry.SlidingExpiration = s_cacheLifetime;

                return (await _dbContext.BibleTexts
                        .Where(x => x.BibleId == bibleId)
                        .GroupBy(x => new { x.BookId, x.ChapterNumber })
                        .Select(grp => new
                        {
                            grp.Key.BookId,
                            grp.Key.ChapterNumber,
                            MaxVerseNumber = grp.Max(x => x.VerseNumber),
                        })
                        .ToListAsync(cancellationToken))
                    .GroupBy(x => x.BookId)
                    .ToDictionary(
                        grp => grp.Key,
                        grp =>
                        (
                            MaxChapterNumber: grp.Max(x => x.ChapterNumber),
                            MaxVerseNumberByChapterNumberMap: grp
                                .ToDictionary(
                                    x => x.ChapterNumber,
                                    x => x.MaxVerseNumber)
                                .AsReadOnly()
                        ))
                    .AsReadOnly();
            })
            ?? throw new InvalidOperationException($"\"{cacheKey}\" unexpectedly had a null value cached.");
    }

    public async Task<ReadOnlySet<int>> GetExcludedVerseIdsAsync(int bibleId, CancellationToken cancellationToken)
    {
        return (await GetExcludedVerseIdsByBibleIdMapAsync(cancellationToken))
            .GetValueOrDefault(bibleId)
            ?? new ReadOnlySet<int>(new HashSet<int>());
    }

    private async Task<ReadOnlyDictionary<int, ReadOnlyDictionary<int, string>>>
        GetBaseVerseIdWithOptionalPartByBibleVerseIdMapByBibleIdMapAsync(CancellationToken ct)
    {
        return await _memoryCache.GetOrCreateAsync(
            BaseVerseIdByBibleVerseIdMapsCacheKey,
            async cacheEntry =>
            {
                cacheEntry.SlidingExpiration = s_cacheLifetime;

                // Verse part handling:
                // 1. Ignore BibleVerseIdPart because consumers don't care about parts when passing verse IDs.
                //    There is part data in the DB, though, so take only the first mapping in the DB for a verse
                //    (either the one with no verse part or the one with an 'a' verse part).
                // 2. Keep BaseVerseIdPart in the mappings because it provides more accurate conversions between source -> base -> target.
                var versifications = await _dbContext.VersificationMappings
                    .GroupBy(x => x.BibleId)
                    .ToDictionaryAsync(
                        bibleGrouping => bibleGrouping.Key,
                        bibleGrouping => bibleGrouping
                            .GroupBy(x => x.BibleVerseId)
                            .Select(bibleVerseIdGrouping => bibleVerseIdGrouping
                                .OrderBy(x => x.BaseVerseIdPart)
                                .First())
                            .ToDictionary(
                                x => x.BibleVerseId,
                                x => $"{x.BaseVerseId}{x.BaseVerseIdPart}")
                            .AsReadOnly(),
                        ct);

                return versifications.AsReadOnly();
            })
            ?? throw new InvalidOperationException($"\"{BaseVerseIdByBibleVerseIdMapsCacheKey}\" unexpectedly had a null value cached.");
    }

    private async Task<ReadOnlyDictionary<int, ReadOnlyDictionary<string, int>>>
        GetBibleVerseIdByBaseVerseIdWithOptionalPartMapByBibleIdMapAsync(CancellationToken ct)
    {
        return await _memoryCache.GetOrCreateAsync(
            BibleVerseIdByBaseVerseIdMapsCacheKey,
            async cacheEntry =>
            {
                cacheEntry.SlidingExpiration = s_cacheLifetime;

                // When inverting the bibleVerseId -> baseVerseIdWithOptionalPart map, there may be multiple Bible verse IDs that map to
                // the same base verse ID with optional part.  In that case, take the first entry ordered by
                // baseVerseIdWithOptionalPart alphabetically (which will usually be no part or the 'a' part).
                return (await GetBaseVerseIdWithOptionalPartByBibleVerseIdMapByBibleIdMapAsync(ct))
                    .ToDictionary(
                        x => x.Key,
                        x => x.Value
                            .GroupBy(y => y.Value)
                            .Select(baseVerseIdWithOptionalPartGrouping => baseVerseIdWithOptionalPartGrouping
                                .OrderBy(y => y.Value)
                                .First())
                            .ToDictionary(y => y.Value, y => y.Key)
                            .AsReadOnly())
                    .AsReadOnly();
            })
            ?? throw new InvalidOperationException($"\"{BibleVerseIdByBaseVerseIdMapsCacheKey}\" unexpectedly had a null value cached.");
    }

    private async Task<ReadOnlyDictionary<int, ReadOnlySet<int>>> GetExcludedVerseIdsByBibleIdMapAsync(CancellationToken ct)
    {
        return await _memoryCache.GetOrCreateAsync(
            ExcludedVerseIdsByBibleIdMapCacheKey,
            async cacheEntry =>
            {
                cacheEntry.SlidingExpiration = s_cacheLifetime;

                return (await _dbContext.VersificationExclusions
                        .GroupBy(x => x.BibleId)
                        .ToDictionaryAsync(
                            x => x.Key,
                            x => new ReadOnlySet<int>(
                                x.Select(v => v.BibleVerseId).ToHashSet()),
                            ct))
                    .AsReadOnly();
            })
            ?? throw new InvalidOperationException($"\"{ExcludedVerseIdsByBibleIdMapCacheKey}\" unexpectedly had a null value cached.");
    }
}