using Aquifer.API.Common;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.BibleReferences.Create;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request>
{
    public override void Configure()
    {
        Post("/resources/bible-references");
        Permissions(PermissionName.EditContentBibleReferences);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var resourceContent = await dbContext.ResourceContents
            .Include(rc => rc.Resource)
            .ThenInclude(r => r.VerseResources)
            .Include(rc => rc.Resource)
            .ThenInclude(r => r.PassageResources)
            .ThenInclude(r => r.Passage)
            .SingleOrDefaultAsync(rc => rc.Id == request.ResourceContentId, ct);

        if (resourceContent == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        if (resourceContent.LanguageId != 1)
        {
            ThrowError("Only English content can be used for updating Bible references.");
            return;
        }

        if (request.StartVerseId == request.EndVerseId)
        {
            if (!resourceContent.Resource.VerseResources.Any(vr => vr.VerseId == request.StartVerseId))
            {
                await dbContext.VerseResources.AddAsync(
                    new VerseResourceEntity { ResourceId = resourceContent.ResourceId, VerseId = request.StartVerseId }, ct);
            }
        }
        else
        {
            if (!resourceContent.Resource.PassageResources.Any(pr =>
                    pr.Passage.StartVerseId == request.StartVerseId && pr.Passage.EndVerseId == request.EndVerseId))
            {
                var passage = await dbContext.Passages
                                  .SingleOrDefaultAsync(p => p.StartVerseId == request.StartVerseId && p.EndVerseId == request.EndVerseId,
                                      ct) ??
                              new PassageEntity { StartVerseId = request.StartVerseId, EndVerseId = request.EndVerseId };
                ;

                await dbContext.PassageResources.AddAsync(
                    new PassageResourceEntity { ResourceId = resourceContent.ResourceId, Passage = passage }, ct);
            }
        }

        await dbContext.SaveChangesAsync(ct);
        await SendOkAsync(ct);
    }
}