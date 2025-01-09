using System.Collections.ObjectModel;
using Aquifer.Common.Services.Caching;
using Aquifer.Data.Enums;

namespace Aquifer.Common.Utilities;

public static class VersificationUtilities
{
    /// <summary>
    /// Returns <c>true</c> if the given verse ID is valid and is contained in the given Bible, <c>false</c> otherwise.
    /// </summary>
    public static async Task<bool> IsValidVerseIdAsync(
        int bibleId,
        int verseId,
        ICachingVersificationService versificationService,
        CancellationToken ct)
    {
        var verse = BibleUtilities.TranslateVerseId(verseId);

        if (verse.bookId == BookId.None || verse.chapter == 0 || verse.verse == 0)
        {
            return false;
        }

        // Some Bibles are missing specific verses.
        var excludedVerseIds = await versificationService.GetExcludedVerseIdsAsync(bibleId, ct);
        if (excludedVerseIds.Contains(verseId))
        {
            return false;
        }

        var maxChapterNumberAndVerseNumbersByBookIdMap =
            await versificationService.GetMaxChapterNumberAndVerseNumbersByBookIdMapAsync(bibleId, ct);

        // Bibles often don't include every possible book.
        if (!maxChapterNumberAndVerseNumbersByBookIdMap.TryGetValue(verse.bookId, out var maxChapterNumberAndVerseNumbers))
        {
            return false;
        }

        if (verse.chapter > maxChapterNumberAndVerseNumbers.MaxChapterNumber)
        {
            return false;
        }

        // Edge Case: It's theoretically possible that a Bible could be missing an entire chapter.
        if (!maxChapterNumberAndVerseNumbers.MaxVerseNumberByChapterNumberMap
                .TryGetValue(verse.chapter, out var maxVerseNumber))
        {
            return false;
        }

        return verse.verse <= maxVerseNumber;
    }

    /// <summary>
    /// Returns an ordered list of verse IDs for any valid start and end verse IDs, even if the range spans across chapters or books.
    /// Only verse IDs present for the given <see paramref="bibleId"/> will be included.
    /// If the range spans across books then the standard <see cref="BookId"/> ordering will be used, even if that's different from
    /// the given Bible's book ordering.
    /// </summary>
    public static async Task<IReadOnlyList<int>> ExpandVerseIdRangeAsync(
        int bibleId,
        int startVerseId,
        int endVerseId,
        ICachingVersificationService versificationService,
        CancellationToken ct)
    {
        if (endVerseId < startVerseId)
        {
            throw new ArgumentException(
                $"{nameof(endVerseId)} ({endVerseId}) must be greater than or equal to {nameof(startVerseId)} ({startVerseId}).",
                nameof(endVerseId));
        }

        if (!await IsValidVerseIdAsync(bibleId, startVerseId, versificationService, ct))
        {
            throw new ArgumentException(
                $"{nameof(startVerseId)} {startVerseId} is invalid for {nameof(bibleId)} {bibleId}.",
                nameof(startVerseId));
        }

        if (!await IsValidVerseIdAsync(bibleId, endVerseId, versificationService, ct))
        {
            throw new ArgumentException(
                $"{nameof(endVerseId)} {endVerseId} is invalid for {nameof(bibleId)} {bibleId}.",
                nameof(endVerseId));
        }

        if (startVerseId == endVerseId)
        {
            return [startVerseId];
        }

        var startVerse = BibleUtilities.TranslateVerseId(startVerseId);
        var endVerse = BibleUtilities.TranslateVerseId(endVerseId);

        var maxChapterNumberAndVerseNumbersByBookIdMap =
            await versificationService.GetMaxChapterNumberAndVerseNumbersByBookIdMapAsync(bibleId, ct);

        var excludedVerseIds = await versificationService.GetExcludedVerseIdsAsync(bibleId, ct);

        var expandedVerseIds = new List<int>();
        for (var bookId = startVerse.bookId; bookId <= endVerse.bookId; bookId++)
        {
            // Bibles often don't include every possible book.
            if (!maxChapterNumberAndVerseNumbersByBookIdMap.TryGetValue(bookId, out var maxChapterNumberAndVerseNumbers))
            {
                continue;
            }

            var startChapterNumber = bookId == startVerse.bookId
                ? startVerse.chapter
                : 1;
            var endChapterNumber = bookId == endVerse.bookId
                ? endVerse.chapter
                : maxChapterNumberAndVerseNumbers.MaxChapterNumber;

            for (var chapterNumber = startChapterNumber; chapterNumber <= endChapterNumber; chapterNumber++)
            {
                // Edge Case: It's theoretically possible that a Bible could be missing an entire chapter.
                if (!maxChapterNumberAndVerseNumbers.MaxVerseNumberByChapterNumberMap
                        .TryGetValue(chapterNumber, out var maxVerseNumber))
                {
                    continue;
                }

                var startVerseNumber = bookId == startVerse.bookId && chapterNumber == startVerse.chapter
                    ? startVerse.verse
                    : 1;
                var endVerseNumber = bookId == endVerse.bookId && chapterNumber == endVerse.chapter
                    ? endVerse.verse
                    : maxVerseNumber;

                for (var verseNumber = startVerseNumber; verseNumber <= endVerseNumber; verseNumber++)
                {
                    var verseId = BibleUtilities.GetVerseId(bookId, chapterNumber, verseNumber);

                    // Some Bibles are missing specific verses.
                    if (!excludedVerseIds.Contains(verseId))
                    {
                        expandedVerseIds.Add(verseId);
                    }
                }
            }
        }

        return expandedVerseIds;
    }

