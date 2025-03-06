using Aquifer.API.Helpers;
using Aquifer.Common.Services.Caching;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Bibles.Texts.Get;

public class Endpoint(AquiferDbContext dbContext, ICachingVersificationService versificationService) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/bibles/{BibleId}/texts");
        ResponseCache(EndpointHelpers.OneDayInSeconds);
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var response = await dbContext.BibleBookContents
            .Where(bbc => bbc.Bible.Enabled && bbc.Bible.Id == req.BibleId &&
                          ((int)bbc.BookId == req.BookNumber || bbc.Book.Code == req.BookCode))
            .Select(bbc => new Response
            {
                BibleId = bbc.Bible.Id,
                BibleName = bbc.Bible.Name,
                BibleAbbreviation = bbc.Bible.Abbreviation,
                BookCode = bbc.Book.Code,
                BookName = bbc.DisplayName,
                BookNumber = (int)bbc.BookId
            })
            .FirstOrDefaultAsync(ct);

        if (response is not null)
        {
            response.Chapters = await dbContext.BibleTexts
                .Where(bt => bt.BibleId == response.BibleId && (int)bt.BookId == response.BookNumber &&
                             bt.ChapterNumber >= req.StartChapter && bt.ChapterNumber <= req.EndChapter &&
                             (bt.ChapterNumber != req.StartChapter || bt.VerseNumber >= req.StartVerse) &&
                             (bt.ChapterNumber != req.EndChapter || bt.VerseNumber <= req.EndVerse))
                .OrderBy(bt => bt.ChapterNumber)
                .GroupBy(bt => bt.ChapterNumber)
                .Select(bt => new ResponseChapters
                {
                    Number = bt.Key,
                    Verses = bt
                        .OrderBy(bti => bti.VerseNumber)
                        .Select(bti => new ResponseChapterVerses
                        {
                            Number = bti.VerseNumber,
                            Text = bti.Text
                        }).ToList()
                })
                .ToListAsync(ct);
            
            await MapVersificationDifferencesToResponseVersesAsync(req, response, ct);
        }

        await (response is null ? SendNotFoundAsync(ct) : SendOkAsync(response, ct));
    }

    private static VerseReference FormatBaseBookChapterVerseMapping(int mappedVerseId)
    {
        var (targetBookId, targetChapter, targetVerse) = BibleUtilities.TranslateVerseId(mappedVerseId);
        var targetBookName = BibleBookCodeUtilities.FullNameFromId(targetBookId);
        
        return new VerseReference
        {
            BookName = targetBookName,
            ChapterNumber = targetChapter,
            VerseNumber = targetVerse
        };
    }

    private async Task MapVersificationDifferencesToResponseVersesAsync(Request req, Response response, CancellationToken ct)
    {
        if (!response.Chapters.Any())
        {
            return;
        }

        var bookId = BibleBookCodeUtilities.IdFromCode(response.BookCode);
        var minChapter = response.Chapters.First();  
        var minVerseId = BibleUtilities.GetVerseId(bookId, minChapter.Number, minChapter.Verses.First().Number);  
        var maxChapter = response.Chapters.Last();  
        var maxVerseId = BibleUtilities.GetVerseId(bookId, maxChapter.Number, maxChapter.Verses.Last().Number);
        
        var versificationMap = await VersificationUtilities.ConvertVersificationRangeAsync(
            CachingVersificationService.EngVersificationSchemeBibleId,
            minVerseId,
            maxVerseId,
            req.BibleId,
            versificationService,
            ct);
        
        var baseBibleVerseMappings = versificationMap
            .Where(mapping => mapping.Value.HasValue && mapping.Value.Value != mapping.Key)
            .ToDictionary(
                mapping => mapping.Key,
                mapping => FormatBaseBookChapterVerseMapping(mapping.Value!.Value));
        
        foreach (var chapter in response.Chapters)
        {
            foreach (var verse in chapter.Verses)
            {
                var verseId = BibleUtilities.GetVerseId(bookId, chapter.Number, verse.Number);
                verse.SourceTextVerseReference = baseBibleVerseMappings.GetValueOrDefault(verseId);
            }
        }
    }
}