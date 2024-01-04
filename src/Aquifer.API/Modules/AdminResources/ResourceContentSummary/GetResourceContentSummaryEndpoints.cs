using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.AdminResources.ResourceContentSummary;

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
                        new ResourceContentSummaryVerseById { VerseId = vr.VerseId }),
                ContentVersions =
                    rc.Versions.Where(v => v.IsPublished || v.IsDraft).Select(v => new ResourceContentSummaryVersion
                    {
                        IsPublished = v.IsPublished,
                        IsDraft = v.IsDraft,
                        ContentValue = v.Content,
                        ContentSize = v.ContentSize,
                        DisplayName = v.DisplayName,
                        AssignedUser =
                            v.AssignedUser == null
                                ? null
                                : new ResourceContentSummaryAssignedUser
                                {
                                    Id = v.AssignedUser.Id,
                                    Name = $"{v.AssignedUser.FirstName} {v.AssignedUser.LastName}"
                                }
                    })
            }).FirstOrDefaultAsync(cancellationToken);

        if (resourceContent?.ContentVersions.Any() != true)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(resourceContent);
    }
}