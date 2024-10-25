using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.AssignEditor;

public class Endpoint(AquiferDbContext dbContext, IUserService userService, IResourceHistoryService historyService) : Endpoint<Request>
{
    public override void Configure()
    {
        Post("/resources/content/{ContentId}/assign-editor",
            "/resources/content/assign-editor");
        Permissions(PermissionName.AssignContent, PermissionName.AssignOverride, PermissionName.AssignOutsideCompany);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);
        var userToAssign = await dbContext.Users
            .AsTracking()
            .SingleOrDefaultAsync(u => u.Id == request.AssignedUserId && u.Enabled, ct);
        var hasSendReviewContentPermission = userService.HasPermission(PermissionName.SendReviewContent);
        var hasAssignOverridePermission = userService.HasPermission(PermissionName.AssignOverride);
        var hasAssignOutsideCompanyPermission = userService.HasPermission(PermissionName.AssignOutsideCompany);

        if (userToAssign is null)
        {
            ThrowError(Helpers.InvalidUserIdResponse);
        }

        if (userToAssign.CompanyId != user.CompanyId && !hasAssignOutsideCompanyPermission)
        {
            ThrowError("Unable to assign to a user outside your company");
        }

        if (request.AssignedReviewerUserId is not null) {
            var reviewer = await dbContext.Users
                .Where(u => u.Id == request.AssignedReviewerUserId
                    && u.Enabled 
                    && u.CompanyId == userToAssign.CompanyId
                    && (u.Role == UserRole.Reviewer || u.Role == UserRole.Manager))
                .SingleOrDefaultAsync(ct);

            if (reviewer is null) {
                ThrowError("Unable to assign that reviewer");
            }
        }

        var contentIds = request.ContentId is not null ? [(int)request.ContentId] : request.ContentIds!;

        var draftVersions = await dbContext.ResourceContentVersions
            .AsTracking()
            .Where(rcv => contentIds.Contains(rcv.ResourceContentId) && rcv.IsDraft).Include(rcv => rcv.ResourceContent)
            .Include(rcv => rcv.AssignedUser).Include(rcv => rcv.ResourceContentVersionSnapshots)
            .ToListAsync(ct);

        if (draftVersions.Count != contentIds.Length)
        {
            ThrowError("One or more resources not found or not in draft status");
        }

        foreach (var draftVersion in draftVersions)
        {
            var originalStatus = draftVersion.ResourceContent.Status;
            var currentUserIsAssigned = draftVersion.AssignedUserId == user.Id;
            var assignedUserIsInCompany = draftVersion.AssignedUser?.CompanyId == user.CompanyId;
            var isTakingBackFromReviewPending = hasSendReviewContentPermission && !currentUserIsAssigned &&
                                                Constants.ReviewPendingStatuses.Contains(originalStatus) &&
                                                await WasLastAssignedToSelfOrIsCompanyLead(draftVersion, user.Id, ct);

            var allowedToAssign = (hasAssignOverridePermission && (assignedUserIsInCompany || hasAssignOutsideCompanyPermission)) ||
                                  isTakingBackFromReviewPending ||
                                  currentUserIsAssigned;

            if (!allowedToAssign)
            {
                ThrowError($"Unable to assign user for id {draftVersion.ResourceContentId}");
            }

            if (Constants.PublisherReviewStatuses.Contains(originalStatus) && draftVersion.AssignedUserId != user.Id)
            {
                ThrowError(
                    $"Must be assigned the in-review content in order to assign to another user for id {draftVersion.ResourceContentId}");
            }

            var newStatus = isTakingBackFromReviewPending
                ? Constants.TranslationStatuses.Contains(originalStatus)
                    ? ResourceContentStatus.TranslationCompanyReview
                    : ResourceContentStatus.AquiferizeCompanyReview
                : Constants.TranslationStatuses.Contains(originalStatus)
                    ? ResourceContentStatus.TranslationEditorReview
                    : ResourceContentStatus.AquiferizeEditorReview;

            var keepCurrentStatus = Constants.CompanyReviewStatuses.Contains(originalStatus);

            if (!keepCurrentStatus)
            {
                draftVersion.ResourceContent.Status = newStatus;
            }

            if (request.AssignedReviewerUserId is not null) {
                draftVersion.AssignedReviewerUserId = request.AssignedReviewerUserId;
            }

            if (draftVersion.AssignedUserId != request.AssignedUserId || draftVersion.ResourceContentVersionSnapshots.Count == 0)
            {
                await historyService.AddSnapshotHistoryAsync(draftVersion, draftVersion.AssignedUserId, originalStatus, ct);
            }

            if (draftVersion.AssignedUserId != request.AssignedUserId)
            {
                draftVersion.AssignedUserId = request.AssignedUserId;
                await historyService.AddAssignedUserHistoryAsync(draftVersion, request.AssignedUserId, user.Id, ct);
            }

            if (draftVersion.ResourceContent.Status != originalStatus)
            {
                await historyService.AddStatusHistoryAsync(draftVersion, draftVersion.ResourceContent.Status, user.Id, ct);
            }
        }

        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
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
}