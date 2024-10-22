using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.NotApplicable;

public class Endpoint(AquiferDbContext dbContext, IUserService userService, IResourceHistoryService historyService) : Endpoint<Request>
{
    public override void Configure()
    {
        Post("/resources/content/{ContentId}/not-applicable");
        Permissions(PermissionName.SetStatusTranslationNotApplicable);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);
        var content = await dbContext.ResourceContentVersions
            .AsTracking()
            .Include(x => x.ResourceContent)
            .SingleOrDefaultAsync(x => x.ResourceContentId == request.ContentId, ct);

        if (content is null)
        {
            ThrowError(Helpers.NoResourceFoundForContentIdResponse);
        }

        content.AssignedUserId = null;
        content.ResourceContent.Status = ResourceContentStatus.TranslationNotApplicable;
        await historyService.AddAssignedUserHistoryAsync(content, null, user.Id, ct);
        await historyService.AddStatusHistoryAsync(content, ResourceContentStatus.TranslationNotApplicable, user.Id, ct);
        await dbContext.SaveChangesAsync(ct);
        await SendOkAsync(ct);
    }
}