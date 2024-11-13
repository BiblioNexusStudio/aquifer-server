using Aquifer.API.Common;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.TranslationPairs.Patch;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request>
{
    public override void Configure()
    {
        Patch("/translation-pairs/{Id}");
        Permissions(PermissionName.SetTranslationPair);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var translationPair = await dbContext.TranslationPairs.Where(x => x.Id == req.Id).FirstOrDefaultAsync(ct);

        if (translationPair is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        translationPair.Key = req.Key;
        translationPair.Value = req.Value;

        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}