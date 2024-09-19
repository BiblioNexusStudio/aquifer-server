using Aquifer.Common.Utilities;
using Aquifer.Public.API.Helpers;
using FastEndpoints;

namespace Aquifer.Public.API.Endpoints.Bibles.Books.List;

public class Endpoint : EndpointWithoutRequest<IEnumerable<Response>>
{
    public override void Configure()
    {
        // "/bible-books" is still supported for legacy compatibility but "/bibles/books" is the new hotness
        Get("/bibles/books");
        Options(EndpointHelpers.UnauthenticatedServerCacheInSeconds(EndpointHelpers.OneHourInSeconds));

        Summary(s =>
        {
            s.Summary = "Get a list of Bible books.";
            s.Description =
                "Returns the list of Bible books in the system and their corresponding book number to be used in other calls.";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = BibleBookCodeUtilities.GetAll().Select(x => new Response { Name = x.BookFullName, Code = x.BookCode });

        await SendOkAsync(response, ct);
    }
}