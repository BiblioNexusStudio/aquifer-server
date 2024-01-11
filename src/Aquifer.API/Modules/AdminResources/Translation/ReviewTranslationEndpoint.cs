using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.AdminResources.Translation;

public static class ReviewTranslationEndpoint
{
    public const string Path = "content/{contentId:int}/review-translation";

    public static async Task<Results<Ok, NotFound<string>, BadRequest<string>>> Handle(int contentId,
        AquiferDbContext dbContext,
        IAdminResourceHistoryService historyService,
        IUserService userService,
        CancellationToken cancellationToken)
    {
        var draft = await dbContext.ResourceContentVersions
            .Where(x => x.ResourceContentId == contentId &&
                        x.IsDraft &&
                        x.ResourceContent.Status == ResourceContentStatus.TranslationReviewPending)
            .Include(x => x.ResourceContent)
            .SingleOrDefaultAsync(cancellationToken);

        if (draft is null)
        {
            return TypedResults.NotFound("No translation draft pending review found");
        }

        var user = await userService.GetUserFromJwtAsync(cancellationToken);
        await historyService.AddAssignedUserHistoryAsync(draft, user.Id, user.Id);
        await historyService.AddStatusHistoryAsync(draft.Id,
            ResourceContentStatus.TranslationInReview,
            user.Id);

        draft.Updated = DateTime.UtcNow;
        draft.AssignedUserId = user.Id;
        draft.ResourceContent.Status = ResourceContentStatus.TranslationInReview;

        await dbContext.SaveChangesAsync(cancellationToken);
        return TypedResults.Ok();
    }
}