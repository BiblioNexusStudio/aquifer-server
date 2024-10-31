using Aquifer.API.Common;
using Aquifer.Data;
using Aquifer.Data.Entities;
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
            .AsTracking()
            .Include(rc => rc.Resource)
            .ThenInclude(r => r.AssociatedResources)
            .SingleOrDefaultAsync(rc => rc.Id == request.ResourceContentId, ct);

        if (resourceContent == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        List<ResourceContentStatus> notAllowedStatuses =
            [ResourceContentStatus.TranslationAiDraftComplete, ResourceContentStatus.TranslationAwaitingAiDraft];
        if (notAllowedStatuses.Contains(resourceContent.Status))
        {
            ThrowError(x => x.ResourceContentId, "Resource is not in the correct status");
        }

        if (!resourceContent.Resource.AssociatedResources.Any(r => r.AssociatedResourceId == request.ReferenceResourceId))
        {
            var referenceResource = await dbContext.Resources.FindAsync([request.ReferenceResourceId], ct);

            if (referenceResource == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            resourceContent.Resource.AssociatedResources.Add(new AssociatedResourceEntity { AssociatedResource = referenceResource });
            await dbContext.SaveChangesAsync(ct);
        }

        await SendNoContentAsync(ct);
    }
}