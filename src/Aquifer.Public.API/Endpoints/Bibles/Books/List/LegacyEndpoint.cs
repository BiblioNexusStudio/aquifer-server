using Aquifer.Public.API.Helpers;

namespace Aquifer.Public.API.Endpoints.Bibles.Books.List;

public class LegacyEndpoint : Endpoint
{
    public override void Configure()
    {
        // "/bible-books" is still supported for legacy compatibility but "/bibles/books" is the new hotness
        Get("/bible-books");
        Options(EndpointHelpers.UnauthenticatedServerCacheInSeconds(EndpointHelpers.OneHourInSeconds));

        // we can eventually hide this endpoint from the docs altogether: https://fast-endpoints.com/docs/swagger-support#filtering-endpoints
        Summary(s => s.Summary = "Deprecated: Use '/bibles/books' instead.");
    }
}
