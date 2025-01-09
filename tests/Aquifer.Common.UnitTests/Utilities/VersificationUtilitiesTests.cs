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
        "Invalid source verse ID.")]
    [InlineData(1, int.MaxValue, false,
        "Invalid source verse ID.")]
    [InlineData(1, 1001004002, false,
        "Source Bible does not have the given verse due to exclusion.")]
    [InlineData(1, 1072001001, false,
        "Source Bible does not have the given book.")]
    [InlineData(1, 1001006001, false,
        "Source Bible does not have the given chapter (greater than max chapter in book).")]
    [InlineData(1, 1001001032, false,
        "Source Bible does not have the given verse (greater than max verse in chapter).")]
    [InlineData(99, 1001001001, false,
        "Source Bible ID doesn't exist.")]
    [InlineData(1, 1001001001, true,
        "Source Bible contains the given verse.")]
    public async Task IsValidVerseId_ValidArguments_Success(
        int bibleId,
        int verseId,
        bool expected,
        string because)
    {
        (await VersificationUtilities.IsValidVerseIdAsync(
                bibleId,
                verseId,
                _versificationService,
                CancellationToken.None))
            .Should()
            .Be(expected, because);
    }

    [Theory]
    [InlineData(1, 1001001001, 1001001001, new[] { 1001001001},
        "A range of a single verse should return that verse.")]
    [InlineData(1, 1001001001, 1001001003, new[] { 1001001001, 1001001002, 1001001003 },
        "A simple range within a single chapter should be correctly returned.")]
    [InlineData(1, 1002001002, 1002001004, new[] { 1002001002, 1002001004 },
        "A range should not include excluded verse IDs for the given Bible.")]
    [InlineData(1, 1001001030, 1001002001, new[] { 1001001030, 1001001031, 1001002001 },
        "A range spanning two chapters should be correctly returned.")]
    [InlineData(1, 1001005032, 1002001002, new[] { 1001005032, 1002001001, 1002001002 },
        "A range spanning two books should be correctly returned.")]
    public async Task ExpandVerseIdRangeAsync_ValidArguments_Success(
        int bibleId,
        int startVerseId,
        int endVerseId,
        IReadOnlyList<int> expectedVerseIds,
        string because)
    {
        (await VersificationUtilities.ExpandVerseIdRangeAsync(
                bibleId,
                startVerseId,
                endVerseId,
                _versificationService,
                CancellationToken.None))
            .Should()
            .Equal(expectedVerseIds, because);
    }

    [Theory]
    [InlineData(1, 1001003005, 1001003002,
        "Start verse ID must be less than end verse ID.")]
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
        "The source and target Bibles are the same (with versification mappings) so the source and target verse IDs should be the same.")]
    [InlineData(3, 3, 1001003001, 1001003001,
        "The source and target Bibles are the same (without versification mappings) so the source and target verse IDs should be the same.")]
    [InlineData(1, 2, 1001003001, 1001003003,
        "Source verse ID from Bible 1 has a different versification in Bible 2.")]
    [InlineData(1, 2, 1001002029, 1001002029,
        "Verse ID does not have an explicit versification map for either source or target and so should map to itself.")]
    [InlineData(1, 3, 1001002029, 1001002029,
        "Target verse ID should match source verse ID because there is no explicit versification mapping for the source verse ID and the target Bible doesn't have a versification map.")]
    [InlineData(1, 2, 1001003004, 1001003005,
        "Target verse ID should match the mapped base verse ID if there is no versification from the target Bible to Base.")]
    [InlineData(3, 4, 1001003004, 1001003004,
        "Neither Bible has a versification map so target verse ID should match source verse ID.")]
    [InlineData(1, 2, 1001004003, null,
        "Target Bible does not have the given verse so the conversion is not possible.")]
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
        "Single verse ID conversion should be successful.")]
    [InlineData(1, 1001003001, 1001003005, 2, new [] { 1001003003, 1001003004, 1001003005, 1001003005, 1001003005 },
        "Range conversion should be successful.")]
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
}

