using Aquifer.API.Helpers;
using Aquifer.Common.Services.Caching;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Enums;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Bibles.Versification;

public class Endpoint(AquiferDbContext dbContext, ICachingVersificationService versificationService) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/bibles/{TargetBibleId}/versification");
        ResponseCache(EndpointHelpers.OneDayInSeconds);
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken cancellationToken)
    {
        if (!IsRequestedBookIdValid(req.BookId))
        {
            AddError($"Invalid book id: {req.BookId}.");
            await SendErrorsAsync(cancellation: cancellationToken, statusCode: 404);
            return;
        }
        
        if (!await IsRequestedBibleIdValidAsync(req.TargetBibleId))
        {
            AddError($"Invalid bible id: {req.TargetBibleId}.");
            await SendErrorsAsync(cancellation: cancellationToken, statusCode: 404);
            return;
        }
        
        // We have ensured that the values returned here will always be valid 
        var (minVerseId, maxVerseId) = await GetMinAndMaxVerseIdFromRequestAsync(req, cancellationToken);
        
        var versificationMap = await VersificationUtilities.ConvertVersificationRangeAsync(
            CachingVersificationService.EngVersificationSchemeBibleId,
            minVerseId,
            maxVerseId,
            req.TargetBibleId,
            versificationService,
            cancellationToken);
            
        var sourceBibleVerseMappings = versificationMap
            .Where(mapping => mapping.Value.HasValue && mapping.Value.Value != mapping.Key)
            .ToDictionary(
                mapping => mapping.Key,
                mapping => FormatBaseBookChapterVerseMapping(mapping.Value!.Value));
        
        var verseMappings = new List<VerseMapping>();
        foreach (var mapping in sourceBibleVerseMappings)
        {
            var (sourceBookId, sourceChapter, sourceVerse) = BibleUtilities.TranslateVerseId(mapping.Key);
            
            verseMappings.Add(new VerseMapping
            {
                SourceBibleVerse = new VerseReference
                {
                    VerseId = mapping.Key,
                    Book = BibleBookCodeUtilities.FullNameFromId(sourceBookId),
                    Chapter = sourceChapter,
                    Verse = sourceVerse
                },
                TargetBibleVerse = mapping.Value
            });
        }

        var response = new Response() { VerseMappings = verseMappings };
        
        await SendOkAsync(response, cancellationToken);
    }
    
     private async Task<(int, int)> GetMinAndMaxChapterNumberFromRequestAsync(Request req, CancellationToken ct)
     {
         var isRequestedStartChapterValid = false;
         var isRequestedEndChapterValid = false;
         
        if (req.StartChapter.HasValue)
        {
            isRequestedStartChapterValid = await IsChapterValidForBookAsync(req.StartChapter.Value, req.BookId, ct);
        }

        if (req.EndChapter.HasValue)
        {
            isRequestedEndChapterValid = await IsChapterValidForBookAsync(req.EndChapter.Value, req.BookId, ct);
        }

        if (isRequestedStartChapterValid && isRequestedEndChapterValid)
        {
            return (req.StartChapter!.Value, req.EndChapter!.Value);
        }
        
        var maxChapter = await dbContext.BookChapters
            .Where(bc => bc.BookId == (BookId)req.BookId)
            .MaxAsync(bc => bc.Number, ct);
        
        if (isRequestedStartChapterValid && !isRequestedEndChapterValid)
        {
            return (req.StartChapter!.Value, maxChapter);
        }
        
        var minChapter = await dbContext.BookChapters
            .Where(bc => bc.BookId == (BookId)req.BookId)
            .MinAsync(bc => bc.Number, ct);

        if (!isRequestedStartChapterValid && isRequestedEndChapterValid)
        {
            return (minChapter, req.EndChapter!.Value);
        }
        
        return (minChapter, maxChapter);
    }
    
    private async Task<(int, int)> GetMinAndMaxVerseIdFromRequestAsync(Request req, CancellationToken ct)
    {
        var (minChapterNumber, maxChapterNumber) = await GetMinAndMaxChapterNumberFromRequestAsync(req, ct);
        
        var isRequestedStartVerseValid = false;
        var isRequestedEndVerseValid = false;

        if (req.StartVerse.HasValue)
        {
            isRequestedStartVerseValid = await IsVerseValidForBookAndChapterAsync(req.StartVerse.Value, minChapterNumber, req.BookId, ct);
        }

        if (req.EndVerse.HasValue)
        {
            isRequestedEndVerseValid = await IsVerseValidForBookAndChapterAsync(req.EndVerse.Value, minChapterNumber, req.BookId, ct);
        }
        
        if (isRequestedStartVerseValid && isRequestedEndVerseValid)
        {
            return (BibleUtilities.GetVerseId((BookId)req.BookId, minChapterNumber, req.StartVerse!.Value),
                    BibleUtilities.GetVerseId((BookId)req.BookId, maxChapterNumber, req.EndVerse!.Value));
        }
        
        var maxVerse = await dbContext.BookChapters
            .Where(bc => bc.BookId == (BookId)req.BookId && bc.Number == maxChapterNumber)
            .Select(bc => bc.MaxVerseNumber)
            .FirstOrDefaultAsync(ct);
        
        if (isRequestedStartVerseValid && !isRequestedEndVerseValid)
        {
            return (BibleUtilities.GetVerseId((BookId)req.BookId, minChapterNumber, req.StartVerse!.Value),
                    BibleUtilities.GetVerseId((BookId)req.BookId, maxChapterNumber, maxVerse));
        }
        
        var minVerse = await dbContext.BibleTexts
            .Where(bt => bt.BookId == (BookId)req.BookId && bt.ChapterNumber == minChapterNumber)
            .OrderBy(bt => bt.VerseNumber)
            .Select(bt => bt.VerseNumber)
            .FirstOrDefaultAsync(ct);

        if (!isRequestedStartVerseValid && isRequestedEndVerseValid)
        {
            return (BibleUtilities.GetVerseId((BookId)req.BookId, minChapterNumber, minVerse),
                    BibleUtilities.GetVerseId((BookId)req.BookId, maxChapterNumber, req.EndVerse!.Value));
        }
        
        return (BibleUtilities.GetVerseId((BookId)req.BookId, minChapterNumber, minVerse),
                BibleUtilities.GetVerseId((BookId)req.BookId, maxChapterNumber, maxVerse));
    }
    
    private static VerseReference FormatBaseBookChapterVerseMapping(int mappedVerseId)
    {
        var (targetBookId, targetChapter, targetVerse) = BibleUtilities.TranslateVerseId(mappedVerseId);
        var targetBookName = BibleBookCodeUtilities.FullNameFromId(targetBookId);
        
        return new VerseReference
        {
            VerseId = mappedVerseId,
            Book = targetBookName,
            Chapter = targetChapter,
            Verse = targetVerse
        };
    }

    private static bool IsRequestedBookIdValid(int bookId)
    {
        return Enum.IsDefined(typeof(BookId), bookId);
    }

    private async Task<bool> IsChapterValidForBookAsync(int chapterNumber, int bookId, CancellationToken ct)
    {
        return await dbContext.BookChapters
            .Where(bc => bc.BookId == (BookId)bookId && bc.Number == chapterNumber)
            .AnyAsync(ct);
    }

    private async Task<bool> IsVerseValidForBookAndChapterAsync(int verseNumber, int chapterNumber, int bookId, CancellationToken ct)
    {
        return await dbContext.BibleTexts
            .Where(bt => bt.BookId == (BookId)bookId && bt.ChapterNumber == chapterNumber && bt.VerseNumber == verseNumber)
            .AnyAsync(ct);
    }

    private async Task<bool> IsRequestedBibleIdValidAsync(int bibleId)
    {
        return await dbContext.Bibles.Where(b => b.Id == bibleId).AnyAsync();
    }
}