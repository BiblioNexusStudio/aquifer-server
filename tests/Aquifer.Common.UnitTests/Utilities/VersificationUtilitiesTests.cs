using System.Collections.ObjectModel;
using Aquifer.Common.Services.Caching;
using Aquifer.Common.Utilities;
using Aquifer.Data.Enums;

namespace Aquifer.Common.UnitTests.Utilities;

public sealed class VersificationUtilitiesTests
{
    private readonly ICachingVersificationService _versificationService = new MockCachingVersificationService();

    [Theory]
    [InlineData(1, 0, false,
        "the source verse ID is invalid")]
    [InlineData(1, int.MaxValue, false,
        "the source verse ID is invalid")]
    [InlineData(1, 1001004002, false,
        "the source Bible does not have the given verse due to exclusion")]
    [InlineData(1, 1072001001, false,
        "the source Bible does not have the given book")]
    [InlineData(1, 1001007001, false,
        "the source Bible does not have the given chapter (greater than max chapter in book)")]
    [InlineData(1, 1001001032, false,
        "the source Bible does not have the given verse (greater than max verse in chapter)")]
    [InlineData(99, 1001001001, false,
        "the source Bible ID doesn't exist.")]
    [InlineData(1, 1001001001, true,
        "the source Bible contains the given verse.")]
    public async Task IsValidVerseId_ValidArguments_Success(
        int bibleId,
        int verseId,
        bool expected,
        string because)
    {
        var result = await VersificationUtilities.IsValidVerseIdAsync(
            bibleId,
            verseId,
            _versificationService,
            CancellationToken.None);

        result.Should().Be(expected, because);
    }

    [Theory]
    [InlineData(1, 1001001001, 1001001001, new[] { 1001001001},
        "a range of a single verse should return that verse")]
    [InlineData(1, 1001001001, 1001001003, new[] { 1001001001, 1001001002, 1001001003 },
        "a simple range within a single chapter should be correctly returned")]
    [InlineData(1, 1002001002, 1002001004, new[] { 1002001002, 1002001004 },
        "a range should not include excluded verse IDs for the given Bible")]
    [InlineData(1, 1001001030, 1001002001, new[] { 1001001030, 1001001031, 1001002001 },
        "a range spanning two chapters should be correctly returned")]
    [InlineData(1, 1001006011, 1002001002, new[] { 1001006011, 1002001001, 1002001002 },
        "a range spanning two books should be correctly returned")]
    public async Task ExpandVerseIdRangeAsync_ValidArguments_Success(
        int bibleId,
        int startVerseId,
        int endVerseId,
        IReadOnlyList<int> expectedVerseIds,
        string because)
    {
        var result = await VersificationUtilities.ExpandVerseIdRangeAsync(
            bibleId,
            startVerseId,
            endVerseId,
            _versificationService,
            CancellationToken.None);

        result.Should().Equal(expectedVerseIds, because);
    }

    [Theory]
    [InlineData(1, 1001003005, 1001003002,
        "the start verse ID must be less than end verse ID")]
    public async Task ExpandVerseIdRange_InvalidArguments_ThrowsArgumentException(
        int bibleId,
        int startVerseId,
        int endVerseId,
        string because)
    {
        await FluentActions.Invoking(
                async () => await VersificationUtilities.ExpandVerseIdRangeAsync(
                    bibleId,
                    startVerseId,
                    endVerseId,
                    _versificationService,
                    CancellationToken.None))
            .Should()
            .ThrowAsync<ArgumentException>(because);
    }

