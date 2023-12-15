using Aquifer.API.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Resources.ResourceContentSummary;

public static class GetResourceContentSummaryEndpoints
{
    public static async Task<Results<Ok<ResourceContentSummaryById>, NotFound>> GetByResourceContentId(
        int resourceContentId,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var resourceContent = await dbContext.ResourceContents.Where(x => x.Id == resourceContentId)
            .Select(rc => new ResourceContentSummaryById
            {
                Label = rc.Resource.EnglishLabel,
                ParentResourceName = rc.Resource.ParentResource.DisplayName,
                ResourceContentId = rc.Id,
                Language = rc.Language.EnglishDisplay,
                Status = rc.Status,
                MediaType = rc.MediaType,
                HasAudio = rc.Resource.ResourceContents.Any(orc =>
                    orc.LanguageId == rc.LanguageId &&
                    orc.MediaType == ResourceContentMediaType.Audio),
                OtherLanguageContentIds = rc.Resource.ResourceContents
                    .Where(orc =>
                        orc.LanguageId != rc.LanguageId &&
                        orc.MediaType == rc.MediaType)
                    .Select(orc => new ResourceContentSummaryContentIdWithLanguageId
                    {
                        ContentId = orc.Id,
                        LanguageId = orc.LanguageId
                    }),
                AssociatedResources =
                    rc.Resource.AssociatedResourceChildren.Select(ar =>
                        new ResourceContentSummaryAssociatedContentById
                        {
                            Label = ar.EnglishLabel,
                            ParentResourceName = ar.ParentResource.DisplayName,
                            MediaTypes = ar.ResourceContents.Select(arrc => arrc.MediaType)
                        }),
                PassageReferences =
                    rc.Resource.PassageResources.Select(pr => new ResourceContentSummaryPassageById
                    {
                        StartVerseId = pr.Passage.StartVerseId,
                        EndVerseId = pr.Passage.EndVerseId
                    }),
                VerseReferences =
                    rc.Resource.VerseResources.Select(vr =>
                        new ResourceContentSummaryVerseById { VerseId = vr.VerseId })
            }).FirstOrDefaultAsync(cancellationToken);

        if (resourceContent is null)
        {
            return TypedResults.NotFound();
        }

        var resourceContentVersion = await dbContext.ResourceContentVersions
            .Where(x => x.ResourceContentId == resourceContentId)
            .OrderBy(x => x.IsDraft ? 0 : x.IsPublished ? 1 : 2) // TODO: make request specific to draft vs published. for now, return the draft first if it exists
            .OrderByDescending(x => x.Version)
            .Include(x => x.AssignedUser).FirstOrDefaultAsync(cancellationToken);

        if (resourceContentVersion is null)
        {
            return TypedResults.NotFound();
        }

        resourceContent.DisplayName = resourceContentVersion.DisplayName;
        resourceContent.IsPublished = resourceContentVersion.IsPublished;
        resourceContent.AssignedUser =
            resourceContentVersion.AssignedUser == null
                ? null
                : new ResourceContentSummaryAssignedUser
                {
                    Id = resourceContentVersion.AssignedUser.Id,
                    Name =
                        $"{resourceContentVersion.AssignedUser.FirstName} {resourceContentVersion.AssignedUser.LastName}"
                };
        resourceContent.ContentSize = resourceContentVersion.ContentSize;
        resourceContent.Content = JsonUtilities.DefaultDeserialize(resourceContentVersion.Content);

        return TypedResults.Ok(resourceContent);
    }
}