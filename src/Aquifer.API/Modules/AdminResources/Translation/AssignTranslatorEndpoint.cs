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
        CancellationToken ct)
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
            .Include(rcv => rcv.AssignedUser)
            .SingleOrDefaultAsync(ct);

        if (translationDraft?.ResourceContent is null)
        {
            return TypedResults.BadRequest("Resource content in translation status not found or not in draft status");
        }

        var user = await userService.GetUserFromJwtAsync(ct);
        var userToAssign = await dbContext.Users.SingleOrDefaultAsync(u => u.Id == request.AssignedUserId, ct);
        var hasAssignOverridePermission = userService.HasPermission(PermissionName.AssignOverride);
        var hasAssignOutsideCompanyPermission = userService.HasPermission(PermissionName.AssignOutsideCompany);

        if (userToAssign is null)
        {
            return TypedResults.BadRequest(AdminResourcesHelpers.InvalidUserIdResponse);
        }

        if (userToAssign.CompanyId != user.CompanyId && !hasAssignOutsideCompanyPermission)
        {
            return TypedResults.BadRequest("Unable to assign to a user outside your company");
        }

        var currentUserIsAssigned = translationDraft.AssignedUserId == user.Id;
        var assignedUserIsInCompany = translationDraft.AssignedUser?.CompanyId == user.CompanyId;
        var allowedToAssign = (hasAssignOverridePermission && (assignedUserIsInCompany || hasAssignOutsideCompanyPermission)) ||
                              currentUserIsAssigned;

        if (!allowedToAssign || translationDraft.AssignedUserId == request.AssignedUserId)
        {
            return TypedResults.BadRequest("Unable to assign user");
        }

        if (translationDraft.ResourceContent.Status == ResourceContentStatus.TranslationInReview &&
            translationDraft.AssignedUserId != user.Id)
        {
            return TypedResults.BadRequest("Must be assigned the in-review content in order to assign to another user");
        }

        await historyService.AddAssignedUserHistoryAsync(translationDraft.Id, request.AssignedUserId, user.Id, ct);
        if (translationDraft.ResourceContent.Status != ResourceContentStatus.TranslationInProgress)
        {
            translationDraft.ResourceContent.Status = ResourceContentStatus.TranslationInProgress;
            await historyService.AddStatusHistoryAsync(translationDraft.Id,
                ResourceContentStatus.TranslationInProgress,
                user.Id, ct);
        }

        translationDraft.AssignedUserId = request.AssignedUserId;
        translationDraft.Updated = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(ct);
        return TypedResults.Ok();
    }
}