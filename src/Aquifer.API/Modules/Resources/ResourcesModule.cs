using Aquifer.API.Data;
using Aquifer.API.Utilities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Resources;

public class ResourcesModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("resources");
        group.MapGet("language/{languageId:int}", GetResourcesByLanguage);

        return endpoints;
    }

    private async Task<Ok<List<ResourceContentResponse>>> GetResourcesByLanguage(
        int languageId,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken
    )
    {
        var resourceContent = await dbContext.ResourceContents.Where(x => x.LanguageId == languageId)
            .Select(x =>
                new ResourceContentResponse
                {
                    LanguageId = x.LanguageId,
                    DisplayName = x.DisplayName,
                    Summary = x.Summary,
                    Content = JsonUtilities.DefaultSerialize(x.Content),
                    ContentSize = x.ContentSize,
                    Type = (int)x.Resource.Type,
                    MediaType = (int)x.Resource.MediaType,
                    EnglishLabel = x.Resource.EnglishLabel,
                    Tag = x.Resource.Tag,
                    Passages = x.Resource.PassageResources.Select(y => new ResourceContentResponsePassage
                    {
                        PassageStartDetails = BibleUtilities.TranslateVerseId(y.Passage.StartVerseId),
                        PassageEndDetails = BibleUtilities.TranslateVerseId(y.Passage.EndVerseId)
                    })
                }).ToListAsync(cancellationToken);

        return TypedResults.Ok(resourceContent);
    }
}