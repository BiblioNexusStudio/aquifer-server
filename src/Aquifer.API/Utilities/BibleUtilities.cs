using Aquifer.API.Common;
using Aquifer.Data.Enums;

namespace Aquifer.API.Utilities;

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

        var bookId = BookCodes.IdFromCode(bookCode);

        if (chapters is null || chapters.Length == 0)
        {
            return [(LowerBoundOfBook(bookId), UpperBoundOfBook(bookId))];
        }

        return chapters.Select(c => (LowerBoundOfChapter(bookId, c), UpperBoundOfChapter(bookId, c))).ToList();
    }
}