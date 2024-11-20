using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.TranslationPairs.Patch;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : Endpoint<Request>
{
    public override void Configure()
    {
        Patch("/translation-pairs/{Id}");
        Permissions(PermissionName.SetTranslationPair);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var translationPair = await dbContext.TranslationPairs.FirstOrDefaultAsync(x => x.Id == req.Id, ct);

        if (translationPair is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var user = await userService.GetUserFromJwtAsync(ct);

        if (translationPair.LanguageId !=
            user.Company.CompanyLanguages.FirstOrDefault(x => x.LanguageId == translationPair.LanguageId)?.LanguageId)
        {
            await SendForbiddenAsync(ct);
            return;
        }

        translationPair.Key = req.Key;
        translationPair.Value = req.Value;

        dbContext.TranslationPairs.Update(translationPair);
        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}