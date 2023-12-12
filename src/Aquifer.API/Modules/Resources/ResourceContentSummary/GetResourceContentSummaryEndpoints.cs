using Aquifer.API.Utilities;
using Aquifer.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Resources.ResourceContentSummary;

public static class GetResourceContentSummaryEndpoints
{
    public static async Task<Ok<ResourceContentSummaryById>> GetByResourceContentId(int resourceContentId,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var resource = await dbContext.ResourceContents
            .Where(x => x.Id == resourceContentId).SelectMany(rc => rc.Versions.Where(rcv => rcv.IsPublished) // TODO: switch this to IsDraft
            .Select(rcv => new ResourceContentSummaryById
            {
                Label = rc.Resource.EnglishLabel,
                ParentResourceName = rc.Resource.ParentResource.DisplayName,
                ResourceContentId = rc.Id,
                Language = rc.Language.EnglishDisplay,
                DisplayName = rcv.DisplayName,
                Status = rc.Status,
                IsPublished = rcv.IsPublished,
                AssignedUser = rcv.AssignedUser == null ? null : new ResourceContentSummaryAssignedUser
                {
                    Id = rcv.AssignedUser.Id,
                    Name = "${rcv.AssignedUser.FirstName} ${rc.AssignedUser.LastName}"
                },
                ContentSize = rcv.ContentSize,
                Content = JsonUtilities.DefaultDeserialize(rcv.Content),
                MediaType = rc.MediaType,
                HasAudio = rc.Resource.ResourceContents.Any(orc => orc.LanguageId == rc.LanguageId
                        && orc.MediaType == Data.Entities.ResourceContentMediaType.Audio),
                OtherLanguageContentIds = rc.Resource.ResourceContents
                    .Where(orc => orc.LanguageId != rc.LanguageId && orc.MediaType == rc.MediaType)
                    .Select(orc => new ResourceContentSummaryContentIdWithLanguageId
                    {
                        ContentId = orc.Id,
                        LanguageId = orc.LanguageId
                    }),
                AssociatedResources =
                    rc.Resource.AssociatedResourceChildren.Select(ar => new ResourceContentSummaryAssociatedContentById
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
                    }).OrderBy(pr => pr.StartVerseId),
                VerseReferences = rc.Resource.VerseResources.Select(vr => new ResourceContentSummaryVerseById { VerseId = vr.VerseId })
            })).FirstOrDefaultAsync(cancellationToken);
        return TypedResults.Ok(resource);
    }
}