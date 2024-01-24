using Aquifer.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Languages;

public class LanguagesModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("languages").WithTags("Languages");
        group.MapGet("/", GetLanguages).CacheOutput(x => x.Expire(TimeSpan.FromHours(1)));

        return endpoints;
    }

    private async Task<Ok<List<LanguageResponse>>> GetLanguages(AquiferDbContext dbContext)
    {
        var languages = await dbContext.Languages
            .Select(x => new LanguageResponse(x.Id, x.ISO6393Code, x.EnglishDisplay, x.ScriptDirection))
            .ToListAsync();

        return TypedResults.Ok(languages);
    }
}