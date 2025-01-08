using Aquifer.Data;
using Aquifer.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Common.Services;

public interface IBibleTextService
{
    Task<List<BibleTextChapter>> GetBibleTextsForBibleIdAsync(int toBibleId, int bookNumber, int startChapter, int endChapter,
        int startVerse, int endVerse, CancellationToken ct);
}

public sealed class BibleTextService(AquiferDbContext dbContext) : IBibleTextService
{
    public async Task<List<BibleTextChapter>> GetBibleTextsForBibleIdAsync(int toBibleId, int bookNumber, int startChapter, int endChapter,
        int startVerse, int endVerse, CancellationToken ct)
    {
        return await dbContext.BibleTexts
            .Where(bt => bt.BibleId == toBibleId && bt.BookId == (BookId)bookNumber &&
                         bt.ChapterNumber >= startChapter && bt.ChapterNumber <= endChapter &&
                         (bt.ChapterNumber != startChapter || bt.VerseNumber >= startVerse) &&
                         (bt.ChapterNumber != endChapter || bt.VerseNumber <= endVerse))
            .OrderBy(bt => bt.ChapterNumber)
            .GroupBy(bt => bt.ChapterNumber)
            .Select(bt => new BibleTextChapter
            {
                ChapterNumber = bt.Key,
                Verses = bt
                    .OrderBy(bti => bti.VerseNumber)
                    .Select(bti => new BibleTextVerse
                    {
                        VerseNumber = bti.VerseNumber,
                        Text = bti.Text
                    }).ToList()
            })
            .ToListAsync(ct);
    }
}

public class BibleTextChapter
{
    public int ChapterNumber { get; set; }
    public required IEnumerable<BibleTextVerse> Verses { get; set; }
}

public class BibleTextVerse
{
    public int VerseNumber { get; set; }
    public required string Text { get; set; }
}