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
        int minVerseId;
        int maxVerseId;

        try
        {
            (minVerseId, maxVerseId) = await GetMinAndMaxVerseIdFromRequestAsync(req, cancellationToken);
        }
        catch (ArgumentException e)
        {
            AddError(e.Message);
            await SendErrorsAsync(cancellation: cancellationToken, statusCode: 400);
            return;
        }
        
        // todo: error handling for versification 
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
                    BookName = BibleBookCodeUtilities.FullNameFromId(sourceBookId),
                    ChapterNumber = sourceChapter,
                    VerseNumber = sourceVerse
                },
                TargetBibleVerse = mapping.Value
            });
        }

        var response = new Response() { VerseMappings = verseMappings };
        
        await SendOkAsync(response, cancellationToken);
    }
    
     private async Task<(int, int)> GetMinAndMaxChapterNumberFromRequestAsync(Request req, CancellationToken ct)
    {
        // validate requested chapters? or fail versification?
        if (req is { StartChapter: not null, EndChapter: not null })
        {
            return (req.StartChapter.Value, req.EndChapter.Value);
        }

        int maxChapter;
        try
        {
            maxChapter = await dbContext.BookChapters
                .Where(bc => bc.BookId == (BookId)req.BookId)
                .MaxAsync(bc => bc.Number, ct);
        }
        catch (InvalidOperationException)
        {
            ThrowIfAnyErrors();
            throw new ArgumentException($"Invalid book id: {req.BookId}.");
        }
        
        if (req.StartChapter is not null)
        {
            return (req.StartChapter.Value, maxChapter);
        }
        
        var minChapter = await dbContext.BookChapters
            .Where(bc => bc.BookId == (BookId)req.BookId)
            .MinAsync(bc => bc.Number, ct);
        
        // user did not specify start chapter and we have to determine both
        return (minChapter, maxChapter);
    }
    
    private async Task<(int, int)> GetMinAndMaxVerseIdFromRequestAsync(Request req, CancellationToken ct)
    {
        var (minChapterNumber, maxChapterNumber) = await GetMinAndMaxChapterNumberFromRequestAsync(req, ct);
        
        // validate requested verses? or fail versification?
        if (req is { StartVerse: not null, EndVerse: not null })
        {
            return (BibleUtilities.GetVerseId((BookId)req.BookId, minChapterNumber!, req.StartVerse.Value),
                    BibleUtilities.GetVerseId((BookId)req.BookId, maxChapterNumber!, req.EndVerse.Value));
        }
        
        var maxVerse = await dbContext.BookChapters
            .Where(bc => bc.BookId == (BookId)req.BookId && bc.Number == maxChapterNumber)
            .Select(bc => bc.MaxVerseNumber)
            .FirstOrDefaultAsync(ct);

        if (maxVerse == 0)
        {
            var bookName = BibleBookCodeUtilities.FullNameFromId((BookId)req.BookId);
            AddError($"Chapter: {maxChapterNumber} does not exists for book: {bookName} ({req.BookId})");
            ThrowIfAnyErrors();
        }
        
        if (req.StartVerse is not null)
        {
            return (BibleUtilities.GetVerseId((BookId)req.BookId, minChapterNumber!, req.StartVerse.Value),
                    BibleUtilities.GetVerseId((BookId)req.BookId, maxChapterNumber!, maxVerse));
        }
        
        var minVerse = await dbContext.BibleTexts
            .Where(bt => bt.BookId == (BookId)req.BookId && bt.ChapterNumber == minChapterNumber)
            .OrderBy(bt => bt.VerseNumber)
            .Select(bt => bt.VerseNumber)
            .FirstOrDefaultAsync(ct);

        if (minVerse == 0)
        {
            var bookName = BibleBookCodeUtilities.FullNameFromId((BookId)req.BookId);
            AddError($"Chapter: {minChapterNumber} does not exists for book: {bookName} ({req.BookId})");
            ThrowIfAnyErrors();
        }
        
        // user did not specify start verse and we have to determine both
        return (BibleUtilities.GetVerseId((BookId)req.BookId, minChapterNumber, minVerse),
                BibleUtilities.GetVerseId((BookId)req.BookId, maxChapterNumber, maxVerse));
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
}