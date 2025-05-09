using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Aquifer.Data.Services;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.PullFromPublisherReview;

public class Endpoint(AquiferDbContext dbContext, IUserService userService, IResourceHistoryService historyService) : Endpoint<Request>
{
    public override void Configure()
    {
        Post(
            "/resources/content/pull-from-publisher-review",
            "/resources/content/{ContentId}/pull-from-publisher-review");
        Permissions(PermissionName.AssignContent, PermissionName.AssignOverride, PermissionName.AssignOutsideCompany);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);
        var userToAssign = await dbContext.Users
            .AsTracking()
            .SingleOrDefaultAsync(u => u.Id == request.AssignedUserId && u.Enabled, ct);
        var permissions = ResourceStatusHelpers.GetAssignmentPermissions(userService);

        await ResourceStatusHelpers.ValidateReviewerAndAssignedUserAsync<Request>(
            request.AssignedUserId,
            null,
            dbContext,
            user,
            permissions,
            ct,
            userToAssign);

        var contentIds = request.ContentId is not null ? [(int)request.ContentId] : request.ContentIds!;

        var draftVersions = await ResourceStatusHelpers.GetDraftVersionsAsync<Request>(
            contentIds,
            [
                ResourceContentStatus.AquiferizePublisherReview, ResourceContentStatus.TranslationPublisherReview,
                ResourceContentStatus.TranslationNotApplicable,
            ],
            dbContext,
            ct);

        foreach (var draftVersion in draftVersions)
        {
            var originalStatus = draftVersion.ResourceContent.Status;

            ResourceStatusHelpers.ValidateAllowedToAssign<Request>(
                permissions,
                draftVersion,
                user);

            ValidateAssignedUserForNotApplicable(userToAssign!, originalStatus);

            SetDraftVersionStatus(originalStatus, draftVersion);

            await ResourceStatusHelpers.SaveHistoryAsync(request.AssignedUserId, historyService, draftVersion, originalStatus, user, ct);
        }

        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }

    private void ValidateAssignedUserForNotApplicable(UserEntity userToAssign, ResourceContentStatus originalStatus)
    {
        if (originalStatus == ResourceContentStatus.TranslationNotApplicable &&
            userToAssign.Role is not UserRole.Manager &&
            userToAssign.Role is not UserRole.Reviewer)
        {
            ThrowError($"Can only assign a manager to resource content when pulling back from status: {originalStatus}");
        }
    }

    private static void SetDraftVersionStatus(
        ResourceContentStatus originalStatus,
        ResourceContentVersionEntity draftVersion)
    {
        if (originalStatus is ResourceContentStatus.TranslationPublisherReview or ResourceContentStatus.TranslationNotApplicable)
        {
            draftVersion.ResourceContent.Status = ResourceContentStatus.TranslationCompanyReview;
        }
        else if (originalStatus is ResourceContentStatus.AquiferizeCompanyReview)
        {
            draftVersion.ResourceContent.Status = ResourceContentStatus.AquiferizeCompanyReview;
        }
    }
}