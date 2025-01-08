using Aquifer.Common.Services.Caching;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Common.Services;

public interface IBibleTextService
{
    Task<List<BibleText>> GetBibleTextsForBibleIdAsync(int toBibleId, int startVerseId, int endVerseId, CancellationToken ct);
}

public sealed class BibleTextService(AquiferDbContext dbContext, ICachingVersificationService versificationService) : IBibleTextService
{
    public async Task<List<BibleText>> GetBibleTextsForBibleIdAsync(int toBibleId, int startVerseId, int endVerseId, CancellationToken ct)
    {
        const int fromBibleId = 1;
        var skipMapping = toBibleId == fromBibleId;

        var mappedStartVerseId = skipMapping
            ? startVerseId
            : await VersificationUtilities.GetVersificationAsync(startVerseId, fromBibleId, toBibleId, versificationService, ct);
        var mappedEndVerseId = skipMapping
            ? endVerseId
            : await VersificationUtilities.GetVersificationAsync(endVerseId, fromBibleId, toBibleId, versificationService, ct);

        var (startBookId, startChapter, startVerse) = BibleUtilities.TranslateVerseId(mappedStartVerseId);
        var (endBookId, endChapter, endVerse) = BibleUtilities.TranslateVerseId(mappedEndVerseId);

        return await dbContext.BibleTexts
            .Where(bt => bt.BibleId == toBibleId && bt.BookId == startBookId &&
                         bt.ChapterNumber >= startChapter && bt.ChapterNumber <= endChapter &&
                         (bt.ChapterNumber != startChapter || bt.VerseNumber >= startVerse) &&
                         (bt.ChapterNumber != endChapter || bt.VerseNumber <= endVerse))
            .OrderBy(bt => bt.ChapterNumber)
            .GroupBy(bt => bt.ChapterNumber)
            .Select(bt => new BibleText
            {
                ChapterNumber = bt.Key,
                Verses = bt
                    .OrderBy(bti => bti.VerseNumber)
                    .Select(bti => new Verse
                    {
                        VerseNumber = bti.VerseNumber,
                        Text = bti.Text
                    }).ToList()
            })
            .ToListAsync(ct);
    }
}

public class BibleText
{
    public int ChapterNumber { get; set; }
    public required IEnumerable<Verse> Verses { get; set; }
}

public class Verse
{
    public int VerseNumber { get; set; }
    public required string Text { get; set; }
}