using Aquifer.API.Common;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.TranslationPairs.List;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, List<Response>>
{
    public override void Configure()
    {
        Get("/translation-pairs");
        Permissions(PermissionName.GetTranslationPair);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var translationPairs = await dbContext.TranslationPairs.Where(x => x.LanguageId == req.LanguageId)
            .Select(x => new Response
            {
                Id = x.Id,
                Key = x.Key,
                Value = x.Value
            })
            .ToListAsync(ct);

        Response = translationPairs;
    }
}