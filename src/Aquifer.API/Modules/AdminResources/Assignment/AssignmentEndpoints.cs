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
        IAdminResourceHistoryService historyService,
        CancellationToken ct)
    {
        //At a high level, this endpoint should take a draft resource and assign the given user to it and set the status to AquiferizeInProgress
        // using the content id, get all the resource content versions from database

        var draftVersion = await dbContext.ResourceContentVersions
            .Where(rcv => rcv.ResourceContentId == contentId && rcv.IsDraft).Include(rcv => rcv.ResourceContent)
            .SingleOrDefaultAsync(ct);

        if (draftVersion?.ResourceContent is null)
        {
            return TypedResults.BadRequest("Resource content not found or not in draft status");
        }

        if (!await userService.ValidateNonNullUserIdAsync(postBody.AssignedUserId, ct))
        {
            return TypedResults.BadRequest("Assigned user not found");
        }

        var user = await userService.GetUserFromJwtAsync(ct);
        var hasAssignOverridePermission = userService.HasJwtClaim(PermissionName.AssignOverride);
        if ((!hasAssignOverridePermission && draftVersion.AssignedUserId != user.Id) ||
            draftVersion.AssignedUserId == postBody.AssignedUserId)
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
                user.Id, ct);
        }

        await dbContext.SaveChangesAsync(ct);
        return TypedResults.Ok();
    }
}