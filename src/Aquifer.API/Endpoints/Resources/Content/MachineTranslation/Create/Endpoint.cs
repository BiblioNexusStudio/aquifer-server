using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.MachineTranslation.Create;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        //TODO: Delete after next deploy
        Post("/resources/content/machine-translation");
        Permissions(PermissionName.AiTranslate);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var existingMts = await dbContext.ResourceContentVersionMachineTranslations
            .AsTracking()
            .Where(x => x.ResourceContentVersionId == req.ResourceContentVersionId && x.ContentIndex == req.ContentIndex)
            .ToListAsync(ct);

        ValidateExistingMachineTranslations(existingMts, req);

        var user = await userService.GetUserFromJwtAsync(ct);
        var mt = new ResourceContentVersionMachineTranslationEntity
        {
            ResourceContentVersionId = req.ResourceContentVersionId,
            DisplayName = req.DisplayName,
            Content = req.Content,
            ContentIndex = req.ContentIndex,
            UserId = user.Id,
            SourceId = req.SourceId,
            RetranslationReason = req.RetranslationReason
        };

        await dbContext.ResourceContentVersionMachineTranslations.AddAsync(mt, ct);
        await dbContext.SaveChangesAsync(ct);

        await SendOkAsync(new Response(mt.Id), ct);
    }

    private void ValidateExistingMachineTranslations(List<ResourceContentVersionMachineTranslationEntity> existingMts, Request req)
    {
        if (existingMts.Count > 2 || existingMts.Any(x => x.RetranslationReason != null))
        {
            ThrowError(x => x.ResourceContentVersionId,
                "No more machine translations are allowed for this resource content version");
        }

        if (existingMts.Count == 1)
        {
            if (string.IsNullOrEmpty(req.RetranslationReason))
            {
                ThrowError(x => x.RetranslationReason, "Retranslation reason is required when retranslating a machine translation");
            }

            if (existingMts.Single().Created.AddHours(1) < DateTime.UtcNow)
            {
                ThrowError(x => x.RetranslationReason, "Retranslation time period of 1 hour has expired");
            }
        }
    }
}