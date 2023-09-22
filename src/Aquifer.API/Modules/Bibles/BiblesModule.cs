using Aquifer.API.Utilities;
using Aquifer.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Bibles;

public class BiblesModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("bibles");
        group.MapGet("language/{languageId:int}", GetBibleContentsByLanguage);

        return endpoints;
    }

    private async Task<Ok<List<BibleBookResponse>>> GetBibleContentsByLanguage(int languageId,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var bibles = await dbContext.Bibles.Where(x => x.LanguageId == languageId)
            .Select(x => new BibleBookResponse
            {
                LanguageId = x.LanguageId,
                Name = x.Name,
                Contents = x.BibleBookContents.Select(y => new BibleBookResponseContent
                {
                    BookId = y.BookId,
                    DisplayName = y.DisplayName,
                    TextUrl = y.TextUrl,
                    TextSize = y.TextSize,
                    AudioUrls = JsonUtilities.DefaultSerialize(y.AudioUrls),
                    AudioSize = y.AudioSize
                })
            }).ToListAsync(cancellationToken);

        return TypedResults.Ok(bibles);
    }
}