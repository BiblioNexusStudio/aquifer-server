using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Aquifer.Data.Services;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content;

public static class ResourceStatusHelpers
{

    public static async Task SaveHistory(int assignedUserId, IResourceHistoryService historyService,
        ResourceContentVersionEntity draftVersion, ResourceContentStatus originalStatus,
        UserEntity user, CancellationToken ct)
    {
        if (draftVersion.AssignedUserId != assignedUserId || draftVersion.ResourceContentVersionSnapshots.Count == 0)
        {
            await historyService.AddSnapshotHistoryAsync(draftVersion, draftVersion.AssignedUserId, originalStatus, ct);
        }

        if (draftVersion.AssignedUserId != assignedUserId)
        {
            draftVersion.AssignedUserId = assignedUserId;
            await historyService.AddAssignedUserHistoryAsync(draftVersion, assignedUserId, user.Id, ct);
        }

        if (draftVersion.ResourceContent.Status != originalStatus)
        {
            await historyService.AddStatusHistoryAsync(draftVersion, draftVersion.ResourceContent.Status, user.Id, ct);
        }
    }

    public static void SetAssignedReviewerUserId(int? assignedReviewerUserId, ResourceContentVersionEntity draftVersion)
    {
        if (assignedReviewerUserId is not null)
        {
            draftVersion.AssignedReviewerUserId = assignedReviewerUserId;
        }
    }

    public static void ValidateAllowedToAssign<TRequest>(AssignmentPermissions permissions,
        ResourceContentVersionEntity draftVersion,
        UserEntity user)
    {
        var currentUserIsAssigned = draftVersion.AssignedUserId == user.Id;
        var assignedUserIsInCompany = draftVersion.AssignedUser?.CompanyId == user.CompanyId;

        var allowedToAssign = (permissions.HasAssignOverridePermission &&
                              (assignedUserIsInCompany || permissions.HasAssignOutsideCompanyPermission)) ||
                              currentUserIsAssigned;

        if (!allowedToAssign)
        {
            ValidationContext<TRequest>.Instance.ThrowError($"Unable to assign user for id {draftVersion.ResourceContentId}");
        }
    }

    public static async Task<List<ResourceContentVersionEntity>> GetDraftVersions<TRequest>(int[] contentIds,
        List<ResourceContentStatus> allowedStatuses, AquiferDbContext dbContext,
        CancellationToken ct)
    {
        var draftVersions = await dbContext.ResourceContentVersions
            .AsTracking()
            .Where(rcv => contentIds.Contains(rcv.ResourceContentId) && allowedStatuses.Contains(rcv.ResourceContent.Status) && rcv.IsDraft)
            .Include(rcv => rcv.ResourceContent)
            .Include(rcv => rcv.AssignedUser).Include(rcv => rcv.ResourceContentVersionSnapshots)
            .ToListAsync(ct);

        if (draftVersions.Count != contentIds.Length)
        {
            ValidationContext<TRequest>.Instance.ThrowError("One or more resources not found or not in draft status");
        }

        return draftVersions;
    }

    public static async Task ValidateReviewerAndAssignedUser<TRequest>(int assignedUserId, int? assignedReviewerUserId,
        AquiferDbContext dbContext, UserEntity user, AssignmentPermissions permissions,
        CancellationToken ct,
        UserEntity? userToAssign = null)
    {
        var assignedUser = userToAssign ?? await dbContext.Users
            .AsTracking()
            .SingleOrDefaultAsync(u => u.Id == assignedUserId && u.Enabled, ct);
        if (assignedUser is null)
        {
            ValidationContext<TRequest>.Instance.ThrowError(Helpers.InvalidUserIdResponse);
        }

        if (assignedUser.CompanyId != user.CompanyId && !permissions.HasAssignOutsideCompanyPermission)
        {
            ValidationContext<TRequest>.Instance.ThrowError("Unable to assign to a user outside your company");
        }

        if (assignedReviewerUserId is not null)
        {
            var reviewer = await dbContext.Users
                .Where(u => u.Id == assignedReviewerUserId
                            && u.Enabled
                            && u.CompanyId == assignedUser.CompanyId
                            && (u.Role == UserRole.Reviewer || u.Role == UserRole.Manager))
                .SingleOrDefaultAsync(ct);

            if (reviewer is null)
            {
                ValidationContext<TRequest>.Instance.ThrowError("Unable to assign that reviewer");
            }
        }
    }

    public static AssignmentPermissions GetAssignmentPermissions(IUserService userService)
    {
        var hasSendReviewContentPermission = userService.HasPermission(PermissionName.SendReviewContent);
        var hasAssignOverridePermission = userService.HasPermission(PermissionName.AssignOverride);
        var hasAssignOutsideCompanyPermission = userService.HasPermission(PermissionName.AssignOutsideCompany);
        return new AssignmentPermissions
        (
            hasSendReviewContentPermission,
            hasAssignOutsideCompanyPermission,
            hasAssignOverridePermission
        );
    }

    public record AssignmentPermissions(
        bool HasSendReviewContentPermission,
        bool HasAssignOutsideCompanyPermission,
        bool HasAssignOverridePermission
    );
}