using Aquifer.API.Common;
using Aquifer.API.Utilities;
using Aquifer.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Passages;

public class PassagesModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("passages");
        group.MapGet("language/{languageId:int}/resource/{resourceTypeName}", GetPassagesByLanguageAndResource);
        group.MapGet("{passageId:int}/language/{languageId:int}", GetPassageDetailsForLanguage);
        return endpoints;
    }

    private async Task<Results<Ok<List<PassagesBookResponse>>, NotFound>> GetPassagesByLanguageAndResource(
        int languageId,
        string resourceTypeName,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        if (!Constants.RootResourceTypes.Contains(resourceTypeName))
        {
            return TypedResults.NotFound();
        }

        var resourceType =
            await dbContext.ResourceTypes.SingleOrDefaultAsync(rt => rt.ShortName == resourceTypeName,
                cancellationToken);

        var passagesByBook = (await dbContext.Passages
                .Where(p => p.PassageResources.Any(pr =>
                    pr.Resource.Type == resourceType &&
                    pr.Resource.ResourceContents.Any(rc => rc.LanguageId == languageId)))
                .Select(passage =>
                    new PassagesResponsePassage
                    {
                        Id = passage.Id,
                        PassageStartDetails = BibleUtilities.TranslateVerseId(passage.StartVerseId),
                        PassageEndDetails = BibleUtilities.TranslateVerseId(passage.EndVerseId)
                    }
                ).ToListAsync(cancellationToken))
            .GroupBy(passage => passage.BookId)
            .Select(grouped => new PassagesBookResponse
            {
                BookId = grouped.Key, Passages = grouped.OrderBy(p => p.StartChapter).ThenBy(p => p.StartVerse)
            })
            .OrderBy(book => book.BookId).ToList();

        return TypedResults.Ok(passagesByBook);
    }

    private async Task<Results<Ok<PassageDetailsResponse>, NotFound>> GetPassageDetailsForLanguage(
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

        var passageResourceContent = await dbContext.PassageResources
            // find all passages that overlap with the current passage
            .Where(pr => (pr.Passage.StartVerseId >= passage.StartVerseId &&
                          pr.Passage.StartVerseId <= passage.EndVerseId) ||
                         (pr.Passage.EndVerseId >= passage.StartVerseId &&
                          pr.Passage.EndVerseId <= passage.EndVerseId))
            .SelectMany(pr => pr.Resource.ResourceContents.Where(rc => rc.LanguageId == languageId).Select(rc =>
                new PassageDetailsResponseContent
                {
                    ContentId = rc.Id,
                    ContentSize = rc.ContentSize,
                    MediaTypeName = rc.MediaType,
                    TypeName = pr.Resource.Type.ShortName
                }))
            .ToListAsync(cancellationToken);

        var verseResourceContent = await dbContext.VerseResources
            // find all verses contained within the current passage
            .Where(vr => vr.VerseId >= passage.StartVerseId && vr.VerseId <= passage.EndVerseId)
            .SelectMany(vr => vr.Resource.ResourceContents.Where(rc => rc.LanguageId == languageId).Select(rc =>
                new PassageDetailsResponseContent
                {
                    ContentId = rc.Id,
                    ContentSize = rc.ContentSize,
                    MediaTypeName = rc.MediaType,
                    TypeName = vr.Resource.Type.ShortName
                }))
            .ToListAsync(cancellationToken);

        var supportingResourceContent = await dbContext.PassageResources.Where(pr => pr.PassageId == passageId)
            .SelectMany(pr => pr.Resource.AssociatedResourceChildren
                .SelectMany(sr => sr.ResourceContents.Where(rc => rc.LanguageId == languageId)
                    .Select(rc => new PassageDetailsResponseContent
                    {
                        ContentId = rc.Id,
                        ContentSize = rc.ContentSize,
                        MediaTypeName = rc.MediaType,
                        TypeName = sr.Type.ShortName
                    })))
            .ToListAsync(cancellationToken);

        return TypedResults.Ok(new PassageDetailsResponse
        {
            Id = passage.Id,
            PassageStartDetails = BibleUtilities.TranslateVerseId(passage.StartVerseId),
            PassageEndDetails = BibleUtilities.TranslateVerseId(passage.EndVerseId),
            Contents = passageResourceContent.Concat(verseResourceContent).Concat(supportingResourceContent)
                .DistinctBy(rc => rc.ContentId)
        });
    }
}