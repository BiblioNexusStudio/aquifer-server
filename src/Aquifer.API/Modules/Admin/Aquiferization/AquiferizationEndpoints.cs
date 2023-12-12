using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Admin.Aquiferization;

public static class AquiferizationEndpoints
{
    public static async Task<Results<Ok<string>, BadRequest<string>>> Aquiferize(int contentId,
        [FromBody] AquiferizationRequest postBody, AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        // First check that NO ResourceContentVersion exists with the given contentId and IsDraft = 1. If one does, return a 400.
        bool isResourceContentVersionNull = await dbContext.ResourceContentVersions
            .FirstOrDefaultAsync(x => x.ResourceContentId == contentId && x.IsDraft, cancellationToken) is null;

        if (!isResourceContentVersionNull)
        {
            return TypedResults.BadRequest(
                "There is already a ResourceContentVersion with the given contentId and IsDraft = 1.");
        }

        // Check that if AssignedUserId was passed, it is a valid id in the Users table. If not, return a 400.
        bool isAssignedUserIdValid = await dbContext.Users
            .FirstOrDefaultAsync(x => x.Id == postBody.AssignedUserId, cancellationToken) is not null;

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

        return TypedResults.Ok("Success");
    }
}