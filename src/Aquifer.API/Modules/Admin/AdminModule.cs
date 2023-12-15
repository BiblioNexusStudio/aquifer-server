using Aquifer.API.Common;
using Aquifer.API.Modules.Admin.Aquiferization;

namespace Aquifer.API.Modules.Admin;

public class AdminModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("admin");
        group.MapPost("resources/content/{contentId:int}/aquiferize", AquiferizationEndpoints.Aquiferize)
            .RequireAuthorization(PermissionName.Aquiferize);
        group.MapPost("resources/content/{contentId:int}/publish", AquiferizationEndpoints.Publish)
            .RequireAuthorization(PermissionName.Publish);
        group.MapPost("resources/content/{contentId:int}/unpublish", AquiferizationEndpoints.UnPublish)
            .RequireAuthorization(PermissionName.Publish);
        return endpoints;
    }
}