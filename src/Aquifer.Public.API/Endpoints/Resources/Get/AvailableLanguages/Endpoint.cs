using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Public.API.Endpoints.Resources.Get.AvailableLanguages;

public class Endpoint(AquiferDbReadOnlyContext dbContext) : Endpoint<Request, IReadOnlyList<Response>>
{
    public override void Configure()
    {
        Get("/resources/{ContentId}/available-languages");
        Description(d => d
            .WithTags("Resources")
            .ProducesProblemFE(404));
        Summary(s =>
        {
            s.Summary = "For a given resource content id, see in what other languages it is available.";
            s.Description = "For a given resource content id, return a list of all other languages that are available for this resource.";
        });
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var foundContent = await dbContext.ResourceContentVersions.Where(x => x.ResourceContentId == req.ContentId && x.IsPublished)
            .Select(x => new ResourceContentQueryResponse(x.ResourceContent.ResourceId, x.ResourceContent.LanguageId))
            .FirstOrDefaultAsync(ct);

        if (foundContent is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        Response = await dbContext.ResourceContentVersions
            .Where(x => x.ResourceContent.ResourceId == foundContent.ResourceId &&
                x.IsPublished &&
                x.ResourceContent.LanguageId != foundContent.LanguageId)
            .Select(x => new Response
            {
                ContentId = x.ResourceContentId,
                ContentDisplayName = x.DisplayName,
                LanguageId = x.ResourceContent.LanguageId,
                LanguageDisplayName = x.ResourceContent.Language.DisplayName,
                LanguageCode = x.ResourceContent.Language.ISO6393Code,
                LanguageEnglishDisplayName = x.ResourceContent.Language.EnglishDisplay
            })
            .ToListAsync(ct);
    }

    private record ResourceContentQueryResponse(int ResourceId, int LanguageId);
}