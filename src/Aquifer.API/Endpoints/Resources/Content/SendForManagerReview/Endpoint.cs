using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.SendForManagerReview;

public class Endpoint(AquiferDbContext dbContext, IUserService userService, IResourceHistoryService historyService) : Endpoint<Request>
{
    public override void Configure()
    {
        Post("/resources/content/{ContentId}/send-for-manager-review",
            "/resources/content/send-for-manager-review");
        Permissions(PermissionName.AssignContent);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var contentIds = request.ContentId is not null ? [request.ContentId.Value] : request.ContentIds!;
        List<ResourceContentStatus> allowedStatuses =
        [
            ResourceContentStatus.AquiferizeInProgress, ResourceContentStatus.TranslationInProgress
        ];

        var draftVersions = await dbContext.ResourceContentVersions
            .Where(x => contentIds.Contains(x.ResourceContentId) && allowedStatuses.Contains(x.ResourceContent.Status) && x.IsDraft)
            .Include(x => x.ResourceContent).ToListAsync(ct);

        if (draftVersions.Count != contentIds.Count)
        {
            ThrowError("One or more resources not found or not in correct status");
        }

        var user = await userService.GetUserWithCompanyUsersFromJwtAsync(ct);
        var shouldSelfAssign = user.Role is UserRole.Manager or UserRole.Publisher;
        var managerIds = user.Company.Users.Where(x => x.Role == UserRole.Manager && user.Enabled).Select(x => x.Id).ToList();
        foreach (var draftVersion in draftVersions)
        {
            if (user.Id != draftVersion.AssignedUserId)
            {
                ThrowError("User must be assigned to content to send for manager review.");
            }

            var reviewPendingStatus = draftVersion.ResourceContent.Status == ResourceContentStatus.TranslationInProgress
                ? ResourceContentStatus.TranslationManagerReview
                : ResourceContentStatus.AquiferizeManagerReview;

            draftVersion.ResourceContent.Status = reviewPendingStatus;
            await SetAssignedUserId(user, managerIds, shouldSelfAssign, draftVersion);

            await historyService.AddAssignedUserHistoryAsync(draftVersion, draftVersion.AssignedUserId, user.Id, ct);
            await historyService.AddStatusHistoryAsync(draftVersion, reviewPendingStatus, user.Id, ct);
        }

        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }

    private async Task SetAssignedUserId(UserEntity user,
        List<int> managers,
        bool shouldSelfAssign,
        ResourceContentVersionEntity draftVersion)
    {
        const string errorMessage = "Can't find manager to assign to";

        if (shouldSelfAssign)
        {
            draftVersion.AssignedUserId = user.Id;
        }
        else if (managers.Count == 1)
        {
            draftVersion.AssignedUserId = managers[0];
        }
        else if (managers.Count == 0)
        {
            ThrowError(x => x.ContentId, errorMessage);
        }
        else
        {
            var lastAssignmentHistory = await dbContext.ResourceContentVersionAssignedUserHistory
                .Where(x =>
                    x.ResourceContentVersionId == draftVersion.Id &&
                    managers.Contains(x.ChangedByUserId))
                .OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            if (lastAssignmentHistory is not null)
            {
                draftVersion.AssignedUserId = lastAssignmentHistory.ChangedByUserId;
            }
            else
            {
                ThrowError(x => x.ContentId, errorMessage);
            }
        }
    }
}