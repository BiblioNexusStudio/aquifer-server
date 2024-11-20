using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.TranslationPairs.Post;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Post("/translation-pairs");
        Permissions(PermissionName.SetTranslationPair);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var checkForExistingTranslationPair =
            await dbContext.TranslationPairs.FirstOrDefaultAsync(x => x.Key.Equals(request.Key) && x.LanguageId == request.LanguageId, ct);

        if (checkForExistingTranslationPair != null)
        {
            AddError("Key already exists for this language");
            await SendErrorsAsync(409, ct);
            return;
        }

        var user = await userService.GetUserWithCompanyLanguagesFromJwtAsync(ct);
        var companyLanguage = user.Company.CompanyLanguages.FirstOrDefault(x => x.LanguageId == request.LanguageId);

        if (request.LanguageId != companyLanguage?.LanguageId)
        {
            await SendForbiddenAsync(ct);
            return;
        }

        var translationPair = new TranslationPairEntity
        {
            LanguageId = request.LanguageId,
            Key = request.Key,
            Value = request.Value
        };

        await dbContext.TranslationPairs.AddAsync(translationPair, ct);
        await dbContext.SaveChangesAsync(ct);

        Response = new Response { Id = translationPair.Id };
    }
}