using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.AdminResources.Translation;

public static class AssignTranslatorEndpoint
{
    public const string Path = "content/{contentId:int}/assign-translator";

    public static async Task<Results<Ok, BadRequest<string>>> Handle(int contentId,
        [FromBody] AssignTranslatorRequest request,
        AquiferDbContext dbContext,
        IAdminResourceHistoryService historyService,
        IUserService userService,
        CancellationToken cancellationToken)
    {
        List<ResourceContentStatus> allowedStatuses =
        [
            ResourceContentStatus.TranslationInProgress,
            ResourceContentStatus.TranslationInReview,
            ResourceContentStatus.TranslationNotStarted,
            ResourceContentStatus.TranslationReviewPending
        ];
        var translationDraft = await dbContext.ResourceContentVersions
            .Where(rcv =>
                rcv.ResourceContentId == contentId &&
                rcv.IsDraft &&
                allowedStatuses.Contains(rcv.ResourceContent.Status)).Include(rcv => rcv.ResourceContent)
            .SingleOrDefaultAsync(cancellationToken);

        if (translationDraft?.ResourceContent is null)
        {
            return TypedResults.BadRequest("Resource content in translation status not found or not in draft status");
        }

        if (!await userService.ValidateNonNullUserIdAsync(request.AssignedUserId, cancellationToken))
        {
            return TypedResults.BadRequest(AdminResourcesHelpers.InvalidUserIdResponse);
        }

        var user = await userService.GetUserFromJwtAsync(cancellationToken);
        bool hasAssignOverridePermission = userService.HasJwtClaim(PermissionName.AssignOverride);
        if ((!hasAssignOverridePermission && translationDraft.AssignedUserId != user.Id) ||
            translationDraft.AssignedUserId == request.AssignedUserId)
        {
            return TypedResults.BadRequest("Unable to assign user");
        }

        await historyService.AddAssignedUserHistoryAsync(translationDraft.Id, request.AssignedUserId, user.Id);
        if (translationDraft.ResourceContent.Status != ResourceContentStatus.TranslationInProgress)
        {
            translationDraft.ResourceContent.Status = ResourceContentStatus.TranslationInProgress;
            await historyService.AddStatusHistoryAsync(translationDraft.Id,
                ResourceContentStatus.TranslationInProgress,
                user.Id);
        }

        translationDraft.AssignedUserId = request.AssignedUserId;
        translationDraft.Updated = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);
        return TypedResults.Ok();
    }
}