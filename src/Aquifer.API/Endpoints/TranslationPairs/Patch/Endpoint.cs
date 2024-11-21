using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Exceptions;
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
        var translationPair = await dbContext.TranslationPairs.AsTracking().FirstOrDefaultAsync(x => x.Id == req.Id, ct);

        if (translationPair is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var user = await userService.GetUserWithCompanyLanguagesFromJwtAsync(ct);
        var hasCompanyLanguage = user.Company.CompanyLanguages.Any(x => x.LanguageId == translationPair.LanguageId);

        if (!hasCompanyLanguage)
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

        try
        {
            await dbContext.SaveChangesAsync(ct);
        }
        catch (TranslationPairKeyNotAllowedException)
        {
            AddError("Keys must be at least 3 characters long. Some keywords are not allowed.");
            await SendErrorsAsync(400, ct);
            return;
        }

        await SendNoContentAsync(ct);
    }
}