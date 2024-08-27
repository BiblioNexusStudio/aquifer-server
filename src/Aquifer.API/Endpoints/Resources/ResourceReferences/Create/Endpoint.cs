using Aquifer.API.Common;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.ResourceReferences.Create;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request>
{
    public override void Configure()
    {
        Post("/resources/resource-references");
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

        if (!resourceContent.Resource.AssociatedResourceChildren.Any(r => r.Id == request.ReferenceResourceId))
        {
            var referenceResource = await dbContext.Resources.FindAsync([request.ReferenceResourceId], ct);

            if (referenceResource == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            resourceContent.Resource.AssociatedResourceChildren.Add(referenceResource);
            await dbContext.SaveChangesAsync(ct);
        }

        await SendNoContentAsync(ct);
    }
}