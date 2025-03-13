using Aquifer.API.Helpers;
using Aquifer.Common.Services.Caching;
using Aquifer.Common.Utilities;
using Aquifer.Data;
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

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        if (!BibleBookCodeUtilities.TryCastBookId(req.BookId, out var bookId))
        {
            AddError($"Invalid book id: {req.BookId}.");
            await SendErrorsAsync(cancellation: ct, statusCode: 404);
            return;
        }
        
        if (!await IsRequestedBibleIdValidAsync(req.TargetBibleId))
        {
            AddError($"Invalid bible id: {req.TargetBibleId}.");
            await SendErrorsAsync(cancellation: ct, statusCode: 404);
            return;
        }
        
        var maxChapterNumberAndVerseNumbersByBookIdMap = await versificationService.GetMaxChapterNumberAndVerseNumbersByBookIdMapAsync(
            CachingVersificationService.EngVersificationSchemeBibleId,
            ct);
        
        var (maxChapterNumber, maxVerseNumberByChapterNumberMap) = maxChapterNumberAndVerseNumbersByBookIdMap[bookId];
        
        // 1 is assumed for Start Chapter and Start Verse for ENG bible only. Will need updated for passing a source bible.
        var startChapter = req.StartChapter is >= 1 && req.StartChapter.Value <= maxChapterNumber 
            ? req.StartChapter.Value 
            : 1;
        var endChapter = req.EndChapter is >= 1 && req.EndChapter.Value <= maxChapterNumber 
            ? req.EndChapter.Value 
            : maxChapterNumber;
        
        var startVerse = req.StartVerse is >= 1 && req.StartVerse.Value <= maxVerseNumberByChapterNumberMap[startChapter] 
            ? req.StartVerse.Value 
            : 1;
        var endVerse = req.EndVerse is >= 1 && req.EndVerse.Value <= maxVerseNumberByChapterNumberMap[endChapter] 
            ? req.EndVerse.Value 
            : maxVerseNumberByChapterNumberMap[endChapter];
        
        var minVerseId = BibleUtilities.GetVerseId(bookId, startChapter, startVerse);
        var maxVerseId = BibleUtilities.GetVerseId(bookId, endChapter, endVerse);
        
        var versificationMap = await VersificationUtilities.ConvertVersificationRangeAsync(
            CachingVersificationService.EngVersificationSchemeBibleId,
            minVerseId,
            maxVerseId,
            req.TargetBibleId,
            versificationService,
            ct);
            
        var sourceBibleVerseMappings = versificationMap
            .Where(mapping => mapping.Value != mapping.Key)
            .ToDictionary(
                mapping => mapping.Key,
                mapping => mapping.Value.HasValue ? MapToVerseReference(mapping.Value.Value) : null);
        
        var verseMappings = new List<VerseMapping>();
        foreach (var mapping in sourceBibleVerseMappings)
        {
            var (sourceBookId, sourceChapter, sourceVerse) = BibleUtilities.TranslateVerseId(mapping.Key);
            
            verseMappings.Add(new VerseMapping
            {
                SourceVerse = new VerseReference
                {
                    VerseId = mapping.Key,
                    Book = BibleBookCodeUtilities.FullNameFromId(sourceBookId),
                    Chapter = sourceChapter,
                    Verse = sourceVerse
                },
                TargetVerse = mapping.Value
            });
        }

        var response = new Response() { VerseMappings = verseMappings };
        
        await SendOkAsync(response, ct);
    }
    
    private static VerseReference MapToVerseReference(int mappedVerseId)
    {
        var (bookId, chapter, verse) = BibleUtilities.TranslateVerseId(mappedVerseId);
        var bookName = BibleBookCodeUtilities.FullNameFromId(bookId);
        
        return new VerseReference
        {
            VerseId = mappedVerseId,
            Book = bookName,
            Chapter = chapter,
            Verse = verse
        };
    }

    private async Task<bool> IsRequestedBibleIdValidAsync(int bibleId)
    {
        return await dbContext.Bibles.Where(b => b.Id == bibleId).AnyAsync();
    }
}