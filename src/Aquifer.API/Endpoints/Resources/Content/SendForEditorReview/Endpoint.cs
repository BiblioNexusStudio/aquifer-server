using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Aquifer.Data.Services;
using FastEndpoints;

namespace Aquifer.API.Endpoints.Resources.Content.SendForEditorReview;

public class Endpoint(AquiferDbContext dbContext, IUserService userService, IResourceHistoryService historyService) : Endpoint<Request>
{
    public override void Configure()
    {
        Post("/resources/content/send-for-editor-review", "/resources/content/{ContentId}/send-for-editor-review");
        Permissions(PermissionName.AssignContent, PermissionName.AssignOverride, PermissionName.AssignOutsideCompany);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);
        var permissions = ResourceStatusHelpers.GetAssignmentPermissions(userService);

        await ResourceStatusHelpers.ValidateReviewerAndAssignedUser<Request>(request.AssignedUserId, request.AssignedReviewerUserId,
            dbContext, user, permissions, ct);

        var contentIds = request.ContentId is not null ? [(int)request.ContentId] : request.ContentIds!;

        var draftVersions = await ResourceStatusHelpers.GetDraftVersions<Request>(contentIds,
        [
            ResourceContentStatus.TranslationEditorReview, ResourceContentStatus.TranslationAiDraftComplete,
            ResourceContentStatus.New, ResourceContentStatus.AquiferizeAiDraftComplete,
            ResourceContentStatus.AquiferizeEditorReview, ResourceContentStatus.AquiferizeCompanyReview,
            ResourceContentStatus.TranslationCompanyReview
        ], dbContext, ct);

        foreach (var draftVersion in draftVersions)
        {
            var originalStatus = draftVersion.ResourceContent.Status;

            ResourceStatusHelpers.ValidateAllowedToAssign<Request>(permissions,
                draftVersion, user);

            SetDraftVersionStatus(originalStatus, draftVersion);

            ResourceStatusHelpers.SetAssignedReviewerUserId(request.AssignedReviewerUserId, draftVersion);

            await ResourceStatusHelpers.SaveHistory(request.AssignedUserId, historyService, draftVersion, originalStatus, user, ct);
        }

        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }

    private static void SetDraftVersionStatus(
        ResourceContentStatus originalStatus,
        ResourceContentVersionEntity draftVersion)
    {
        if (originalStatus == ResourceContentStatus.TranslationAiDraftComplete)
        {
            draftVersion.ResourceContent.Status = ResourceContentStatus.TranslationEditorReview;
        }
        else if (originalStatus is ResourceContentStatus.New or ResourceContentStatus.AquiferizeAiDraftComplete)
        {
            draftVersion.ResourceContent.Status = ResourceContentStatus.AquiferizeEditorReview;
        }
    }
}