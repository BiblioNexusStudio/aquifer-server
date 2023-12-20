﻿using Aquifer.API.Common;
using Aquifer.API.Utilities;
using Aquifer.Data;
using Aquifer.Data.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Resources.LanguageResources;

public static class LanguageResourcesEndpoints
{
    public static async Task<Results<Ok<ResourceItemsByChapterResponse>, NotFound, BadRequest<string>>>
        GetBookByLanguage(
            int languageId,
            string bookCode,
            AquiferDbContext dbContext,
            CancellationToken cancellationToken,
            [FromQuery] string[]? parentResourceNames = null
        )
    {
        var bookId = BookCodes.IdFromCode(bookCode);
        if (bookId == BookId.None)
        {
            return TypedResults.NotFound();
        }

        if (parentResourceNames == null)
        {
            return TypedResults.BadRequest("parentResourceNames query param must be specified");
        }

        int englishLanguageId = (await dbContext.Languages.Where(language => language.ISO6393Code.ToLower() == "eng")
                                    .FirstOrDefaultAsync(cancellationToken))?.Id ??
                                -1;

        var parentResourceEntities = await dbContext.ParentResources
            .Where(rt => parentResourceNames.Contains(rt.ShortName))
            .ToListAsync(cancellationToken);

        var passageResourceContent = await dbContext.PassageResources
            // find all passages that overlap with the current book
            .Where(pr => parentResourceEntities.Contains(pr.Resource.ParentResource) &&
                         ((pr.Passage.StartVerseId > BibleUtilities.LowerBoundOfBook(bookId) &&
                           pr.Passage.StartVerseId < BibleUtilities.UpperBoundOfBook(bookId)) ||
                          (pr.Passage.EndVerseId > BibleUtilities.LowerBoundOfBook(bookId) &&
                           pr.Passage.EndVerseId < BibleUtilities.UpperBoundOfBook(bookId))))
            .SelectMany(pr => pr.Resource.ResourceContents
                .Where(rc => rc.LanguageId == languageId ||
                             (rc.LanguageId == englishLanguageId &&
                              Constants.FallbackToEnglishForMediaTypes.Contains(rc.MediaType)))
                .SelectMany(rc => rc.Versions.Where(rcv => rcv.IsPublished)
                    .Select(rcv =>
                        new
                        {
                            StartChapter = pr.Passage.StartVerseId / 1000 % 1000,
                            EndChapter = pr.Passage.EndVerseId / 1000 % 1000,
                            ContentId = rc.Id,
                            rcv.ContentSize,
                            rc.MediaType,
                            rc.LanguageId,
                            pr.ResourceId,
                            pr.Resource.ParentResource
                        })))
            .ToListAsync(cancellationToken);

        var verseResourceContent = await dbContext.VerseResources
            // find all verses contained in the current book
            .Where(vr => parentResourceEntities.Contains(vr.Resource.ParentResource) &&
                         vr.VerseId > BibleUtilities.LowerBoundOfBook(bookId) &&
                         vr.VerseId < BibleUtilities.UpperBoundOfBook(bookId))
            .SelectMany(vr => vr.Resource.ResourceContents
                .Where(rc => rc.LanguageId == languageId ||
                             (rc.LanguageId == englishLanguageId &&
                              Constants.FallbackToEnglishForMediaTypes.Contains(rc.MediaType)))
                .SelectMany(rc => rc.Versions.Where(rcv => rcv.IsPublished)
                    .Select(rcv =>
                        new
                        {
                            StartChapter = vr.VerseId / 1000 % 1000,
                            EndChapter = vr.VerseId / 1000 % 1000,
                            ContentId = rc.Id,
                            rcv.ContentSize,
                            rc.MediaType,
                            rc.LanguageId,
                            vr.ResourceId,
                            vr.Resource.ParentResource
                        })))
            .ToListAsync(cancellationToken);

        // for resource types that are used as the "root", we want to be sure to grab their associated resources
        var associatedResourceContent = await dbContext.PassageResources
            // find all passages that overlap with the current book
            .Where(pr => Constants.RootParentResourceNames.Contains(pr.Resource.ParentResource.ShortName) &&
                         ((pr.Passage.StartVerseId > BibleUtilities.LowerBoundOfBook(bookId) &&
                           pr.Passage.StartVerseId < BibleUtilities.UpperBoundOfBook(bookId)) ||
                          (pr.Passage.EndVerseId > BibleUtilities.LowerBoundOfBook(bookId) &&
                           pr.Passage.EndVerseId < BibleUtilities.UpperBoundOfBook(bookId))))
            .SelectMany(pr => pr.Resource.AssociatedResourceChildren
                .Where(ar => parentResourceEntities.Contains(ar.ParentResource))
                .SelectMany(sr => sr.ResourceContents
                    .Where(rc => rc.LanguageId == languageId ||
                                 (rc.LanguageId == englishLanguageId &&
                                  Constants.FallbackToEnglishForMediaTypes.Contains(rc.MediaType)))
                    .SelectMany(rc => rc.Versions.Where(rcv => rcv.IsPublished)
                        .Select(rcv =>
                            new
                            {
                                StartChapter = pr.Passage.StartVerseId / 1000 % 1000,
                                EndChapter = pr.Passage.EndVerseId / 1000 % 1000,
                                ContentId = rc.Id,
                                rcv.ContentSize,
                                rc.MediaType,
                                rc.LanguageId,
                                ResourceId = sr.Id,
                                sr.ParentResource
                            }))))
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
                    first.ParentResource
                };
            });

        var groupedContent = filteredDownToOneLanguage
            .SelectMany(content => Enumerable.Range(content.StartChapter, content.EndChapter - content.StartChapter + 1)
                .Select(chapter => new
                {
                    ChapterNumber = chapter,
                    Content = new ResourceItemResponse
                    {
                        ContentId = content.ContentId,
                        ContentSize = content.ContentSize,
                        MediaTypeName = content.MediaType,
                        ParentResourceName = content.ParentResource.ShortName
                    }
                }))
            .GroupBy(item => item.ChapterNumber)
            .Select(g => new ResourceItemsWithChapterNumberResponse
            {
                ChapterNumber = g.Key,
                Contents = g
                    .Select(item => item.Content)
                    .DistinctBy(item => item.ContentId)
            })
            .OrderBy(item => item.ChapterNumber)
            .ToList();

        return TypedResults.Ok(new ResourceItemsByChapterResponse { Chapters = groupedContent });
    }
}