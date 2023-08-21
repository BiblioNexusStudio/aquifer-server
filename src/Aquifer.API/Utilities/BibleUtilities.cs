namespace Aquifer.API.Utilities;

public static class BibleUtilities
{
    public static (string bookName, int bookId, int chapter, int verse) TranslateVerseId(int verseId)
    {
        if (verseId is < 1001001001 or >= 1068000000)
        {
            throw new ArgumentOutOfRangeException(nameof(verseId), verseId, null);
        }

        int verse = verseId % 1000;
        int chapter = verseId / 1000 % 1000;
        int bookId = verseId / 1000000 % 1000;

        return (((BibleBook)bookId).ToString(), bookId, chapter, verse);
    }
}

public enum BibleBook
{
    Genesis = 1,
    Exodus,
    Leviticus,
    Numbers,
    Deuteronomy,
    Joshua,
    Judges,
    Ruth,
    FirstSamuel,
    SecondSamuel,
    FirstKings,
    SecondKings,
    FirstChronicles,
    SecondChronicles,
    Ezra,
    Nehemiah,
    Esther,
    Job,
    Psalm,
    Proverbs,
    Ecclesiastes,
    SongOfSolomon,
    Isaiah,
    Jeremiah,
    Lamentations,
    Ezekiel,
    Daniel,
    Hosea,
    Joel,
    Amos,
    Obadiah,
    Jonah,
    Micah,
    Nahum,
    Habakkuk,
    Zephaniah,
    Haggai,
    Zechariah,
    Malachi,
    Matthew = 41,
    Mark,
    Luke,
    John,
    Acts,
    Romans,
    FirstCorinthians,
    SecondCorinthians,
    Galatians,
    Ephesians,
    Philippians,
    Colossians,
    FirstThessalonians,
    SecondThessalonians,
    FirstTimothy,
    SecondTimothy,
    Titus,
    Philemon,
    Hebrews,
    James,
    FirstPeter,
    SecondPeter,
    FirstJohn,
    SecondJohn,
    ThirdJohn,
    Jude,
    Revelation
}