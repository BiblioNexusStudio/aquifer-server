using Aquifer.API.Common;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.ResourceReferences.Search;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, IEnumerable<Response>>
{
    public override void Configure()
    {
        Get("/resources/resource-references/search");
        Permissions(PermissionName.EditContentResourceReferences);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var query = dbContext.Resources.Where(r =>
            r.ParentResourceId == request.ParentResourceId && r.EnglishLabel.Contains(request.Query));

        var resources = await query
            .OrderBy(r => r.SortOrder)
            .ThenBy(r => r.EnglishLabel)
            .Select(r => new Response
            {
                ResourceId = r.Id,
                EnglishLabel = r.EnglishLabel,
            })
            .ToListAsync(ct);

        await SendOkAsync(resources, ct);
    }
}