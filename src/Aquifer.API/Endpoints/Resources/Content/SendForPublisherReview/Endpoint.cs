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
        var draftVersions = await GetDraftVersions(contentIds, ct);

        var user = await userService.GetUserFromJwtAsync(ct);
        var isPublisher = user.Role == UserRole.Publisher;
        foreach (var draftVersion in draftVersions)
        {
            await ValidateAssignedUser(user, request.AssignedUserId, draftVersion, ct);

            var newStatus = GetNewStatus(isPublisher, draftVersion.ResourceContent.Status);

            await historyService.AddSnapshotHistoryAsync(draftVersion,
                draftVersion.AssignedUserId,
                draftVersion.ResourceContent.Status,
                ct);

            draftVersion.ResourceContent.Status = newStatus;
            await AssignUserToContent(newStatus, request.AssignedUserId, draftVersion, user, ct);

            if (newStatus != draftVersion.ResourceContent.Status)
            {
                await historyService.AddStatusHistoryAsync(draftVersion, newStatus, user.Id, ct);
            }

            Helpers.SanitizeTiptapContent(draftVersion);
        }

        await dbContext.SaveChangesAsync(ct);

        Response.ChangedByPublisher = isPublisher;
    }

    private async Task<List<ResourceContentVersionEntity>> GetDraftVersions(List<int> contentIds, CancellationToken ct)
    {
        List<ResourceContentStatus> allowedStatuses =
        [
            ResourceContentStatus.AquiferizeCompanyReview, ResourceContentStatus.TranslationCompanyReview,
            ResourceContentStatus.AquiferizeReviewPending, ResourceContentStatus.TranslationReviewPending,
            ResourceContentStatus.AquiferizePublisherReview, ResourceContentStatus.TranslationPublisherReview
        ];

        var draftVersions = await dbContext.ResourceContentVersions
            .AsTracking()
            .Where(x => contentIds.Contains(x.ResourceContentId) && allowedStatuses.Contains(x.ResourceContent.Status) && x.IsDraft)
            .Include(x => x.ResourceContent).ToListAsync(ct);

        if (draftVersions.Count != contentIds.Count)
        {
            ThrowError("One or more resources not found or not in correct status");
        }

        return draftVersions;
    }

    private async Task AssignUserToContent(ResourceContentStatus newStatus, int? assignedUserId, ResourceContentVersionEntity draftVersion,
        UserEntity user, CancellationToken ct)
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
                await historyService.AddSnapshotHistoryAsync(draftVersion,
                    draftVersion.AssignedUserId,
                    draftVersion.ResourceContent.Status,
                    ct);
                await historyService.AddAssignedUserHistoryAsync(draftVersion, assignedUserId, user.Id, ct);
                draftVersion.AssignedUserId = assignedUserId;
            }
        }
    }

    private async Task ValidateAssignedUser(UserEntity user, int? assignedUserId, ResourceContentVersionEntity draftVersion,
        CancellationToken ct)
    {
        if (user.Id != draftVersion.AssignedUserId && Constants.CompanyReviewStatuses.Contains(draftVersion.ResourceContent.Status))
        {
            ThrowError("User must be assigned to content to send for publisher review.");
        }

        if (assignedUserId is not null)
        {
            var assignedUser = await dbContext.Users
                .AsTracking()
                .SingleOrDefaultAsync(u => u.Id == assignedUserId && u.Enabled, ct);

            if (assignedUser?.Role != UserRole.Publisher)
            {
                ThrowError("Assigned user must be a publisher.");
            }
        }
    }

    private ResourceContentStatus GetNewStatus(bool isPublisher, ResourceContentStatus currentStatus)
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
                _ => ResourceContentStatus.AquiferizeReviewPending
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