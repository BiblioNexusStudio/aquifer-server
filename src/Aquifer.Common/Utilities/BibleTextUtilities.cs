using Aquifer.Common.Services.Caching;
using Aquifer.Data;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Common.Utilities;

public static class BibleTextUtilities
{
    public static async Task<IEnumerable<Chapter>> GetTextsByBibleId(int bookNumber, int chapter, int verse,
        int bibleId, AquiferDbContext dbContext, ICachingVersificationService cachingService, CancellationToken ct)
    {
        var verseId = BibleUtilities.GetVerseId(bookNumber, chapter, verse);

        var mappedVerseId = await cachingService.GetVersificationMapping(bibleId, verseId, ct);
        var (mappedBookNumber, mappedChapter, mappedVerse) = BibleUtilities.TranslateVerseId(mappedVerseId);

        var bibleTexts = await dbContext.BibleTexts
            .Where(bt => bt.VerseNumber == mappedVerse && bt.ChapterNumber == mappedChapter && bt.BookId == mappedBookNumber &&
                         bt.BibleId == bibleId).Select(b => new Chapter
            {
                Number = b.ChapterNumber,
                Verse = new ChapterVerse
                {
                    Number = b.VerseNumber,
                    Text = b.Text
                }
            }).ToListAsync(ct);
        return bibleTexts;
    }
}

public class Chapter
{
    public required int Number { get; set; }
    public required ChapterVerse Verse { get; set; }
}

public class ChapterVerse
{
    public required int Number { get; set; }
    public required string Text { get; set; }
}