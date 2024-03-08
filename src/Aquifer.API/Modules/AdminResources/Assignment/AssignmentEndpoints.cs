using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.AdminResources.Assignment;

public class AssignmentEndpoints
{
    public static async Task<Results<Ok, BadRequest<string>>> AssignEditor(
        int contentId,
        [FromBody] AssignEditorRequest postBody,
        AquiferDbContext dbContext,
        IUserService userService,
        IResourceHistoryService historyService,
        CancellationToken ct)
    {
        var draftVersion = await dbContext.ResourceContentVersions
            .Where(rcv => rcv.ResourceContentId == contentId && rcv.IsDraft).Include(rcv => rcv.ResourceContent)
            .Include(rcv => rcv.AssignedUser)
            .SingleOrDefaultAsync(ct);

        if (draftVersion?.ResourceContent is null)
        {
            return TypedResults.BadRequest("Resource content not found or not in draft status");
        }

        var user = await userService.GetUserFromJwtAsync(ct);
        var userToAssign = await dbContext.Users.SingleOrDefaultAsync(u => u.Id == postBody.AssignedUserId, ct);
        var hasAssignOverridePermission = userService.HasPermission(PermissionName.AssignOverride);
        var hasAssignOutsideCompanyPermission = userService.HasPermission(PermissionName.AssignOutsideCompany);

        if (userToAssign is null)
        {
            return TypedResults.BadRequest(AdminResourcesHelpers.InvalidUserIdResponse);
        }

        if (userToAssign.CompanyId != user.CompanyId && !hasAssignOutsideCompanyPermission)
        {
            return TypedResults.BadRequest("Unable to assign to a user outside your company");
        }

        var currentUserIsAssigned = draftVersion.AssignedUserId == user.Id;
        var assignedUserIsInCompany = draftVersion.AssignedUser?.CompanyId == user.CompanyId;
        var allowedToAssign = (hasAssignOverridePermission && (assignedUserIsInCompany || hasAssignOutsideCompanyPermission)) ||
                              currentUserIsAssigned;

        if (!allowedToAssign || draftVersion.AssignedUserId == postBody.AssignedUserId)
        {
            return TypedResults.BadRequest("Unable to assign user");
        }

        if (draftVersion.ResourceContent.Status == ResourceContentStatus.AquiferizeInReview &&
            draftVersion.AssignedUserId != user.Id)
        {
            return TypedResults.BadRequest("Must be assigned the in-review content in order to assign to another user");
        }

        draftVersion.AssignedUserId = postBody.AssignedUserId;
        draftVersion.Updated = DateTime.UtcNow;

        await historyService.AddAssignedUserHistoryAsync(draftVersion.Id, postBody.AssignedUserId, user.Id, ct);
        if (draftVersion.ResourceContent.Status != ResourceContentStatus.AquiferizeInProgress)
        {
            draftVersion.ResourceContent.Status = ResourceContentStatus.AquiferizeInProgress;
            await historyService.AddStatusHistoryAsync(draftVersion.Id,
                ResourceContentStatus.AquiferizeInProgress,
                user.Id,
                ct);
        }

        await dbContext.SaveChangesAsync(ct);
        return TypedResults.Ok();
    }
}