    /// <summary>
    /// Returns the verse ID in the target Bible that corresponds to the given verse ID in the source Bible.
    /// <c>null</c> will be returned if it's not possible to map between the versification schemes.
    /// </summary>
    public static async Task<int?> ConvertVersificationAsync(
        int sourceBibleId,
        int sourceVerseId,
        int targetBibleId,
        ICachingVersificationService versificationService,
        CancellationToken ct)
    {
        if (!await IsValidVerseIdAsync(sourceBibleId, sourceVerseId, versificationService, ct))
        {
            throw new ArgumentException(
                $"{nameof(sourceVerseId)} {sourceVerseId} is invalid for {nameof(sourceBibleId)} {sourceBibleId}.",
                nameof(sourceVerseId));
        }

        var baseVerseIdBySourceBibleVerseIdMap = await versificationService.GetBaseVerseIdByBibleVerseIdMapAsync(sourceBibleId, ct);
        var targetBibleVerseIdByBaseVerseIdMap = await versificationService.GetBibleVerseIdByBaseVerseIdMapAsync(targetBibleId, ct);
        var targetBibleExcludedVerseIds = await versificationService.GetExcludedVerseIdsAsync(targetBibleId, ct);

        return ConvertVersificationCore(
            sourceVerseId,
            baseVerseIdBySourceBibleVerseIdMap,
            targetBibleVerseIdByBaseVerseIdMap,
            targetBibleExcludedVerseIds);
    }

    /// <summary>
    /// This method assumes that the <paramref name="sourceVerseId"/> is valid for the given maps and exclusions.
    /// </summary>
    /// <returns>The converted verse ID or <c>null</c> if conversion is not possible.</returns>
    private static int? ConvertVersificationCore(
        int sourceVerseId,
        ReadOnlyDictionary<int, int> baseVerseIdBySourceBibleVerseIdMap,
        ReadOnlyDictionary<int, int> targetBibleVerseIdByBaseVerseIdMap,
        ReadOnlySet<int> targetBibleExcludedVerseIds)
    {
        // The dictionary only contains mappings where the key is different from the value.
        // If the key verse ID is not present in the mapping (and is not in the exclusions list) then the value matches the key.
        var baseVerseId = baseVerseIdBySourceBibleVerseIdMap.GetValueOrDefault(sourceVerseId, sourceVerseId);
        var targetVerseId = targetBibleVerseIdByBaseVerseIdMap.GetValueOrDefault(baseVerseId, baseVerseId);

        return targetBibleExcludedVerseIds.Contains(targetVerseId)
            ? null
            : targetVerseId;
    }

    /// <summary>
    /// Returns the verse IDs in the target Bible that corresponds to the given verse IDs in the source Bible.
    /// Ranges will be expanded to their component verse IDs.
    /// The source verse ID is the dictionary key and the target verse ID is the dictionary value.
    /// <c>null</c> will be returned for the target verse ID if it's not possible to map between the versification schemes.
    /// </summary>
    public static async Task<ReadOnlyDictionary<int, int?>> ConvertVersificationRangeAsync(
        int sourceBibleId,
        int sourceStartVerseId,
        int sourceEndVerseId,
        int targetBibleId,
        ICachingVersificationService versificationService,
        CancellationToken ct)
    {
        // expanding will throw exceptions if the given start/end verse IDs are invalid
        var expandedSourceVerseIds = await ExpandVerseIdRangeAsync(
            sourceBibleId,
            sourceStartVerseId,
            sourceEndVerseId,
            versificationService,
            ct);

        var baseVerseIdBySourceBibleVerseIdMap = await versificationService.GetBaseVerseIdByBibleVerseIdMapAsync(sourceBibleId, ct);
        var targetBibleVerseIdByBaseVerseIdMap = await versificationService.GetBibleVerseIdByBaseVerseIdMapAsync(targetBibleId, ct);
        var targetBibleExcludedVerseIds = await versificationService.GetExcludedVerseIdsAsync(targetBibleId, ct);

        return expandedSourceVerseIds
            .ToDictionary(
                sourceVerseId => sourceVerseId,
                sourceVerseId => ConvertVersificationCore(
                    sourceVerseId,
                    baseVerseIdBySourceBibleVerseIdMap,
                    targetBibleVerseIdByBaseVerseIdMap,
                    targetBibleExcludedVerseIds))
            .AsReadOnly();
    }
}