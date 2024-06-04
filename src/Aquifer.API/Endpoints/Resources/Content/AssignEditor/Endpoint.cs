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
        Post("/admin/resources/content/{ContentId}/assign-editor",
            "/admin/resources/content/{ContentId}/assign-translator",
            "/resources/content/{ContentId}/assign-editor",
            "/resources/content/assign-editor");
        Permissions(PermissionName.AssignContent, PermissionName.AssignOverride, PermissionName.AssignOutsideCompany);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);
        var userToAssign = await dbContext.Users.SingleOrDefaultAsync(u => u.Id == request.AssignedUserId && u.Enabled, ct);
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

        var contentIds = request.ContentId is not null ? [(int)request.ContentId] : request.ContentIds!;

        var draftVersions = await dbContext.ResourceContentVersions
            .Where(rcv => contentIds.Contains(rcv.ResourceContentId) && rcv.IsDraft).Include(rcv => rcv.ResourceContent)
            .Include(rcv => rcv.AssignedUser)
            .ToListAsync(ct);

        if (draftVersions.Count != contentIds.Length)
        {
            ThrowError("One or more resources not found or not in draft status");
        }

        foreach (var draftVersion in draftVersions)
        {
            var currentUserIsAssigned = draftVersion.AssignedUserId == user.Id;
            var assignedUserIsInCompany = draftVersion.AssignedUser?.CompanyId == user.CompanyId;
            var allowedToAssign = (hasAssignOverridePermission && (assignedUserIsInCompany || hasAssignOutsideCompanyPermission)) ||
                currentUserIsAssigned;

            if (!allowedToAssign)
            {
                ThrowError($"Unable to assign user for id {draftVersion.ResourceContentId}");
            }

            var inReviewStatus = Constants.TranslationStatuses.Contains(draftVersion.ResourceContent.Status)
                ? ResourceContentStatus.TranslationInReview
                : ResourceContentStatus.AquiferizeInReview;

            if (draftVersion.ResourceContent.Status == inReviewStatus && draftVersion.AssignedUserId != user.Id)
            {
                ThrowError(
                    $"Must be assigned the in-review content in order to assign to another user for id {draftVersion.ResourceContentId}");
            }

            var inProgressStatus = Constants.TranslationStatuses.Contains(draftVersion.ResourceContent.Status)
                ? ResourceContentStatus.TranslationInProgress
                : ResourceContentStatus.AquiferizeInProgress;

            var originalStatus = draftVersion.ResourceContent.Status;
            var keepCurrentStatus = userToAssign.Role is UserRole.Manager &&
                draftVersion.ResourceContent.Status is ResourceContentStatus.AquiferizeManagerReview
                    or ResourceContentStatus.TranslationManagerReview;

            if (!keepCurrentStatus)
            {
                draftVersion.ResourceContent.Status = inProgressStatus;
            }

            if (draftVersion.AssignedUserId != request.AssignedUserId)
            {
                draftVersion.AssignedUserId = request.AssignedUserId;
                await historyService.AddAssignedUserHistoryAsync(draftVersion, request.AssignedUserId, user.Id, ct);
            }
            else
            {
                await historyService.AddSnapshotHistoryAsync(draftVersion, ct);
            }

            if (draftVersion.ResourceContent.Status != originalStatus)
            {
                await historyService.AddStatusHistoryAsync(draftVersion, draftVersion.ResourceContent.Status, user.Id, ct);
            }
        }

        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}