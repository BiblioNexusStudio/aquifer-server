using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Common.Tiptap;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.SendForPublisherReview;

public class Endpoint(AquiferDbContext dbContext, IUserService userService, IResourceHistoryService historyService) : Endpoint<Request>
{
    // TODO: Remove admin and send-for-review endpoints after prod deploy
    public override void Configure()
    {
        Post("/admin/resources/content/{ContentId}/send-review",
            "/admin/resources/content/{ContentId}/send-translation-review",
            "/resources/content/{ContentId}/send-for-review",
            "/resources/content/send-for-review",
            "/resources/content/{ContentId}/send-for-publisher-review",
            "/resources/content/send-for-publisher-review");
        Permissions(PermissionName.SendReviewContent);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var contentIds = request.ContentId is not null ? [request.ContentId.Value] : request.ContentIds!;
        // TODO: Remove InProgressStatuses after prod deploy
        List<ResourceContentStatus> allowedStatuses =
        [
            ResourceContentStatus.AquiferizeInProgress,
            ResourceContentStatus.TranslationInProgress,
            ResourceContentStatus.AquiferizeManagerReview,
            ResourceContentStatus.TranslationManagerReview
        ];

        var draftVersions = await dbContext.ResourceContentVersions
            .Where(x => contentIds.Contains(x.ResourceContentId) && allowedStatuses.Contains(x.ResourceContent.Status) && x.IsDraft)
            .Include(x => x.ResourceContent).ToListAsync(ct);

        if (draftVersions.Count != contentIds.Count)
        {
            ThrowError("One or more resources not found or not in correct status");
        }

        var user = await userService.GetUserFromJwtAsync(ct);
        foreach (var draftVersion in draftVersions)
        {
            if (user.Id != draftVersion.AssignedUserId)
            {
                ThrowError("User must be assigned to content to send for publisher review.");
            }

            var reviewPendingStatus = Constants.TranslationStatuses.Contains(draftVersion.ResourceContent.Status)
                ? ResourceContentStatus.TranslationReviewPending
                : ResourceContentStatus.AquiferizeReviewPending;

            await historyService.AddSnapshotHistoryAsync(draftVersion,
                draftVersion.AssignedUserId ?? user.Id,
                draftVersion.ResourceContent.Status,
                ct);
            await historyService.AddAssignedUserHistoryAsync(draftVersion, null, user.Id, ct);

            // Remove inline comments or anything else that needs to be sanitized at this point.
            var deserializedContent = JsonUtilities.DefaultDeserialize<List<TiptapModel<TiptapRootContentFiltered>>>(draftVersion.Content);

            draftVersion.Content = JsonUtilities.DefaultSerialize(deserializedContent);
            draftVersion.ResourceContent.Status = reviewPendingStatus;
            draftVersion.AssignedUserId = null;

            await historyService.AddStatusHistoryAsync(draftVersion, reviewPendingStatus, user.Id, ct);
        }

        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}