    [Theory]
    [InlineData(1, 1, 1001003001, 1001003001,
        "the source and target Bibles are the same (with versification mappings) so the source and target verse IDs should be the same")]
    [InlineData(3, 3, 1001003001, 1001003001,
        "the source and target Bibles are the same (without versification mappings) so the source and target verse IDs should be the same")]
    [InlineData(1, 2, 1001003001, 1001003003,
        "the source verse ID from Bible 1 has a different versification in Bible 2.")]
    [InlineData(1, 2, 1001002029, 1001002029,
        "the verse ID does not have an explicit versification map for either source or target and so should map to itself")]
    [InlineData(1, 3, 1001002029, 1001002029,
        "the target verse ID should match source verse ID because there is no explicit versification mapping for the source verse ID and the target Bible doesn't have a versification map")]
    [InlineData(1, 2, 1001003004, 1001003005,
        "the target verse ID should match the mapped base verse ID if there is no versification from the target Bible to Base")]
    [InlineData(3, 4, 1001003004, 1001003004,
        "neither Bible has a versification map so the target verse ID should match the source verse ID")]
    [InlineData(1, 2, 1001004003, null,
        "the target Bible does not have the given verse so the conversion is not possible")]
    [InlineData(1, 2, 1001006001, 1001006001,
        "the target Bible does not have the given verse so the conversion is the source's mapped base verse ID without verse part")]
    [InlineData(1, 2, 1001006002, 1001006001,
        "the target Bible does not have the given verse so the conversion is the source's mapped base verse ID without verse part")]
    public async Task ConvertVersification_ValidArguments_Success(
        int sourceBibleId,
        int targetBibleId,
        int sourceVerseId,
        int? expectedTargetVerseId,
        string because)
    {
        var result = await VersificationUtilities.ConvertVersificationAsync(
            sourceBibleId,
            sourceVerseId,
            targetBibleId,
            _versificationService,
            CancellationToken.None);

        result.Should().Be(expectedTargetVerseId, because);
    }

    [Theory]
    [InlineData(1, 1001002001, 1001002001, 2, new[] { 1001002001 },
        "a single verse ID conversion should be successful")]
    [InlineData(1, 1002003002, 1002003006, 2, new[] { 1002003002, 1002003003, 1002003004, 1002003005, 1002003006 },
        "a range conversion should be successful when neither source nor target both have explicit mappings")]
    [InlineData(1, 1001003001, 1001003005, 2, new [] { 1001003003, 1001003004, 1001003005, 1001003005, 1001003005 },
        "a range conversion should be successful when source and target both have mappings")]
    [InlineData(1, 1001005001, 1001005003, 2, new[] { 1001005001, 1001005002, 1001005003 },
        "a range conversion using base parts for both source and target should be successful")]
    public async Task ConvertVersificationRange_ValidArguments_Success(
        int sourceBibleId,
        int sourceStartVerseId,
        int sourceEndVerseId,
        int targetBibleId,
        IReadOnlyList<int> expectedTargetVerseIds,
        string because)
    {
        var result = await VersificationUtilities.ConvertVersificationRangeAsync(
            sourceBibleId,
            sourceStartVerseId,
            sourceEndVerseId,
            targetBibleId,
            _versificationService,
            CancellationToken.None);

        result
            .OrderBy(kvp => kvp.Key)
            .Select(kvp => kvp.Value)
            .Should()
            .Equal(expectedTargetVerseIds.Cast<int?>(), because);
    }

    [Theory]
    [InlineData(99, 1001001001, 1001001001,
        "a Bible that doesn't exist in the data should return null")]
    [InlineData(1, 1079001001, 1079001003,
        "a Book that doesn't exist in the data should return null")]
    [InlineData(1, int.MaxValue, int.MaxValue,
        "a Book that doesn't exist at all should return null")]
    public async Task GetValidVerseIdRangeAsync_NoData_ReturnsNull(
        int bibleId,
        int startVerseId,
        int endVerseId,
        string because)
    {
        var result = await VersificationUtilities.GetValidVerseIdRangeAsync(
            bibleId,
            startVerseId,
            endVerseId,
            _versificationService,
            CancellationToken.None);
        result.Should().BeNull(because);
    }

