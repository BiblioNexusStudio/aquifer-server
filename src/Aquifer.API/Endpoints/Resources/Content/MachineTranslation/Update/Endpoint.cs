using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.MachineTranslation.Update;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : Endpoint<Request>
{
    public override void Configure()
    {
        Patch("/resources/content/machine-translation/{Id}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);
        var existingRcv = await dbContext
            .ResourceContentVersionMachineTranslations
            .AsTracking()
            .Join(
                dbContext.ResourceContentVersionAssignedUserHistory,
                mt => mt.ResourceContentVersionId,
                rah => rah.ResourceContentVersionId,
                (mt, rah) => new{ mt, rah })
            .FirstOrDefaultAsync(x =>
                    x.mt.Id == req.Id &&
                    (
                        (x.mt.ResourceContentVersion.AssignedUserId == user.Id && 
                         x.mt.ResourceContentVersion.ResourceContent.Status == ResourceContentStatus.TranslationEditorReview)
                     || x.rah.AssignedUserId == user.Id),
                ct);

        if (existingRcv!.mt is null)
        {
            ThrowError(x => x.Id, "No machine translation exists for user");
        }

        existingRcv.mt.UserId = user.Id;

        if (req.UserRating.HasValue)
        {
            existingRcv.mt.UserRating = req.UserRating.Value;
        }

        if (req.ImproveClarity.HasValue)
        {
            existingRcv.mt.ImproveClarity = req.ImproveClarity.Value;
        }

        if (req.ImproveConsistency.HasValue)
        {
            existingRcv.mt.ImproveConsistency = req.ImproveConsistency.Value;
        }

        if (req.ImproveTone.HasValue)
        {
            existingRcv.mt.ImproveTone = req.ImproveTone.Value;
        }

        await dbContext.SaveChangesAsync(ct);
        await SendNoContentAsync(ct);
    }
}