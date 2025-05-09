using Aquifer.Data.Enums;

namespace Aquifer.Common.Utilities;

/// <summary>
/// Only use with Bible word identifiers, NOT Greek word identifiers!!!
/// The word identifier in the dbo.BibleVersionWords.WordIdentifier column has one more digit than the
/// dbo.GreekNewTestamentWords.WordIdentifier column
/// because Greek alignment data does not include word segments.
/// Format for Bible word identifiers is BBCCCVVVWWWS.
/// Format for Greek word identifiers is BBCCCVVVWWW and thus this class should not be used with Greek data.
/// Key:
/// B: Book
/// C: Chapter
/// V: Verse
/// W: Word
/// S: Word segment
/// </summary>
public sealed class BibleWordIdentifier
{
    private const long BookMultiplier = ChapterMultiplier * 1000L;
    private const long ChapterMultiplier = VerseMultiplier * 1000L;
    private const long VerseMultiplier = WordMultiplier * 1000L;
    private const long WordMultiplier = 10L;

    private const int MinChapter = 0;
    private const int MaxChapter = 999;

    private const int MinVerse = 0;
    private const int MaxVerse = 999;

    private const int MinWord = 0;
    private const int MaxWord = 999;

    private const int MinWordSegment = 0;
    private const int MaxWordSegment = 9;

    private const long MinWordIdentifier = 0L;

    private static readonly BookId s_maxBookId = Enum.GetValues(typeof(BookId)).Cast<BookId>().Max();
    private static readonly long s_maxWordIdentifier = ((int)s_maxBookId * BookMultiplier) - 1L;

    /// <summary>
    /// Creates a <see cref="BibleWordIdentifier" /> from the raw word identifier value (must include word segment part).
    /// </summary>
    public BibleWordIdentifier(long wordIdentifier)
    {
        if (wordIdentifier < MinWordIdentifier || wordIdentifier > s_maxWordIdentifier)
        {
            throw new ArgumentOutOfRangeException(
                nameof(wordIdentifier),
                wordIdentifier,
                $"{nameof(wordIdentifier)} must be between {MinWordIdentifier} and {s_maxWordIdentifier} but was \"{wordIdentifier}\".");
        }

        WordIdentifier = wordIdentifier;

        var num = wordIdentifier;

        WordSegment = (int)(num % 10L);
        num /= 10L;

        Word = (int)(num % 1000L);
        num /= 1000L;

        Verse = (int)(num % 1000L);
        num /= 1000L;

        Chapter = (int)(num % 1000L);
        num /= 1000L;

        BookId = (BookId)(num < 40 ? num : num + 1);
    }

    /// <summary>
    /// Creates a <see cref="BibleWordIdentifier" /> representing the lower bound of a Book.
    /// </summary>
    public BibleWordIdentifier(BookId bookId)
        : this(bookId, MinChapter, MinVerse, MinWord, MinWordSegment)
    {
    }

    /// <summary>
    /// Creates a <see cref="BibleWordIdentifier" /> representing the lower bound of a Chapter.
    /// </summary>
    public BibleWordIdentifier(BookId bookId, int chapter)
        : this(bookId, chapter, MinVerse, MinWord, MinWordSegment)
    {
    }

    /// <summary>
    /// Creates a <see cref="BibleWordIdentifier" /> representing the lower bound of a Verse.
    /// </summary>
    public BibleWordIdentifier(BookId bookId, int chapter, int verse)
        : this(bookId, chapter, verse, MinWord, MinWordSegment)
    {
    }

    /// <summary>
    /// Creates a <see cref="BibleWordIdentifier" /> representing the lower bound of a Word.
    /// </summary>
    public BibleWordIdentifier(BookId bookId, int chapter, int verse, int word)
        : this(bookId, chapter, verse, word, MinWordSegment)
    {
    }

