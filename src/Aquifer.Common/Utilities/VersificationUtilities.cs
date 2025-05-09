using System.Collections.ObjectModel;
using Aquifer.Common.Services.Caching;
using Aquifer.Data.Enums;

namespace Aquifer.Common.Utilities;

public static class VersificationUtilities
{
    // Assumption: The minimum chapter number in a book is always 1.  As of 2025-03-25, this is true for all imported Bibles.
    private const int MinChapterNumber = 1;

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

        if (verse.bookId == BookId.None || verse.chapter == 0)
        {
            return false;
        }

        // Some Bibles are missing specific verses.
        var excludedVerseIds = await versificationService.GetExcludedVerseIdsAsync(bibleId, ct);
        if (excludedVerseIds.Contains(verseId))
        {
            return false;
        }

        var maxChapterNumberAndBookendVerseNumbersByBookIdMap =
            await versificationService.GetMaxChapterNumberAndBookendVerseNumbersByBookIdMapAsync(bibleId, ct);

        // Bibles often don't include every possible book.
        if (!maxChapterNumberAndBookendVerseNumbersByBookIdMap.TryGetValue(verse.bookId, out var maxChapterNumberAndBookendVerseNumbers))
        {
            return false;
        }

        if (verse.chapter > maxChapterNumberAndBookendVerseNumbers.MaxChapterNumber)
        {
            return false;
        }

        // Edge Case: It's theoretically possible that a Bible could be missing an entire chapter.
        if (!maxChapterNumberAndBookendVerseNumbers.BookendVerseNumbersByChapterNumberMap
            .TryGetValue(verse.chapter, out var bookendVerseNumbers))
        {
            return false;
        }

