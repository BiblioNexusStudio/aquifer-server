using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.AdminResources.Aquiferization;

public static class AquiferizationEndpoints
{
    public static async Task<Results<Ok, BadRequest<string>>> Aquiferize(int contentId,
        [FromBody] AquiferizationRequest postBody,
        AquiferDbContext dbContext,
        IResourceHistoryService historyService,
        IUserService userService,
        CancellationToken ct)
    {
        if (!await userService.ValidateNonNullUserIdAsync(postBody.AssignedUserId, ct))
        {
            return TypedResults.BadRequest(AdminResourcesHelpers.InvalidUserIdResponse);
        }

        var (mostRecentContentVersion, _, currentDraftVersion) =
            await AdminResourcesHelpers.GetResourceContentVersions(contentId,
                dbContext,
                ct);

        if (mostRecentContentVersion is null)
        {
            return TypedResults.BadRequest(AdminResourcesHelpers.NoResourceFoundForContentIdResponse);
        }

        if (currentDraftVersion is not null)
        {
            return TypedResults.BadRequest(AdminResourcesHelpers.DraftAlreadyExistsResponse);
        }

        await CreateNewDraft(dbContext,
            contentId,
            postBody.AssignedUserId,
            mostRecentContentVersion,
            false,
            userService,
            null,
            historyService,
            ct);

        await dbContext.SaveChangesAsync(ct);
        return TypedResults.Ok();
    }

    public static async Task<Results<Ok, BadRequest<string>>> Publish(int contentId,
        [FromBody] PublishRequest postBody,
        AquiferDbContext dbContext,
        IResourceHistoryService historyService,
        IUserService userService,
        CancellationToken ct)
    {
        if (!await userService.ValidateNonNullUserIdAsync(postBody.AssignedUserId, ct))
        {
            return TypedResults.BadRequest(AdminResourcesHelpers.InvalidUserIdResponse);
        }

        var (mostRecentContentVersion, currentlyPublishedVersion, currentDraftVersion) =
            await AdminResourcesHelpers.GetResourceContentVersions(contentId,
                dbContext,
                ct);

        if (mostRecentContentVersion is null)
        {
            return TypedResults.BadRequest(AdminResourcesHelpers.NoResourceFoundForContentIdResponse);
        }

        if (postBody.CreateDraft && currentDraftVersion is not null)
        {
            return TypedResults.BadRequest(AdminResourcesHelpers.DraftAlreadyExistsResponse);
        }

        // If there is currently a published version, then unpublish so this new one can become published
        if (currentlyPublishedVersion is not null &&
            currentlyPublishedVersion.Id != mostRecentContentVersion.Id)
        {
            currentlyPublishedVersion.IsPublished = false;
        }

        mostRecentContentVersion.IsDraft = false;
        mostRecentContentVersion.IsPublished = true;
        mostRecentContentVersion.Updated = DateTime.UtcNow;

        var user = await userService.GetUserFromJwtAsync(ct);
        if (mostRecentContentVersion.AssignedUserId is not null)
        {
            mostRecentContentVersion.AssignedUserId = null;
            await historyService.AddAssignedUserHistoryAsync(mostRecentContentVersion, null, user.Id, ct);
        }

        if (postBody.CreateDraft)
        {
            // create draft of published version
            await CreateNewDraft(dbContext,
                contentId,
                postBody.AssignedUserId,
                mostRecentContentVersion,
                true,
                userService,
                user,
                historyService,
                ct);
        }
        else
        {
            var resourceContent =
                await dbContext.ResourceContents.FirstOrDefaultAsync(x => x.Id == contentId, ct) ??
                throw new ArgumentNullException();
            resourceContent.Status = ResourceContentStatus.Complete;
            resourceContent.Updated = DateTime.UtcNow;

            await historyService.AddStatusHistoryAsync(mostRecentContentVersion,
                ResourceContentStatus.Complete,
                user.Id,
                ct);
        }

        await dbContext.SaveChangesAsync(ct);
        return TypedResults.Ok();
    }

    public static async Task<Results<Ok, BadRequest<string>>> Unpublish(
        int contentId,
        AquiferDbContext dbContext,
        IResourceHistoryService historyService,
        IUserService userService,
        CancellationToken ct)
    {
        var (mostRecentResourceContentVersion, currentlyPublishedVersion, currentDraftVersion) =
            await AdminResourcesHelpers.GetResourceContentVersions(contentId,
                dbContext,
                ct);

        if (mostRecentResourceContentVersion is null)
        {
            return TypedResults.BadRequest(AdminResourcesHelpers.NoResourceFoundForContentIdResponse);
        }

        if (currentlyPublishedVersion is null)
        {
            return TypedResults.BadRequest("There is no published ResourceContentVersion with the given contentId.");
        }

        currentlyPublishedVersion.IsPublished = false;
        currentlyPublishedVersion.Updated = DateTime.UtcNow;

        if (currentDraftVersion is null)
        {
            var resourceContent =
                await dbContext.ResourceContents.FindAsync([contentId], ct) ??
                throw new ArgumentNullException();

            resourceContent.Status = ResourceContentStatus.New;
            resourceContent.Updated = DateTime.UtcNow;

            var user = await userService.GetUserFromJwtAsync(ct);
            await historyService.AddStatusHistoryAsync(currentlyPublishedVersion,
                ResourceContentStatus.New,
                user.Id,
                ct);
        }

        await dbContext.SaveChangesAsync(ct);
        return TypedResults.Ok();
    }

    private static async Task CreateNewDraft(AquiferDbContext dbContext,
        int contentId,
        int? assignedUserId,
        ResourceContentVersionEntity mostRecentResourceContentVersion,
        bool resourceContentIsUpdating,
        IUserService userService,
        UserEntity? user,
        IResourceHistoryService historyService,
        CancellationToken ct)
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
            WordCount = mostRecentResourceContentVersion.WordCount,
            ContentSize = mostRecentResourceContentVersion.ContentSize,
            AssignedUserId = assignedUserId,
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow
        };

        await dbContext.ResourceContentVersions.AddAsync(newResourceContentVersion, ct);

        user ??= await userService.GetUserFromJwtAsync(ct);
        if (assignedUserId is not null)
        {
            await historyService.AddAssignedUserHistoryAsync(newResourceContentVersion, assignedUserId, user.Id, ct);
        }
        else
        {
            await historyService.AddSnapshotHistoryAsync(newResourceContentVersion, ct);
        }

        var resourceContent =
            await dbContext.ResourceContents.FindAsync([contentId], ct) ??
            throw new ArgumentNullException();

        resourceContent.Status = ResourceContentStatus.AquiferizeInProgress;
        if (resourceContentIsUpdating)
        {
            resourceContent.Updated = DateTime.UtcNow;
        }

        await historyService.AddStatusHistoryAsync(newResourceContentVersion,
            ResourceContentStatus.AquiferizeInProgress,
            user.Id,
            ct);
    }
}