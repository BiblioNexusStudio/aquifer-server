using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.SendForPublisherReview.Community;

public class Endpoint(AquiferDbContext dbContext, IUserService userService, IResourceHistoryService historyService) 
    : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Post("/resources/content/{ContentId}/send-for-publisher-review-community", "/resources/content/send-for-publisher-review-community");
        Permissions(PermissionName.SendReviewCommunityContent);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        if (request.ContentId is null)
        {
            ThrowError("ContentId is required");
        }

        var user = await userService.GetUserFromJwtAsync(ct);
        var contentVersion = await dbContext.ResourceContentVersions
            .Where(x => x.ResourceContentId == request.ContentId && x.AssignedUserId == user.Id && x.IsDraft)
            .Include(x => x.ResourceContent)
            .SingleAsync(ct);

        if (contentVersion is null)
        {
            ThrowError("Content not found");
        }

        if (contentVersion.ResourceContent.Status != ResourceContentStatus.TranslationInProgress)
        {
            ThrowError("Content not in correct status");
        }

        await historyService.AddSnapshotHistoryAsync(
            contentVersion, 
            contentVersion.AssignedUserId,
            contentVersion.ResourceContent.Status,
            ct
        );

        Helpers.SanitizeTiptapContent(contentVersion);

        contentVersion.ResourceContent.Status = ResourceContentStatus.TranslationReviewPending;

        contentVersion.AssignedUserId = null;

        await historyService.AddAssignedUserHistoryAsync(
            contentVersion,
            null,
            user.Id,
            ct
        );

        await dbContext.SaveChangesAsync(ct);
        await SendOkAsync(ct);
    }
}
