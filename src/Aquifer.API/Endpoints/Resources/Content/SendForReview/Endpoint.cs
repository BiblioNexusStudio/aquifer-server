using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.SendForReview;

public class Endpoint(AquiferDbContext dbContext, IUserService userService, IResourceHistoryService historyService) : Endpoint<Request>
{
    public override void Configure()
    {
        Post("/admin/resources/content/{ContentId}/send-review",
            "/admin/resources/content/{ContentId}/send-translation-review",
            "/resources/content/{ContentId}/send-for-review",
            "/resources/content/send-for-review");
        Permissions(PermissionName.SendReviewContent);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var contentIds = request.ContentId is not null ? [request.ContentId.Value] : request.ContentIds!;
        var draftVersions = await dbContext.ResourceContentVersions
            .Where(x => contentIds.Contains(x.ResourceContentId) && x.IsDraft).Include(x => x.ResourceContent).ToListAsync(ct);

        if (draftVersions.Count != contentIds.Count)
        {
            ThrowError("Resource content not found");
        }

        foreach (var draftVersion in draftVersions)
        {
            var inProgressStatus = Constants.TranslationStatuses.Contains(draftVersion.ResourceContent.Status)
                ? ResourceContentStatus.TranslationInProgress
                : ResourceContentStatus.AquiferizeInProgress;

            if (draftVersion.ResourceContent.Status is not ResourceContentStatus.AquiferizeInProgress
                and not ResourceContentStatus.TranslationInProgress
                and not ResourceContentStatus.AquiferizeManagerReview
                and not ResourceContentStatus.TranslationManagerReview)
            {
                ThrowError("Resource content not in progress");
            }

            var user = await userService.GetUserFromJwtAsync(ct);
            if (user.Id != draftVersion.AssignedUserId)
            {
                ThrowError("Unable to change status of resource content");
            }

            var reviewPendingStatus = Constants.TranslationStatuses.Contains(draftVersion.ResourceContent.Status)
                ? ResourceContentStatus.TranslationReviewPending
                : ResourceContentStatus.AquiferizeReviewPending;

            await historyService.AddAssignedUserHistoryAsync(draftVersion, null, user.Id, ct);

            draftVersion.ResourceContent.Status = reviewPendingStatus;
            draftVersion.AssignedUserId = null;

            await historyService.AddStatusHistoryAsync(draftVersion, reviewPendingStatus, user.Id, ct);
        }

        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}