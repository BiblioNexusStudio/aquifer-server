using Aquifer.API.Common;
using Aquifer.API.Modules.Admin.Aquiferization;
using Aquifer.API.Modules.Admin.Assignment;

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
        group.MapPost("resources/content/{contentId:int}/assign-editor", AssignmentEndpoints.AssignEditor)
            .RequireAuthorization([PermissionName.AssignOverride, PermissionName.AssignContent]);

        return endpoints;
    }
}