using Aquifer.Data;
using Aquifer.Data.Entities;
using Aquifer.Public.API.Helpers;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Public.API.Endpoints.Resources.Collections.List;

public sealed class Endpoint(AquiferDbReadOnlyContext _dbContext)
    : Endpoint<Request, IReadOnlyList<Response>>
{
    public override void Configure()
    {
        Get("/resources/collections");
        Options(EndpointHelpers.UnauthenticatedServerCacheInSeconds(EndpointHelpers.OneHourInSeconds));
        Description(d => d
            .WithTags("Resources/Collections")
            .ProducesProblemFE());
        Summary(s =>
        {
            s.Summary = "Get a list of resource collections.";
            s.Description =
                "Returns summary data for all resource collections, optionally filtering by resource type.  Note that additional collection information can be retrieved via the individual GET route.";
        });
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var parentResources = await _dbContext.ParentResources
            .Where(pr =>
                pr.Enabled &&
                (req.ResourceType == ResourceType.None || pr.ResourceType == req.ResourceType))
            .OrderBy(pr => pr.DisplayName)
            .Skip(req.Offset)
            .Take(req.Limit)
            .Select(pr => new Response
            {
                Code = pr.Code,
                DisplayName = pr.DisplayName,
                ShortName = pr.ShortName,
                ResourceType = pr.ResourceType,
            })
            .ToListAsync(ct);

        await SendOkAsync(parentResources, ct);
    }
}