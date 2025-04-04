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

        if (!await IsRequestedBibleIdValidAsync(req.TargetBibleId, ct))
        {
            AddError($"Invalid bible id: {req.TargetBibleId}.");
            await SendErrorsAsync(cancellation: ct, statusCode: 404);
            return;
        }

        var verseRange = await VersificationUtilities.GetValidVerseIdRangeAsync(
            CachingVersificationService.EngVersificationSchemeBibleId,
            bookId,
            req.StartChapter,
            req.StartVerse,
            req.EndChapter,
            req.EndVerse,
            versificationService,
            ct);

        IReadOnlyList<VerseMapping> verseMappings;
        if (!verseRange.HasValue)
        {
            verseMappings = [];
        }
        else
        {
            var versificationMap = await VersificationUtilities.ConvertVersificationRangeAsync(
                CachingVersificationService.EngVersificationSchemeBibleId,
                verseRange.Value.StartVerseId,
                verseRange.Value.EndVerseId,
                req.TargetBibleId,
                versificationService,
                ct);

            verseMappings = versificationMap
                .Select(
                    mapping => new VerseMapping
                    {
                        SourceVerse = MapToVerseReference(mapping.Key),
                        TargetVerses = mapping.Value.Select(MapToVerseReference).ToList(),
                    })
                .ToList();
        }

        var response = new Response { VerseMappings = verseMappings };

        await SendOkAsync(response, ct);
    }

    private static VerseReference MapToVerseReference(int verseId)
    {
        var (bookId, chapter, verse) = BibleUtilities.TranslateVerseId(verseId);
        var bookName = BibleBookCodeUtilities.FullNameFromId(bookId);

        return new VerseReference
        {
            VerseId = verseId,
            Book = bookName,
            Chapter = chapter,
            Verse = verse
        };
    }

    private async Task<bool> IsRequestedBibleIdValidAsync(int bibleId, CancellationToken ct)
    {
        return await dbContext.Bibles.Where(b => b.Id == bibleId).AnyAsync(ct);
    }
}