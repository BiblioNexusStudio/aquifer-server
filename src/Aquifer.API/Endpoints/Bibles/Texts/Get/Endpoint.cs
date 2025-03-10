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
            
            if (req.IncludeVersification)
            {
                await MapVersificationDifferencesToResponseVersesAsync(req, response, ct);
            }
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
        var bookId = BibleBookCodeUtilities.IdFromCode(response.BookCode);
        var (minChapter, maxChapter) = await GetMinAndMaxChapterNumberFromResponseOrRequestAsync(req, response,ct);

        if (minChapter is null || maxChapter is null)
        {
            // cannot do versification due to no chapter data
            return;
        }
        
        var ( minVerse, maxVerse) = await GetMinAndMaxVerseNumberFromResponseOrRequestAsync(req, response, (int)minChapter, (int)maxChapter, ct);
        
        if (minVerse is null || maxVerse is null)
        {
            // cannot do versification due to no verse data
            return;
        }
        
        var minVerseId = BibleUtilities.GetVerseId(bookId, (int)minChapter, (int)minVerse);  
        var maxVerseId = BibleUtilities.GetVerseId(bookId, (int)maxChapter, (int)maxVerse);

        Dictionary<int, VerseReference> baseBibleVerseMappings;
        var verseMappings = new List<VerseMapping>();
        
        try
        {
            var versificationMap = await VersificationUtilities.ConvertVersificationRangeAsync(
                req.BibleId,
                minVerseId,
                maxVerseId,
                CachingVersificationService.EngVersificationSchemeBibleId,
                versificationService,
                ct);

            baseBibleVerseMappings = versificationMap
                .Where(mapping => mapping.Value.HasValue && mapping.Value.Value != mapping.Key)
                .ToDictionary(
                    mapping => mapping.Key,
                    mapping => FormatBaseBookChapterVerseMapping(mapping.Value!.Value));
            
            foreach (var mapping in baseBibleVerseMappings)
            {
                var (_, targetChapter, targetVerse) = BibleUtilities.TranslateVerseId(mapping.Key);
            
                verseMappings.Add(new VerseMapping
                {
                    TargetBibleVerse = new VerseReference
                    {
                        BookName = response.BookName,
                        ChapterNumber = targetChapter,
                        VerseNumber = targetVerse
                    },
                    SourceBibleVerse = mapping.Value
                });
            }
        }
        // catch start verse {VerseId} is invalid for bible {BibleId}
        catch (ArgumentException e)
        {
            // Ideally, we would be able to figure out that the verse does not exist in the target bible and still map the verse if a mapping
            // does exist in the source bible.
            var versificationMap = await VersificationUtilities.ConvertVersificationRangeAsync(
                CachingVersificationService.EngVersificationSchemeBibleId,
                minVerseId,
                maxVerseId,
                req.BibleId,
                versificationService,
                ct);
            
            var sourceBibleVerseMappings = versificationMap
                .Where(mapping => mapping.Value.HasValue && mapping.Value.Value != mapping.Key)
                .ToDictionary(
                    mapping => mapping.Key,
                    mapping => FormatBaseBookChapterVerseMapping(mapping.Value!.Value));
        
            foreach (var mapping in sourceBibleVerseMappings)
            {
                var (_, sourceChapter, sourceVerse) = BibleUtilities.TranslateVerseId(mapping.Key);
            
                verseMappings.Add(new VerseMapping
                {
                    SourceBibleVerse = new VerseReference
                    {
                        BookName = response.BookName,
                        ChapterNumber = sourceChapter,
                        VerseNumber = sourceVerse
                    },
                    TargetBibleVerse = mapping.Value
                });
            }
        }
        
        if (verseMappings.Count != 0)
        {
            response.VerseMappings = verseMappings;
        }
    }

    private async Task<(int?, int?)> GetMinAndMaxChapterNumberFromResponseOrRequestAsync(Request req, Response response, CancellationToken ct)
    {
        if (response.Chapters.Any())
        {
            return (response.Chapters.First().Number, response.Chapters.Last().Number);
        }
        
        // User requested a passage that does not exist in the requested bible but does in the source e.g. Jonah 1:17 ENG to LSB.
        // StartChapter will only be 0 when the request is for an entire book and not a specified chapter or chapter range.
        if (req.StartChapter > 0 && req.EndChapter != 999)
        {
            return (req.StartChapter, req.EndChapter);
        }
        
        // User specified start chapter but not end chapter, call db to get EndChapter.
        if (req.StartVerse > 0 && req.EndVerse == 999)
        {
            var maxChapter = await dbContext.BibleTexts
                .Where(bt => bt.BibleId == response.BibleId && (int)bt.BookId == response.BookNumber)
                .MaxAsync(bt => (int?)bt.ChapterNumber, ct);
            
            return (req.StartChapter, maxChapter);
        }
        
        return (null, null); // user did not specify start chapter and there were no results
    }
    
    private async Task<(int?, int?)> GetMinAndMaxVerseNumberFromResponseOrRequestAsync(Request req, Response response, int minChapter, int maxChapter, CancellationToken ct)
    {
        if (response.Chapters.Any())
        {
            return (response.Chapters.First().Verses.First().Number, response.Chapters.Last().Verses.Last().Number);
        }
        
        if (req.StartVerse > 0 && req.EndVerse != 999)
        {
            return (req.StartVerse, req.EndVerse);
        }
        
        // User specified start verse but not end verse
        if (req.StartVerse > 0 && req.EndVerse == 999)
        {
            var maxVerse = await dbContext.BibleTexts
                .Where(bt => bt.BibleId == response.BibleId && (int)bt.BookId == response.BookNumber && bt.ChapterNumber == maxChapter)
                .MaxAsync(bt => (int?)bt.VerseNumber, ct);
            
            // with one chapter, if req StartVerse = 17, but 17 does not exist in target bible, maxVerse will = 16. 
            if (minChapter == maxChapter && maxVerse < req.StartVerse)
            {
                maxVerse = req.StartVerse;
            }
            
            return (req.StartVerse, maxVerse);
        }
        
        return (null, null); // user did not specify start verse and there were no results
    }
}