using Aquifer.API.Common;
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

    public static async Task<Results<Ok<string>, BadRequest<string>>> UnPublish(
        int contentId,
        AquiferDbContext dbContext,
        ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken)
    {
        //First check that a ResourceContentVersion exists with the given contentId and IsPublished = 1. If none does, return a 400.
        var resourceContentVersions = await dbContext.ResourceContentVersions
            .Where(x => x.ResourceContentId == contentId).ToListAsync(cancellationToken);

        if (resourceContentVersions.Count == 0)
        {
            return TypedResults.BadRequest("There are no Resources with the given contentId.");
        }

        // find the published resource Content Version using resourceContentVersions
        var publishedResourceContentVersion = resourceContentVersions.Find(rcv => rcv.IsPublished);

        if (publishedResourceContentVersion is null)
        {
            return TypedResults.BadRequest("There is no published ResourceContentVersion with the given contentId.");
        }

        // set the published ResourceContentVersion.IsPublished = 0
        publishedResourceContentVersion.IsPublished = false;
        publishedResourceContentVersion.Updated = DateTime.UtcNow;

        // If there is a draft existing for the contentId, then we're done
        var draftResourceContentVersion = resourceContentVersions.Find(rcv => rcv.IsDraft);
        if (draftResourceContentVersion is null)
        {
            // If there is not a draft existing for the contentId, then set the ResourceContent.Status to New
            var resourceContent =
                await dbContext.ResourceContents.FindAsync(contentId, cancellationToken) ??
                throw new ArgumentNullException();

            resourceContent.Status = ResourceContentStatus.New;

            // Insert into ResourceContentVersionStatusHistory with published version id, status, and the user id who made the request
            string providerId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.ProviderId == providerId, cancellationToken) ??
                       throw new ArgumentNullException();
            var resourceContentVersionStatusHistory = new ResourceContentVersionStatusHistoryEntity
            {
                ResourceContentVersionId = publishedResourceContentVersion.Id,
                Status = ResourceContentStatus.New,
                ChangedByUserId = user.Id,
                Created = DateTime.UtcNow
            };
            dbContext.ResourceContentVersionStatusHistory.Add(resourceContentVersionStatusHistory);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
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

    public static async Task<Results<Ok<string>, BadRequest<string>>> SendReview(
        AquiferDbContext dbContext,
        int contentId,
        ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken)
    {
        //At a high level, this endpoint should take a draft resource in AquiferizationInProgress and change its status to AquiferizationPendingReview

        bool statusChanged = false;

        // get all the resource content versions from database
        var resourceContentVersions = await dbContext.ResourceContentVersions
            .Where(rcv => rcv.ResourceContentId == contentId)
            .ToListAsync(cancellationToken);

        var mostRecentResourceContentVersionForReview = resourceContentVersions
            .Where(rvc => rvc.IsDraft)
            .MaxBy(rcv => rcv.Version);

        // get ResourceContent of given contentId
        var resourceContent =
            await dbContext.ResourceContents.FindAsync(contentId, cancellationToken) ??
            throw new ArgumentNullException();

        // null check and status check. If status is not AquiferizeInProgress, return a 400.
        if (mostRecentResourceContentVersionForReview is null ||
            resourceContent.Status != ResourceContentStatus.AquiferizeInProgress)
        {
            return TypedResults.BadRequest("Resource content not found or not in draft status");
        }

        // get the user from the claims principal
        string providerId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.ProviderId == providerId, cancellationToken) ??
                   throw new ArgumentNullException();

        // check to see if claimsPrincipal user has send-review:override permissions
        bool claimsPrincipalHasSendReviewOverridePermission =
            claimsPrincipal.HasClaim(c =>
                c.Type == Constants.PermissionsClaim && c.Value == PermissionName.SendReviewOverride);

        // if the user has the send-review:override permission, change the status of the resource content to AquiferizePendingReview
        if (claimsPrincipalHasSendReviewOverridePermission)
        {
            resourceContent.Status = ResourceContentStatus.AquiferizeReviewPending;
            mostRecentResourceContentVersionForReview.AssignedUserId = null;
            statusChanged = true;
        }
        else
        {
            // if the user does not have the send-review:override permission, check to see if the user is assigned to the resource
            if (user.Id == mostRecentResourceContentVersionForReview.AssignedUserId)
            {
                resourceContent.Status = ResourceContentStatus.AquiferizeReviewPending;
                mostRecentResourceContentVersionForReview.AssignedUserId = null;
                statusChanged = true;
            }
        }

        // if the status was changed, then update the resource content version status history and user history tables
        if (statusChanged)
        {
            var resourceContentVersionStatusHistory = new ResourceContentVersionStatusHistoryEntity
            {
                ResourceContentVersionId = mostRecentResourceContentVersionForReview.Id,
                Status = ResourceContentStatus.AquiferizeReviewPending,
                ChangedByUserId = user.Id,
                Created = DateTime.UtcNow
            };
            dbContext.ResourceContentVersionStatusHistory.Add(resourceContentVersionStatusHistory);

            // update user history
            var resourceContentVersionAssignedUserHistory = new ResourceContentVersionAssignedUserHistoryEntity
            {
                ResourceContentVersionId = mostRecentResourceContentVersionForReview.Id,
                AssignedUserId = null,
                ChangedByUserId = user.Id,
                Created = DateTime.UtcNow
            };
            dbContext.ResourceContentVersionAssignedUserHistory.Add(resourceContentVersionAssignedUserHistory);

            await dbContext.SaveChangesAsync(cancellationToken);
            return TypedResults.Ok("Success");
        }

        return TypedResults.BadRequest("Unable to change status of resource content");
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