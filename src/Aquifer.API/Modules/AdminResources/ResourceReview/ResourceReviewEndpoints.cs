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
        IAdminResourceHistoryService historyService,
        CancellationToken cancellationToken)
    {
        var draftVersion = await dbContext.ResourceContentVersions
            .Where(x => x.ResourceContentId == contentId && x.IsDraft).Include(x => x.ResourceContent)
            .SingleOrDefaultAsync(cancellationToken);

        if (draftVersion is null || draftVersion.ResourceContent.Status != ResourceContentStatus.AquiferizeInProgress)
        {
            return TypedResults.BadRequest("Resource content not found or not in draft status");
        }

        var user = userService.GetUserFromJwtAsync(cancellationToken);
        bool claimsPrincipalHasSendReviewOverridePermission = userService.HasClaim(PermissionName.SendReviewOverride);

        if (!claimsPrincipalHasSendReviewOverridePermission && user.Id != draftVersion.AssignedUserId)
        {
            return TypedResults.BadRequest("Unable to change status of resource content");
        }

        draftVersion.ResourceContent.Status = ResourceContentStatus.AquiferizeReviewPending;
        draftVersion.AssignedUserId = null;

        await historyService.AddAssignedUserHistoryAsync(draftVersion.Id, null, user.Id);
        await historyService.AddStatusHistoryAsync(draftVersion.Id,
            ResourceContentStatus.AquiferizeReviewPending,
            user.Id);

        await dbContext.SaveChangesAsync(cancellationToken);
        return TypedResults.Ok();
    }
}