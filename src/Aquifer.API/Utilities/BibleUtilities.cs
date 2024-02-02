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
}