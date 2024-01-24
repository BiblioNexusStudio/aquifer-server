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
        IAdminResourceHistoryService historyService,
        IUserService userService,
        CancellationToken cancellationToken)
    {
        if (!await userService.ValidateNonNullUserIdAsync(postBody.AssignedUserId, cancellationToken))
        {
            return TypedResults.BadRequest(AdminResourcesHelpers.InvalidUserIdResponse);
        }

        var (mostRecentContentVersion, _, currentDraftVersion) =
            await AdminResourcesHelpers.GetResourceContentVersions(contentId,
                dbContext,
                cancellationToken);

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
            cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
        return TypedResults.Ok();
    }

    public static async Task<Results<Ok, BadRequest<string>>> Publish(int contentId,
        [FromBody] PublishRequest postBody,
        AquiferDbContext dbContext,
        IAdminResourceHistoryService historyService,
        IUserService userService,
        CancellationToken cancellationToken)
    {
        if (!await userService.ValidateNonNullUserIdAsync(postBody.AssignedUserId, cancellationToken))
        {
            return TypedResults.BadRequest(AdminResourcesHelpers.InvalidUserIdResponse);
        }

        var (mostRecentContentVersion, currentlyPublishedVersion, currentDraftVersion) =
            await AdminResourcesHelpers.GetResourceContentVersions(contentId,
                dbContext,
                cancellationToken);

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

        var user = await userService.GetUserFromJwtAsync(cancellationToken);
        if (mostRecentContentVersion.AssignedUserId is not null)
        {
            mostRecentContentVersion.AssignedUserId = null;
            await historyService.AddAssignedUserHistoryAsync(mostRecentContentVersion, null, user.Id);
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
                cancellationToken);
        }
        else
        {
            var resourceContent =
                await dbContext.ResourceContents.FirstOrDefaultAsync(x => x.Id == contentId, cancellationToken) ??
                throw new ArgumentNullException();
            resourceContent.Status = ResourceContentStatus.Complete;
            resourceContent.Updated = DateTime.UtcNow;

            await historyService.AddStatusHistoryAsync(mostRecentContentVersion.Id,
                ResourceContentStatus.Complete,
                user.Id);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        return TypedResults.Ok();
    }

    public static async Task<Results<Ok, BadRequest<string>>> Unpublish(
        int contentId,
        AquiferDbContext dbContext,
        IAdminResourceHistoryService historyService,
        IUserService userService,
        CancellationToken cancellationToken)
    {
        var (mostRecentResourceContentVersion, currentlyPublishedVersion, currentDraftVersion) =
            await AdminResourcesHelpers.GetResourceContentVersions(contentId,
                dbContext,
                cancellationToken);

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
                await dbContext.ResourceContents.FindAsync([contentId], cancellationToken) ??
                throw new ArgumentNullException();

            resourceContent.Status = ResourceContentStatus.New;
            resourceContent.Updated = DateTime.UtcNow;

            var user = await userService.GetUserFromJwtAsync(cancellationToken);
            await historyService.AddStatusHistoryAsync(currentlyPublishedVersion.Id,
                ResourceContentStatus.New,
                user.Id);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        return TypedResults.Ok();
    }

    private static async Task CreateNewDraft(AquiferDbContext dbContext,
        int contentId,
        int? assignedUserId,
        ResourceContentVersionEntity mostRecentResourceContentVersion,
        bool resourceContentIsUpdating,
        IUserService userService,
        UserEntity? user,
        IAdminResourceHistoryService historyService,
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
            WordCount = mostRecentResourceContentVersion.WordCount,
            ContentSize = mostRecentResourceContentVersion.ContentSize,
            AssignedUserId = assignedUserId,
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow
        };

        dbContext.ResourceContentVersions.Add(newResourceContentVersion);

        user ??= await userService.GetUserFromJwtAsync(cancellationToken);
        if (assignedUserId is not null)
        {
            await historyService.AddAssignedUserHistoryAsync(newResourceContentVersion, assignedUserId, user.Id);
        }

        var resourceContent =
            await dbContext.ResourceContents.FindAsync([contentId], cancellationToken) ??
            throw new ArgumentNullException();

        resourceContent.Status = ResourceContentStatus.AquiferizeInProgress;
        if (resourceContentIsUpdating)
        {
            resourceContent.Updated = DateTime.UtcNow;
        }

        await historyService.AddStatusHistoryAsync(newResourceContentVersion,
            ResourceContentStatus.AquiferizeInProgress,
            user.Id);
    }
}