    [Theory]
    [InlineData(1, 1001002001, 1001002001, 1001002001, 1001002001,
        "a single valid verse ID range should return itself")]
    [InlineData(1, 1002001003, 1002001003, 1002001004, 1002001004,
        "a single excluded verse ID should return the next verse")]
    [InlineData(1, 1001003001, 1001003005, 1001003001, 1001003005,
        "a valid range should return the same range")]
    [InlineData(1, 1002001001, 1002001005, 1002001001, 1002001005,
        "a range containing an excluded verse in the middle should still return itself")]
    [InlineData(1, 1001004001, 1001004004, 1001004003, 1001004004,
        "a range starting with excluded verses should adjust to the next non-excluded verse")]
    [InlineData(1, 1001001000, 1001001999, 1001001001, 1001001031,
        "a range outside of the valid range for the chapter should adjust to the valid range")]
    [InlineData(1, 1001000000, 1001999999, 1001001001, 1001006011,
        "a range outside of the valid range for the book should adjust to the valid range")]
    public async Task GetValidVerseIdRangeAsync_ValidArguments_Success(
        int bibleId,
        int startVerseId,
        int endVerseId,
        int expectedStartVerseId,
        int expectedEndVerseId,
        string because)
    {
        var result = await VersificationUtilities.GetValidVerseIdRangeAsync(
            bibleId,
            startVerseId,
            endVerseId,
            _versificationService,
            CancellationToken.None);
        result.Should().Be((expectedStartVerseId, expectedEndVerseId), because);
    }

    // The tests above cover all the other valid scenarios for this method, too.
    [Theory]
    [InlineData(1, BookId.BookGEN, null, null, null, null, 1001001001, 1001006011,
        "not specifying a range should adjust to the valid range for the book")]
    public async Task GetValidVerseIdRangeAsync_EmptyRange_Success(
        int bibleId,
        BookId bookId,
        int? startChapterNumber,
        int? startVerseNumber,
        int? endChapterNumber,
        int? endVerseNumber,
        int expectedStartVerseId,
        int expectedEndVerseId,
        string because)
    {
        var result = await VersificationUtilities.GetValidVerseIdRangeAsync(
            bibleId,
            bookId,
            startChapterNumber,
            startVerseNumber,
            endChapterNumber,
            endVerseNumber,
            _versificationService,
            CancellationToken.None);
        result.Should().Be((expectedStartVerseId, expectedEndVerseId), because);
    }
}

public class MockCachingVersificationService : ICachingVersificationService
{
    private static readonly ReadOnlyDictionary<int, ReadOnlyDictionary<int, string>>
        s_baseVerseIdWithOptionalPartByBibleVerseIdMapByBibleIdMap =
        new Dictionary<int, ReadOnlyDictionary<int, string>>
        {
            // All mappings are in GEN 3.
            [1] = new Dictionary<int, string>
                {
                    [1001003001] = "1001003002",
                    [1001003002] = "1001003003",
                    [1001003003] = "1001003004",
                    [1001003004] = "1001003005",
                    [1001005001] = "1001005001a",
                    [1001005002] = "1001005001b",
                    [1001005003] = "1001005001c",
                    [1001006001] = "1001006001a",
                    [1001006002] = "1001006001b",
            }
                .AsReadOnly(),
            [2] = new Dictionary<int, string>
                {
                    [1001003003] = "1001003002",
                    [1001003004] = "1001003003",
                    [1001003005] = "1001003004",
                    [1001005001] = "1001005001a",
                    [1001005002] = "1001005001b",
                    [1001005003] = "1001005001c",
            }
                .AsReadOnly(),
        }
        .AsReadOnly();

    private static readonly ReadOnlyDictionary<int, ReadOnlySet<int>> s_excludedVerseIdsByBibleIdMap =
        new Dictionary<int, ReadOnlySet<int>>
        {
            [1] = new(new HashSet<int>(
                [
                    1001004001, // GEN 4:1
                    1001004002, // GEN 4:2
                    1002001003, // EXO 1:3
                ])
            ),
            [2] = new(new HashSet<int>(
                [
                    1001004002, // GEN 4:2
                    1001004003, // GEN 4:3
                    1072001003, // SIR 1:3
                ])),
        }
        .AsReadOnly();