    /// <summary>
    /// Creates a fully specified <see cref="BibleWordIdentifier" />.
    /// </summary>
    public BibleWordIdentifier(BookId bookId, int chapter, int verse, int word, int wordSegment)
    {
        if (bookId <= BookId.None || bookId > s_maxBookId)
        {
            throw new ArgumentOutOfRangeException(
                nameof(bookId),
                $"Invalid {nameof(BookId)} value for calculating word identifiers: \"{bookId}\".");
        }

        if (chapter is < MinChapter or > MaxChapter)
        {
            throw new ArgumentOutOfRangeException(
                nameof(chapter),
                chapter,
                $"{nameof(chapter)} must be between {MinChapter} and {MaxChapter} but was \"{chapter}\".");
        }

        if (verse is < MinVerse or > MaxVerse)
        {
            throw new ArgumentOutOfRangeException(
                nameof(chapter),
                chapter,
                $"{nameof(chapter)} must be between {MinVerse} and {MaxVerse} but was \"{verse}\".");
        }

        if (word is < MinWord or > MaxWord)
        {
            throw new ArgumentOutOfRangeException(
                nameof(chapter),
                chapter,
                $"{nameof(chapter)} must be between {MinWord} and {MaxWord} but was \"{word}\".");
        }

        if (wordSegment is < MinWordSegment or > MaxWordSegment)
        {
            throw new ArgumentOutOfRangeException(
                nameof(chapter),
                chapter,
                $"{nameof(chapter)} must be between {MinWordSegment} and {MaxWordSegment} but was \"{wordSegment}\".");
        }

        BookId = bookId;
        Chapter = chapter;
        Verse = verse;
        Word = word;
        WordSegment = wordSegment;

        WordIdentifier = CalculateWordIdentifier((int)bookId - 1, chapter, verse, word, wordSegment);
    }

    public long WordIdentifier { get; }
    public BookId BookId { get; }
    public int Chapter { get; }
    public int Verse { get; }
    public int Word { get; }
    public int WordSegment { get; }

    /// <summary>
    /// Creates a <see cref="BibleWordIdentifier" /> representing the upper bound of a Book.
    /// </summary>
    public static BibleWordIdentifier GetUpperBoundOfBook(BookId bookId)
    {
        return new BibleWordIdentifier(bookId, MaxChapter, MaxVerse, MaxWord, MaxWordSegment);
    }

    /// <summary>
    /// Creates a <see cref="BibleWordIdentifier" /> representing the upper bound of a Chapter.
    /// </summary>
    public static BibleWordIdentifier GetUpperBoundOfChapter(BookId bookId, int chapter)
    {
        return new BibleWordIdentifier(bookId, chapter, MaxVerse, MaxWord, MaxWordSegment);
    }

    /// <summary>
    /// Creates a <see cref="BibleWordIdentifier" /> representing the upper bound of a Verse.
    /// </summary>
    public static BibleWordIdentifier GetUpperBoundOfVerse(BookId bookId, int chapter, int verse)
    {
        return new BibleWordIdentifier(bookId, chapter, verse, MaxWord, MaxWordSegment);
    }

    /// <summary>
    /// Creates a <see cref="BibleWordIdentifier" /> representing the upper bound of a Word.
    /// </summary>
    public static BibleWordIdentifier GetUpperBoundOfWord(BookId bookId, int chapter, int verse, int word)
    {
        return new BibleWordIdentifier(bookId, chapter, verse, word, MaxWordSegment);
    }

    public static implicit operator long(BibleWordIdentifier source)
    {
        return source.WordIdentifier;
    }

    public override string ToString()
    {
        return WordIdentifier.ToString("D12");
    }

    private static long CalculateWordIdentifier(int book, int chapter, int verse, int word, int wordSegment)
    {
        return (book * BookMultiplier) + (chapter * ChapterMultiplier) + (verse * VerseMultiplier) + (word * WordMultiplier) + wordSegment;
    }
}