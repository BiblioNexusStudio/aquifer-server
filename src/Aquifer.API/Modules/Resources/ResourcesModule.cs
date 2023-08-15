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

    private async Task<Ok<List<ResourceContentResponse>>> GetResourcesByLanguage(int languageId,
        AquiferDbContext dbContext, CancellationToken cancellationToken)
    {
        var resourceContent = await dbContext.ResourceContents.Where(x => x.LanguageId == languageId)
            .Select(x =>
                new ResourceContentResponse
                {
                    LanguageId = x.LanguageId,
                    DisplayName = x.DisplayName,
                    Summary = x.Summary,
                    Content = JsonUtility.DefaultSerialize(x.Content),
                    ContentSizeKb = x.ContentSizeKb,
                    Parent = new ResourceContentResponseParent
                    {
                        Type = (int)x.Resource.Type,
                        MediaType = (int)x.Resource.MediaType,
                        EnglishLabel = x.Resource.EnglishLabel,
                        Tag = x.Resource.Tag
                    }
                }).ToListAsync(cancellationToken);

        return TypedResults.Ok(resourceContent);
    }
}