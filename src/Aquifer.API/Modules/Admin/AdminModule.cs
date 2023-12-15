using Aquifer.API.Common;
using Aquifer.API.Modules.Admin.Aquiferization;
using Aquifer.API.Modules.Admin.Assignment;
using Aquifer.API.Modules.Admin.ResourceReview;

namespace Aquifer.API.Modules.Admin;

public class AdminModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("admin");
        group.MapPost("resources/content/{contentId:int}/aquiferize", AquiferizationEndpoints.Aquiferize)
            .RequireAuthorization(PermissionName.AquiferizeContent);
        group.MapPost("resources/content/{contentId:int}/publish", AquiferizationEndpoints.Publish)
            .RequireAuthorization(PermissionName.PublishContent);
        group.MapPost("resources/content/{contentId:int}/unpublish", AquiferizationEndpoints.UnPublish)
            .RequireAuthorization(PermissionName.PublishContent);
        group.MapPost("resources/content/{contentId:int}/assign-editor", AssignmentEndpoints.AssignEditor)
            .RequireAuthorization([PermissionName.AssignOverride, PermissionName.AssignContent]);
        group.MapPost("resources/content/{contentId:int}/send-review", AquiferizationEndpoints.SendReview)
            .RequireAuthorization([PermissionName.SendReviewOverride, PermissionName.SendReviewContent]);
        group.MapPost("resources/content/{contentId:int}/review", ResourceReviewEndpoints.Review).RequireAuthorization(PermissionName.ReviewContent);

        return endpoints;
    }
}