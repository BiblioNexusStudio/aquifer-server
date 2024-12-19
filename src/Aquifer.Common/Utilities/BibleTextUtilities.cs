using System.Collections.ObjectModel;
using Aquifer.Common.Services.Caching;
using Aquifer.Data;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Common.Utilities;

public static class BibleTextUtilities
{

    public static async Task<List<BibleText>> GetVersificationForBibleId(int toBibleId, int startVerseId, int endVerseId,
        ICachingVersificationService versificationService, AquiferDbContext dbContext, CancellationToken ct)
    {
        const int fromBibleId = 1;
        var bibleTexts = new List<FlatBibleText>();
        try
        {
            var verses = await dbContext.Verses.Where(v => v.Id >= startVerseId && v.Id <= endVerseId).Select(v => v.Id).ToListAsync(ct);
            var fromBibleToBaseMapping = await versificationService.GetVersificationsByBibleIdAsync(fromBibleId, ct);
            var toBibleToBaseMapping = await versificationService.GetVersificationsByBibleIdAsync(toBibleId, ct);

            var fromBibleToBibleMapping = GetMergedMappingsOnBaseId(fromBibleToBaseMapping, toBibleToBaseMapping);

            await GetMappedBibleTexts(toBibleId, dbContext, verses, fromBibleToBibleMapping, bibleTexts, ct);

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
                .Where(bt => bt.BibleId == toBibleId && bt.BookId == bookId &&
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

    public static async Task GetMappedBibleTexts(int toBibleId, AquiferDbContext dbContext, List<int> verses,
        Dictionary<(int, char?), (int, char?)> fromBibleToBibleMapping, List<FlatBibleText> bibleTexts, CancellationToken ct)
    {
        foreach (var verse in verses)
        {
            var (mappedVerseId, mappedVersePart) =
                fromBibleToBibleMapping.GetValueOrDefault((verse, null)); // not sure how to handle part from the request right now
            if (mappedVerseId.Equals(default))
            {
                mappedVerseId = verse;
            }

            var (mappedBookId, mappedChapter, mappedVerseNumber) = BibleUtilities.TranslateVerseId(mappedVerseId);

            var (_, tChapter, tVerse) = BibleUtilities.TranslateVerseId(verse);

            var mappedText = await dbContext.BibleTexts.Where(bt =>
                bt.ChapterNumber == mappedChapter && bt.VerseNumber == mappedVerseNumber && bt.BookId == mappedBookId &&
                bt.BibleId == toBibleId).FirstOrDefaultAsync(ct);

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
    }

    public static Dictionary<(int, char?), (int, char?)> GetMergedMappingsOnBaseId(
        ReadOnlyDictionary<(int bibleVerseId, char? bibleVersePart), (int baseVerseId, char? baseVersePart)> fromDictionary,
        ReadOnlyDictionary<(int bibleVerseId, char? bibleVersePart), (int baseVerseId, char? baseVersePart)> toDictionary)
    {
        var merged = new Dictionary<(int, char?), (int, char?)>();
        foreach (var englishMapping in fromDictionary)
        {
            var mappedBibleVerse = toDictionary.FirstOrDefault(x => x.Value == englishMapping.Value).Key;
            merged[englishMapping.Key] = mappedBibleVerse;
        }

        return merged;
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