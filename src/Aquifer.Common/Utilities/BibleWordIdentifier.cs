using Aquifer.Data.Enums;

namespace Aquifer.Common.Utilities;

/// <summary>
/// Only use with Bible word identifiers, NOT Greek word identifers!!!
/// The word identifier in the dbo.BibleVersionWords.WordIdentifier column has one more digit than the dbo.GreekNewTestamentWords.WordIdentifier column
/// because Greek alignment data does not include word segments.
/// Format for Bible word identifiers is BBCCCVVVWWWS.
/// Format for Greek word identifiers is BBCCCVVVWWW and thus this class should not be used with Greek data.
/// Key:
///   B: Book
///   C: Chapter
///   V: Verse
///   W: Word
///   S: Word segment
/// </summary>
public sealed class BibleWordIdentifier
{
    public long WordIdentifier { get; }
    public BookId BookId { get; }
    public int Chapter { get; }
    public int Verse { get; }
    public int Word { get; }
    public int WordSegment { get; }

    /// <summary>
    /// Creates a <see cref="BibleWordIdentifier"/> from the raw word identifier value.
    /// </summary>
    public BibleWordIdentifier(long wordIdentifier)
    {
        if (wordIdentifier < _minWordIdentifier || wordIdentifier > _maxWordIdentifier)
        {
            throw new ArgumentOutOfRangeException(nameof(wordIdentifier), wordIdentifier, $"{nameof(wordIdentifier)} must be between {_minWordIdentifier} and {_maxWordIdentifier} but was \"{wordIdentifier}\".");
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

        BookId = (BookId)(num + 1);
    }

    /// <summary>
    /// Creates a <see cref="BibleWordIdentifier"/> representing the lower bound of a Book.
    /// </summary>
    public BibleWordIdentifier(BookId bookId)
:       this(bookId, _minChapter, _minVerse, _minWord, _minWordSegment)
    {
    }

    /// <summary>
    /// Creates a <see cref="BibleWordIdentifier"/> representing the lower bound of a Chapter.
    /// </summary>
    public BibleWordIdentifier(BookId bookId, int chapter)
:       this(bookId, chapter, _minVerse, word: _minWord, _minWordSegment)
    {
    }

    /// <summary>
    /// Creates a <see cref="BibleWordIdentifier"/> representing the lower bound of a Verse.
    /// </summary>
    public BibleWordIdentifier(BookId bookId, int chapter, int verse)
    : this(bookId, chapter, verse, _minWord, _minWordSegment)
    {
    }

    /// <summary>
    /// Creates a <see cref="BibleWordIdentifier"/> representing the lower bound of a Word.
    /// </summary>
    public BibleWordIdentifier(BookId bookId, int chapter, int verse, int word)
        : this(bookId, chapter, verse, word, _minWordSegment)
    {
    }

    /// <summary>
    /// Creates a fully specified <see cref="BibleWordIdentifier"/>.
    /// </summary>
    public BibleWordIdentifier(BookId bookId, int chapter, int verse, int word, int wordSegment)
    {
        if (bookId <= BookId.None || bookId > _maxBookId)
        {
            throw new ArgumentOutOfRangeException($"Invalid {nameof(BookId)} value for calculating word identifiers: \"{bookId}\".", nameof(bookId));
        }

        if (chapter is < _minChapter or > _maxChapter)
        {
            throw new ArgumentOutOfRangeException(nameof(chapter), chapter, $"{nameof(chapter)} must be between {_minChapter} and {_maxChapter} but was \"{chapter}\".");
        }

        if (verse is < _minVerse or > _maxVerse)
        {
            throw new ArgumentOutOfRangeException(nameof(chapter), chapter, $"{nameof(chapter)} must be between {_minVerse} and {_maxVerse} but was \"{verse}\".");
        }

        if (word is < _minWord or > _maxWord)
        {
            throw new ArgumentOutOfRangeException(nameof(chapter), chapter, $"{nameof(chapter)} must be between {_minWord} and {_maxWord} but was \"{word}\".");
        }

        if (wordSegment is < _minWordSegment or > _maxWordSegment)
        {
            throw new ArgumentOutOfRangeException(nameof(chapter), chapter, $"{nameof(chapter)} must be between {_minWordSegment} and {_maxWordSegment} but was \"{wordSegment}\".");
        }

        BookId = bookId;
        Chapter = chapter;
        Verse = verse;
        Word = word;
        WordSegment = wordSegment;

        WordIdentifier = CalculateWordIdentifier((int)bookId - 1, chapter, verse, word, wordSegment);
    }

    public static BibleWordIdentifier GetUpperBoundOfBook(BookId bookId)
    {
        return new BibleWordIdentifier(bookId, _maxChapter, _maxVerse, _maxWord, _maxWordSegment);
    }

    public static BibleWordIdentifier GetUpperBoundOfChapter(BookId bookId, int chapter)
    {
        return new BibleWordIdentifier(bookId, chapter, _maxVerse, _maxWord, _maxWordSegment);
    }
    public static BibleWordIdentifier GetUpperBoundOfVerse(BookId bookId, int chapter, int verse)
    {
        return new BibleWordIdentifier(bookId, chapter, verse, _maxWord, _maxWordSegment);
    }

    public static BibleWordIdentifier GetUpperBoundOfWord(BookId bookId, int chapter, int verse, int word)
    {
        return new BibleWordIdentifier(bookId, chapter, verse, word, _maxWordSegment);
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
        return (book * _bookMultiplier) + (chapter * _chapterMultiplier) + (verse * _verseMultiplier) + (word * _wordMultiplier) + wordSegment;
    }

    private static readonly BookId _maxBookId = Enum.GetValues(typeof(BookId)).Cast<BookId>().Max();

    private const long _bookMultiplier = _chapterMultiplier * 1000L;
    private const long _chapterMultiplier = _verseMultiplier * 1000L;
    private const long _verseMultiplier = _wordMultiplier * 1000L;
    private const long _wordMultiplier = 10L;

    private const int _minChapter = 0;
    private const int _maxChapter = 999;

    private const int _minVerse = 0;
    private const int _maxVerse = 999;

    private const int _minWord = 0;
    private const int _maxWord = 999;

    private const int _minWordSegment = 0;
    private const int _maxWordSegment = 9;

    private const long _minWordIdentifier = 0L;
    private static readonly long _maxWordIdentifier = (((int)_maxBookId) * _bookMultiplier) - 1L;
}