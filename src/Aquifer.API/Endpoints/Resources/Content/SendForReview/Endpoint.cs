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
        Post("/admin/resources/content/{ContentId}/send-review", "/admin/resources/content/{ContentId}/send-translation-review",
            "/resources/content/{ContentId}/send-for-review");
        Permissions(PermissionName.SendReviewContent);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var draftVersion = await dbContext.ResourceContentVersions
            .Where(x => x.ResourceContentId == request.ContentId && x.IsDraft).Include(x => x.ResourceContent)
            .SingleOrDefaultAsync(ct);

        if (draftVersion is null)
        {
            ThrowError("Resource content not found");
        }

        var inProgressStatus = Constants.TranslationStatuses.Contains(draftVersion.ResourceContent.Status)
            ? ResourceContentStatus.TranslationInProgress
            : ResourceContentStatus.AquiferizeInProgress;

        if (draftVersion.ResourceContent.Status != inProgressStatus)
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

        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}