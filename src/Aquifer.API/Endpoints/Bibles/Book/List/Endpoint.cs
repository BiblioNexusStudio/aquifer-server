using Aquifer.API.Helpers;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Bibles.Book.List;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, List<Response>>
{
    public override void Configure()
    {
        Get("/bibles/{BibleId}/books");
        ResponseCache(EndpointHelpers.OneDayInSeconds);
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var response = await dbContext.BibleBookContents
            .Where(bbc => bbc.Bible.Enabled && bbc.BibleId == req.BibleId)
            .Select(bbc => new Response
            {
                Code = bbc.Book.Code,
                Number = (int)bbc.BookId,
                LocalizedName = bbc.DisplayName,
                TotalChapters = bbc.Book.Chapters.Count,
                Chapters = bbc.Book.Chapters.OrderBy(c => c.Number).Select(c => new ResponseChapter
                {
                    Number = c.Number,
                    TotalVerses = c.VerseCount
                })
            }).ToListAsync(ct);

        await SendOkAsync(response, ct);
    }
}