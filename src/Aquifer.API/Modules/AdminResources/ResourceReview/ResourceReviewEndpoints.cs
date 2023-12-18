using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.AdminResources.ResourceReview;

public static class ResourceReviewEndpoints
{
    public static async Task<Results<Ok, BadRequest<string>>> Review(int contentId,
        AquiferDbContext dbContext,
        IAdminResourceHistoryService historyService,
        IUserService userService,
        CancellationToken cancellationToken)
    {
        var contentVersionDraft = await dbContext.ResourceContentVersions
            .Where(x => x.ResourceContentId == contentId && x.IsDraft).Include(x => x.ResourceContent)
            .SingleOrDefaultAsync(cancellationToken);

        if (contentVersionDraft == null)
        {
            return TypedResults.BadRequest(AdminResourcesHelpers.NoDraftExistsResponse);
        }

        if (contentVersionDraft.ResourceContent?.Status != ResourceContentStatus.AquiferizeReviewPending)
        {
            return TypedResults.BadRequest(AdminResourcesHelpers.NotInReviewPendingResponse);
        }

        var user = userService.GetUserFromJwtAsync(cancellationToken);

        contentVersionDraft.Updated = DateTime.UtcNow;
        contentVersionDraft.AssignedUserId = user.Id;
        contentVersionDraft.ResourceContent.Status = ResourceContentStatus.AquiferizeInReview;

        await historyService.AddAssignedUserHistoryAsync(contentVersionDraft, user.Id, user.Id);
        await historyService.AddStatusHistoryAsync(contentVersionDraft.Id,
            ResourceContentStatus.AquiferizeInReview,
            user.Id);

        await dbContext.SaveChangesAsync(cancellationToken);
        return TypedResults.Ok();
    }

    public static async Task<Results<Ok, BadRequest<string>>> SendToReview(
        AquiferDbContext dbContext,
        int contentId,
        IUserService userService,
        CancellationToken cancellationToken)
    {
        bool statusChanged = false;

        var resourceContentVersions = await dbContext.ResourceContentVersions
            .Where(rcv => rcv.ResourceContentId == contentId)
            .ToListAsync(cancellationToken);

        var mostRecentResourceContentVersionForReview = resourceContentVersions
            .Where(rvc => rvc.IsDraft)
            .MaxBy(rcv => rcv.Version);

        var resourceContent =
            await dbContext.ResourceContents.FindAsync(contentId, cancellationToken) ??
            throw new ArgumentNullException();

        if (mostRecentResourceContentVersionForReview is null ||
            resourceContent.Status != ResourceContentStatus.AquiferizeInProgress)
        {
            return TypedResults.BadRequest("Resource content not found or not in draft status");
        }

        var user = userService.GetUserFromJwtAsync(cancellationToken);
        bool claimsPrincipalHasSendReviewOverridePermission = userService.HasClaim(PermissionName.SendReviewOverride);

        if (claimsPrincipalHasSendReviewOverridePermission)
        {
            resourceContent.Status = ResourceContentStatus.AquiferizeReviewPending;
            mostRecentResourceContentVersionForReview.AssignedUserId = null;
            statusChanged = true;
        }
        else
        {
            if (user.Id == mostRecentResourceContentVersionForReview.AssignedUserId)
            {
                resourceContent.Status = ResourceContentStatus.AquiferizeReviewPending;
                mostRecentResourceContentVersionForReview.AssignedUserId = null;
                statusChanged = true;
            }
        }

        // if the status was changed, then update the resource content version status history and user history tables
        if (statusChanged)
        {
            var resourceContentVersionStatusHistory = new ResourceContentVersionStatusHistoryEntity
            {
                ResourceContentVersionId = mostRecentResourceContentVersionForReview.Id,
                Status = ResourceContentStatus.AquiferizeReviewPending,
                ChangedByUserId = user.Id,
                Created = DateTime.UtcNow
            };
            dbContext.ResourceContentVersionStatusHistory.Add(resourceContentVersionStatusHistory);

            var resourceContentVersionAssignedUserHistory = new ResourceContentVersionAssignedUserHistoryEntity
            {
                ResourceContentVersionId = mostRecentResourceContentVersionForReview.Id,
                AssignedUserId = null,
                ChangedByUserId = user.Id,
                Created = DateTime.UtcNow
            };
            dbContext.ResourceContentVersionAssignedUserHistory.Add(resourceContentVersionAssignedUserHistory);

            await dbContext.SaveChangesAsync(cancellationToken);
            return TypedResults.Ok();
        }

        return TypedResults.BadRequest("Unable to change status of resource content");
    }

    private static async Task<(ResourceContentVersionEntity?, ResourceContentEntity?, string)>
        GetResourceContentVersionValidation(
            int contentId,
            AquiferDbContext dbContext,
            CancellationToken cancellationToken)
    {
        var resourceContentVersionDraft = await dbContext.ResourceContentVersions
            .Where(x => x.ResourceContentId == contentId && x.IsDraft == true).SingleOrDefaultAsync(cancellationToken);

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