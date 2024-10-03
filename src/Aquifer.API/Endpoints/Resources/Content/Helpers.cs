using Aquifer.API.Common.Dtos;
using Aquifer.API.Services;
using Aquifer.Common.Tiptap;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content;

public static class Helpers
{
    public const string InvalidUserIdResponse = "The AssignedUserId was not valid.";

    public const string NoResourceFoundForContentIdResponse = "There is no ResourceContentVersion with the given contentId.";

    public const string DraftAlreadyExistsResponse = "This resource is already being aquiferized.";
    public const string NoDraftExistsResponse = "No draft currently exists for this resource.";
    public const string NotInReviewPendingResponse = "This resource is not in review pending status";

    public static async
        Task<(ResourceContentVersionEntity? latestVersion, ResourceContentVersionEntity? publishedVersion, ResourceContentVersionEntity?
            draftVersion)> GetResourceContentVersions(int contentId, AquiferDbContext dbContext, CancellationToken cancellationToken)
    {
        var resourceContentVersions = await dbContext.ResourceContentVersions
            .AsTracking()
            .Where(x => x.ResourceContentId == contentId)
            .Include(x => x.ResourceContent)
            .ToListAsync(cancellationToken);

        return (resourceContentVersions.MaxBy(x => x.Version), resourceContentVersions.SingleOrDefault(x => x.IsPublished),
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
            ReviewLevel = ResourceContentVersionReviewLevel.Professional,
            DisplayName = mostRecentResourceContentVersion.DisplayName,
            Content = mostRecentResourceContentVersion.Content,
            WordCount = mostRecentResourceContentVersion.WordCount,
            SourceWordCount = mostRecentResourceContentVersion.WordCount,
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

        var resourceContent = await dbContext.ResourceContents.FindAsync([contentId], ct) ?? throw new ArgumentNullException();

        resourceContent.Status = ResourceContentStatus.AquiferizeInProgress;
        if (resourceContentIsUpdating)
        {
            resourceContent.Updated = DateTime.UtcNow;
        }

        await historyService.AddStatusHistoryAsync(newResourceContentVersion, ResourceContentStatus.AquiferizeInProgress, user.Id, ct);
    }

    public static void SanitizeTiptapContent(ResourceContentVersionEntity version)
    {
        // Remove inline comments or anything else that needs to be sanitized.
        var deserializedContent = JsonUtilities.DefaultDeserialize<List<TiptapModel<TiptapRootContentFiltered>>>(version.Content);
        version.Content = JsonUtilities.DefaultSerialize(deserializedContent);
    }

    public static async Task<List<(int resourceContentVersionId, UserDto? user)>> GetLastAssignmentsAsync(
        IEnumerable<int> resourceContentVersionIds,
        AquiferDbContext dbContext,
        CancellationToken ct)
    {
        var assignmentHistory = await dbContext.ResourceContentVersionAssignedUserHistory
            .AsTracking()
            .Where(x => resourceContentVersionIds.Contains(x.ResourceContentVersionId))
            .OrderByDescending(x => x.Id)
            .Include(x => x.AssignedUser)
            .ToListAsync(ct);

        var groupedAssignmentsHistories = assignmentHistory.GroupBy(x => x.ResourceContentVersionId);

        var lastAssignments = new List<(int resourceContentVersionId, UserDto? user)>();
        foreach (var versionHistory in groupedAssignmentsHistories)
        {
            var lastAssignment = versionHistory.OrderByDescending(x => x.Id).Skip(1).Take(1).FirstOrDefault();
            if (lastAssignment != null)
            {
                lastAssignments.Add((lastAssignment.ResourceContentVersionId, UserDto.FromUserEntity(lastAssignment.AssignedUser)));
            }
        }

        return lastAssignments;
    }
}