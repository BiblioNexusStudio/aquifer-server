using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.AdminResources.Translation;

public static class SendTranslationReviewEndpoint
{
    public const string Path = "content/{contentId:int}/send-translation-review";

    public static async Task<Results<Ok, BadRequest<string>>> Handle(int contentId,
        AquiferDbContext dbContext,
        IUserService userService,
        IAdminResourceHistoryService historyService,
        CancellationToken cancellationToken)
    {
        var translationDraft = await dbContext.ResourceContentVersions
            .Where(x => x.ResourceContentId == contentId &&
                        x.IsDraft &&
                        x.ResourceContent.Status == ResourceContentStatus.TranslationInProgress)
            .Include(x => x.ResourceContent)
            .SingleOrDefaultAsync(cancellationToken);

        if (translationDraft is null)
        {
            return TypedResults.BadRequest("Resource content draft with Translation - In Progress status not found");
        }

        var user = await userService.GetUserFromJwtAsync(cancellationToken);
        if (user.Id != translationDraft.AssignedUserId)
        {
            return TypedResults.BadRequest("Only assigned users can send to review");
        }

        await historyService.AddAssignedUserHistoryAsync(translationDraft.Id, null, user.Id);
        await historyService.AddStatusHistoryAsync(translationDraft.Id,
            ResourceContentStatus.TranslationReviewPending,
            user.Id);

        translationDraft.ResourceContent.Status = ResourceContentStatus.TranslationReviewPending;
        translationDraft.AssignedUserId = null;

        await dbContext.SaveChangesAsync(cancellationToken);
        return TypedResults.Ok();
    }
}