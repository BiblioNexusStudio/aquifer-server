using Aquifer.API.Helpers;
using Aquifer.Common.Utilities;
using FastEndpoints;

namespace Aquifer.API.Endpoints.BibleBooks.List;

public class Endpoint : EndpointWithoutRequest<IReadOnlyList<Response>>
{
    public override void Configure()
    {
        Get("/bible-books");
        Options(EndpointHelpers.UnauthenticatedServerCacheInSeconds(EndpointHelpers.OneHourInSeconds));
        ResponseCache(EndpointHelpers.OneHourInSeconds);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = BibleBookCodeUtilities.GetAll().Select(x => new Response
        {
            Id = (int)x.BookId,
            Name = x.BookFullName,
            Code = x.BookCode
        })
        .ToList();

        await SendOkAsync(response, ct);
    }
}