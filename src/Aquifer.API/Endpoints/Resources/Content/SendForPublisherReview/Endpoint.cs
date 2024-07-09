using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.SendForPublisherReview;

public class Endpoint(AquiferDbContext dbContext, IUserService userService, IResourceHistoryService historyService)
    : Endpoint<Request, Response>
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
            ResourceContentStatus.AquiferizeManagerReview, ResourceContentStatus.TranslationManagerReview
        ];

        var draftVersions = await dbContext.ResourceContentVersions
            .Where(x => contentIds.Contains(x.ResourceContentId) && allowedStatuses.Contains(x.ResourceContent.Status) && x.IsDraft)
            .Include(x => x.ResourceContent).ToListAsync(ct);

        if (draftVersions.Count != contentIds.Count)
        {
            ThrowError("One or more resources not found or not in correct status");
        }

        var user = await userService.GetUserFromJwtAsync(ct);
        var isPublisher = user.Role == UserRole.Publisher;
        foreach (var draftVersion in draftVersions)
        {
            if (user.Id != draftVersion.AssignedUserId)
            {
                ThrowError("User must be assigned to content to send for publisher review.");
            }

            var newStatus = GetNewStatus(isPublisher, draftVersion.ResourceContent.Status);

            await historyService.AddSnapshotHistoryAsync(draftVersion,
                draftVersion.AssignedUserId,
                draftVersion.ResourceContent.Status,
                ct);

            Helpers.SanitizeTiptapContent(draftVersion);
            draftVersion.ResourceContent.Status = newStatus;
            if (!isPublisher)
            {
                await historyService.AddAssignedUserHistoryAsync(draftVersion, null, user.Id, ct);
                draftVersion.AssignedUserId = null;
            }

            await historyService.AddStatusHistoryAsync(draftVersion, newStatus, user.Id, ct);
        }

        await dbContext.SaveChangesAsync(ct);

        Response.ChangedByPublisher = isPublisher;
    }

    private static ResourceContentStatus GetNewStatus(bool isPublisher, ResourceContentStatus currentStatus)
    {
        var isCurrentStatusTranslation = Constants.TranslationStatuses.Contains(currentStatus);
        return isPublisher switch
        {
            true when isCurrentStatusTranslation => ResourceContentStatus.TranslationPublisherReview,
            true when !isCurrentStatusTranslation => ResourceContentStatus.AquiferizePublisherReview,
            false when isCurrentStatusTranslation => ResourceContentStatus.TranslationReviewPending,
            _ => ResourceContentStatus.AquiferizeReviewPending
        };
    }
}