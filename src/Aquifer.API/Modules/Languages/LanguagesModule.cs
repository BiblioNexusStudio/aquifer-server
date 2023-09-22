using Aquifer.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Languages;

public class LanguagesModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("languages");
        group.MapGet("/", GetLanguages);

        return endpoints;
    }

    public async Task<Ok<List<LanguageResponse>>> GetLanguages(AquiferDbContext dbContext)
    {
        var languages = await dbContext.Languages.Select(x => new LanguageResponse
        {
            Id = x.Id,
            Iso6393Code = x.ISO6393Code,
            EnglishDisplay = x.EnglishDisplay
        }).ToListAsync();

        return TypedResults.Ok(languages);
    }
}