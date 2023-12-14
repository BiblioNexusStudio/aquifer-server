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
        (var mostRecentContentVersion, string badRequestResponse) =
            await GetResourceContentVersionValidation(contentId,
                postBody.AssignedUserId,
                true,
                dbContext,
                cancellationToken);

        if (mostRecentContentVersion is null)
        {
            return TypedResults.BadRequest(badRequestResponse);
        }

        await CreateNewDraft(dbContext,
            contentId,
            postBody.AssignedUserId,
            mostRecentContentVersion,
            claimsPrincipal,
            cancellationToken);

        return TypedResults.Ok("Success");
    }

    public static async Task<Results<Ok<string>, BadRequest<string>>> Publish(int contentId,
        [FromBody] PublishRequest postBody,
        AquiferDbContext dbContext,
        ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken)
    {
        (var mostRecentResourceContentVersion, string badRequestResponse) =
            await GetResourceContentVersionValidation(contentId,
                postBody.AssignedUserId,
                postBody.CreateDraft,
                dbContext,
                cancellationToken);

        if (mostRecentResourceContentVersion is null)
        {
            return TypedResults.BadRequest(badRequestResponse);
        }

        mostRecentResourceContentVersion.IsDraft = false;
        mostRecentResourceContentVersion.IsPublished = true;
        mostRecentResourceContentVersion.Updated = DateTime.UtcNow;
        mostRecentResourceContentVersion.AssignedUserId = null;
        //dbContext.ResourceContentVersions.Update(mostRecentResourceContentVersion);
        await dbContext.SaveChangesAsync(cancellationToken);

        if (postBody.CreateDraft)
        {
            // create draft of published version
            await CreateNewDraft(dbContext,
                contentId,
                postBody.AssignedUserId,
                mostRecentResourceContentVersion,
                claimsPrincipal,
                cancellationToken);
        }
        else
        {
            string providerId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.ProviderId == providerId, cancellationToken) ??
                       throw new ArgumentNullException();

            var resourceContent =
                await dbContext.ResourceContents.FirstOrDefaultAsync(x => x.Id == contentId, cancellationToken) ??
                throw new ArgumentNullException();
            resourceContent.Status = ResourceContentStatus.Complete;

            var resourceContentVersionStatusHistory = new ResourceContentVersionStatusHistoryEntity
            {
                ResourceContentVersionId = mostRecentResourceContentVersion.Id,
                Status = ResourceContentStatus.Complete,
                ChangedByUserId = user.Id,
                Created = DateTime.UtcNow
            };
            dbContext.ResourceContentVersionStatusHistory.Add(resourceContentVersionStatusHistory);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        // return a 200
        return TypedResults.Ok("Success");
    }

    private static async Task CreateNewDraft(AquiferDbContext dbContext,
        int contentId,
        int? assignedUserId,
        ResourceContentVersionEntity mostRecentResourceContentVersion,
        ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken)
    {
        // Create a duplicate of the most recent ResourceContentVersion with the given contentId, incrementing the Version and setting IsDraft = 1.
        var newResourceContentVersion = new ResourceContentVersionEntity
        {
            ResourceContentId = mostRecentResourceContentVersion.ResourceContentId,
            Version = mostRecentResourceContentVersion.Version + 1,
            IsDraft = true,
            IsPublished = false,
            DisplayName = mostRecentResourceContentVersion.DisplayName,
            Content = mostRecentResourceContentVersion.Content,
            ContentSize = mostRecentResourceContentVersion.ContentSize,
            AssignedUserId = assignedUserId,
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow
        };

        // Save the new ResourceContentVersion to the database.
        dbContext.ResourceContentVersions.Add(newResourceContentVersion);
        // await dbContext.SaveChangesAsync(cancellationToken);

        // get requester user info
        string providerId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.ProviderId == providerId, cancellationToken) ??
                   throw new ArgumentNullException();

        if (assignedUserId is not null)
        {
            // Insert into ResourceContentVersionAssignedUserHistory with the appropriate version id, the assigned user id, and the user id who made the request
            var resourceContentVersionAssignedUserHistory = new ResourceContentVersionAssignedUserHistoryEntity
            {
                //ResourceContentVersionId = newResourceContentVersion.Id,
                ResourceContentVersion = newResourceContentVersion,
                AssignedUserId = assignedUserId,
                ChangedByUserId = user.Id,
                Created = DateTime.UtcNow
            };
            dbContext.ResourceContentVersionAssignedUserHistory.Add(resourceContentVersionAssignedUserHistory);
            //await dbContext.SaveChangesAsync(cancellationToken);
        }

        // Set the ResourceContent.Status to AquiferizeInProgress
        var resourceContent =
            await dbContext.ResourceContents.FindAsync(contentId, cancellationToken) ??
            throw new ArgumentNullException();
        resourceContent.Status = ResourceContentStatus.AquiferizeInProgress;
        //await dbContext.SaveChangesAsync(cancellationToken);

        // Insert into ResourceContentVersionStatusHistory with the new version id, status, and the user id who made the request
        var resourceContentVersionStatusHistory = new ResourceContentVersionStatusHistoryEntity
        {
            //ResourceContentVersionId = newResourceContentVersion.Id,
            ResourceContentVersion = newResourceContentVersion,
            Status = ResourceContentStatus.AquiferizeInProgress,
            ChangedByUserId = user.Id,
            Created = DateTime.UtcNow
        };
        dbContext.ResourceContentVersionStatusHistory.Add(resourceContentVersionStatusHistory);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static async Task<(ResourceContentVersionEntity?, string)> GetResourceContentVersionValidation(
        int contentId,
        int? assignedUserId,
        bool shouldCreateDraft,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var resourceContentVersions = await dbContext.ResourceContentVersions
            .Where(x => x.ResourceContentId == contentId).ToListAsync(cancellationToken);

        // First check that NO ResourceContentVersion exists with the given contentId and IsDraft = 1. If one does, return a 400.
        bool hasResourceContentDraft = shouldCreateDraft &&
                                       resourceContentVersions
                                           .Any(x => x.ResourceContentId == contentId && x.IsDraft);

        if (hasResourceContentDraft)
        {
            return (null, "This resource is already being aquiferized.");
        }

        // Check that if AssignedUserId was passed, it is a valid id in the Users table. If not, return a 400.
        if (assignedUserId is not null)
        {
            bool isAssignedUserIdValid = await dbContext.Users
                .FindAsync(assignedUserId, cancellationToken) is not null;

            if (!isAssignedUserIdValid)
            {
                return (null, "The AssignedUserId was not valid.");
            }
        }

        // Grab the most recent ResourceContentVersion with the given contentId and create a new row duplicating its information (not the timestamps), and incrementing the Version and setting IsDraft = 1.
        var mostRecentResourceContentVersion = resourceContentVersions
            .Where(x => x.ResourceContentId == contentId).MaxBy(x => x.Version);

        if (mostRecentResourceContentVersion is null)
        {
            return (null, "There is no ResourceContentVersion with the given contentId.");
        }

        return (mostRecentResourceContentVersion, string.Empty);
    }
}