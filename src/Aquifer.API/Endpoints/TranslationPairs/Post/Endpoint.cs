using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Aquifer.Data.Exceptions;
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
            await dbContext.TranslationPairs.FirstOrDefaultAsync(x => x.Key == request.Key && x.LanguageId == request.LanguageId, ct);

        if (checkForExistingTranslationPair != null)
        {
            AddError("Key already exists for this language");
            await SendErrorsAsync(409, ct);
            return;
        }

        var user = await userService.GetUserWithCompanyLanguagesFromJwtAsync(ct);
        var hasCompanyLanguage = user.Company.CompanyLanguages.Any(x => x.LanguageId == request.LanguageId);

        if (!hasCompanyLanguage)
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

        Response = new Response { Id = translationPair.Id };
    }
}