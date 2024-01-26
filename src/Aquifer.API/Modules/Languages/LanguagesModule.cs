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
            .Select(x => new LanguageResponse
            {
                Id = x.Id,
                Iso6393Code = x.ISO6393Code,
                EnglishDisplay = x.EnglishDisplay,
                DisplayName = x.DisplayName,
                Enabled = x.Enabled,
                ScriptDirection = x.ScriptDirection
            })
            .ToListAsync();

        return TypedResults.Ok(languages);
    }
}