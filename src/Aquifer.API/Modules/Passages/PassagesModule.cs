using Aquifer.API.Data;
using Aquifer.API.Utilities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Passages;

public class PassagesModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("passages");
        group.MapGet("/resources/language/{languageId:int}", GetPassageResourcesByLanguage);
        return endpoints;
    }

    public async Task<Ok<List<PassageResourcesResponse>>> GetPassageResourcesByLanguage(int languageId,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var passageContent = Enumerable.ToList((await dbContext.Passages.Select(x =>
                new PassageResourcesResponse
                {
                    PassageStartDetails = BibleUtilities.TranslateVerseId(x.StartVerseId),
                    PassageEndDetails = BibleUtilities.TranslateVerseId(x.EndVerseId),
                    Resources = x.PassageResources.Select(y => new PassageResourcesResponseResource
                    {
                        Type = (int)y.Resource.Type,
                        MediaType = (int)y.Resource.MediaType,
                        EnglishLabel = y.Resource.EnglishLabel,
                        Tag = y.Resource.Tag,
                        Contents = y.Resource.ResourceContents.Select(z => new PassageResourcesResponseResourceContent
                        {
                            LanguageId = z.LanguageId,
                            DisplayName = z.DisplayName,
                            Summary = z.Summary,
                            Content = JsonUtilities.DefaultSerialize(z.Content),
                            ContentSize = z.ContentSize
                        }).Where(content => content.LanguageId == languageId)
                    })
                }).ToListAsync(cancellationToken))
            .OrderBy(x => x.BookId)
            .ThenBy(x => x.StartChapter).ThenBy(x => x.StartVerse));

        return TypedResults.Ok(passageContent);
    }
}