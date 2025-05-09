using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Common.Messages.Publishers;
using Aquifer.Data;
using Aquifer.Data.Services;
using FastEndpoints;

namespace Aquifer.API.Endpoints.Resources.Content.Aquiferize;

public class Endpoint(
    AquiferDbContext dbContext,
    ITranslationMessagePublisher translationMessagePublisher,
    IUserService userService,
    IResourceHistoryService historyService) : Endpoint<Request>
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
            await Helpers.GetResourceContentVersionsAsync(
                request.ContentId,
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

        await Helpers.CreateNewDraftAsync(
            dbContext,
            translationMessagePublisher,
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