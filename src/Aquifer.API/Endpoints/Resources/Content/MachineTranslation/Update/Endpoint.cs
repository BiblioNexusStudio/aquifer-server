﻿using Aquifer.API.Services;
using Aquifer.Data;
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
        var existingMt = await dbContext.ResourceContentVersionMachineTranslations
            .AsTracking()
            .FirstOrDefaultAsync(
                x =>
                    x.Id == req.Id &&
                    x.ResourceContentVersion
                        .ResourceContentVersionAssignedUserHistories
                        .Any(h => h.AssignedUserId == user.Id),
                ct);

        if (existingMt is null)
        {
            ThrowError(x => x.Id, "No machine translation exists for user");
        }

        existingMt.UserId = user.Id;

        if (req.UserRating.HasValue)
        {
            existingMt.UserRating = req.UserRating.Value;
        }

        if (req.ImproveClarity.HasValue)
        {
            existingMt.ImproveClarity = req.ImproveClarity.Value;
        }

        if (req.ImproveConsistency.HasValue)
        {
            existingMt.ImproveConsistency = req.ImproveConsistency.Value;
        }

        if (req.ImproveTone.HasValue)
        {
            existingMt.ImproveTone = req.ImproveTone.Value;
        }

        await dbContext.SaveChangesAsync(ct);
        await SendNoContentAsync(ct);
    }
}