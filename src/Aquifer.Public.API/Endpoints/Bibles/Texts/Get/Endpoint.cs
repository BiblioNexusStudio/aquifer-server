using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Public.API.Endpoints.Bibles.Texts.Get;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, Response>
{
    private readonly AquiferDbContext _dbContext = dbContext;
    public override void Configure()
    {
        Get("/bibles/{BibleId}/texts");
        Description(d => d.ProducesProblemFE());
        Summary(s =>
        {
            s.Summary = "Gets the Bible text contained within a book of the Bible.";
            s.Description =
                "For a given Bible and book of the Bible, returns the Bible text for all verses within the chapter and verse parameters.";
        });
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var response = await _dbContext.BibleBooks
            .Where(bb => bb.Bible.Enabled && bb.Bible.Id == request.BibleId && bb.Code == request.BookCode!.ToUpper())
            .Select(bb => new Response
            {
                BibleId = bb.Bible.Id,
                BibleName = bb.Bible.Name,
                BibleAbbreviation = bb.Bible.Abbreviation,
                BookCode = bb.Code,
                BookName = bb.LocalizedName,
                Chapters = bb.Chapters
                    .Where(ch => ch.Number >= (request.StartChapter ?? 1) && ch.Number <= (request.EndChapter ?? 999))
                    .Select(ch => new ResponseChapters
                    {
                        Number = ch.Number,
                        Verses = ch.Verses
                            .Where(v => v.Number >= (request.StartChapter.HasValue && ch.Number == request.StartChapter.Value ? request.StartVerse ?? 1 : 1) &&
                                v.Number <= (request.EndChapter.HasValue && ch.Number == request.EndChapter.Value ? request.EndVerse ?? 999 : 999))
                            .Select(v => new ResponseChapterVerses
                            {
                                Number = v.Number,
                                Text = v.Text,
                            })
                            .ToList(),
                    })
                    .ToList(),
            })
            .FirstOrDefaultAsync(ct);

        await (response is null ? SendNotFoundAsync(ct) : SendOkAsync(response, ct));
    }
}