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

    private async Task<Ok<AvailableContentResponse>> GetResourcesByLanguage(int languageId,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var bibleContent = await dbContext.Bibles.Where(x => x.LanguageId == languageId)
            .Select(x => new AvailableContentResponseBible
            {
                LanguageId = x.LanguageId,
                Name = x.Name,
                Contents = x.BibleBookContents.Select(y => new AvailableContentResponseBibleContent
                {
                    BookId = y.BookId,
                    DisplayName = y.DisplayName,
                    TextUrl = y.TextUrl,
                    TextSizeKb = y.TextSizeKb,
                    AudioUrls = JsonUtility.DefaultSerialize(y.AudioUrls),
                    AudioSizeKb = y.AudioSizeKb
                })
            }).ToListAsync(cancellationToken);

        var resourceContent = await dbContext.ResourceContents.Where(x => x.LanguageId == languageId)
            .Select(x =>
                new AvailableContentResponseResourceContent
                {
                    LanguageId = x.LanguageId,
                    DisplayName = x.DisplayName,
                    Summary = x.Summary,
                    Content = JsonUtility.DefaultSerialize(x.Content),
                    ContentSizeKb = x.ContentSizeKb,
                    Parent = new AvailableContentResponseResourceParent
                    {
                        Type = (int)x.Resource.Type,
                        MediaType = (int)x.Resource.MediaType,
                        EnglishLabel = x.Resource.EnglishLabel,
                        Tag = x.Resource.Tag
                    }
                }).ToListAsync(cancellationToken);

        return TypedResults.Ok(new AvailableContentResponse { Bibles = bibleContent, Resources = resourceContent });
    }
}