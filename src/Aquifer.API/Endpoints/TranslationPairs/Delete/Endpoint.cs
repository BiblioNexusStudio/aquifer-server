using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.TranslationPairs.Delete;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : Endpoint<Request>
{
    public override void Configure()
    {
        Delete("/translation-pairs/{Id}");
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

        dbContext.TranslationPairs.Remove(translationPair);
        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}