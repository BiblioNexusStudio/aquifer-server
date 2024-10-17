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

        var lastChapterInBook = await dbContext.BibleBookChapters
            .Where(bbc => bbc.BibleBook.BibleId == 1 && bbc.BibleBook.Number == bookId)
            .MaxAsync(bbc => bbc.Number, cancellationToken);

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
                            StartVerseId = pr.Passage.StartVerseId,
                            EndVerseId = pr.Passage.EndVerseId,
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
                            StartVerseId = vr.VerseId,
                            EndVerseId = vr.VerseId,
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
            .GroupBy(rc =>
            {
                var startVerse = BibleUtilities.TranslateVerseId(rc.StartVerseId);
                var endVerse = BibleUtilities.TranslateVerseId(rc.EndVerseId);

                return new
                {
                    StartBook = startVerse.bookId,
                    StartChapter = startVerse.chapter,
                    EndBook = endVerse.bookId,
                    EndChapter = endVerse.chapter,
                    rc.MediaType,
                    rc.ResourceId
                };
            })
            .Select(grc =>
                (Passage: (grc.Key.StartBook, grc.Key.StartChapter, grc.Key.EndBook, grc.Key.EndChapter),
                ResourceItem: grc.OrderBy(rc => rc.LanguageId == languageId ? 0 : 1).First()));

        var groupedContent = filteredDownToOneLanguage
            .SelectMany(x => GetPassageChapterOverlapWithBook(x.Passage, bookId, lastChapterInBook)
                .Select(chapter => new
                {
                    ChapterNumber = chapter,
                    Content = new ResourceItemResponse
                    {
                        ContentId = x.ResourceItem.ContentId,
                        ContentSize = x.ResourceItem.ContentSize,
                        InlineMediaSize = x.ResourceItem.InlineMediaSize,
                        Version = x.ResourceItem.Version,
                        MediaTypeName = x.ResourceItem.MediaType,
                        ParentResourceId = x.ResourceItem.ParentResource.Id
                    }
                }))
            .GroupBy(item => item.ChapterNumber)
            .Select(g => new ResourceItemsWithChapterNumberResponse
            {
                ChapterNumber = g.Key,
                Contents = g
                    .Select(item => item.Content)
                    .DistinctBy(item => item.ContentId)
                    .OrderBy(item => item.ContentId)
            })
            .OrderBy(item => item.ChapterNumber)
            .ToList();

        return TypedResults.Ok(new ResourceItemsByChapterResponse { Chapters = groupedContent });
    }

    private static IEnumerable<int> GetPassageChapterOverlapWithBook((BookId StartBook, int StartChapter, BookId EndBook, int EndChapter) passage, BookId bookId, int lastChapterInBook)
    {
        var startChapter = passage.StartBook == bookId ? passage.StartChapter : 1;
        var endChapter = passage.EndBook == bookId ? passage.EndChapter : lastChapterInBook;
        return Enumerable.Range(startChapter, endChapter - startChapter + 1);
    }
}

public record IntermediateResourceItem
{
    public required int StartVerseId { get; init; }
    public required int EndVerseId { get; init; }
    public required int ContentId { get; init; }
    public required int Version { get; init; }
    public required int ContentSize { get; init; }
    public required int? InlineMediaSize { get; init; }
    public required ResourceContentMediaType MediaType { get; init; }
    public required int LanguageId { get; init; }
    public required int ResourceId { get; init; }
    public required ParentResourceEntity ParentResource { get; init; }
}