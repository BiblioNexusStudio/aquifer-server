using Aquifer.API.Helpers;
using Aquifer.Common.Utilities;
using FastEndpoints;

namespace Aquifer.API.Endpoints.BibleBooks.List;

public class Endpoint : EndpointWithoutRequest<IEnumerable<Response>>
{
    public override void Configure()
    {
        Get("/bible-books");
        Options(EndpointHelpers.SetCacheOption(60));
        ResponseCache(EndpointHelpers.OneDayInSeconds);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = BibleBookCodeUtilities.GetAll().Select(x => new Response
        {
            Id = (int)x.BookId,
            Name = x.BookFullName,
            Code = x.BookCode
        });
        await SendOkAsync(response, ct);
    }
}