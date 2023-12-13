using Aquifer.API.Common;
using Aquifer.API.Utilities;
using Aquifer.Data;
using Aquifer.Data.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Aquifer.API.Modules.Resources;

namespace Aquifer.API.Modules.Passages;

public class PassagesModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("passages").WithTags("Passages");
        group.MapGet("language/{languageId:int}/resource/{parentResourceName}", GetPassagesByLanguageAndResource);
        group.MapGet("{passageId:int}/language/{languageId:int}", GetPassageDetailsForLanguage);
        return endpoints;
    }

    private async Task<Results<Ok<List<PassagesByBookResponse>>, NotFound>> GetPassagesByLanguageAndResource(
        int languageId,
        string parentResourceName,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        if (!Constants.RootParentResourceNames.Contains(parentResourceName))
        {
            return TypedResults.NotFound();
        }

        var parentResource =
            await dbContext.ParentResources.SingleOrDefaultAsync(rt => rt.ShortName == parentResourceName,
                cancellationToken);

        var passagesByBook = (await dbContext.Passages
                .Where(p => p.PassageResources.Any(pr =>
                    pr.Resource.ParentResource == parentResource &&
                    pr.Resource.ResourceContents.Any(rc => rc.Versions.Any(rcv => rcv.IsPublished) && rc.LanguageId == languageId)))
                .Select(passage =>
                    new
                    {
                        Id = passage.Id,
                        PassageStartDetails = BibleUtilities.TranslateVerseId(passage.StartVerseId),
                        PassageEndDetails = BibleUtilities.TranslateVerseId(passage.EndVerseId)
                    }
                ).ToListAsync(cancellationToken))
            .GroupBy(passage => passage.PassageStartDetails.bookId)
            .OrderBy(grouped => grouped.Key)
            .Select(grouped => new PassagesByBookResponse
            {
                BookCode = BookCodes.CodeFromEnum(grouped.Key),
                Passages = grouped.OrderBy(p => p.PassageStartDetails.chapter).ThenBy(p => p.PassageStartDetails.verse)
                    .Select(p =>
                        new PassageResponse
                        {
                            Id = p.Id,
                            PassageStartDetails = p.PassageStartDetails,
                            PassageEndDetails = p.PassageEndDetails
                        })
            })
            .ToList();

        return TypedResults.Ok(passagesByBook);
    }

    private async Task<Results<Ok<PassageWithResourceItemsResponse>, NotFound>> GetPassageDetailsForLanguage(
        int passageId,
        int languageId,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var passage = await dbContext.Passages.FindAsync(passageId, cancellationToken);
        if (passage == null)
        {
            return TypedResults.NotFound();
        }

        int englishLanguageId = (await dbContext.Languages.Where(language => language.ISO6393Code.ToLower() == "eng")
            .FirstOrDefaultAsync(cancellationToken))?.Id ?? -1;

        var passageResourceContent = await dbContext.PassageResources
            // find all passages that overlap with the current passage
            .Where(pr => (pr.Passage.StartVerseId >= passage.StartVerseId &&
                          pr.Passage.StartVerseId <= passage.EndVerseId) ||
                         (pr.Passage.EndVerseId >= passage.StartVerseId &&
                          pr.Passage.EndVerseId <= passage.EndVerseId))
            .SelectMany(pr => pr.Resource.ResourceContents
                .Where(rc => rc.LanguageId == languageId || (rc.LanguageId == englishLanguageId &&
                                                             Constants.FallbackToEnglishForMediaTypes.Contains(
                                                                 rc.MediaType)))
                .SelectMany(rc => rc.Versions.Where(rcv => rcv.IsPublished)
                    .Select(rcv =>
                        new
                        {
                            ContentId = rc.Id,
                            rcv.ContentSize,
                            MediaTypeName = rc.MediaType,
                            rc.LanguageId,
                            pr.ResourceId,
                            ParentResourceName = pr.Resource.ParentResource.ShortName
                        })))
            .ToListAsync(cancellationToken);

        var verseResourceContent = await dbContext.VerseResources
            // find all verses contained within the current passage
            .Where(vr => vr.VerseId >= passage.StartVerseId && vr.VerseId <= passage.EndVerseId)
            .SelectMany(vr => vr.Resource.ResourceContents
                .Where(rc => rc.LanguageId == languageId || (rc.LanguageId == englishLanguageId &&
                                                             Constants.FallbackToEnglishForMediaTypes.Contains(
                                                                 rc.MediaType)))
                .SelectMany(rc => rc.Versions.Where(rcv => rcv.IsPublished)
                    .Select(rcv =>
                        new
                        {
                            ContentId = rc.Id,
                            rcv.ContentSize,
                            MediaTypeName = rc.MediaType,
                            rc.LanguageId,
                            vr.ResourceId,
                            ParentResourceName = vr.Resource.ParentResource.ShortName
                        }))
            )
            .ToListAsync(cancellationToken);

        var associatedResourceContent = await dbContext.PassageResources.Where(pr => pr.PassageId == passageId)
            .SelectMany(pr => pr.Resource.AssociatedResourceChildren
                .SelectMany(sr => sr.ResourceContents
                    .Where(rc => rc.LanguageId == languageId || (rc.LanguageId == englishLanguageId &&
                                                                 Constants.FallbackToEnglishForMediaTypes.Contains(
                                                                     rc.MediaType)))
                    .SelectMany(rc => rc.Versions.Where(rcv => rcv.IsPublished)
                        .Select(rcv =>
                            new
                            {
                                ContentId = rc.Id,
                                rcv.ContentSize,
                                MediaTypeName = rc.MediaType,
                                rc.LanguageId,
                                ResourceId = sr.Id,
                                ParentResourceName = sr.ParentResource.ShortName
                            }))))
            .ToListAsync(cancellationToken);

        // The above queries return resource contents in English + the current language (if available).
        // This filters them by grouping appropriately and selecting the current language resource (if available) then falling back to English.
        var filteredDownToOneLanguage = passageResourceContent.Concat(verseResourceContent)
            .Concat(associatedResourceContent)
            .GroupBy(rc => new { rc.MediaTypeName, rc.ResourceId })
            .Select(grc =>
            {
                var first = grc.OrderBy(rc => rc.LanguageId == languageId ? 0 : 1).First();
                return new ResourceItemResponse
                {
                    ContentId = first.ContentId,
                    ContentSize = first.ContentSize,
                    MediaTypeName = first.MediaTypeName,
                    ParentResourceName = first.ParentResourceName
                };
            });

        return TypedResults.Ok(new PassageWithResourceItemsResponse
        {
            Id = passage.Id,
            PassageStartDetails = BibleUtilities.TranslateVerseId(passage.StartVerseId),
            PassageEndDetails = BibleUtilities.TranslateVerseId(passage.EndVerseId),
            Contents = filteredDownToOneLanguage.DistinctBy(rc => rc.ContentId)
        });
    }
}
