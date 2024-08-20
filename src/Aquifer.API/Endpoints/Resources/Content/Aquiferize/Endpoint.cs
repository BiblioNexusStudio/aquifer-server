using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using FastEndpoints;

namespace Aquifer.API.Endpoints.Resources.Content.Aquiferize;

public class Endpoint(AquiferDbContext dbContext, IUserService userService, IResourceHistoryService historyService) : Endpoint<Request>
{
    public override void Configure()
    {
        Post("/resources/content/{ContentId}/aquiferize");
        Permissions(PermissionName.CreateContent);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        if (!await userService.ValidateNonNullUserIdAsync(request.AssignedUserId, ct))
        {
            ThrowError(Helpers.InvalidUserIdResponse);
        }

        var (mostRecentContentVersion, _, currentDraftVersion) =
            await Helpers.GetResourceContentVersions(request.ContentId,
                dbContext,
                ct);

        if (mostRecentContentVersion is null)
        {
            ThrowError(Helpers.NoResourceFoundForContentIdResponse);
        }

        if (currentDraftVersion is not null)
        {
            ThrowError(Helpers.DraftAlreadyExistsResponse);
        }

        await Helpers.CreateNewDraft(dbContext,
            request.ContentId,
            request.AssignedUserId,
            mostRecentContentVersion,
            false,
            userService,
            null,
            historyService,
            ct);

        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}