using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Aquifer.API.Modules.Admin.Aquiferization;

public static class AquiferizationEndpoints
{
    public static async Task<Results<Ok<string>, BadRequest<string>>> Aquiferize(int contentId,
        [FromBody] AquiferizationRequest postBody,
        AquiferDbContext dbContext,
        ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken)
    {
        // First check that NO ResourceContentVersion exists with the given contentId and IsDraft = 1. If one does, return a 400.
        bool isResourceContentVersionNull = await dbContext.ResourceContentVersions
            .FirstOrDefaultAsync(x => x.ResourceContentId == contentId && x.IsDraft, cancellationToken) is null;

        if (!isResourceContentVersionNull)
        {
            return TypedResults.BadRequest(
                "This resource is already being aquiferized.");
        }

        // Check that if AssignedUserId was passed, it is a valid id in the Users table. If not, return a 400.
        bool isAssignedUserIdValid = await dbContext.Users
            .FindAsync(postBody.AssignedUserId, cancellationToken) is not null;

        if (!isAssignedUserIdValid)
        {
            return TypedResults.BadRequest("The AssignedUserId was not valid.");
        }

        // Grab the most recent ResourceContentVersion with the given contentId and create a new row duplicating its information (not the timestamps), and incrementing the Version and setting IsDraft = 1.
        var mostRecentResourceContentVersion = await dbContext.ResourceContentVersions
            .Where(x => x.ResourceContentId == contentId)
            .OrderByDescending(x => x.Version)
            .FirstOrDefaultAsync(cancellationToken);

        if (mostRecentResourceContentVersion is null)
        {
            return TypedResults.BadRequest("There is no ResourceContentVersion with the given contentId.");
        }

        // Create a duplicate of the most recent ResourceContentVersion with the given contentId, incrementing the Version and setting IsDraft = 1.
        var newResourceContentVersion = new ResourceContentVersionEntity
        {
            ResourceContentId = mostRecentResourceContentVersion.ResourceContentId,
            Version = ++mostRecentResourceContentVersion.Version,
            IsDraft = true,
            IsPublished = false,
            DisplayName = mostRecentResourceContentVersion.DisplayName,
            Content = mostRecentResourceContentVersion.Content,
            ContentSize = mostRecentResourceContentVersion.ContentSize,
            AssignedUserId = postBody.AssignedUserId,
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow
        };

        // Save the new ResourceContentVersion to the database.
        dbContext.ResourceContentVersions.Add(newResourceContentVersion);
        await dbContext.SaveChangesAsync(cancellationToken);

        // get requester user info
        string providerId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.ProviderId == providerId, cancellationToken);

        // get the new id of the new ResourceContentVersion
        var newlyCreatedResourceContentVersion = await dbContext.ResourceContentVersions
            .Where(x => x.Version == newResourceContentVersion.Version && x.ResourceContentId == contentId)
            .FirstOrDefaultAsync(cancellationToken);

        if (isAssignedUserIdValid)
        {
            int assignedUserId = postBody.AssignedUserId ?? 0;

            // Insert into ResourceContentVersionAssignedUserHistory with the appropriate version id, the assigned user id, and the user id who made the request
            var resourceContentVersionAssignedUserHistory = new ResourceContentVersionAssignedUserHistoryEntity
            {
                ResourceContentVersionId = newlyCreatedResourceContentVersion.Id,
                AssignedUserId = assignedUserId,
                ChangedByUserId = user.Id,
                Created = DateTime.UtcNow
            };
            dbContext.ResourceContentVersionAssignedUserHistory.Add(resourceContentVersionAssignedUserHistory);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        // Set the ResourceContent.Status to AquiferizeInProgress
        var resourceContent =
            await dbContext.ResourceContents.FirstOrDefaultAsync(x => x.Id == contentId, cancellationToken);
        resourceContent.Status = ResourceContentStatus.AquiferizeInProgress;
        await dbContext.SaveChangesAsync(cancellationToken);

        // Insert into ResourceContentVersionStatusHistory with the new version id, status, and the user id who made the request
        var resourceContentVersionStatusHistory = new ResourceContentVersionStatusHistoryEntity
        {
            ResourceContentVersionId = newlyCreatedResourceContentVersion.Id,
            Status = ResourceContentStatus.AquiferizeInProgress,
            ChangedByUserId = user.Id,
            Created = DateTime.UtcNow
        };
        dbContext.ResourceContentVersionStatusHistory.Add(resourceContentVersionStatusHistory);
        await dbContext.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok("Success");
    }
}