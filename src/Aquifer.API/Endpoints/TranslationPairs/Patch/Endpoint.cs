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

        var user = await userService.GetUserWithCompanyLanguagesFromJwtAsync(ct);
        var companyLanguage = user.Company.CompanyLanguages.FirstOrDefault(x => x.LanguageId == translationPair.LanguageId);

        if (translationPair.LanguageId != companyLanguage?.LanguageId)
        {
            await SendForbiddenAsync(ct);
            return;
        }

        if (req.Key is not null)
        {
            translationPair.Key = req.Key;
        }

        if (req.Value is not null)
        {
            translationPair.Value = req.Value;
        }

        dbContext.TranslationPairs.Update(translationPair);
        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}