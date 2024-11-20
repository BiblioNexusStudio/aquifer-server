using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;

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
        var user = await userService.GetUserFromJwtAsync(ct);

        if (request.LanguageId != user.Company.CompanyLanguages.FirstOrDefault(x => x.LanguageId == request.LanguageId)?.LanguageId)
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

        Response = new Response
        {
            Id = translationPair.Id,
            LanguageId = translationPair.LanguageId,
            Key = translationPair.Key,
            Value = translationPair.Value,
            LanguageEnglishDisplay = translationPair.Language.EnglishDisplay
        };
    }
}