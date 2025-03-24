using System.Collections.ObjectModel;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Aquifer.Common.Services.Caching;

public interface ICachingVersificationService
{
    Task<ReadOnlyDictionary<int, IReadOnlyList<string>>> GetBaseVerseIdWithOptionalPartByBibleVerseIdMapAsync(
        int bibleId,
        CancellationToken cancellationToken);

    Task<ReadOnlyDictionary<string, IReadOnlyList<int>>> GetBibleVerseIdByBaseVerseIdWithOptionalPartMapAsync(
        int bibleId,
        CancellationToken cancellationToken);

    Task<ReadOnlySet<int>> GetExcludedVerseIdsAsync(int bibleId, CancellationToken cancellationToken);

    Task<bool> DoesBibleIncludeBookAsync(int bibleId, BookId bookId, CancellationToken cancellationToken);

    /// <summary>
    /// Returns <c>null</c> if there is no Bible content for the given arguments.
    /// </summary>
    Task<int?> GetMaxChapterNumberForBookAsync(int bibleId, BookId bookId, CancellationToken cancellationToken);

    /// <summary>
    /// Returns <c>null</c> if there is no Bible content for the given arguments.
    /// </summary>
    Task<(int MinVerseNumber, int MaxVerseNumber)?> GetBookendVerseNumbersForChapterAsync(
        int bibleId,
        BookId bookId,
        int chapterNumber,
        CancellationToken cancellationToken);

    /// <summary>
    /// Prefer to use <see cref="GetMaxChapterNumberForBookAsync"/> or <see cref="GetBookendVerseNumbersForChapterAsync"/> instead
    /// for ease of use.
    /// </summary>
    Task<ReadOnlyDictionary<
            BookId,
            (int MaxChapterNumber, ReadOnlyDictionary<int, (int MinVerseNumber, int MaxVerseNumber)>
                BookendVerseNumbersByChapterNumberMap)>>
        GetMaxChapterNumberAndBookendVerseNumbersByBookIdMapAsync(int bibleId, CancellationToken cancellationToken);
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

    private const string BookendVersesNumberByChapterNumberMapByBookIdMapCacheKey =
        $"{nameof(CachingVersificationService)}:{nameof(BookendVersesNumberByChapterNumberMapByBookIdMapCacheKey)}";

    private static readonly TimeSpan s_cacheLifetime = TimeSpan.FromMinutes(120);

    public async Task<ReadOnlyDictionary<int, IReadOnlyList<string>>>
        GetBaseVerseIdWithOptionalPartByBibleVerseIdMapAsync(int bibleId, CancellationToken cancellationToken)
    {
        return (await GetBaseVerseIdsWithOptionalPartByBibleVerseIdMapByBibleIdMapAsync(cancellationToken))
            .GetValueOrDefault(bibleId)
            ?? new Dictionary<int, IReadOnlyList<string>>().AsReadOnly();
    }

    public async Task<ReadOnlyDictionary<string, IReadOnlyList<int>>> GetBibleVerseIdByBaseVerseIdWithOptionalPartMapAsync(int bibleId, CancellationToken cancellationToken)
    {
        return (await GetBibleVerseIdsByBaseVerseIdWithOptionalPartMapByBibleIdMapAsync(cancellationToken))
            .GetValueOrDefault(bibleId)
            ?? new Dictionary<string, IReadOnlyList<int>>().AsReadOnly();
    }

