using Microsoft.AspNetCore.Http.HttpResults;

namespace Aquifer.API.Modules.Passages;

public class PassagesModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("passages");
        group.MapGet("/", GetAllPassages);
        return endpoints;
    }

    public Ok<string> GetAllPassages()
    {
        return TypedResults.Ok($"{nameof(GetAllPassages)}");
    }
}