public class MockCachingVersificationService : ICachingVersificationService
{
    private static readonly ReadOnlyDictionary<int, ReadOnlyDictionary<int, int>> s_baseVerseIdByBibleVerseIdMapByBibleIdMap =
        new Dictionary<int, ReadOnlyDictionary<int, int>>
        {
            // All mappings are in GEN 3.
            [1] = new Dictionary<int, int>
                {
                    [1001003001] = 1001003002,
                    [1001003002] = 1001003003,
                    [1001003003] = 1001003004,
                    [1001003004] = 1001003005,
                }
                .AsReadOnly(),
            [2] = new Dictionary<int, int>
                {
                    [1001003003] = 1001003002,
                    [1001003004] = 1001003003,
                    [1001003005] = 1001003004,
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

    private static readonly
        ReadOnlyDictionary<BookId, (int MaxChapterNumber, ReadOnlyDictionary<int, int> MaxVerseNumberByChapterNumberMap)>
        s_baseMaxChapterNumberAndVerseNumbersByBookIdMap =
            new Dictionary<BookId, (int MaxChapterNumber, ReadOnlyDictionary<int, int> MaxVerseNumberByChapterNumberMap)>
            {
                [BookId.BookGEN] =
                (
                    5,
                    new Dictionary<int, int>
                    {
                        [1] = 31,
                        [2] = 29,
                        [3] = 24,
                        [4] = 26,
                        [5] = 32,
                    }
                    .AsReadOnly()
                ),
                [BookId.BookEXO] =
                (
                    4,
                    new Dictionary<int, int>
                    {
                        [1] = 5,
                        [2] = 9,
                        [3] = 24,
                        [4] = 11,
                    }
                    .AsReadOnly()
                ),
            }
            .AsReadOnly();

    private static readonly
        ReadOnlyDictionary<BookId, (int MaxChapterNumber, ReadOnlyDictionary<int, int> MaxVerseNumberByChapterNumberMap)>
            s_additionalMaxChapterNumberAndVerseNumbersByBookIdMap =
                new Dictionary<BookId, (int MaxChapterNumber, ReadOnlyDictionary<int, int> MaxVerseNumberByChapterNumberMap)>
                    {
                        [BookId.BookSIR] =
                        (
                            3,
                            new Dictionary<int, int>
                                {
                                    [1] = 4,
                                    [2] = 5,
                                    [3] = 6,
                                }
                                .AsReadOnly()
                        ),
                    }
                    .AsReadOnly();

    public Task<ReadOnlyDictionary<int, int>>
        GetBaseVerseIdByBibleVerseIdMapAsync(int bibleId, CancellationToken cancellationToken)
    {
        return Task.FromResult(s_baseVerseIdByBibleVerseIdMapByBibleIdMap.GetValueOrDefault(bibleId) ?? new Dictionary<int, int>().AsReadOnly());
    }

    public async Task<ReadOnlyDictionary<int, int>> GetBibleVerseIdByBaseVerseIdMapAsync(int bibleId, CancellationToken cancellationToken)
    {
        return (await GetBaseVerseIdByBibleVerseIdMapAsync(bibleId, cancellationToken))
            .ToDictionary(x => x.Value, x => x.Key)
            .AsReadOnly();
    }

    public Task<ReadOnlySet<int>> GetExcludedVerseIdsAsync(int bibleId, CancellationToken cancellationToken)
    {
        return Task.FromResult(s_excludedVerseIdsByBibleIdMap.GetValueOrDefault(bibleId, new ReadOnlySet<int>(new HashSet<int>())));
    }

    public Task<ReadOnlyDictionary<BookId, (int MaxChapterNumber, ReadOnlyDictionary<int, int> MaxVerseNumberByChapterNumberMap)>>
        GetMaxChapterNumberAndVerseNumbersByBookIdMapAsync(int bibleId, CancellationToken cancellationToken)
    {
        return Task.FromResult(bibleId switch
        {
            // Bible 99 doesn't exist.
            99 => new Dictionary<BookId, (int MaxChapterNumber, ReadOnlyDictionary<int, int> MaxVerseNumberByChapterNumberMap)>()
                .AsReadOnly(),

            //  Bible 2 gets an additional book.
            2 => s_baseMaxChapterNumberAndVerseNumbersByBookIdMap
                .Concat(s_additionalMaxChapterNumberAndVerseNumbersByBookIdMap)
                .ToDictionary()
                .AsReadOnly(),

            // Return the same max chapters/verses for all other Bibles.
            _ => s_baseMaxChapterNumberAndVerseNumbersByBookIdMap,
        });
    }
}