        return bookendVerseNumbers.MinVerseNumber <= verse.verse && verse.verse <= bookendVerseNumbers.MaxVerseNumber;
    }

    /// <summary>
    /// Returns <c>null</c> if the <paramref name="bibleId" /> or the relevant book do not contain text.  Also returns <c>null</c>
    /// if the two verse IDs refer to different books.
    /// Otherwise, if the given start and end chapter and verse numbers are not valid for the given Bible and Book then the range will be
    /// adjusted to the nearest valid chapter and verse numbers for the given Bible and Book based upon the text.
    /// </summary>
    public static async Task<(int StartVerseId, int EndVerseId)?> GetValidVerseIdRangeAsync(
        int bibleId,
        int startVerseId,
        int endVerseId,
        ICachingVersificationService versificationService,
        CancellationToken ct)
    {
        var startVerse = BibleUtilities.TranslateVerseId(startVerseId);
        var endVerse = BibleUtilities.TranslateVerseId(endVerseId);

        if (startVerse.bookId != endVerse.bookId)
        {
            return null;
        }

        return await GetValidVerseIdRangeAsync(
            bibleId,
            startVerse.bookId,
            startVerse.chapter,
            startVerse.verse,
            endVerse.chapter,
            endVerse.verse,
            versificationService,
            ct);
    }

    /// <summary>
    /// Returns <c>null</c> if the <paramref name="bibleId" /> or <paramref name="bookId" /> do not contain text.
    /// Otherwise, if the given start and end chapter and verse numbers are not valid for the given Bible and Book then the range will be
    /// adjusted to the nearest valid chapter and verse numbers for the given Bible and Book based upon the text.
    /// </summary>
    public static async Task<(int StartVerseId, int EndVerseId)?> GetValidVerseIdRangeAsync(
        int bibleId,
        BookId bookId,
        int? startChapterNumber,
        int? startVerseNumber,
        int? endChapterNumber,
        int? endVerseNumber,
        ICachingVersificationService versificationService,
        CancellationToken ct)
    {
        var maxChapterNumberAndBookendVerseNumbersByBookIdMap =
            await versificationService.GetMaxChapterNumberAndBookendVerseNumbersByBookIdMapAsync(bibleId, ct);

        if (!maxChapterNumberAndBookendVerseNumbersByBookIdMap.TryGetValue(bookId, out var maxChapterNumberAndBookendVerseNumbers))
        {
            return null;
        }

        var (maxChapterNumber, bookendVerseNumbersByChapterNumberMap) = maxChapterNumberAndBookendVerseNumbers;

        // Assumption: There are no missing chapters in any Bibles.
        var adjustedStartChapterNumber = startChapterNumber is >= MinChapterNumber && startChapterNumber.Value <= maxChapterNumber
            ? startChapterNumber.Value
            : MinChapterNumber;
        var adjustedEndChapterNumber = endChapterNumber is >= MinChapterNumber && endChapterNumber.Value <= maxChapterNumber
            ? endChapterNumber.Value
            : maxChapterNumber;

        var (minVerseNumberForStartChapter, maxVerseNumberForStartChapter) =
            bookendVerseNumbersByChapterNumberMap[adjustedStartChapterNumber];
        var adjustedStartVerseNumber =
            startVerseNumber >= minVerseNumberForStartChapter && startVerseNumber.Value <= maxVerseNumberForStartChapter
                ? startVerseNumber.Value
                : minVerseNumberForStartChapter;

        var (minVerseNumberForEndChapter, maxVerseNumberForEndChapter) =
            bookendVerseNumbersByChapterNumberMap[adjustedEndChapterNumber];
        var adjustedEndVerseNumber = endVerseNumber >= minVerseNumberForEndChapter && endVerseNumber.Value <= maxVerseNumberForEndChapter
            ? endVerseNumber.Value
            : maxVerseNumberForEndChapter;

        var minVerseId = BibleUtilities.GetVerseId(bookId, adjustedStartChapterNumber, adjustedStartVerseNumber);
        var maxVerseId = BibleUtilities.GetVerseId(bookId, adjustedEndChapterNumber, adjustedEndVerseNumber);

        // the min/max verse IDs are within the valid chapter ranges but there's no guarantee that the verses are not excluded
        return (await GetNearestNonExcludedVerseIdAsync(bibleId, minVerseId, versificationService, ct),
            await GetNearestNonExcludedVerseIdAsync(bibleId, maxVerseId, versificationService, ct));
    }

    /// <summary>
    /// If the <paramref name="verseId" /> is outside the valid chapter/verse range for the given <paramref name="bibleId" />  then the
    /// <paramref name="verseId" /> will be adjusted to a valid verse ID within the book/chapter.  Furthermore, the returned verse ID is
    /// guaranteed to not be an excluded verse in the given <paramref name="bibleId" />.
    /// If the Bible doesn't exist or the book is not present in the Bible then <c>null</c> will be returned.
    /// Note: If a verse ID is invalid then this method will prefer to increment the verse whenever possible.
    /// </summary>
    public static async Task<int?> ConstrainToValidVerseIdAsync(
        int bibleId,
        int verseId,
        ICachingVersificationService versificationService,
        CancellationToken ct)
    {
        var notOutOfBoundsVerseId = await ConstrainOutOfBoundsVerseIdAsync(bibleId, verseId, versificationService, ct);

        return notOutOfBoundsVerseId == null
            ? null
            : await GetNearestNonExcludedVerseIdAsync(bibleId, verseId, versificationService, ct);
    }

    /// <summary>
    /// Note: The returned verse ID may still be outside the bounds of the chapter.
    /// See <see cref="ConstrainOutOfBoundsVerseIdAsync" /> for solving that problem.
    /// </summary>
    private static async Task<int> GetNearestNonExcludedVerseIdAsync(
        int bibleId,
        int verseId,
        ICachingVersificationService versificationService,
        CancellationToken ct)
    {
        var excludedVerseIds = await versificationService.GetExcludedVerseIdsAsync(bibleId, ct);

        var adjustedVerseId = verseId;
        while (excludedVerseIds.Contains(adjustedVerseId))
        {
            adjustedVerseId++;
        }

        return adjustedVerseId;
    }

    /// <summary>
    /// If the verseId is outside the valid chapter/verse range for the given Bible and Book then the verse ID will be adjusted to
    /// a valid verse ID. If the Bible doesn't exist or the book is not present in the Bible then <c>null</c> will be returned.
    /// Note: The returned verse ID may be excluded.  See <see cref="GetNearestNonExcludedVerseIdAsync" /> for solving that problem.
    /// </summary>
    private static async Task<int?> ConstrainOutOfBoundsVerseIdAsync(
        int bibleId,
        int verseId,
        ICachingVersificationService versificationService,
        CancellationToken ct)
    {
        var maxChapterNumberAndBookendVerseNumbersByBookIdMap =
            await versificationService.GetMaxChapterNumberAndBookendVerseNumbersByBookIdMapAsync(bibleId, ct);

        return ConstrainOutOfBoundsVerseId(verseId, maxChapterNumberAndBookendVerseNumbersByBookIdMap);
    }

    /// <summary>
    /// If the verseId is outside the valid chapter/verse range for the given Bible and Book then the verse ID will be adjusted to
    /// a valid verse ID. If the Bible doesn't exist or the book is not present in the Bible then <c>null</c> will be returned.
    /// Note: The returned verse ID may be excluded.  See <see cref="GetNearestNonExcludedVerseIdAsync" /> for solving that problem.
    /// </summary>
    private static int? ConstrainOutOfBoundsVerseId(
        int verseId,
        ReadOnlyDictionary<
                BookId,
                (int MaxChapterNumber, ReadOnlyDictionary<int, (int MinVerseNumber, int MaxVerseNumber)>
                BookendVerseNumbersByChapterNumberMap)>
            maxChapterNumberAndBookendVerseNumbersByBookIdMap)
    {
        var (bookId, chapter, verse) = BibleUtilities.TranslateVerseId(verseId);

        if (!maxChapterNumberAndBookendVerseNumbersByBookIdMap.TryGetValue(bookId, out var maxChapterNumberAndBookendVerseNumbers))
        {
            return null;
        }

        var (maxChapterNumber, bookendVerseNumbersByChapterNumberMap) = maxChapterNumberAndBookendVerseNumbers;

        if (chapter < MinChapterNumber)
        {
            return BibleUtilities.GetVerseId(
                bookId,
                MinChapterNumber,
                bookendVerseNumbersByChapterNumberMap[maxChapterNumber].MinVerseNumber);
        }

        if (chapter > maxChapterNumber)
        {
            return BibleUtilities.GetVerseId(
                bookId,
                maxChapterNumber,
                bookendVerseNumbersByChapterNumberMap[maxChapterNumber].MaxVerseNumber);
        }

        if (verse < bookendVerseNumbersByChapterNumberMap[chapter].MinVerseNumber)
        {
            return BibleUtilities.GetVerseId(bookId, chapter, bookendVerseNumbersByChapterNumberMap[chapter].MinVerseNumber);
        }

        if (verse > bookendVerseNumbersByChapterNumberMap[chapter].MaxVerseNumber)
        {
            // increment the out-of-chapter verse to the start of the next chapter if possible
            return chapter < maxChapterNumber
                ? BibleUtilities.GetVerseId(bookId, chapter + 1, bookendVerseNumbersByChapterNumberMap[chapter + 1].MinVerseNumber)
                : BibleUtilities.GetVerseId(bookId, chapter, bookendVerseNumbersByChapterNumberMap[chapter].MaxVerseNumber);
        }

        return verseId;
    }

    /// <summary>
    /// Returns an ordered list of verse IDs for any valid start and end verse IDs, even if the range spans across chapters or books.
    /// Only verse IDs present for the given <see paramref="bibleId" /> will be included.
    /// If the range spans across books then the standard <see cref="BookId" /> ordering will be used, even if that's different from
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

        var maxChapterNumberAndBookendVerseNumbersByBookIdMap =
            await versificationService.GetMaxChapterNumberAndBookendVerseNumbersByBookIdMapAsync(bibleId, ct);

        var excludedVerseIds = await versificationService.GetExcludedVerseIdsAsync(bibleId, ct);

        var expandedVerseIds = new List<int>();
        for (var bookId = startVerse.bookId; bookId <= endVerse.bookId; bookId++)
        {
            // Bibles often don't include every possible book.
            if (!maxChapterNumberAndBookendVerseNumbersByBookIdMap.TryGetValue(bookId, out var maxChapterNumberAndBookendVerseNumbers))
            {
                continue;
            }

            var startChapterNumber = bookId == startVerse.bookId
                ? startVerse.chapter
                : MinChapterNumber;
            var endChapterNumber = bookId == endVerse.bookId
                ? endVerse.chapter
                : maxChapterNumberAndBookendVerseNumbers.MaxChapterNumber;

            for (var chapterNumber = startChapterNumber; chapterNumber <= endChapterNumber; chapterNumber++)
            {
                // Edge Case: It's theoretically possible that a Bible could be missing an entire chapter.
                if (!maxChapterNumberAndBookendVerseNumbers.BookendVerseNumbersByChapterNumberMap
                    .TryGetValue(chapterNumber, out var bookendVerseNumbers))
                {
                    continue;
                }

                var startVerseNumber = bookId == startVerse.bookId && chapterNumber == startVerse.chapter
                    ? startVerse.verse
                    : bookendVerseNumbers.MinVerseNumber;
                var endVerseNumber = bookId == endVerse.bookId && chapterNumber == endVerse.chapter
                    ? endVerse.verse
                    : bookendVerseNumbers.MaxVerseNumber;

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
    /// Returns the verse ID(s) in the target Bible that corresponds to the given verse ID in the source Bible.
    /// An empty collection will be returned if it's not possible to map between the versification schemes.
    /// </summary>
    public static async Task<IReadOnlyList<int>> ConvertVersificationAsync(
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

        var baseVerseIdsWithOptionalPartBySourceBibleVerseIdMap =
            await versificationService.GetBaseVerseIdWithOptionalPartByBibleVerseIdMapAsync(
                sourceBibleId,
                true,
                ct);
        var targetBibleVerseIdsByBaseVerseIdWithOptionalPartMap =
            await versificationService.GetBibleVerseIdByBaseVerseIdWithOptionalPartMapAsync(
                targetBibleId,
                true,
                ct);
        var targetBibleExcludedVerseIds = await versificationService.GetExcludedVerseIdsAsync(targetBibleId, ct);
        var maxChapterNumberAndBookendVerseNumbersByBookIdMap =
            await versificationService.GetMaxChapterNumberAndBookendVerseNumbersByBookIdMapAsync(targetBibleId, ct);

        return ConvertVersificationCore(
            sourceVerseId,
            baseVerseIdsWithOptionalPartBySourceBibleVerseIdMap,
            targetBibleVerseIdsByBaseVerseIdWithOptionalPartMap,
            targetBibleExcludedVerseIds,
            maxChapterNumberAndBookendVerseNumbersByBookIdMap);
    }

    /// <summary>
    /// This method assumes that the <paramref name="sourceVerseId" /> is valid for the given maps and exclusions.
    /// </summary>
    /// <returns>The converted verse IDs (which may be empty if conversion is not possible).</returns>
    private static IReadOnlyList<int> ConvertVersificationCore(
        int sourceVerseId,
        ReadOnlyDictionary<int, IReadOnlyList<string>> baseVerseIdsWithOptionalPartBySourceBibleVerseIdMap,
        ReadOnlyDictionary<string, IReadOnlyList<int>> targetBibleVerseIdsByBaseVerseIdWithOptionalPartMap,
        ReadOnlySet<int> targetBibleExcludedVerseIds,
        ReadOnlyDictionary<
            BookId,
            (int MaxChapterNumber, ReadOnlyDictionary<int, (int MinVerseNumber, int MaxVerseNumber)>
            BookendVerseNumbersByChapterNumberMap)> targetBibleMaxChapterNumberAndBookendVerseNumbersByBookIdMap)
    {
        // The dictionary only contains mappings where the key is different from the value.
        // If the key verse ID is not present in the mapping (and is not in the exclusions list) then the value matches the key.
        var baseVerseIdsWithOptionalPart = baseVerseIdsWithOptionalPartBySourceBibleVerseIdMap.GetValueOrDefault(sourceVerseId) ??
            [sourceVerseId.ToString()];

        return baseVerseIdsWithOptionalPart
            .SelectMany(baseVerseIdWithOptionalPart =>
            {
                // Base Verse Data includes a Verse ID with an optional verse part at the end (such as 'a', 'b', etc.).
                var baseVerseId = baseVerseIdWithOptionalPart[..10];

                // Rules when a part is present on base verse ID:
                // 1. Use the base verse ID with part if present in the target inverse mapping (the direct map).
                // 2. If not found, then try to map using the base verse ID without a part (in case the target mapping doesn't use parts).
                // 3. Default to the base verse ID without a part (no explicit mapping).
                // Rules when a part is not present on base verse ID:
                // 1. Use the base verse ID without a part if present in the target inverse mapping (the direct map).
                // 2. If not found, then try to map using the base verse ID with part 'a' (in case the target mapping uses parts).
                // 3. Default to the base verse ID without a part (no explicit mapping).
                var targetVerseIds =
                    targetBibleVerseIdsByBaseVerseIdWithOptionalPartMap.GetValueOrDefault(baseVerseIdWithOptionalPart) ??
                    targetBibleVerseIdsByBaseVerseIdWithOptionalPartMap.GetValueOrDefault(
                        baseVerseIdWithOptionalPart.Length > 10
                            ? baseVerseId
                            : $"{baseVerseId}a") ??
                    [int.Parse(baseVerseId)];

                // Handle two scenarios:
                // 1. The target verse ID is in the exclusions list.  In this case omit it.
                // 2. The target verse ID is not in a valid book for the target Bible even though it exists in the base mapping.
                //    In this case omit it.
                // 3. The target verse ID is not in a valid chapter/verse range for the target Bible/book
                //    (e.g. it's "excluded" but at the very end of a chapter so it's not in the exclusions list).
                //    In this case constrain the verse to the nearest valid verse.
                return targetVerseIds
                    .Select(targetVerseId => ConstrainOutOfBoundsVerseId(
                        targetVerseId,
                        targetBibleMaxChapterNumberAndBookendVerseNumbersByBookIdMap))
                    .OfType<int>()
                    .Where(constrainedTargetVerseId => !targetBibleExcludedVerseIds.Contains(constrainedTargetVerseId));
            })
            .Order()
            .Distinct()
            .ToList();
    }

    /// <summary>
    /// Returns the verse IDs in the target Bible that corresponds to the given verse IDs in the source Bible.
    /// Ranges will be expanded to their component verse IDs.
    /// The source verse ID is the dictionary key and the target verse ID(s) are the dictionary value.
    /// An empty collection will be returned for the target verse IDs if it's not possible to map between the versification schemes.
    /// </summary>
    public static async Task<ReadOnlyDictionary<int, IReadOnlyList<int>>> ConvertVersificationRangeAsync(
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

        var baseVerseIdsWithOptionalPartBySourceBibleVerseIdMap =
            await versificationService.GetBaseVerseIdWithOptionalPartByBibleVerseIdMapAsync(
                sourceBibleId,
                true,
                ct);

        var targetBibleVerseIdsByBaseVerseIdWithOptionalPartMap =
            await versificationService.GetBibleVerseIdByBaseVerseIdWithOptionalPartMapAsync(
                targetBibleId,
                true,
                ct);
        var targetBibleExcludedVerseIds = await versificationService.GetExcludedVerseIdsAsync(targetBibleId, ct);
        var maxChapterNumberAndBookendVerseNumbersByBookIdMap =
            await versificationService.GetMaxChapterNumberAndBookendVerseNumbersByBookIdMapAsync(targetBibleId, ct);

        return expandedSourceVerseIds
            .ToDictionary(
                sourceVerseId => sourceVerseId,
                sourceVerseId => ConvertVersificationCore(
                    sourceVerseId,
                    baseVerseIdsWithOptionalPartBySourceBibleVerseIdMap,
                    targetBibleVerseIdsByBaseVerseIdWithOptionalPartMap,
                    targetBibleExcludedVerseIds,
                    maxChapterNumberAndBookendVerseNumbersByBookIdMap))
            .AsReadOnly();
    }
}