using Aquifer.Common.Services.Caching;
using Aquifer.Data;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Common.Utilities;

public static class BibleTextUtilities
{

    public static async Task<List<BibleText>> GetVersificationForBibleId(int bibleId, int startVerseId, int endVerseId,
        ICachingVersificationService versificationService, AquiferDbContext dbContext, CancellationToken ct)
    {
        var bibleTexts = new List<FlatBibleText>();
        try
        {
            var verses = await dbContext.Verses.Where(v => v.Id >= startVerseId && v.Id <= endVerseId).Select(v => v.Id).ToListAsync(ct);
            var englishMappingCache = await versificationService.GetVersificationsByBibleIdAsync(1, ct);
            var bibleIdMappingsCache = await versificationService.GetVersificationsByBibleIdAsync(bibleId, ct);

            var englishToBibleIdDictionary = new Dictionary<(int, char?), (int, char?)>();
            foreach (var englishMapping in englishMappingCache)
            {
                var mappedBibleVerse = bibleIdMappingsCache.FirstOrDefault(x => x.Value == englishMapping.Value).Key;
                englishToBibleIdDictionary[englishMapping.Key] = mappedBibleVerse;
            }

            foreach (var verse in verses)
            {
                var (mappedVerseId, mappedVersePart) =
                    englishToBibleIdDictionary.GetValueOrDefault((verse, null)); // not sure how to handle part from the request right now
                if (mappedVerseId.Equals(default))
                {
                    mappedVerseId = verse;
                }

                var (mappedBookId, mappedChapter, mappedVerseNumber) = BibleUtilities.TranslateVerseId(mappedVerseId);

                var (_, tChapter, tVerse) = BibleUtilities.TranslateVerseId(verse);

                var mappedText = await dbContext.BibleTexts.Where(bt =>
                    bt.ChapterNumber == mappedChapter && bt.VerseNumber == mappedVerseNumber && bt.BookId == mappedBookId &&
                    bt.BibleId == bibleId).FirstOrDefaultAsync(ct);

                if (mappedText is not null)
                {
                    bibleTexts.Add(new FlatBibleText
                    {
                        Text = mappedText.Text,
                        ChapterNumber = tChapter,
                        VerseNumber = tVerse,
                        MappedChapterNumber = mappedChapter,
                        MappedVerseNumber = mappedVerseNumber,
                        MappedVersePart = mappedVersePart
                    });
                }
            }

            return bibleTexts.GroupBy(bt => bt.MappedChapterNumber).OrderBy(bt => bt.Key).Select(bt => new BibleText
            {
                MappedChapterNumber = bt.Key,
                ChapterNumber = bt.Select(x => x.ChapterNumber).FirstOrDefault(),
                Verses = bt.Select(x => new Verse
                {
                    MappedVerseNumber = x.MappedVerseNumber,
                    MappedVersePart = x.MappedVersePart,
                    VerseNumber = x.VerseNumber,
                    Text = x.Text
                }).ToList()
            }).ToList();
        }
        // no mappings exist for the given bibleId
        catch (KeyNotFoundException)
        {
            var (bookId, startChapter, startVerse) = BibleUtilities.TranslateVerseId(startVerseId);
            var (_, endChapter, endVerse) = BibleUtilities.TranslateVerseId(endVerseId);

            return await dbContext.BibleTexts
                .Where(bt => bt.BibleId == bibleId && bt.BookId == bookId &&
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
}

public class FlatBibleText
{
    public int ChapterNumber { get; set; }
    public int VerseNumber { get; set; }
    public int MappedChapterNumber { get; set; }
    public int MappedVerseNumber { get; set; }
    public char? MappedVersePart { get; set; }
    public required string Text { get; set; }
}

public class BibleText
{
    public int ChapterNumber { get; set; }
    public int MappedChapterNumber { get; set; }
    public required IEnumerable<Verse> Verses { get; set; }
}

public class Verse
{
    public int VerseNumber { get; set; }
    public int MappedVerseNumber { get; set; }
    public char? MappedVersePart { get; set; }
    public required string Text { get; set; }
}