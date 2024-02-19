using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using static Aquifer.API.Helpers.EndpointHelpers;

namespace Aquifer.API.Endpoints.Resources.Content.AssignReview;

public class Endpoint(AquiferDbContext dbContext, IAdminResourceHistoryService historyService, IUserService userService) : Endpoint<Request>
{
    public override void Configure()
    {
        Post("/resources/content/{ContentId}/assign-review");
        Permissions(PermissionName.ReviewContent);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var draft = await dbContext.ResourceContentVersions
            .Where(x => x.ResourceContentId == request.ContentId &&
                        x.IsDraft &&
                        ((x.ResourceContent.Language.ISO6393Code == "eng" &&
                          (x.ResourceContent.Status == ResourceContentStatus.AquiferizeReviewPending ||
                           x.ResourceContent.Status == ResourceContentStatus.AquiferizeInReview)) ||
                         (x.ResourceContent.Language.ISO6393Code != "eng" &&
                          (x.ResourceContent.Status == ResourceContentStatus.TranslationReviewPending ||
                           x.ResourceContent.Status == ResourceContentStatus.TranslationInReview))))
            .Include(x => x.ResourceContent)
            .ThenInclude(x => x.Language)
            .SingleOrDefaultAsync(ct);

        if (draft is null)
        {
            await SendStringAsync("No pending review or in review draft found", 404, cancellation: ct);
            return;
        }

        await ValidateAssignedUser(request, ct);

        var user = await userService.GetUserFromJwtAsync(ct);
        var newStatus = draft.ResourceContent.Language.ISO6393Code == "eng"
            ? ResourceContentStatus.AquiferizeInReview
            : ResourceContentStatus.TranslationInReview;

        if (newStatus != draft.ResourceContent.Status)
        {
            await historyService.AddStatusHistoryAsync(draft, newStatus, user.Id, ct);
            draft.ResourceContent.Status = newStatus;
        }

        if (request.AssignedUserId != draft.AssignedUserId)
        {
            await historyService.AddAssignedUserHistoryAsync(draft, request.AssignedUserId, user.Id, ct);
            draft.AssignedUserId = request.AssignedUserId;
        }

        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }

    private async Task ValidateAssignedUser(Request request, CancellationToken ct)
    {
        var assignedUser = await dbContext.Users.SingleOrDefaultAsync(u => u.Id == request.AssignedUserId, ct);
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