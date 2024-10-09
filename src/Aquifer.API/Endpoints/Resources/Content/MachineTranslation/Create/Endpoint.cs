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
        Post("/resources/content/machine-translation");
        Permissions(PermissionName.AiTranslate);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var existingMts = await dbContext.ResourceContentVersionMachineTranslations
            .AsTracking()
            .Where(x => x.ResourceContentVersionId == req.ResourceContentVersionId && x.ContentIndex == req.ContentIndex)
            .ToListAsync(ct);

        if (existingMts.Any(x => x.RetranslationReason != null))
        {
            ThrowError(x => x.ResourceContentVersionId,
                "Machine translation already exists for this resource content version id and has a non-null retranslation reason");
        }

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
}