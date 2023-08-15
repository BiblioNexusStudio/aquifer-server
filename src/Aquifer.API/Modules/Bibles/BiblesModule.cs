using Aquifer.API.Data;
using Aquifer.API.Utilities;
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
        AquiferDbContext dbContext, CancellationToken cancellationToken)
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
                    TextSizeKb = y.TextSizeKb,
                    AudioUrls = JsonUtility.DefaultSerialize(y.AudioUrls),
                    AudioSizeKb = y.AudioSizeKb
                })
            }).ToListAsync(cancellationToken);

        return TypedResults.Ok(bibles);
    }
}