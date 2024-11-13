using Aquifer.API.Common;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;

namespace Aquifer.API.Endpoints.TranslationPairs.Post;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Post("/translation-pairs");
        Permissions(PermissionName.SetTranslationPair);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var translationPair = new TranslationPairEntity
        {
            LanguageId = request.LanguageId,
            Key = request.Key,
            Value = request.Value
        };

        dbContext.TranslationPairs.Add(translationPair);
        await dbContext.SaveChangesAsync(ct);

        Response = new Response { Id = translationPair.Id };
    }
}