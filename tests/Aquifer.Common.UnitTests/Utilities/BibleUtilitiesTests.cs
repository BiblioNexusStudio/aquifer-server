using Aquifer.Common.Utilities;
using Aquifer.Data.Enums;

namespace Aquifer.Common.UnitTests.Utilities;

public sealed class BibleUtilitiesTests
{
    public static TheoryData<BookId, IReadOnlyList<int>, IReadOnlyList<(int StartVerseId, int EndVerseId)>>
        GetVerseRangesForBookAndChaptersTestData => new()
    {
        {
            BookId.BookMAT,
            [],
            [(1_041_000_000, 1_041_999_999)]
        },
        {
            BookId.BookMAT,
            [1],
            [(1_041_001_000, 1_041_001_999)]
        },
        {
            BookId.BookMAT,
            [1, 2, 3],
            [(1_041_001_000, 1_041_003_999)]
        },
        {
            BookId.BookMAT,
            [1, 3, 5],
            [
                (1_041_001_000, 1_041_001_999),
                (1_041_003_000, 1_041_003_999),
                (1_041_005_000, 1_041_005_999),
            ]
        },
        {
            BookId.BookMAT,
            [1, 3, 4, 5, 7, 10, 11, 12, 13, 14, 20],
            [
                (1_041_001_000, 1_041_001_999),
                (1_041_003_000, 1_041_005_999),
                (1_041_007_000, 1_041_007_999),
                (1_041_010_000, 1_041_014_999),
                (1_041_020_000, 1_041_020_999),
            ]
        },
    };

    [Theory]
    [MemberData(nameof(GetVerseRangesForBookAndChaptersTestData))]
    public void VerseRangesForBookAndChapters_WithConsecutiveChapters_ReturnsCollapsedRange(
        BookId bookId,
        IReadOnlyList<int> chapters,
        IReadOnlyList<(int StartVerseId, int EndVerseId)> expectedValue)
    {
        Assert.Equal(expectedValue, BibleUtilities.VerseRangesForBookAndChapters(bookId, chapters));
    }
}