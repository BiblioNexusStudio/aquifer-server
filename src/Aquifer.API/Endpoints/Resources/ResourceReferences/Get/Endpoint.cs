using Aquifer.API.Common;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.ResourceReferences.Get;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/resources/resource-references");
        Permissions(PermissionName.EditContentResourceReferences);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var query = dbContext.Resources.Where(r => r.ParentResourceId == request.ParentResourceId && r.Id == request.ResourceId);

        var resource = await query
            .Select(r => new Response { ResourceId = r.Id, EnglishLabel = r.EnglishLabel })
            .FirstOrDefaultAsync(ct);

        if (resource == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(resource, ct);
    }
}