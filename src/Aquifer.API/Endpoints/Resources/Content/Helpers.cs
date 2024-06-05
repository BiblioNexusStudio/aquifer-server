using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content;

public static class Helpers
{
    public const string InvalidUserIdResponse = "The AssignedUserId was not valid.";

    public const string NoResourceFoundForContentIdResponse =
        "There is no ResourceContentVersion with the given contentId.";

    public const string DraftAlreadyExistsResponse = "This resource is already being aquiferized.";
    public const string NoDraftExistsResponse = "No draft currently exists for this resource.";
    public const string NotInReviewPendingResponse = "This resource is not in review pending status";

    public static async
        Task<(ResourceContentVersionEntity? latestVersion, ResourceContentVersionEntity? publishedVersion,
            ResourceContentVersionEntity? draftVersion)> GetResourceContentVersions(
            int contentId,
            AquiferDbContext dbContext,
            CancellationToken cancellationToken)
    {
        var resourceContentVersions = await dbContext.ResourceContentVersions
            .Where(x => x.ResourceContentId == contentId).Include(x => x.ResourceContent).ToListAsync(cancellationToken);

        return (
            resourceContentVersions.MaxBy(x => x.Version),
            resourceContentVersions.SingleOrDefault(x => x.IsPublished),
            resourceContentVersions.SingleOrDefault(x => x.IsDraft));
    }

    public static async Task CreateNewDraft(AquiferDbContext dbContext,
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
            await historyService.AddSnapshotHistoryAsync(newResourceContentVersion, user.Id, ResourceContentStatus.New, ct);
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