using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Aquifer.Data.Services;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.SendForPublisherReview.Community;

public class Endpoint(AquiferDbContext dbContext, IUserService userService, IResourceHistoryService historyService)
    : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Post("/resources/content/{ContentId}/send-for-publisher-review-community");
        Permissions(PermissionName.SendReviewCommunityContent);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);
        var contentVersion = await dbContext.ResourceContentVersions
            .AsTracking()
            .Where(x => x.ResourceContentId == request.ContentId && x.AssignedUserId == user.Id && x.IsDraft)
            .Include(x => x.ResourceContent)
            .SingleOrDefaultAsync(ct);

        if (contentVersion is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        if (contentVersion.ResourceContent.Status != ResourceContentStatus.TranslationEditorReview)
        {
            ThrowError("Content not in correct status");
        }

        await historyService.AddStatusHistoryAsync(contentVersion, ResourceContentStatus.TranslationReviewPending, user.Id, ct);
        await historyService.AddSnapshotHistoryAsync(
            contentVersion,
            contentVersion.AssignedUserId,
            contentVersion.ResourceContent.Status,
            ct);

        contentVersion.ResourceContent.Status = ResourceContentStatus.TranslationReviewPending;
        contentVersion.AssignedUserId = null;

        await historyService.AddAssignedUserHistoryAsync(contentVersion, null, user.Id, ct);
        await dbContext.SaveChangesAsync(ct);
        await SendNoContentAsync(ct);
    }
}