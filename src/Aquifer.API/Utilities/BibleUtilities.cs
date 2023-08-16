namespace Aquifer.API.Utilities;

public static class BibleUtilities
{
    public static (int bookId, int chapter, int verse) TranslateVerseId(int verseId)
    {
        if (verseId is < 1001001001 or >= 1068000000)
        {
            throw new ArgumentOutOfRangeException(nameof(verseId), verseId, null);
        }

        int verse = verseId % 1000;
        int chapter = verseId / 1000 % 1000;
        int bookId = verseId / 1000000 % 1000;

        return (bookId, chapter, verse);
    }
}