using Aquifer.API.Common;
using Aquifer.API.Modules.Resources.ResourcesList;
using Aquifer.API.Modules.Resources.ResourcesSummary;
using Aquifer.API.Modules.Resources.ResourceTypes;
using Aquifer.API.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Aquifer.Data.Enums;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Resources;

public class ResourcesModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("resources");
        group.MapGet("{contentId:int}/content", GetResourceContentById);
        group.MapGet("{contentId:int}/metadata", GetResourceMetadataById);
        group.MapGet("batch/metadata", GetResourceMetadataByIds);
        group.MapGet("batch/content/text", GetResourceTextContentByIds);
        group.MapGet("{contentId:int}/thumbnail", GetResourceThumbnailById);
        group.MapGet("language/{languageId:int}/book/{bookCode}", GetResourcesForBook);
        group.MapGet("summary", GetResourcesSummaryEndpoints.Get).CacheOutput(x => x.Expire(TimeSpan.FromHours(1)));
        group.MapGet("summary/{resourceId:int}", GetResourcesSummaryEndpoints.GetByResourceId);
        group.MapPut("summary/{contentId:int}", UpdateResourcesSummaryEndpoints.UpdateResourcesSummaryItem);
        group.MapGet("types", ResourceTypesEndpoints.Get).CacheOutput(x => x.Expire(TimeSpan.FromMinutes(5)));
        group.MapGet("list", ResourcesListEndpoints.Get);
        group.MapGet("list/count", ResourcesListEndpoints.GetCount);

        return endpoints;
    }

    private async Task<Results<Ok<ResourceContentInfoForBookResponse>, NotFound, BadRequest<string>>>
        GetResourcesForBook(
            int languageId,
            string bookCode,
            AquiferDbContext dbContext,
            CancellationToken cancellationToken,
            [FromQuery] string[]? resourceTypes = null
        )
    {
        var bookId = BookIdSerializer.FromCode(bookCode);
        if (bookId == BookId.None)
        {
            return TypedResults.NotFound();
        }

        if (resourceTypes == null)
        {
            return TypedResults.BadRequest("resourceTypes query param must be specified");
        }

        int englishLanguageId = (await dbContext.Languages.Where(language => language.ISO6393Code.ToLower() == "eng")
                                    .FirstOrDefaultAsync(cancellationToken))?.Id ??
                                -1;

        var resourceTypeEntities = await dbContext.ResourceTypes.Where(rt => resourceTypes.Contains(rt.ShortName))
            .ToListAsync(cancellationToken);

        var passageResourceContent = await dbContext.PassageResources
            // find all passages that overlap with the current book
            .Where(pr => resourceTypeEntities.Contains(pr.Resource.Type) &&
                         ((pr.Passage.StartVerseId > BibleUtilities.LowerBoundOfBook(bookId) &&
                           pr.Passage.StartVerseId < BibleUtilities.UpperBoundOfBook(bookId)) ||
                          (pr.Passage.EndVerseId > BibleUtilities.LowerBoundOfBook(bookId) &&
                           pr.Passage.EndVerseId < BibleUtilities.UpperBoundOfBook(bookId))))
            .SelectMany(pr => pr.Resource.ResourceContents
                .Where(rc => rc.LanguageId == languageId ||
                             (rc.LanguageId == englishLanguageId &&
                              Constants.FallbackToEnglishForMediaTypes.Contains(rc.MediaType)))
                .Select(rc =>
                    new
                    {
                        StartChapter = pr.Passage.StartVerseId / 1000 % 1000,
                        EndChapter = pr.Passage.EndVerseId / 1000 % 1000,
                        ContentId = rc.Id,
                        rc.ContentSize,
                        rc.MediaType,
                        rc.LanguageId,
                        pr.ResourceId,
                        pr.Resource.Type
                    }))
            .ToListAsync(cancellationToken);

        var verseResourceContent = await dbContext.VerseResources
            // find all verses contained in the current book
            .Where(vr => resourceTypeEntities.Contains(vr.Resource.Type) &&
                         vr.VerseId > BibleUtilities.LowerBoundOfBook(bookId) &&
                         vr.VerseId < BibleUtilities.UpperBoundOfBook(bookId))
            .SelectMany(vr => vr.Resource.ResourceContents
                .Where(rc => rc.LanguageId == languageId ||
                             (rc.LanguageId == englishLanguageId &&
                              Constants.FallbackToEnglishForMediaTypes.Contains(rc.MediaType)))
                .Select(rc =>
                    new
                    {
                        StartChapter = vr.VerseId / 1000 % 1000,
                        EndChapter = vr.VerseId / 1000 % 1000,
                        ContentId = rc.Id,
                        rc.ContentSize,
                        rc.MediaType,
                        rc.LanguageId,
                        vr.ResourceId,
                        vr.Resource.Type
                    }))
            .ToListAsync(cancellationToken);

        // for resource types that are used as the "root", we want to be sure to grab their supporting resources
        var associatedResourceContent = await dbContext.PassageResources
            // find all passages that overlap with the current book
            .Where(pr => Constants.RootResourceTypes.Contains(pr.Resource.Type.ShortName) &&
                         ((pr.Passage.StartVerseId > BibleUtilities.LowerBoundOfBook(bookId) &&
                           pr.Passage.StartVerseId < BibleUtilities.UpperBoundOfBook(bookId)) ||
                          (pr.Passage.EndVerseId > BibleUtilities.LowerBoundOfBook(bookId) &&
                           pr.Passage.EndVerseId < BibleUtilities.UpperBoundOfBook(bookId))))
            .SelectMany(pr => pr.Resource.AssociatedResourceChildren.SelectMany(sr => sr.ResourceContents
                .Where(rc => rc.LanguageId == languageId ||
                             (rc.LanguageId == englishLanguageId &&
                              Constants.FallbackToEnglishForMediaTypes.Contains(rc.MediaType)))
                .Select(rc =>
                    new
                    {
                        StartChapter = pr.Passage.StartVerseId / 1000 % 1000,
                        EndChapter = pr.Passage.EndVerseId / 1000 % 1000,
                        ContentId = rc.Id,
                        rc.ContentSize,
                        rc.MediaType,
                        rc.LanguageId,
                        ResourceId = sr.Id,
                        sr.Type
                    })))
            .ToListAsync(cancellationToken);

        // The above queries return resource contents in English + the current language (if available).
        // This filters them by grouping appropriately and selecting the current language resource (if available) then falling back to English.
        var filteredDownToOneLanguage = passageResourceContent.Concat(verseResourceContent)
            .Concat(associatedResourceContent)
            .GroupBy(rc => new
            {
                rc.StartChapter,
                rc.EndChapter,
                rc.MediaType,
                rc.ResourceId
            })
            .Select(grc =>
            {
                var first = grc.OrderBy(rc => rc.LanguageId == languageId ? 0 : 1).First();
                return new
                {
                    first.StartChapter,
                    first.EndChapter,
                    first.ContentId,
                    first.ContentSize,
                    first.MediaType,
                    first.Type
                };
            });

        var groupedContent = filteredDownToOneLanguage
            .SelectMany(content => Enumerable.Range(content.StartChapter, content.EndChapter - content.StartChapter + 1)
                .Select(chapter => new
                {
                    ChapterNumber = chapter,
                    Content = new ResourceContentInfo
                    {
                        ContentId = content.ContentId,
                        ContentSize = content.ContentSize,
                        MediaTypeName = content.MediaType,
                        TypeName = content.Type.ShortName
                    }
                }))
            .GroupBy(item => item.ChapterNumber)
            .Select(g => new ResourceContentInfoForChapter
            {
                ChapterNumber = g.Key,
                Contents = g
                    .Select(item => item.Content)
                    .DistinctBy(item => item.ContentId)
            })
            .OrderBy(item => item.ChapterNumber)
            .ToList();

        return TypedResults.Ok(new ResourceContentInfoForBookResponse { Chapters = groupedContent });
    }

    private async Task<Results<Ok<object>, NotFound, RedirectHttpResult>> GetResourceContentById(
        int contentId,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken,
        TelemetryClient telemetry,
        [FromQuery] string audioType = "webm"
    )
    {
        var content = await dbContext.ResourceContents.FindAsync(contentId, cancellationToken);
        if (content == null)
        {
            return TypedResults.NotFound();
        }

        if (content.MediaType == ResourceContentMediaType.Text)
        {
            return TypedResults.Ok(JsonUtilities.DefaultDeserialize(content.Content));
        }

        string? url = null;

        if (content.MediaType == ResourceContentMediaType.Audio)
        {
            var deserialized = JsonUtilities.DefaultDeserialize<ResourceContentAudioJsonSchema>(content.Content);
            if (audioType == "webm" && deserialized.Webm != null)
            {
                url = deserialized.Webm.Url;
            }
            else if (deserialized.Mp3 != null)
            {
                url = deserialized.Mp3.Url;
            }
        }
        else
        {
            var deserialized = JsonUtilities.DefaultDeserialize<ResourceContentUrlJsonSchema>(content.Content);
            url = deserialized.Url;
        }

        if (url != null)
        {
            return TypedResults.Redirect(url);
        }

        telemetry.TrackTrace($"Content with ID {contentId} exists but has unexpected `Content` JSON.",
            SeverityLevel.Error);
        return TypedResults.NotFound();
    }

    private async Task<Results<Ok<List<ResourceTextContentResponse>>, BadRequest<string>, NotFound<string>>>
        GetResourceTextContentByIds(
            [FromQuery] int[] ids,
            AquiferDbContext dbContext,
            CancellationToken cancellationToken,
            TelemetryClient telemetry
        )
    {
        if (ids.Length > 10)
        {
            return TypedResults.BadRequest("A maximum of 10 ids may be passed.");
        }

        var contents = await dbContext.ResourceContents
            .Where(rc => ids.Contains(rc.Id) && rc.MediaType == ResourceContentMediaType.Text)
            .Select(content => new ResourceTextContentResponse
            {
                Id = content.Id,
                Content = JsonUtilities.DefaultDeserialize(content.Content)
            })
            .ToListAsync(cancellationToken);

        if (contents.Count != ids.Length)
        {
            telemetry.TrackTrace("IDs and content found have different lengths.",
                SeverityLevel.Error,
                new Dictionary<string, string> { { "Ids", string.Join(", ", ids) } });
            return TypedResults.NotFound("One or more couldn't be found. Were some IDs for non-text content?");
        }

        return TypedResults.Ok(contents);
    }

    private async Task<Results<Ok<ResourceContentMetadataResponse>, NotFound>> GetResourceMetadataById(
        int contentId,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken
    )
    {
        var content = await dbContext.ResourceContents.FindAsync(contentId, cancellationToken);
        if (content == null)
        {
            return TypedResults.NotFound();
        }

        var response = new ResourceContentMetadataResponse
        {
            DisplayName = content.DisplayName,
            Metadata = content.MediaType == ResourceContentMediaType.Text
                ? null
                : JsonUtilities.DefaultDeserialize(content.Content)
        };

        return TypedResults.Ok(response);
    }

    private async Task<Results<Ok<List<ResourceContentMetadataResponseWithId>>, BadRequest<string>, NotFound<string>>>
        GetResourceMetadataByIds(
            [FromQuery] int[] ids,
            AquiferDbContext dbContext,
            CancellationToken cancellationToken,
            TelemetryClient telemetry
        )
    {
        if (ids.Length > 100)
        {
            return TypedResults.BadRequest("A maximum of 100 ids may be passed.");
        }

        var metadata = await dbContext.ResourceContents
            .Where(rc => ids.Contains(rc.Id))
            .Select(content => new ResourceContentMetadataResponseWithId
            {
                Id = content.Id,
                DisplayName = content.DisplayName,
                Metadata = content.MediaType == ResourceContentMediaType.Text
                    ? null
                    : JsonUtilities.DefaultDeserialize(content.Content)
            })
            .ToListAsync(cancellationToken);

        if (metadata.Count != ids.Length)
        {
            telemetry.TrackTrace("IDs and metadata found have different lengths.",
                SeverityLevel.Error,
                new Dictionary<string, string> { { "Ids", string.Join(", ", ids) } });
            return TypedResults.NotFound("One or more couldn't be found");
        }

        return TypedResults.Ok(metadata);
    }

    private async Task<Results<RedirectHttpResult, NotFound>> GetResourceThumbnailById(
        int contentId,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken
    )
    {
        var content = await dbContext.ResourceContents.FindAsync(contentId, cancellationToken);

        if (content?.MediaType == ResourceContentMediaType.Video)
        {
            var deserialized = JsonUtilities.DefaultDeserialize<ResourceContentVideoJsonSchema>(content.Content);
            if (deserialized.ThumbnailUrl != null)
            {
                return TypedResults.Redirect(deserialized.ThumbnailUrl);
            }
        }

        return TypedResults.NotFound();
    }
}