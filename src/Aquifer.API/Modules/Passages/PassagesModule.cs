using Aquifer.API.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
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
        var passageContent = (await dbContext.Passages.Select(passage =>
                new PassageResourcesResponse
                {
                    PassageStartDetails = BibleUtilities.TranslateVerseId(passage.StartVerseId),
                    PassageEndDetails = BibleUtilities.TranslateVerseId(passage.EndVerseId),
                    Resources = passage.PassageResources
                        .Where(pr => pr.Resource.Type != ResourceEntityType.TyndaleBibleDictionary)
                        .Select(passageResource =>
                            new PassageResourcesResponseResource
                            {
                                Type = (int)passageResource.Resource.Type,
                                MediaType = (int)passageResource.Resource.MediaType,
                                EnglishLabel = passageResource.Resource.EnglishLabel,
                                Tag = passageResource.Resource.Tag,
                                Content =
                                    passageResource.Resource.ResourceContents
                                        .Select(resourceContent =>
                                            new PassageResourcesResponseResourceContent
                                            {
                                                LanguageId = resourceContent.LanguageId,
                                                DisplayName = resourceContent.DisplayName,
                                                Summary = resourceContent.Summary,
                                                Content = JsonUtilities.DefaultSerialize(resourceContent.Content),
                                                ContentSize = resourceContent.ContentSize
                                            }).FirstOrDefault(content => content.LanguageId == languageId),
                                SupportingResources = passageResource.Resource.SupportingResources
                                    .Where(sr => sr.Type != ResourceEntityType.TyndaleBibleDictionary)
                                    .Select(
                                        supportingResource => new PassageResourcesResponseResource
                                        {
                                            Type = (int)supportingResource.Type,
                                            MediaType = (int)supportingResource.MediaType,
                                            EnglishLabel = supportingResource.EnglishLabel,
                                            Tag = supportingResource.Tag,
                                            Content = supportingResource.ResourceContents.Select(resourceContent =>
                                                new PassageResourcesResponseResourceContent
                                                {
                                                    LanguageId = resourceContent.LanguageId,
                                                    DisplayName = resourceContent.DisplayName,
                                                    Summary = resourceContent.Summary,
                                                    Content =
                                                        JsonUtilities.DefaultSerialize(resourceContent.Content),
                                                    ContentSize = resourceContent.ContentSize
                                                }).FirstOrDefault(content => content.LanguageId == languageId)
                                        })
                            })
                }).ToListAsync(cancellationToken))
            .OrderBy(passage => passage.BookId)
            .ThenBy(passage => passage.StartChapter).ThenBy(passage => passage.StartVerse).ToList();

        return TypedResults.Ok(passageContent);
    }
}