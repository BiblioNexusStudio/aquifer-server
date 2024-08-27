using Aquifer.API.Common;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.ResourceReferences.Delete;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request>
{
    public override void Configure()
    {
        Delete("/resources/resource-references");
        Permissions(PermissionName.EditContentResourceReferences);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var resourceContent = await dbContext.ResourceContents
            .Include(rc => rc.Resource)
            .ThenInclude(r => r.AssociatedResourceChildren)
            .SingleOrDefaultAsync(rc => rc.Id == request.ResourceContentId, ct);

        if (resourceContent == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var associatedResource = resourceContent.Resource.AssociatedResourceChildren
            .FirstOrDefault(r => r.Id == request.ReferenceResourceId);

        if (associatedResource == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        resourceContent.Resource.AssociatedResourceChildren.Remove(associatedResource);

        await dbContext.SaveChangesAsync(ct);
        await SendNoContentAsync(ct);
    }
}