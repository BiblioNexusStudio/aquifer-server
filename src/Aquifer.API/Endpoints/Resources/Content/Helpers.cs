using Aquifer.API.Common.Dtos;
using Aquifer.API.Services;
using Aquifer.Common;
using Aquifer.Common.Tiptap;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Aquifer.Data.Services;
using Microsoft.EntityFrameworkCore;
using Aquifer.Common.Messages.Publishers;
using Aquifer.Common.Messages.Models;

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

    public static async Task CreateNewDraft(
        AquiferDbContext dbContext,
        ITranslationMessagePublisher translationMessagePublisher,
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

        var resourceContent = await dbContext.ResourceContents.AsTracking().SingleOrDefaultAsync(rc => rc.Id == contentId, ct) ?? throw new ArgumentNullException();

        if (assignedUserId is null || resourceContent.LanguageId == Constants.EnglishLanguageId)
        {
            await historyService.AddSnapshotHistoryAsync(newResourceContentVersion, user.Id, ResourceContentStatus.New, ct);
        }

        if (resourceContent.LanguageId == Constants.EnglishLanguageId)
        {
            resourceContent.Status = ResourceContentStatus.AquiferizeAwaitingAiDraft;
            await historyService.AddStatusHistoryAsync(newResourceContentVersion, ResourceContentStatus.AquiferizeAwaitingAiDraft, user.Id, ct);

            // a save has to happen here (even though the callers will also do their own saves)
            // to ensure data is in the right state for TranslationMessageSubscriber
            await dbContext.SaveChangesAsync(ct);

            await translationMessagePublisher.PublishTranslateResourceMessageAsync(
                    new TranslateResourceMessage(
                        contentId,
                        user.Id,
                        ShouldForceRetranslation: false,
                        TranslationOrigin.CreateAquiferization),
                    ct);
        }
        else
        {
            resourceContent.Status = ResourceContentStatus.AquiferizeEditorReview;
            if (resourceContentIsUpdating)
            {
                resourceContent.Updated = DateTime.UtcNow;
            }
            await historyService.AddStatusHistoryAsync(newResourceContentVersion, ResourceContentStatus.AquiferizeEditorReview, user.Id, ct);
        }
    }

    public static void SanitizeTiptapContent(ResourceContentVersionEntity version)
    {
        // Remove inline comments or anything else that needs to be sanitized.
        var deserializedContent = TiptapConverter.DeserializeForPublish(version.Content);
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