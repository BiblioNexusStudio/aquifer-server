using Aquifer.Public.API.Helpers;

namespace Aquifer.Public.API.Endpoints.Bibles.Books.List;

public class LegacyEndpoint : Endpoint
{
    public override void Configure()
    {
        Get("/bible-books");
        Tags(EndpointHelpers.EndpointTags.ExcludeFromSwaggerDocument);
        Options(EndpointHelpers.UnauthenticatedServerCacheInSeconds(EndpointHelpers.OneHourInSeconds));
    }
}
