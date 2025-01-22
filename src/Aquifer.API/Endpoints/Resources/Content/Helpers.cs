using Aquifer.API.Services;
using Aquifer.Common;
using Aquifer.Common.Messages.Models;
using Aquifer.Common.Messages.Publishers;
using Aquifer.Common.Tiptap;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Aquifer.Data.Services;
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
            draftVersion)> GetResourceContentVersionsAsync(int contentId, AquiferDbContext dbContext, CancellationToken cancellationToken)
    {
        var resourceContentVersions = await dbContext.ResourceContentVersions
            .AsTracking()
            .Where(x => x.ResourceContentId == contentId)
            .Include(x => x.ResourceContent)
            .ToListAsync(cancellationToken);

        return (resourceContentVersions.MaxBy(x => x.Version), resourceContentVersions.SingleOrDefault(x => x.IsPublished),
            resourceContentVersions.SingleOrDefault(x => x.IsDraft));
    }

    public static async Task CreateNewDraftAsync(
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

        // new English resources go through the AI Aquiferization (English text simplification) process
        if (resourceContent is { LanguageId: Constants.EnglishLanguageId, Status: ResourceContentStatus.New })
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

    public static async Task<Dictionary<int, List<(int? UserId, DateTime Created)>>>
        GetLastUserAssignmentsByResourceContentVersionIdMapAsync(
            IEnumerable<int> resourceContentVersionIds,
            int numberOfAssignments,
            AquiferDbContext dbContext,
            CancellationToken ct)
    {
        var assignmentHistory = await dbContext.ResourceContentVersionAssignedUserHistory
            .AsTracking()
            .Where(x => resourceContentVersionIds.Contains(x.ResourceContentVersionId))
            .OrderByDescending(x => x.Id)
            .ToListAsync(ct);

        return assignmentHistory
            .GroupBy(x => x.ResourceContentVersionId)
            .ToDictionary(
                grp => grp.Key,
                grp => grp
                    .OrderByDescending(x => x.Id)
                    .Take(numberOfAssignments)
                    .Select(rcvauh => (rcvauh.AssignedUserId, rcvauh.Created))
                    .ToList());
    }
}