    private static readonly ReadOnlyDictionary<
            BookId,
            (int MaxChapterNumber, ReadOnlyDictionary<int, (int MinVerseNumber, int MaxVerseNumber)> BookendVerseNumbersByChapterNumberMap)>
        s_baseMaxChapterNumberAndBookendVerseNumbersByBookIdMap =
            new Dictionary<BookId, (int, ReadOnlyDictionary<int, (int, int)>)>
            {
                [BookId.BookGEN] =
                (
                    6,
                    new Dictionary<int, (int, int)>
                    {
                        [1] = (1, 31),
                        [2] = (1, 29),
                        [3] = (1, 24),
                        [4] = (1, 26),
                        [5] = (1, 32),
                        [6] = (1, 11),
                    }
                    .AsReadOnly()
                ),
                [BookId.BookEXO] =
                (
                    4,
                    new Dictionary<int, (int, int)>
                    {
                        [1] = (1, 5),
                        [2] = (1, 9),
                        [3] = (1, 24),
                        [4] = (1, 11),
                    }
                    .AsReadOnly()
                ),
            }
            .AsReadOnly();

    private static readonly ReadOnlyDictionary<
            BookId,
            (int MaxChapterNumber, ReadOnlyDictionary<int, (int MinVerseNumber, int MaxVerseNumber)> BookendVerseNumbersByChapterNumberMap)>
        s_additionalMaxChapterNumberAndVerseNumbersByBookIdMap =
            new Dictionary<BookId, (int, ReadOnlyDictionary<int, (int, int)>)>
                {
                    [BookId.BookSIR] =
                    (
                        3,
                        new Dictionary<int, (int, int)>
                            {
                                [1] = (1, 4),
                                [2] = (1, 5),
                                [3] = (1, 6),
                            }
                            .AsReadOnly()
                    ),
                }
                .AsReadOnly();

    public Task<ReadOnlyDictionary<int, string>>
        GetBaseVerseIdWithOptionalPartByBibleVerseIdMapAsync(int bibleId, CancellationToken cancellationToken)
    {
        return Task.FromResult(s_baseVerseIdWithOptionalPartByBibleVerseIdMapByBibleIdMap.GetValueOrDefault(bibleId)
            ?? new Dictionary<int, string>().AsReadOnly());
    }

    public async Task<ReadOnlyDictionary<string, int>> GetBibleVerseIdByBaseVerseIdWithOptionalPartMapAsync(
        int bibleId,
        CancellationToken cancellationToken)
    {
        return (await GetBaseVerseIdWithOptionalPartByBibleVerseIdMapAsync(bibleId, cancellationToken))
            .ToDictionary(x => x.Value, x => x.Key)
            .AsReadOnly();
    }

    public Task<ReadOnlySet<int>> GetExcludedVerseIdsAsync(int bibleId, CancellationToken cancellationToken)
    {
        return Task.FromResult(s_excludedVerseIdsByBibleIdMap.GetValueOrDefault(bibleId, new ReadOnlySet<int>(new HashSet<int>())));
    }

    public Task<bool> DoesBibleIncludeBookAsync(int bibleId, BookId bookId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<int?> GetMaxChapterNumberForBookAsync(int bibleId, BookId bookId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<(int MinVerseNumber, int MaxVerseNumber)?> GetBookendVerseNumbersForChapterAsync(int bibleId, BookId bookId, int chapterNumber, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ReadOnlyDictionary<
            BookId,
            (int MaxChapterNumber,
                ReadOnlyDictionary<int, (int MinVerseNumber, int MaxVerseNumber)> BookendVerseNumbersByChapterNumberMap)>>
        GetMaxChapterNumberAndBookendVerseNumbersByBookIdMapAsync(int bibleId, CancellationToken cancellationToken)
    {
        return Task.FromResult(bibleId switch
        {
            // Bible 99 doesn't exist.
            99 => new Dictionary<BookId, (int, ReadOnlyDictionary<int, (int, int)>)>()
                .AsReadOnly(),

            //  Bible 2 gets an additional book.
            2 => s_baseMaxChapterNumberAndBookendVerseNumbersByBookIdMap
                .Concat(s_additionalMaxChapterNumberAndVerseNumbersByBookIdMap)
                .ToDictionary()
                .AsReadOnly(),

            // Return the same max chapters/verses for all other Bibles.
            _ => s_baseMaxChapterNumberAndBookendVerseNumbersByBookIdMap,
        });
    }
}