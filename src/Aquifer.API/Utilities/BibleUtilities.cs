using Aquifer.Data.Enums;

namespace Aquifer.API.Utilities;

public static class BibleUtilities
{
    public static (BookCode bookId, int chapter, int verse) TranslateVerseId(int verseId)
    {
        int verse = verseId % 1000;
        int chapter = verseId / 1000 % 1000;
        var bookId = (BookCode)(verseId / 1000000 % 1000);

        return (bookId, chapter, verse);
    }

    public static int LowerBoundOfBook(BookCode bookId)
    {
        return ((int)bookId * 1000000) + 1000000000;
    }

    public static int UpperBoundOfBook(BookCode bookId)
    {
        return ((int)bookId * 1000000) + 1000999999;
    }
}