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
        CancellationToken cancellationToken)
    {
        //At a high level, this endpoint should take a draft resource and assign the given user to it and set the status to AquiferizeInProgress
        // using the content id, get all the resource content versions from database

        var draftVersion = await dbContext.ResourceContentVersions
            .Where(rcv => rcv.ResourceContentId == contentId && rcv.IsDraft).Include(rcv => rcv.ResourceContent)
            .SingleOrDefaultAsync(cancellationToken);

        if (draftVersion?.ResourceContent is null)
        {
            return TypedResults.BadRequest("Resource content not found or not in draft status");
        }

        if (!await userService.ValidateNonNullUserIdAsync(postBody.AssignedUserId, cancellationToken))
        {
            return TypedResults.BadRequest("Assigned user not found");
        }

        var user = await userService.GetUserFromJwtAsync(cancellationToken);
        var hasAssignOverridePermission = userService.HasJwtClaim(PermissionName.AssignOverride);
        if (hasAssignOverridePermission || draftVersion.AssignedUserId == user.Id)
        {
            return TypedResults.BadRequest("Unable to assign user");
        }

        draftVersion.AssignedUserId = postBody.AssignedUserId;
        draftVersion.Updated = DateTime.UtcNow;

        await historyService.AddAssignedUserHistoryAsync(draftVersion.Id, postBody.AssignedUserId, user.Id);
        if (draftVersion.ResourceContent.Status != ResourceContentStatus.AquiferizeInProgress)
        {
            draftVersion.ResourceContent.Status = ResourceContentStatus.AquiferizeInProgress;
            await historyService.AddStatusHistoryAsync(draftVersion.Id,
                ResourceContentStatus.AquiferizeInProgress,
                user.Id);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        return TypedResults.Ok();
    }
}