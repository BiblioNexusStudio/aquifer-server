using Aquifer.API.Common;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
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
            [FromQuery] int[]? parentResourceIds = null
        )
    {
        var bookId = BibleBookCodeUtilities.IdFromCode(bookCode);
        if (bookId == BookId.None)
        {
            return TypedResults.NotFound();
        }

        if (parentResourceIds == null)
        {
            return TypedResults.BadRequest("parentResourceIds query param must be specified");
        }

        var parentResourceEntities = await dbContext.ParentResources
            .Where(pr => parentResourceIds.Contains(pr.Id))
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
                             (rc.LanguageId == Constants.EnglishLanguageId &&
                              Constants.FallbackToEnglishForMediaTypes.Contains(rc.MediaType)))
                .SelectMany(rc => rc.Versions.Where(rcv => rcv.IsPublished)
                    .Select(rcv =>
                        new IntermediateResourceItem
                        {
                            StartChapter = pr.Passage.StartVerseId / 1000 % 1000,
                            EndChapter = pr.Passage.EndVerseId / 1000 % 1000,
                            ContentId = rc.Id,
                            Version = rcv.Version,
                            ContentSize = rcv.ContentSize,
                            InlineMediaSize = rcv.InlineMediaSize,
                            MediaType = rc.MediaType,
                            LanguageId = rc.LanguageId,
                            ResourceId = pr.ResourceId,
                            ParentResource = pr.Resource.ParentResource
                        })))
            .ToListAsync(cancellationToken);

        var verseResourceContent = await dbContext.VerseResources
            // find all verses contained in the current book
            .Where(vr => parentResourceEntities.Contains(vr.Resource.ParentResource) &&
                         vr.VerseId > BibleUtilities.LowerBoundOfBook(bookId) &&
                         vr.VerseId < BibleUtilities.UpperBoundOfBook(bookId))
            .SelectMany(vr => vr.Resource.ResourceContents
                .Where(rc => rc.LanguageId == languageId ||
                             (rc.LanguageId == Constants.EnglishLanguageId &&
                              Constants.FallbackToEnglishForMediaTypes.Contains(rc.MediaType)))
                .SelectMany(rc => rc.Versions.Where(rcv => rcv.IsPublished)
                    .Select(rcv =>
                        new IntermediateResourceItem
                        {
                            StartChapter = vr.VerseId / 1000 % 1000,
                            EndChapter = vr.VerseId / 1000 % 1000,
                            ContentId = rc.Id,
                            Version = rcv.Version,
                            ContentSize = rcv.ContentSize,
                            InlineMediaSize = rcv.InlineMediaSize,
                            MediaType = rc.MediaType,
                            LanguageId = rc.LanguageId,
                            ResourceId = vr.ResourceId,
                            ParentResource = vr.Resource.ParentResource
                        })))
            .ToListAsync(cancellationToken);

        // The above queries return resource contents in English + the current language (if available).
        // This filters them by grouping appropriately and selecting the current language resource (if available) then falling back to English.
        var filteredDownToOneLanguage = passageResourceContent.Concat(verseResourceContent)
            .GroupBy(rc => new
            {
                rc.StartChapter,
                rc.EndChapter,
                rc.MediaType,
                rc.ResourceId
            })
            .Select(grc => grc.OrderBy(rc => rc.LanguageId == languageId ? 0 : 1).First());

        var groupedContent = filteredDownToOneLanguage
            .SelectMany(content => Enumerable.Range(content.StartChapter, content.EndChapter - content.StartChapter + 1)
                .Select(chapter => new
                {
                    ChapterNumber = chapter,
                    Content = new ResourceItemResponse
                    {
                        ContentId = content.ContentId,
                        ContentSize = content.ContentSize,
                        InlineMediaSize = content.InlineMediaSize,
                        Version = content.Version,
                        MediaTypeName = content.MediaType,
                        ParentResourceId = content.ParentResource.Id
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

public record IntermediateResourceItem
{
    public int StartChapter { get; set; }
    public int EndChapter { get; set; }
    public int ContentId { get; set; }
    public int Version { get; set; }
    public int ContentSize { get; set; }
    public int? InlineMediaSize { get; set; }
    public ResourceContentMediaType MediaType { get; set; }
    public int LanguageId { get; set; }
    public int ResourceId { get; set; }
    public ParentResourceEntity ParentResource { get; set; } = null!;
}