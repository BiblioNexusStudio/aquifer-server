using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.PullFromReviewPending;

public class Endpoint(AquiferDbContext dbContext, IUserService userService, IResourceHistoryService historyService) : Endpoint<Request>
{
    public override void Configure()
    {
        Post(
            "/resources/content/pull-from-review-pending",
            "/resources/content/{ContentId}/pull-from-review-pending");
        Permissions(PermissionName.AssignContent, PermissionName.AssignOverride, PermissionName.AssignOutsideCompany);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);
        var permissions = ResourceStatusHelpers.GetAssignmentPermissions(userService);

        await ResourceStatusHelpers.ValidateReviewerAndAssignedUser<Request>(request.AssignedUserId, request.AssignedReviewerUserId,
            dbContext, user, permissions, ct);

        var contentIds = request.ContentId is not null ? [(int)request.ContentId] : request.ContentIds!;

        var draftVersions = await ResourceStatusHelpers.GetDraftVersions<Request>(contentIds,
            [ResourceContentStatus.AquiferizeReviewPending, ResourceContentStatus.TranslationReviewPending], dbContext, ct);

        foreach (var draftVersion in draftVersions)
        {
            var originalStatus = draftVersion.ResourceContent.Status;

            await ValidateAllowedToAssign(permissions,
                draftVersion, user, ct);

            SetDraftVersionStatus(originalStatus, draftVersion);

            ResourceStatusHelpers.SetAssignedReviewerUserId(request.AssignedReviewerUserId, draftVersion);

            await ResourceStatusHelpers.SaveHistory(request.AssignedUserId, historyService, draftVersion, originalStatus, user, ct);
        }

        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }

    private async Task ValidateAllowedToAssign(ResourceStatusHelpers.AssignmentPermissions permissions,
        ResourceContentVersionEntity draftVersion,
        UserEntity user,
        CancellationToken ct)
    {
        var originalStatus = draftVersion.ResourceContent.Status;
        var currentUserIsAssigned = draftVersion.AssignedUserId == user.Id;
        var isTakingBackFromReviewPending = permissions.HasSendReviewContentPermission && !currentUserIsAssigned &&
                                            Constants.ReviewPendingStatuses.Contains(originalStatus) &&
                                            await WasLastAssignedToSelfOrIsCompanyLead(draftVersion, user.Id, ct);
        if (!isTakingBackFromReviewPending)
        {
            ThrowError(
                $"Must be in review pending state before pulling back for review. Current state: {draftVersion.ResourceContent.Status}");
        }
    }

    private async Task<bool> WasLastAssignedToSelfOrIsCompanyLead(ResourceContentVersionEntity version, int userId, CancellationToken ct)
    {
        var wasLastAssignedToSelf = await dbContext.ResourceContentVersionAssignedUserHistory
            .AsTracking()
            .Where(h => h.ResourceContentVersionId == version.Id && h.AssignedUserId != null)
            .OrderByDescending(h => h.Created)
            .Select(h => h.AssignedUserId == userId)
            .FirstOrDefaultAsync(ct);

        if (wasLastAssignedToSelf)
        {
            return true;
        }

        var isCompanyLead = await dbContext.Projects
            .AsTracking()
            .Where(p => p.ProjectResourceContents.Any(prc => prc.ResourceContent.Id == version.ResourceContentId))
            .Select(p => p.CompanyLeadUserId == userId)
            .FirstOrDefaultAsync(ct);

        return isCompanyLead;
    }

    private static void SetDraftVersionStatus(
        ResourceContentStatus originalStatus,
        ResourceContentVersionEntity draftVersion)
    {
        draftVersion.ResourceContent.Status = originalStatus == ResourceContentStatus.TranslationReviewPending
            ? ResourceContentStatus.TranslationCompanyReview
            : ResourceContentStatus.AquiferizeCompanyReview;
    }
}