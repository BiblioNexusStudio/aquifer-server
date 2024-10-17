using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Public.API.Endpoints.Resources.Updates.List;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/resources/updates");
        Description(d => d
            .WithTags("Resources")
            .ProducesProblemFE());
        Summary(s =>
        {
            s.Summary = "Get resource ids that are new or updated since the given UTC timestamp.";
            s.Description =
                "For a given UTC timestamp and optional language id, get a list of resource ids that are new or have been updated since the provided timestamp. This is intended for users who are storing Aquifer data locally and want to fetch new content.";
        });
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Response.Items = await dbContext.ResourceContentVersions
            .Where(x => (!req.LanguageId.HasValue || x.ResourceContent.LanguageId == req.LanguageId) &&
                x.IsPublished &&
                x.Updated >= req.Timestamp)
            .Select(x => new ResponseContent
            {
                ResourceId = x.ResourceContentId,
                LanguageId = x.ResourceContent.LanguageId,
                UpdateType = x.Version == 1 ? ResponseContentUpdateType.New : ResponseContentUpdateType.Updated,
                Timestamp = x.Updated
            })
            .ToListAsync(ct);
    }
}