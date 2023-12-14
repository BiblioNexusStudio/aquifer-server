using Aquifer.API.Common;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Aquifer.API.Modules.Admin.Assignment;

public class AssignmentEndpoints
{
    public static async Task<Results<Ok<string>, BadRequest<string>>> AssignEditor(
        int contentId,
        [FromBody] AssignEditorRequest postBody,
        AquiferDbContext dbContext,
        ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken)
    {
        //At a high level, this endpoint should take a draft resource and assign the given user to it and set the status to AquiferizeInProgress
        // using the content id, get all the resource content versions from database

        // wasUserAssigned bool
        bool wasUserAssigned = false;

        var resourceContentVersions = await dbContext.ResourceContentVersions
            .Where(rcv => rcv.ResourceContentId == contentId)
            .ToListAsync(cancellationToken);

        // if the resource content count is 0, then return a bad request
        if (resourceContentVersions.Count == 0)
        {
            return TypedResults.BadRequest("Resource content not found");
        }

        // Check the AssignedUserId is a valid id in the Users table and that it is different than the current AssignedUserId for the version. If not, return a 400.
        bool isAssignedUserValid = await dbContext.Users
            .AnyAsync(u => u.Id == postBody.AssignedUserId, cancellationToken);

        if (!isAssignedUserValid)
        {
            return TypedResults.BadRequest("Assigned user not found");
        }

        // get the requesting user from User table using the provider id from the claims principal 
        string providerId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.ProviderId == providerId, cancellationToken) ??
                   throw new ArgumentNullException();

        if (user.Id == postBody.AssignedUserId)
        {
            return TypedResults.BadRequest("Cannot assign to self");
        }

        // get the most recent draft resource content version from the list
        var mostRecentResourceContentVersion = resourceContentVersions
            .Where(rvc => rvc.IsDraft)
            .MaxBy(rcv => rcv.Version);

        if (mostRecentResourceContentVersion is null)
        {
            return TypedResults.BadRequest("Resource content not found or not in draft status");
        }

        // check to see if claimsPrincipal user has override permissions
        bool claimsPrincipalHasAssignOverridePermission =
            claimsPrincipal.HasClaim(c =>
                c.Type == PermissionName.Permissions && c.Value == PermissionName.AssignOverride);

        // if the user has the assign override permission, then assign the resource to the given user
        if (claimsPrincipalHasAssignOverridePermission)
        {
            mostRecentResourceContentVersion.AssignedUserId = postBody.AssignedUserId;
            mostRecentResourceContentVersion.Updated = DateTime.UtcNow;
            wasUserAssigned = true;
        }
        else
        {
            // if the user is the author of the resource, then assign the resource to the given user
            if (mostRecentResourceContentVersion.AssignedUserId == user.Id)
            {
                mostRecentResourceContentVersion.AssignedUserId = postBody.AssignedUserId;
                mostRecentResourceContentVersion.Updated = DateTime.UtcNow;
                wasUserAssigned = true;
            }
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

            resourceContent.Status = ResourceContentStatus.AquiferizeInProgress;

            await dbContext.SaveChangesAsync(cancellationToken);

            return TypedResults.Ok("Success");
        }

        return TypedResults.BadRequest("Unable to assign user");
    }
}