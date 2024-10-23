using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.CompleteNotApplicable.Update;

public class Endpoint(AquiferDbContext dbContext, IUserService userService, IResourceHistoryService historyService) : Endpoint<Request>
{
    public override void Configure()
    {
        Post("/resources/content/{ContentId}/complete-not-applicable");
        Permissions(PermissionName.SetStatusCompleteNotApplicable);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var content = await dbContext.ResourceContentVersions.AsTracking().Include(x => x.ResourceContent)
            .SingleOrDefaultAsync(x => x.ResourceContentId == request.ContentId && x.IsDraft, ct);

        if (content is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var user = await userService.GetUserFromJwtAsync(ct);

        content.IsDraft = false;
        content.ResourceContent.Status = ResourceContentStatus.CompleteNotApplicable;
        await historyService.AddStatusHistoryAsync(content, ResourceContentStatus.TranslationNotApplicable, user.Id, ct);
        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}