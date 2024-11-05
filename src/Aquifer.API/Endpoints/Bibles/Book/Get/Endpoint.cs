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

        var response = await dbContext.BibleBookContents
            .Where(bbc => bbc.Bible.Enabled && bbc.BibleId == request.BibleId && bbc.BookId == bookCodeEnum)
            .Select(bbc => new Response
            {
                AudioSize = bbc.AudioSize,
                AudioUrls = bbc.AudioUrls != null ? JsonUtilities.DefaultDeserialize(bbc.AudioUrls) : null,
                BookCode = BibleBookCodeUtilities.CodeFromId(bbc.BookId),
                ChapterCount = bbc.Book.Chapters.Count,
                DisplayName = bbc.DisplayName,
                TextSize = bbc.TextSize
            })
            .SingleOrDefaultAsync(ct);

        if (response == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(response, ct);
    }
}