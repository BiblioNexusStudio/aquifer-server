using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Common;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Aquifer.Data.Services;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using static Aquifer.API.Helpers.EndpointHelpers;

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
        var draftVersions = await GetDraftVersionsAsync(contentIds, ct);

        var user = await userService.GetUserFromJwtAsync(ct);
        var isPublisher = user.Role == UserRole.Publisher;
        foreach (var draftVersion in draftVersions)
        {
            await ValidateAssignedUserAsync(user, request.AssignedUserId, isPublisher, draftVersion, ct);

            var newStatus = GetNewStatus(isPublisher, draftVersion.ResourceContent.Status);

            await historyService.AddSnapshotHistoryAsync(
                draftVersion,
                draftVersion.AssignedUserId,
                draftVersion.ResourceContent.Status,
                ct);

            if (newStatus != draftVersion.ResourceContent.Status)
            {
                await historyService.AddStatusHistoryAsync(draftVersion, newStatus, user.Id, ct);
            }

            draftVersion.ResourceContent.Status = newStatus;
            await AssignUserToContentAsync(newStatus, request.AssignedUserId, draftVersion, user, ct);

            Helpers.SanitizeTiptapContent(draftVersion);
        }

        await dbContext.SaveChangesAsync(ct);

        Response.ChangedByPublisher = isPublisher;
    }

    private async Task<List<ResourceContentVersionEntity>> GetDraftVersionsAsync(List<int> contentIds, CancellationToken ct)
    {
        List<ResourceContentStatus> allowedStatuses =
        [
            ResourceContentStatus.AquiferizeCompanyReview, ResourceContentStatus.TranslationCompanyReview,
            ResourceContentStatus.AquiferizeReviewPending, ResourceContentStatus.TranslationReviewPending,
            ResourceContentStatus.AquiferizePublisherReview, ResourceContentStatus.TranslationPublisherReview,
        ];

        var draftVersions = await dbContext.ResourceContentVersions
            .AsTracking()
            .Where(x => contentIds.Contains(x.ResourceContentId) && allowedStatuses.Contains(x.ResourceContent.Status) && x.IsDraft)
            .Include(x => x.ResourceContent)
            .ToListAsync(ct);

        if (draftVersions.Count != contentIds.Count)
        {
            ThrowError("One or more resources not found or not in correct status");
        }

        return draftVersions;
    }

    private async Task AssignUserToContentAsync(
        ResourceContentStatus newStatus,
        int? assignedUserId,
        ResourceContentVersionEntity draftVersion,
        UserEntity user,
        CancellationToken ct)
    {
        // there shouldn't be an assigned user if it's going into review pending
        if (Constants.ReviewPendingStatuses.Contains(newStatus))
        {
            await historyService.AddAssignedUserHistoryAsync(draftVersion, null, user.Id, ct);
            draftVersion.AssignedUserId = null;
        }
        else
        {
            if (assignedUserId is not null && assignedUserId != draftVersion.AssignedUserId)
            {
                await historyService.AddSnapshotHistoryAsync(
                    draftVersion,
                    draftVersion.AssignedUserId,
                    draftVersion.ResourceContent.Status,
                    ct);
                await historyService.AddAssignedUserHistoryAsync(draftVersion, assignedUserId, user.Id, ct);
                draftVersion.AssignedUserId = assignedUserId;
            }
        }
    }

    private async Task ValidateAssignedUserAsync(
        UserEntity user,
        int? assignedUserId,
        bool isPublisher,
        ResourceContentVersionEntity draftVersion,
        CancellationToken ct)
    {
        if (user.Id != draftVersion.AssignedUserId && !isPublisher)
        {
            ThrowError("Non-publishers must be assigned to content to call send-for-publisher-review.");
        }

        if (assignedUserId is not null)
        {
            var assignedUser = await dbContext.Users
                .AsTracking()
                .SingleOrDefaultAsync(u => u.Id == assignedUserId && u.Enabled && u.Role == UserRole.Publisher, ct);

            if (assignedUser is null)
            {
                ThrowEntityNotFoundError<Request>(r => r.AssignedUserId);
            }
        }
    }

    private static ResourceContentStatus GetNewStatus(bool isPublisher, ResourceContentStatus currentStatus)
    {
        var isCurrentStatusTranslation = Constants.TranslationStatuses.Contains(currentStatus);

        // CompanyReview -> ReviewPending, (if the user is the publisher skip that and go straight to Review)
        if (Constants.CompanyReviewStatuses.Contains(currentStatus))
        {
            return isPublisher switch
            {
                true when isCurrentStatusTranslation => ResourceContentStatus.TranslationPublisherReview,
                true when !isCurrentStatusTranslation => ResourceContentStatus.AquiferizePublisherReview,
                false when isCurrentStatusTranslation => ResourceContentStatus.TranslationReviewPending,
                _ => ResourceContentStatus.AquiferizeReviewPending,
            };
        }

        // ReviewPending -> PublisherReview
        if (Constants.ReviewPendingStatuses.Contains(currentStatus))
        {
            return currentStatus == ResourceContentStatus.TranslationReviewPending
                ? ResourceContentStatus.TranslationPublisherReview
                : ResourceContentStatus.AquiferizePublisherReview;
        }

        // PublisherReview -> status is same, only assignment changes
        return currentStatus == ResourceContentStatus.TranslationPublisherReview
            ? ResourceContentStatus.TranslationPublisherReview
            : ResourceContentStatus.AquiferizePublisherReview;
    }
}