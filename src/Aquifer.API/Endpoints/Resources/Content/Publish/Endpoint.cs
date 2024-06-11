using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.Publish;

public class Endpoint(AquiferDbContext dbContext, IUserService userService, IResourceHistoryService historyService) : Endpoint<Request>
{
    public override void Configure()
    {
        Post("/admin/resources/content/{ContentId}/publish", "/resources/content/{ContentId}/publish");
        Permissions(PermissionName.PublishContent);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        if (!await userService.ValidateNonNullUserIdAsync(request.AssignedUserId, ct))
        {
            ThrowError(Helpers.InvalidUserIdResponse);
        }

        var (mostRecentContentVersion, currentlyPublishedVersion, currentDraftVersion) =
            await Helpers.GetResourceContentVersions(request.ContentId, dbContext, ct);

        if (mostRecentContentVersion is null)
        {
            ThrowError(Helpers.NoResourceFoundForContentIdResponse);
        }

        if (request.CreateDraft && currentDraftVersion is not null)
        {
            ThrowError(Helpers.DraftAlreadyExistsResponse);
        }

        // If there is currently a published version, then unpublish so this new one can become published
        if (currentlyPublishedVersion is not null && currentlyPublishedVersion.Id != mostRecentContentVersion.Id)
        {
            currentlyPublishedVersion.IsPublished = false;
        }

        mostRecentContentVersion.IsDraft = false;
        mostRecentContentVersion.IsPublished = true;
        Helpers.SanitizeTiptapContent(mostRecentContentVersion);

        var user = await userService.GetUserFromJwtAsync(ct);
        if (mostRecentContentVersion.AssignedUserId is not null)
        {
            await historyService.AddSnapshotHistoryAsync(mostRecentContentVersion,
                mostRecentContentVersion.AssignedUserId,
                mostRecentContentVersion.ResourceContent.Status,
                ct);
            mostRecentContentVersion.AssignedUserId = null;
            await historyService.AddAssignedUserHistoryAsync(mostRecentContentVersion, null, user.Id, ct);
        }

        if (request.CreateDraft)
        {
            // create draft of published version
            await Helpers.CreateNewDraft(dbContext,
                request.ContentId,
                request.AssignedUserId,
                mostRecentContentVersion,
                true,
                userService,
                user,
                historyService,
                ct);
        }
        else
        {
            var resourceContent = await dbContext.ResourceContents.FirstOrDefaultAsync(x => x.Id == request.ContentId, ct) ??
                throw new ArgumentNullException();
            resourceContent.Status = ResourceContentStatus.Complete;

            await historyService.AddStatusHistoryAsync(mostRecentContentVersion, ResourceContentStatus.Complete, user.Id, ct);
        }

        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}