using Aquifer.API.Helpers;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Bibles.Texts.Get;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/bibles/{BibleId}/texts");
        ResponseCache(EndpointHelpers.OneDayInSeconds, varyByQueryKeys: ["*"]);
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var response = await dbContext.BibleBooks
            .Where(x => x.Bible.Enabled && x.Bible.Id == req.BibleId && ((int)x.Number == req.BookNumber || x.Code == req.BookCode))
            .Select(x => new Response
            {
                BibleId = x.Bible.Id,
                BibleName = x.Bible.Name,
                BibleAbbreviation = x.Bible.Abbreviation,
                BookCode = x.Code,
                BookName = x.LocalizedName,
                BookNumber = (int)x.Number,
                Chapters = x.Chapters.Where(ch => ch.Number >= req.StartChapter && ch.Number <= req.EndChapter)
                    .Select(ch => new ResponseChapters
                    {
                        Number = ch.Number,
                        Verses = ch.Verses
                            .Where(v => v.Number >= (ch.Number == req.StartChapter ? req.StartVerse : 1) &&
                                v.Number <= (ch.Number == req.EndChapter ? req.EndVerse : 999))
                            .Select(v => new ResponseChapterVerses
                            {
                                Number = v.Number,
                                Text = v.Text
                            })
                    })
            })
            .FirstOrDefaultAsync(ct);

        await (response is null ? SendNotFoundAsync(ct) : SendOkAsync(response, ct));
    }
}