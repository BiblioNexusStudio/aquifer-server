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
    public static async Task<Results<Ok<string>, BadRequest<string>>> AssignEditor(
        int contentId,
        [FromBody] AssignEditorRequest postBody,
        AquiferDbContext dbContext,
        IUserService userService,
        CancellationToken cancellationToken)
    {
        //At a high level, this endpoint should take a draft resource and assign the given user to it and set the status to AquiferizeInProgress
        // using the content id, get all the resource content versions from database

        var resourceContentVersions = await dbContext.ResourceContentVersions
            .Where(rcv => rcv.ResourceContentId == contentId)
            .ToListAsync(cancellationToken);

        if (resourceContentVersions.Count == 0)
        {
            return TypedResults.BadRequest("Resource content not found");
        }

        bool isAssignedUserValid = await dbContext.Users
            .AnyAsync(u => u.Id == postBody.AssignedUserId, cancellationToken);

        if (!isAssignedUserValid)
        {
            return TypedResults.BadRequest("Assigned user not found");
        }

        var mostRecentResourceContentVersion = resourceContentVersions
            .Where(rvc => rvc.IsDraft)
            .MaxBy(rcv => rcv.Version);

        if (mostRecentResourceContentVersion is null)
        {
            return TypedResults.BadRequest("Resource content not found or not in draft status");
        }

        bool claimsPrincipalHasAssignOverridePermission = userService.HasClaim(PermissionName.AssignOverride);
        bool wasUserAssigned = false;

        var user = await userService.GetUserFromJwtAsync(cancellationToken);
        if ((claimsPrincipalHasAssignOverridePermission ||
             mostRecentResourceContentVersion.AssignedUserId == user.Id) &&
            mostRecentResourceContentVersion.AssignedUserId != postBody.AssignedUserId)
        {
            mostRecentResourceContentVersion.AssignedUserId = postBody.AssignedUserId;
            mostRecentResourceContentVersion.Updated = DateTime.UtcNow;
            wasUserAssigned = true;
        }

        if (wasUserAssigned)
        {
            var resourceContentVersionAssignedUserHistory = new ResourceContentVersionAssignedUserHistoryEntity
            {
                ResourceContentVersionId = mostRecentResourceContentVersion.Id,
                AssignedUserId = postBody.AssignedUserId,
                ChangedByUserId = user.Id,
                Created = DateTime.UtcNow
            };
            dbContext.ResourceContentVersionAssignedUserHistory.Add(resourceContentVersionAssignedUserHistory);

            var resourceContent = await dbContext.ResourceContents
                .FirstOrDefaultAsync(rc => rc.Id == contentId, cancellationToken);

            if (resourceContent is null)
            {
                return TypedResults.BadRequest("Resource contents not found");
            }

            if (resourceContent.Status != ResourceContentStatus.AquiferizeInProgress)
            {
                resourceContent.Status = ResourceContentStatus.AquiferizeInProgress;
                var resourceContentVersionStatusHistory = new ResourceContentVersionStatusHistoryEntity
                {
                    ResourceContentVersionId = mostRecentResourceContentVersion.Id,
                    Status = ResourceContentStatus.AquiferizeInProgress,
                    ChangedByUserId = user.Id,
                    Created = DateTime.UtcNow
                };
                dbContext.ResourceContentVersionStatusHistory.Add(resourceContentVersionStatusHistory);
            }

            await dbContext.SaveChangesAsync(cancellationToken);

            return TypedResults.Ok("Success");
        }

        return TypedResults.BadRequest("Unable to assign user");
    }
}