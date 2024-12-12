using Aquifer.Common.Utilities;
using Aquifer.Data.Enums;

namespace Aquifer.Common.UnitTests.Utilities;

public sealed class BibleWordIdentifierTests
{
    [Theory]
    [InlineData(0L, BookId.BookGEN, 0, 0, 0, 0)]
    [InlineData(40_001_001_001_1L, BookId.BookMAT, 1, 1, 1, 1)]
    [InlineData(40_001_001_001_9L, BookId.BookMAT, 1, 1, 1, 9)]
    [InlineData(40_001_001_021_1L, BookId.BookMAT, 1, 1, 21, 1)]
    [InlineData(40_001_025_001_1L, BookId.BookMAT, 1, 25, 1, 1)]
    [InlineData(40_028_001_001_1L, BookId.BookMAT, 28, 1, 1, 1)]
    [InlineData(66_001_001_001_1L, BookId.BookREV, 1, 1, 1, 1)]
    public void Constructor_FromWordIdentifier_ValidArguments_ReturnsExpectedValue(long wordIdentifier, BookId expectedBookId, int expectedChapter, int expectedVerse, int expectedWord, int expectedWordSegment)
    {
        var actual = new BibleWordIdentifier(wordIdentifier);
        Assert.Equal(expectedBookId, actual.BookId);
        Assert.Equal(expectedChapter, actual.Chapter);
        Assert.Equal(expectedVerse, actual.Verse);
        Assert.Equal(expectedWord, actual.Word);
        Assert.Equal(expectedWordSegment, actual.WordSegment);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(87_000_000_000_0L)]
    [InlineData(long.MinValue)]
    [InlineData(long.MaxValue)]
    public void Constructor_FromWordIdentifier_InvalidWordIdentifier_ThrowsException(long wordIdentifier)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new BibleWordIdentifier(wordIdentifier));
    }

    [Theory]
    [InlineData(BookId.BookMAT, 40_000_000_000_0L)]
    [InlineData(BookId.BookREV, 66_000_000_000_0L)]
    public void Constructor_LowerBoundOfBook_ValidArguments_ReturnsExpectedValue(BookId bookId, long expectedValue)
    {
        Assert.Equal(expectedValue, new BibleWordIdentifier(bookId));
    }

    [Theory]
    [InlineData(BookId.None)]
    public void Constructor_LowerBoundOfBook_InvalidBookId_ThrowsException(BookId bookId)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new BibleWordIdentifier(bookId));
    }

    [Theory]
    [InlineData(BookId.BookMAT, 40_999_999_999_9L)]
    [InlineData(BookId.BookREV, 66_999_999_999_9L)]
    public void GetUpperBoundOfBook_ValidArguments_ReturnsExpectedValue(BookId bookId, long expectedValue)
    {
        Assert.Equal(expectedValue, BibleWordIdentifier.GetUpperBoundOfBook(bookId));
    }

    [Theory]
    [InlineData(BookId.None)]
    public void GetUpperBoundOfBook_InvalidBookId_ThrowsException(BookId bookId)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => BibleWordIdentifier.GetUpperBoundOfBook(bookId));
    }

    [Theory]
    [InlineData(BookId.BookMAT, 1, 40_001_000_000_0L)]
    [InlineData(BookId.BookMAT, 28, 40_028_000_000_0L)]
    [InlineData(BookId.BookREV, 1, 66_001_000_000_0L)]
    public void Constructor_LowerBoundOfChapter_ValidArguments_ReturnsExpectedValue(BookId bookId, int chapter, long expectedValue)
    {
        Assert.Equal(expectedValue, new BibleWordIdentifier(bookId, chapter));
    }

    [Theory]
    [InlineData(BookId.None)]
    public void Constructor_LowerBoundOfChapter_InvalidBookId_ThrowsException(BookId bookId)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new BibleWordIdentifier(bookId, chapter: 1));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1_000)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void Constructor_LowerBoundOfChapter_InvalidChapter_ThrowsException(int chapter)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new BibleWordIdentifier(BookId.BookMAT, chapter));
    }

    [Theory]
    [InlineData(BookId.BookMAT, 1, 40_001_999_999_9L)]
    [InlineData(BookId.BookMAT, 28, 40_028_999_999_9L)]
    [InlineData(BookId.BookREV, 1, 66_001_999_999_9L)]
    public void GetUpperBoundOfChapter_ValidArguments_ReturnsExpectedValue(BookId bookId, int chapter, long expectedValue)
    {
        Assert.Equal(expectedValue, BibleWordIdentifier.GetUpperBoundOfChapter(bookId, chapter));
    }

    [Theory]
    [InlineData(BookId.None)]
    public void GetUpperBoundOfChapter_InvalidBookId_ThrowsException(BookId bookId)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => BibleWordIdentifier.GetUpperBoundOfChapter(bookId, chapter: 1));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1_000)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void GetUpperBoundOfChapter_InvalidChapter_ThrowsException(int chapter)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => BibleWordIdentifier.GetUpperBoundOfChapter(BookId.BookMAT, chapter));
    }

    [Theory]
    [InlineData(BookId.BookMAT, 1, 1, 40_001_001_000_0L)]
    [InlineData(BookId.BookMAT, 1, 25, 40_001_025_000_0L)]
    [InlineData(BookId.BookMAT, 28, 1, 40_028_001_000_0L)]
    [InlineData(BookId.BookREV, 1, 1, 66_001_001_000_0L)]
    public void Constructor_LowerBoundOfVerse_ValidArguments_ReturnsExpectedValue(BookId bookId, int chapter, int verse, long expectedValue)
    {
        Assert.Equal(expectedValue, new BibleWordIdentifier(bookId, chapter, verse));
    }

    [Theory]
    [InlineData(BookId.None)]
    public void Constructor_LowerBoundOfVerse_InvalidBookId_ThrowsException(BookId bookId)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new BibleWordIdentifier(bookId, chapter: 1, verse: 1));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1_000)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void Constructor_LowerBoundOfVerse_InvalidChapter_ThrowsException(int chapter)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new BibleWordIdentifier(BookId.BookMAT, chapter, verse: 1));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1_000)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void Constructor_LowerBoundOfVerse_InvalidVerse_ThrowsException(int verse)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new BibleWordIdentifier(BookId.BookMAT, chapter: 1, verse));
    }

    [Theory]
    [InlineData(BookId.BookMAT, 1, 1, 40_001_001_999_9L)]
    [InlineData(BookId.BookMAT, 1, 25, 40_001_025_999_9L)]
    [InlineData(BookId.BookMAT, 28, 1, 40_028_001_999_9L)]
    [InlineData(BookId.BookREV, 1, 1, 66_001_001_999_9L)]
    public void GetUpperBoundOfVerse_ValidArguments_ReturnsExpectedValue(BookId bookId, int chapter, int verse, long expectedValue)
    {
        Assert.Equal(expectedValue, BibleWordIdentifier.GetUpperBoundOfVerse(bookId, chapter, verse));
    }

    [Theory]
    [InlineData(BookId.None)]
    public void GetUpperBoundOfVerse_InvalidBookId_ThrowsException(BookId bookId)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => BibleWordIdentifier.GetUpperBoundOfVerse(bookId, chapter: 1, verse: 1));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1_000)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void GetUpperBoundOfVerse_InvalidChapter_ThrowsException(int chapter)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => BibleWordIdentifier.GetUpperBoundOfVerse(BookId.BookMAT, chapter, verse: 1));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1_000)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void GetUpperBoundOfVerse_InvalidVerse_ThrowsException(int verse)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => BibleWordIdentifier.GetUpperBoundOfVerse(BookId.BookMAT, chapter: 1, verse));
    }

    [Theory]
    [InlineData(BookId.BookMAT, 1, 1, 1, 40_001_001_001_0L)]
    [InlineData(BookId.BookMAT, 1, 1, 21, 40_001_001_021_0L)]
    [InlineData(BookId.BookMAT, 1, 25, 1, 40_001_025_001_0L)]
    [InlineData(BookId.BookMAT, 28, 1, 1, 40_028_001_001_0L)]
    [InlineData(BookId.BookREV, 1, 1, 1, 66_001_001_001_0L)]
    public void Constructor_LowerBoundOfWord_ValidArguments_ReturnsExpectedValue(BookId bookId, int chapter, int verse, int word, long expectedValue)
    {
        Assert.Equal(expectedValue, new BibleWordIdentifier(bookId, chapter, verse, word));
    }

    [Theory]
    [InlineData(BookId.None)]
    public void Constructor_LowerBoundOfWord_InvalidBookId_ThrowsException(BookId bookId)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new BibleWordIdentifier(bookId, chapter: 1, verse: 1, word: 1));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1_000)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void Constructor_LowerBoundOfWord_InvalidChapter_ThrowsException(int chapter)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new BibleWordIdentifier(BookId.BookMAT, chapter, verse: 1, word: 1));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1_000)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void Constructor_LowerBoundOfWord_InvalidVerse_ThrowsException(int verse)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new BibleWordIdentifier(BookId.BookMAT, chapter: 1, verse, word: 1));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1_000)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void Constructor_LowerBoundOfWord_InvalidWord_ThrowsException(int word)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new BibleWordIdentifier(BookId.BookMAT, chapter: 1, verse: 1, word));
    }

    [Theory]
    [InlineData(BookId.BookMAT, 1, 1, 1, 40_001_001_001_9L)]
    [InlineData(BookId.BookMAT, 1, 1, 21, 40_001_001_021_9L)]
    [InlineData(BookId.BookMAT, 1, 25, 1, 40_001_025_001_9L)]
    [InlineData(BookId.BookMAT, 28, 1, 1, 40_028_001_001_9L)]
    [InlineData(BookId.BookREV, 1, 1, 1, 66_001_001_001_9L)]
    public void GetUpperBoundOfWord_ValidArguments_ReturnsExpectedValue(BookId bookId, int chapter, int verse, int word, long expectedValue)
    {
        Assert.Equal(expectedValue, BibleWordIdentifier.GetUpperBoundOfWord(bookId, chapter, verse, word));
    }

    [Theory]
    [InlineData(BookId.None)]
    public void GetUpperBoundOfWord_InvalidBookId_ThrowsException(BookId bookId)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => BibleWordIdentifier.GetUpperBoundOfWord(bookId, chapter: 1, verse: 1, word: 1));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1_000)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void GetUpperBoundOfWord_InvalidChapter_ThrowsException(int chapter)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => BibleWordIdentifier.GetUpperBoundOfWord(BookId.BookMAT, chapter, verse: 1, word: 1));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1_000)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void GetUpperBoundOfWord_InvalidVerse_ThrowsException(int verse)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => BibleWordIdentifier.GetUpperBoundOfWord(BookId.BookMAT, chapter: 1, verse, word: 1));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1_000)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void GetUpperBoundOfWord_InvalidWord_ThrowsException(int word)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => BibleWordIdentifier.GetUpperBoundOfWord(BookId.BookMAT, chapter: 1, verse: 1, word));
    }

    [Theory]
    [InlineData(BookId.BookMAT, 1, 1, 1, 1, 40_001_001_001_1L)]
    [InlineData(BookId.BookMAT, 1, 1, 1, 9, 40_001_001_001_9L)]
    [InlineData(BookId.BookMAT, 1, 1, 21, 1, 40_001_001_021_1L)]
    [InlineData(BookId.BookMAT, 1, 25, 1, 1, 40_001_025_001_1L)]
    [InlineData(BookId.BookMAT, 28, 1, 1, 1, 40_028_001_001_1L)]
    [InlineData(BookId.BookREV, 1, 1, 1, 1, 66_001_001_001_1L)]
    public void Constructor_FullySpecifiedComponents_ValidArguments_ReturnsExpectedValue(BookId bookId, int chapter, int verse, int word, int wordSegment, long expectedValue)
    {
        Assert.Equal(expectedValue, new BibleWordIdentifier(bookId, chapter, verse, word, wordSegment));
    }

    [Theory]
    [InlineData(BookId.None)]
    public void Constructor_FullySpecifiedComponents_InvalidBookId_ThrowsException(BookId bookId)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new BibleWordIdentifier(bookId, chapter: 1, verse: 1, word: 1, wordSegment: 1));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1_000)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void Constructor_FullySpecifiedComponents_InvalidChapter_ThrowsException(int chapter)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new BibleWordIdentifier(BookId.BookMAT, chapter, verse: 1, word: 1, wordSegment: 1));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1_000)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void Constructor_FullySpecifiedComponents_InvalidVerse_ThrowsException(int verse)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new BibleWordIdentifier(BookId.BookMAT, chapter: 1, verse, word: 1, wordSegment: 1));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1_000)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void Constructor_FullySpecifiedComponents_InvalidWord_ThrowsException(int word)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new BibleWordIdentifier(BookId.BookMAT, chapter: 1, verse: 1, word, wordSegment: 1));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(10)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void Constructor_FullySpecifiedComponents_InvalidWordSegment_ThrowsException(int wordSegment)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new BibleWordIdentifier(BookId.BookMAT, chapter: 1, verse: 1, word: 1, wordSegment));
    }
}