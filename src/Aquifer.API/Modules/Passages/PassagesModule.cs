using Aquifer.API.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Aquifer.API.Modules.Passages;

public class PassagesModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("passages");
        group.MapGet("/", GetAllPassages);
        group.MapGet("/{id:int}", Get);
        return endpoints;
    }

    public Ok<string> GetAllPassages()
    {
        return TypedResults.Ok($"{nameof(GetAllPassages)}");
    }

    public async Task<Results<Ok<PassageResponse>, NotFound>> Get(int id, AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var passage = await dbContext.Passages.Where(x => x.Id == id).Select(x => new PassageResponse
        {
            StartVerseId = x.StartVerseId,
            EndVerseId = x.EndVerseId,
            Resources = x.PassageResources.Select(y => new PassageResourceResponse
            {
                DisplayName = y.Resource.ResourceContent.DisplayName,
                Content = JsonSerializer.Deserialize<object>(y.Resource.ResourceContent.Content,
                    JsonSerializerOptions.Default)
            }).ToList()
        }).SingleOrDefaultAsync(cancellationToken);

        return passage != null ? TypedResults.Ok(passage) : TypedResults.NotFound();
    }
}