using Aquifer.Data.Enums;

namespace Aquifer.Common.Utilities;

public static class BibleUtilities
{
    public static (BookId bookId, int chapter, int verse) TranslateVerseId(int verseId)
    {
        var verse = verseId % 1000;
        var chapter = verseId / 1000 % 1000;
        var bookId = (BookId)(verseId / 1000000 % 1000);

        return (bookId, chapter, verse);
    }

    public static int LowerBoundOfBook(BookId bookId)
    {
        return ((int)bookId * 1000000) + 1000000000;
    }

    public static int UpperBoundOfBook(BookId bookId)
    {
        return ((int)bookId * 1000000) + 1000999999;
    }

    public static int LowerBoundOfChapter(BookId bookId, int chapter)
    {
        return ((int)bookId * 1000000) + (chapter * 1000) + 1000000000;
    }

    public static int UpperBoundOfChapter(BookId bookId, int chapter)
    {
        return ((int)bookId * 1000000) + ((chapter + 1) * 1000) - 1 + 1000000000;
    }

    public static List<(int, int)> VerseRangesForBookAndChapters(string? bookCode, int[]? chapters)
    {
        if (bookCode is null)
        {
            return [];
        }

        var bookId = BibleBookCodeUtilities.IdFromCode(bookCode);

        return chapters is null || chapters.Length == 0
            ? [(LowerBoundOfBook(bookId), UpperBoundOfBook(bookId))]
            : chapters.Select(c => (LowerBoundOfChapter(bookId, c), UpperBoundOfChapter(bookId, c))).ToList();
    }

    public static (int startVerseId, int endVerseId)? VerseRangeForBookAndChapters(string? bookCode, int? startChapter, int? endChapter)
    {
        if (bookCode is null)
        {
            return null;
        }

        var bookId = BibleBookCodeUtilities.IdFromCode(bookCode);

        if (startChapter is not null || endChapter is not null)
        {
            if (startChapter is null || endChapter is null)
            {
                throw new ArgumentException("startChapter and endChapter must be specified together.");
            }

            if (startChapter.Value > endChapter.Value)
            {
                throw new ArgumentException("startChapter must not be greater than endChapter.");
            }

            return (LowerBoundOfChapter(bookId, startChapter.Value), UpperBoundOfChapter(bookId, endChapter.Value));
        }

        return (LowerBoundOfBook(bookId), UpperBoundOfBook(bookId));
    }

    public static (int startVerseId, int endVerseId) GetVerseIds(string bookCode,
        int startChapter,
        int endChapter,
        int startVerse,
        int endVerse)
    {
        var bookId = BibleBookCodeUtilities.IdFromCode(bookCode);

        if (startChapter == 0 && endChapter == 0)
        {
            return (LowerBoundOfBook(bookId), UpperBoundOfBook(bookId));
        }

        if (startVerse == 0 && endVerse == 0)
        {
            return (LowerBoundOfChapter(bookId, startChapter), UpperBoundOfChapter(bookId, endChapter));
        }

        var bookNumber = (int)bookId;
        return (GetVerseId(bookNumber, startChapter, startVerse), GetVerseId(bookNumber, endChapter, endVerse));
    }

    public static string PadVerseId(int value)
    {
        return value.ToString().PadLeft(3, '0');
    }

    public static int GetVerseId(BookId bookId, int chapterNumber, int verseNumber)
    {
        return GetVerseId((int)bookId, chapterNumber, verseNumber);
    }

    public static int GetVerseId(int bookNumber, int chapterNumber, int verseNumber)
    {
        return int.Parse($"1{PadVerseId(bookNumber)}{PadVerseId(chapterNumber)}{PadVerseId(verseNumber)}");
    }
}