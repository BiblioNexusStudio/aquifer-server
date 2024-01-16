using Aquifer.Data;
using FastEndpoints;

namespace Aquifer.Public.API.Endpoints.Resources.Search.GetResources;

public class Endpoint : Endpoint<Request, Response>
{
    public AquiferDbContext _DbContext { get; set; }

    public override void Configure()
    {
        Get("/resources/search");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        // x.EnglishLabel.Contains(query) &&
        //     (request.ParentResourceId == default || x.ParentResourceId == request.ParentResourceId) &&
        //     (request.LanguageId == default || x.ResourceContents.Any(rc => rc.LanguageId == request.LanguageId))

        // var resources = await _DbContext.Resources.Where(x => x.EnglishLabel.Contains(req.Query) && )
        //     .OrderBy(x => x.EnglishLabel)
        //     .Skip(request.Skip).Take(request.Take)

        await SendAsync(new Response
            {
                Value = "Hello Fast Endpoints"
            },
            200,
            ct);
    }
}