    public async Task<ReadOnlyDictionary<
            BookId,
            (int MaxChapterNumber, ReadOnlyDictionary<int, (int MinVerseNumber, int MaxVerseNumber)>
                BookendVerseNumbersByChapterNumberMap)>>
        GetMaxChapterNumberAndBookendVerseNumbersByBookIdMapAsync(int bibleId, CancellationToken cancellationToken)
    {
        // Cache each Bible's data independently in order to have cheaper SQL queries for each Bible.
        var cacheKey = $"{BookendVersesNumberByChapterNumberMapByBookIdMapCacheKey}:{bibleId}";

        // Special case: The "eng" (common superset of most English Bibles) versification scheme's max verses are saved in the
        // BookChapters DB table because we use that scheme for Resource verse references (Bible ID 0 has no BibleTexts data).
        if (bibleId == EngVersificationSchemeBibleId)
        {
            return await _memoryCache.GetOrCreateAsync(
                cacheKey,
                async cacheEntry =>
                {
                    cacheEntry.AbsoluteExpirationRelativeToNow = s_cacheLifetime;

                    return (await _dbContext.BookChapters
                            .Select(bc => new
                            {
                                bc.BookId,
                                bc.Number,
                                bc.MinVerseNumber,
                                bc.MaxVerseNumber,
                            })
                            .ToListAsync(cancellationToken))
                        .GroupBy(x => x.BookId)
                        .ToDictionary(
                            grp => grp.Key,
                            grp =>
                            (
                                MaxChapterNumber: grp.Max(x => x.Number),
                                BookendVerseNumbersByChapterNumberMap: grp
                                    .ToDictionary(
                                        x => x.Number,
                                        x => (x.MinVerseNumber, x.MaxVerseNumber))
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
                cacheEntry.AbsoluteExpirationRelativeToNow = s_cacheLifetime;

                return (await _dbContext.BibleTexts
                        .Where(x => x.BibleId == bibleId)
                        .GroupBy(x => new { x.BookId, x.ChapterNumber })
                        .Select(grp => new
                        {
                            grp.Key.BookId,
                            grp.Key.ChapterNumber,
                            MinVerseNumber = grp.Min(x => x.VerseNumber),
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
                                    x => (x.MinVerseNumber, x.MaxVerseNumber))
                                .AsReadOnly()
                        ))
                    .AsReadOnly();
            })
            ?? throw new InvalidOperationException($"\"{cacheKey}\" unexpectedly had a null value cached.");
    }

    public async Task<bool> DoesBibleIncludeBookAsync(int bibleId, BookId bookId, CancellationToken cancellationToken)
    {
        return (await GetMaxChapterNumberAndBookendVerseNumbersByBookIdMapAsync(bibleId, cancellationToken))
            .ContainsKey(bookId);
    }

    public async Task<int?> GetMaxChapterNumberForBookAsync(int bibleId, BookId bookId, CancellationToken cancellationToken)
    {
        return (await GetMaxChapterNumberAndBookendVerseNumbersByBookIdMapAsync(bibleId, cancellationToken))
            .GetValueOrNull(bookId)
            ?.MaxChapterNumber;
    }

    public async Task<(int MinVerseNumber, int MaxVerseNumber)?> GetBookendVerseNumbersForChapterAsync(
        int bibleId,
        BookId bookId,
        int chapterNumber,
        CancellationToken cancellationToken)
    {
        return (await GetMaxChapterNumberAndBookendVerseNumbersByBookIdMapAsync(bibleId, cancellationToken))
            .GetValueOrNull(bookId)
            ?.BookendVerseNumbersByChapterNumberMap
            .GetValueOrNull(chapterNumber);
    }

    public async Task<ReadOnlySet<int>> GetExcludedVerseIdsAsync(int bibleId, CancellationToken cancellationToken)
    {
        return (await GetExcludedVerseIdsByBibleIdMapAsync(cancellationToken))
            .GetValueOrDefault(bibleId)
            ?? new ReadOnlySet<int>(new HashSet<int>());
    }

    private async Task<ReadOnlyDictionary<int, ReadOnlyDictionary<int, IReadOnlyList<string>>>>
        GetBaseVerseIdsWithOptionalPartByBibleVerseIdMapByBibleIdMapAsync(CancellationToken ct)
    {
        return await _memoryCache.GetOrCreateAsync(
            BaseVerseIdByBibleVerseIdMapsCacheKey,
            async cacheEntry =>
            {
                cacheEntry.AbsoluteExpirationRelativeToNow = s_cacheLifetime;

                // Verse part handling:
                // 1. Ignore BibleVerseIdPart because consumers don't care about parts when passing verse IDs.
                //    (but if the BibleVerseId has multiple mappings using different parts keep all the mappings, just without a part).
                // 3. Keep BaseVerseIdPart in the mappings because it provides more accurate conversions between source -> base -> target.
                var versifications = await _dbContext.VersificationMappings
                    .GroupBy(x => x.BibleId)
                    .ToDictionaryAsync(
                        bibleGrouping => bibleGrouping.Key,
                        bibleGrouping => bibleGrouping
                            .GroupBy(x => x.BibleVerseId)
                            .ToDictionary(
                                bibleVerseIdGrouping => bibleVerseIdGrouping.Key,
                                IReadOnlyList<string> (bibleVerseIdGrouping) => bibleVerseIdGrouping
                                    .OrderBy(x => x.BaseVerseId)
                                    .ThenBy(x => x.BaseVerseIdPart)
                                    .Select(x => $"{x.BaseVerseId}{x.BaseVerseIdPart}")
                                    .Distinct()
                                    .ToList())
                            .AsReadOnly(),
                        ct);

                return versifications.AsReadOnly();
            })
            ?? throw new InvalidOperationException($"\"{BaseVerseIdByBibleVerseIdMapsCacheKey}\" unexpectedly had a null value cached.");
    }

    private async Task<ReadOnlyDictionary<int, ReadOnlyDictionary<string, IReadOnlyList<int>>>>
        GetBibleVerseIdsByBaseVerseIdWithOptionalPartMapByBibleIdMapAsync(CancellationToken ct)
    {
        return await _memoryCache.GetOrCreateAsync(
            BibleVerseIdByBaseVerseIdMapsCacheKey,
            async cacheEntry =>
            {
                cacheEntry.AbsoluteExpirationRelativeToNow = s_cacheLifetime;

                // When inverting the bibleVerseId -> baseVerseIdWithOptionalPart map, there may be multiple Bible verse IDs that map to
                // the same base verse ID with optional part.  In that case, get all such mappings but drop the BibleVersePart.
                return (await GetBaseVerseIdsWithOptionalPartByBibleVerseIdMapByBibleIdMapAsync(ct))
                    .ToDictionary(
                        x => x.Key,
                        x => x.Value
                            .SelectMany(kvp => kvp.Value.Select(bvwp => (BibleVerseId: kvp.Key, BaseVerseIdWithOptionalVersePart: bvwp)))
                            .GroupBy(vm => vm.BaseVerseIdWithOptionalVersePart)
                            .ToDictionary(
                                baseVerseIdWithOptionalPartGrouping => baseVerseIdWithOptionalPartGrouping.Key,
                                IReadOnlyList<int> (baseVerseIdWithOptionalPartGrouping) => baseVerseIdWithOptionalPartGrouping
                                    .Select(vm => vm.BibleVerseId)
                                    .Order()
                                    .Distinct()
                                    .ToList())
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
                cacheEntry.AbsoluteExpirationRelativeToNow = s_cacheLifetime;

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