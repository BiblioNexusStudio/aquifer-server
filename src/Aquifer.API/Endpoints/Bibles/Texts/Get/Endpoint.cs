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
        }

        await (response is null ? SendNotFoundAsync(ct) : SendOkAsync(response, ct));
    }
}