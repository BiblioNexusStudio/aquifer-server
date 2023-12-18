using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.AdminResources.ResourceReview;

public static class ResourceReviewEndpoints
{
    public static async Task<Results<Ok<string>, BadRequest<string>>> Review(int contentId,
        AquiferDbContext dbContext,
        IUserService userService,
        CancellationToken cancellationToken)
    {
        (var contentVersionDraft, var resourceContent, string badRequestResponse) =
            await GetResourceContentVersionValidation(contentId,
                dbContext,
                cancellationToken);

        if (contentVersionDraft is null || resourceContent is null)
        {
            return TypedResults.BadRequest(badRequestResponse);
        }

        var user = userService.GetUserFromJwtAsync(cancellationToken);

        contentVersionDraft.Updated = DateTime.UtcNow;
        contentVersionDraft.AssignedUserId = user.Id;

        resourceContent.Status = ResourceContentStatus.AquiferizeInReview;

        var resourceContentVersionStatusHistory = new ResourceContentVersionStatusHistoryEntity
        {
            ResourceContentVersionId = contentVersionDraft.Id,
            Status = ResourceContentStatus.AquiferizeInReview,
            ChangedByUserId = user.Id,
            Created = DateTime.UtcNow
        };
        dbContext.ResourceContentVersionStatusHistory.Add(resourceContentVersionStatusHistory);

        var resourceContentVersionAssignedUserHistory = new ResourceContentVersionAssignedUserHistoryEntity
        {
            ResourceContentVersion = contentVersionDraft,
            AssignedUserId = user.Id,
            ChangedByUserId = user.Id,
            Created = DateTime.UtcNow
        };
        dbContext.ResourceContentVersionAssignedUserHistory.Add(resourceContentVersionAssignedUserHistory);
        await dbContext.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok("Success");
    }

    private static async Task<(ResourceContentVersionEntity?, ResourceContentEntity?, string)>
        GetResourceContentVersionValidation(
            int contentId,
            AquiferDbContext dbContext,
            CancellationToken cancellationToken)
    {
        var resourceContentVersionDraft = await dbContext.ResourceContentVersions
            .Where(x => x.ResourceContentId == contentId && x.IsDraft == true).SingleOrDefaultAsync(cancellationToken);

        // First check that a ResourceContentVersion exists with the given contentId and IsDraft = 1
        if (resourceContentVersionDraft == null)
        {
            return (null, null, "This resource does not have a draft");
        }

        var resourceContent =
            await dbContext.ResourceContents.FirstOrDefaultAsync(x => x.Id == contentId, cancellationToken) ??
            throw new ArgumentNullException();

        if (resourceContent.Status != ResourceContentStatus.AquiferizeReviewPending)
        {
            return (null, null, "This resource is not in AquiferizeReviewPending status");
        }

        return (resourceContentVersionDraft, resourceContent, string.Empty);
    }
}