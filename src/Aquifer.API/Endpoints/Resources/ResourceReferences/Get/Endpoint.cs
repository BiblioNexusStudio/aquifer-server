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
        var query = dbContext.Resources.Where(r => r.ParentResourceId == request.ParentResourceId);

        query = int.TryParse(request.ResourceIdOrExternalId, out var resourceId)
            ? query.Where(r => r.Id == resourceId || r.ExternalId == request.ResourceIdOrExternalId)
                .OrderBy(r => r.Id == resourceId ? 1 : 0)
            : query.Where(r => r.ExternalId == request.ResourceIdOrExternalId);

        var resource = await query
            .Select(r => new Response
            {
                ResourceId = r.Id,
                EnglishDisplayName =
                    r.ResourceContents.Where(rc => rc.LanguageId == 1)
                        .SelectMany(rc => rc.Versions.Where(v => v.IsPublished).Select(v => v.DisplayName)).FirstOrDefault(),
                EnglishLabel = r.EnglishLabel
            })
            .FirstOrDefaultAsync(ct);

        if (resource == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(resource, ct);
    }
}