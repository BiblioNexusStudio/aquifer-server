using Aquifer.API.Common;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.BibleReferences.Delete;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request>
{
    public override void Configure()
    {
        Delete("/resources/bible-references");
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
            var verseResource = resourceContent.Resource.VerseResources
                .SingleOrDefault(vr => vr.VerseId == request.StartVerseId);
            if (verseResource != null)
            {
                dbContext.VerseResources.Remove(verseResource);
            }
        }
        else
        {
            var passageResource = resourceContent.Resource.PassageResources
                .SingleOrDefault(pr => pr.Passage.StartVerseId == request.StartVerseId && pr.Passage.EndVerseId == request.EndVerseId);
            if (passageResource != null)
            {
                dbContext.PassageResources.Remove(passageResource);
            }
        }

        await dbContext.SaveChangesAsync(ct);
        await SendNoContentAsync(ct);
    }
}