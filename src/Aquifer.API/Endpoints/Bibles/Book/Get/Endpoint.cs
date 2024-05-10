using Aquifer.Common.Utilities;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Bibles.Book.Get;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/bibles/{BibleId}/book/{BookCode}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var bookCodeEnum = BibleBookCodeUtilities.IdFromCode(request.BookCode);

        var book = await dbContext.BibleBookContents
            .SingleOrDefaultAsync(b => b.Bible.Enabled && b.BibleId == request.BibleId && b.BookId == bookCodeEnum, ct);

        if (book == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var response = new Response
        {
            AudioSize = book.AudioSize,
            AudioUrls = book.AudioUrls is not null ? JsonUtilities.DefaultDeserialize(book.AudioUrls) : null,
            BookCode = BibleBookCodeUtilities.CodeFromId(book.BookId),
            ChapterCount = book.ChapterCount,
            DisplayName = book.DisplayName,
            TextSize = book.TextSize,
            TextUrl = book.TextUrl
        };

        await SendOkAsync(response, ct);
    }
}