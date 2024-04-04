using Aquifer.API.Helpers;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Bibles.Language.Book.Text.Get;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request>
{
    public override void Configure()
    {
        Get("/bibles/language/{LanguageId}/book/{BookCode}/text");
        Options(EndpointHelpers.SetCacheOption(24 * 60 * 60));
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var bookCodeEnum = BibleBookCodeUtilities.IdFromCode(request.BookCode);

        var textUrl = await dbContext.BibleBookContents
            .Where(bbc => bbc.Bible.LanguageId == request.LanguageId && bbc.BookId == bookCodeEnum)
            .Select(bbc => bbc.TextUrl)
            .FirstOrDefaultAsync(ct);

        if (textUrl == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendRedirectAsync(textUrl, true, ct);
    }
}