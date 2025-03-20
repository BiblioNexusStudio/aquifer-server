using Aquifer.API.Helpers;
using Aquifer.Common.Services.Caching;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Bibles.Book.List;

public class Endpoint(AquiferDbContext _dbContext, ICachingVersificationService _cachingVersificationService)
    : Endpoint<Request, IReadOnlyList<Response>>
{
    public override void Configure()
    {
        Get("/bibles/{BibleId}/books");
        ResponseCache(EndpointHelpers.OneDayInSeconds);
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var maxChapterNumberAndBookendVerseNumbersByBookIdMap =
            await _cachingVersificationService.GetMaxChapterNumberAndBookendVerseNumbersByBookIdMapAsync(req.BibleId, ct);

        var response = (await _dbContext.BibleBookContents
                .Where(bbc => bbc.Bible.Enabled && bbc.BibleId == req.BibleId)
                .Select(
                    bbc => new
                    {
                        bbc.BookId,
                        bbc.Book.Code,
                        bbc.DisplayName,
                        bbc.ChapterCount,
                    })
                .ToListAsync(ct))
            .Select(
                x => new Response
                {
                    Code = x.Code,
                    Number = (int)x.BookId,
                    LocalizedName = x.DisplayName,
                    TotalChapters = x.ChapterCount,
                    Chapters = maxChapterNumberAndBookendVerseNumbersByBookIdMap
                            .GetValueOrNull(x.BookId)
                            ?.BookendVerseNumbersByChapterNumberMap
                            .OrderBy(kvp => kvp.Key)
                            .Select(
                                kvp => new ResponseChapter
                                {
                                    Number = kvp.Key,
                                    TotalVerses = kvp.Value.MaxVerseNumber,
                                })
                            .ToList()
                        ?? [],
                })
            .ToList();

        await SendOkAsync(response, ct);
    }
}