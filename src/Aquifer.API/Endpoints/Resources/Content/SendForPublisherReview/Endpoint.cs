using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.SendForPublisherReview;

public class Endpoint(AquiferDbContext dbContext, IUserService userService, IResourceHistoryService historyService) : Endpoint<Request>
{
    public override void Configure()
    {
        Post("/resources/content/{ContentId}/send-for-publisher-review", "/resources/content/send-for-publisher-review");
        Permissions(PermissionName.SendReviewContent);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var contentIds = request.ContentId is not null ? [request.ContentId.Value] : request.ContentIds!;
        List<ResourceContentStatus> allowedStatuses =
        [
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
                draftVersion.AssignedUserId,
                draftVersion.ResourceContent.Status,
                ct);
            await historyService.AddAssignedUserHistoryAsync(draftVersion, null, user.Id, ct);

            Helpers.SanitizeTiptapContent(draftVersion);
            draftVersion.ResourceContent.Status = reviewPendingStatus;
            draftVersion.AssignedUserId = null;

            await historyService.AddStatusHistoryAsync(draftVersion, reviewPendingStatus, user.Id, ct);
        }

        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}