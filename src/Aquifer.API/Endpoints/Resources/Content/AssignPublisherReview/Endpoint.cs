using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using static Aquifer.API.Helpers.EndpointHelpers;

namespace Aquifer.API.Endpoints.Resources.Content.AssignPublisherReview;

public class Endpoint(AquiferDbContext dbContext, IResourceHistoryService historyService, IUserService userService) : Endpoint<Request>
{
    public override void Configure()
    {
        Post("/resources/content/{ContentId}/assign-review",
            "/resources/content/assign-review",
            "/resources/content/{ContentId}/assign-publisher-review",
            "/resources/content/assign-publisher-review");
        Permissions(PermissionName.ReviewContent);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        await ValidateAssignedUser(request, ct);
        var user = await userService.GetUserFromJwtAsync(ct);

        var contentIds = request.ContentId is not null ? [(int)request.ContentId] : request.ContentIds!;

        var draftVersions = await dbContext.ResourceContentVersions
            .Where(x => contentIds.Contains(x.ResourceContentId) &&
                x.IsDraft &&
                (x.ResourceContent.Status == ResourceContentStatus.AquiferizeReviewPending ||
                    x.ResourceContent.Status == ResourceContentStatus.AquiferizePublisherReview ||
                    x.ResourceContent.Status == ResourceContentStatus.TranslationReviewPending ||
                    x.ResourceContent.Status == ResourceContentStatus.TranslationPublisherReview))
            .Include(x => x.ResourceContent)
            .ThenInclude(x => x.Language)
            .ToListAsync(ct);

        if (draftVersions.Count != contentIds.Length)
        {
            ThrowError("One or more resources not found or not in draft status");
        }

        foreach (var draftVersion in draftVersions)
        {
            var newStatus = draftVersion.ResourceContent.Language.ISO6393Code == "eng"
                ? ResourceContentStatus.AquiferizePublisherReview
                : ResourceContentStatus.TranslationPublisherReview;

            if (request.AssignedUserId != draftVersion.AssignedUserId)
            {
                await historyService.AddSnapshotHistoryAsync(draftVersion,
                    draftVersion.AssignedUserId ?? user.Id,
                    draftVersion.ResourceContent.Status,
                    ct);
                await historyService.AddAssignedUserHistoryAsync(draftVersion, request.AssignedUserId, user.Id, ct);
                draftVersion.AssignedUserId = request.AssignedUserId;
            }

            if (newStatus != draftVersion.ResourceContent.Status)
            {
                await historyService.AddStatusHistoryAsync(draftVersion, newStatus, user.Id, ct);
                draftVersion.ResourceContent.Status = newStatus;
            }
        }

        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }

    private async Task ValidateAssignedUser(Request request, CancellationToken ct)
    {
        var assignedUser = await dbContext.Users.SingleOrDefaultAsync(u => u.Id == request.AssignedUserId && u.Enabled, ct);
        if (assignedUser is null)
        {
            ThrowEntityNotFoundError<Request>(r => r.AssignedUserId);
        }

        if (assignedUser.Role != UserRole.Publisher)
        {
            ThrowError("Assigned user must be a publisher.");
        }
    }
}