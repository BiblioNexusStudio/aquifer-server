using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;

namespace Aquifer.API.Endpoints.Resources.Content.Unpublish;

public class Endpoint(AquiferDbContext dbContext, IUserService userService, IResourceHistoryService historyService) : Endpoint<Request>
{
    public override void Configure()
    {
        Post("/resources/content/{ContentId}/unpublish");
        Permissions(PermissionName.PublishContent);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var (mostRecentResourceContentVersion, currentlyPublishedVersion, currentDraftVersion) =
            await Helpers.GetResourceContentVersions(request.ContentId,
                dbContext,
                ct);

        if (mostRecentResourceContentVersion is null)
        {
            ThrowError(Helpers.NoResourceFoundForContentIdResponse);
        }

        if (currentlyPublishedVersion is null)
        {
            ThrowError("There is no published ResourceContentVersion with the given contentId.");
        }

        currentlyPublishedVersion.IsPublished = false;
        currentlyPublishedVersion.Updated = DateTime.UtcNow;

        if (currentDraftVersion is null)
        {
            var resourceContent =
                await dbContext.ResourceContents.FindAsync([request.ContentId], ct) ??
                throw new ArgumentNullException();

            resourceContent.Status = ResourceContentStatus.New;
            resourceContent.Updated = DateTime.UtcNow;

            var user = await userService.GetUserFromJwtAsync(ct);
            await historyService.AddStatusHistoryAsync(currentlyPublishedVersion,
                ResourceContentStatus.New,
                user.Id, ct);
        }

        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}