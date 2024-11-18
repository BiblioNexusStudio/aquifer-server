using Aquifer.Common;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Passages;

public class PassagesModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("passages").WithTags("Passages");
        group.MapGet("language/{languageId:int}/resource/{parentResourceId:int}", GetPassagesByLanguageAndResource);
        return endpoints;
    }

    private async Task<Results<Ok<List<PassagesByBookResponse>>, NotFound>> GetPassagesByLanguageAndResource(
        int languageId,
        int parentResourceId,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        if (!Constants.PredeterminedPassageGuideIds.Contains(parentResourceId))
        {
            return TypedResults.NotFound();
        }

        var parentResource = await dbContext.ParentResources.SingleOrDefaultAsync(pr => pr.Id == parentResourceId, cancellationToken);

        var passagesByBook = (await dbContext.Passages
                .Where(p => p.PassageResources.Any(pr =>
                    pr.Resource.ParentResource == parentResource &&
                    pr.Resource.ResourceContents.Any(rc =>
                        rc.Versions.Any(rcv => rcv.IsPublished) && rc.LanguageId == languageId)))
                .Select(passage =>
                    new
                    {
                        passage.Id,
                        PassageStartDetails = BibleUtilities.TranslateVerseId(passage.StartVerseId),
                        PassageEndDetails = BibleUtilities.TranslateVerseId(passage.EndVerseId)
                    }).ToListAsync(cancellationToken))
            .GroupBy(passage => passage.PassageStartDetails.bookId)
            .OrderBy(grouped => grouped.Key)
            .Select(grouped => new PassagesByBookResponse
            {
                BookCode = BibleBookCodeUtilities.CodeFromId(grouped.Key),
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
}