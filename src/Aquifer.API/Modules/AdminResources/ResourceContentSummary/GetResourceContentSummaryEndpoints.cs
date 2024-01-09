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
                ResourceId = rc.ResourceId,
                Language = rc.Language.EnglishDisplay,
                Status = rc.Status,
                MediaType = rc.MediaType,
                HasAudio = rc.Resource.ResourceContents.Any(orc =>
                    orc.LanguageId == rc.LanguageId &&
                    orc.MediaType == ResourceContentMediaType.Audio),
                ContentTranslations = rc.Resource.ResourceContents
                    .Where(orc => orc.MediaType == rc.MediaType)
                    .Select(orc => new ResourceContentSummaryContentTranslations
                    {
                        ContentId = orc.Id,
                        LanguageId = orc.LanguageId,
                        Status = orc.Status
                    }),
                AssociatedResources =
                    rc.Resource.AssociatedResourceChildren.Select(ar =>
                        new ResourceContentSummaryAssociatedContentById
                        {
                            Label = ar.EnglishLabel,
                            ParentResourceName = ar.ParentResource.DisplayName,
                            MediaTypes = ar.ResourceContents.Select(arrc => arrc.MediaType)
                        }),
                ContentVersions =
                    rc.Versions.Select(v => new ResourceContentSummaryVersion
                    {
                        IsPublished = v.IsPublished,
                        IsDraft = v.IsDraft,
                        Version = v.Version,
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

        resourceContent.VerseReferences = await dbContext.VerseResources
            .Where(x => x.ResourceId == resourceContent.ResourceId)
            .Select(vr => new ResourceContentSummaryVerseById { VerseId = vr.VerseId }).ToListAsync(cancellationToken);

        resourceContent.PassageReferences = await dbContext.PassageResources
            .Where(x => x.ResourceId == resourceContent.ResourceId)
            .Select(pr => new ResourceContentSummaryPassageById
            {
                StartVerseId = pr.Passage.StartVerseId,
                EndVerseId = pr.Passage.EndVerseId
            }).ToListAsync(cancellationToken);

        var contentVersions = resourceContent.ContentVersions.Where(x => x.IsPublished || x.IsDraft).ToList();
        if (contentVersions.Count == 0)
        {
            contentVersions = resourceContent.ContentVersions.OrderByDescending(x => x.Version).Take(1).ToList();
        }

        resourceContent.ContentVersions = contentVersions;

        return TypedResults.Ok(resourceContent);
    }
}