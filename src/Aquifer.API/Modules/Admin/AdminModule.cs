using Aquifer.API.Modules.Admin.Aquiferization;

namespace Aquifer.API.Modules.Admin;

public class AdminModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("admin");
        group.MapPost("resources/content/{contentId:int}/aquiferize", AquiferizationEndpoints.Aquiferize)
            .RequireAuthorization("aquiferize");

        return endpoints;
    }
}