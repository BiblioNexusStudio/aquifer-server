﻿using Aquifer.API.Common;
using Aquifer.API.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Aquifer.Data.Enums;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
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
        group.MapGet("language/{languageId:int}/book/{bookCode}", GetResourcesForBook);

        return endpoints;
    }

    private async Task<Results<Ok<ResourceContentInfoForBookResponse>, NotFound, BadRequest<string>>>
        GetResourcesForBook(
            int languageId,
            string bookCode,
            AquiferDbContext dbContext,
            CancellationToken cancellationToken,
            [FromQuery] ResourceEntityType[]? resourceTypes = null
        )
    {
        var bookId = BookIdSerializer.FromCode(bookCode);
        if (bookId == null)
        {
            return TypedResults.NotFound();
        }

        if (resourceTypes == null)
        {
            return TypedResults.BadRequest("resourceTypes query param must be specified");
        }

        var passageResourceContent = await dbContext.PassageResources
            // find all passages that overlap with the current book
            .Where(pr => resourceTypes.Contains(pr.Resource.Type) &&
                         ((pr.Passage.StartVerseId > BibleUtilities.LowerBoundOfBook((BookId)bookId) &&
                           pr.Passage.StartVerseId < BibleUtilities.UpperBoundOfBook((BookId)bookId)) ||
                          (pr.Passage.EndVerseId > BibleUtilities.LowerBoundOfBook((BookId)bookId) &&
                           pr.Passage.EndVerseId < BibleUtilities.UpperBoundOfBook((BookId)bookId))))
            .SelectMany(pr => pr.Resource.ResourceContents.Where(rc => rc.LanguageId == languageId).Select(rc =>
                new
                {
                    StartChapter = pr.Passage.StartVerseId / 1000 % 1000,
                    EndChapter = pr.Passage.EndVerseId / 1000 % 1000,
                    ContentId = rc.Id,
                    rc.ContentSize,
                    rc.MediaType,
                    pr.Resource.Type
                }))
            .ToListAsync(cancellationToken);

        var verseResourceContent = await dbContext.VerseResources
            // find all verses contained in the current book
            .Where(vr => resourceTypes.Contains(vr.Resource.Type) &&
                         vr.VerseId > BibleUtilities.LowerBoundOfBook((BookId)bookId) &&
                         vr.VerseId < BibleUtilities.UpperBoundOfBook((BookId)bookId))
            .SelectMany(vr => vr.Resource.ResourceContents.Where(rc => rc.LanguageId == languageId).Select(rc =>
                new
                {
                    StartChapter = vr.VerseId / 1000 % 1000,
                    EndChapter = vr.VerseId / 1000 % 1000,
                    ContentId = rc.Id,
                    rc.ContentSize,
                    rc.MediaType,
                    vr.Resource.Type
                }))
            .ToListAsync(cancellationToken);

        // for resource types that are used as the "root", we want to be sure to grab their supporting resources
        var supportingResourceContent = await dbContext.PassageResources
            // find all passages that overlap with the current book
            .Where(pr => Constants.RootResourceTypes.Contains(pr.Resource.Type) &&
                         resourceTypes.Contains(pr.Resource.Type) &&
                         ((pr.Passage.StartVerseId > BibleUtilities.LowerBoundOfBook((BookId)bookId) &&
                           pr.Passage.StartVerseId < BibleUtilities.UpperBoundOfBook((BookId)bookId)) ||
                          (pr.Passage.EndVerseId > BibleUtilities.LowerBoundOfBook((BookId)bookId) &&
                           pr.Passage.EndVerseId < BibleUtilities.UpperBoundOfBook((BookId)bookId))))
            .SelectMany(pr => pr.Resource.SupportingResources.SelectMany(sr => sr.ResourceContents
                .Where(rc => rc.LanguageId == languageId).Select(rc =>
                    new
                    {
                        StartChapter = pr.Passage.StartVerseId / 1000 % 1000,
                        EndChapter = pr.Passage.EndVerseId / 1000 % 1000,
                        ContentId = rc.Id,
                        rc.ContentSize,
                        rc.MediaType,
                        sr.Type
                    })))
            .ToListAsync(cancellationToken);

        var allContent = passageResourceContent.Concat(verseResourceContent).Concat(supportingResourceContent);

        var groupedContent = allContent
            .SelectMany(content => Enumerable.Range(content.StartChapter, content.EndChapter - content.StartChapter + 1)
                .Select(chapter => new
                {
                    ChapterNumber = chapter,
                    Content = new ResourceContentInfo
                    {
                        ContentId = content.ContentId,
                        ContentSize = content.ContentSize,
                        MediaTypeName = content.MediaType,
                        TypeName = content.Type
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

    private async Task<Results<Ok<object>, NotFound, ProblemHttpResult, RedirectHttpResult>> GetResourceContentById(
        int contentId,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken,
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

        var telemetry = new TelemetryClient(TelemetryConfiguration.CreateDefault());
        telemetry.TrackException(
            new Exception($"Content with ID {contentId} exists but has unexpected `Content` JSON."));
        return TypedResults.NotFound();
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
}