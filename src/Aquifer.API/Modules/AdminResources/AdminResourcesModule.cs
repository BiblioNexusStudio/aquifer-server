using Aquifer.API.Common;
using Aquifer.API.Modules.AdminResources.Aquiferization;
using Aquifer.API.Modules.AdminResources.Assignment;
using Aquifer.API.Modules.AdminResources.ResourceContent;
using Aquifer.API.Modules.AdminResources.ResourceContentSummary;
using Aquifer.API.Modules.AdminResources.ResourceReview;
using Aquifer.API.Modules.AdminResources.ResourcesList;
using Aquifer.API.Modules.AdminResources.ResourcesSummary;

namespace Aquifer.API.Modules.AdminResources;

public class AdminResourcesModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("admin/resources").WithTags("Resources (Admin)");

        group.MapPost("content/{contentId:int}/aquiferize", AquiferizationEndpoints.Aquiferize)
            .RequireAuthorization(PermissionName.AquiferizeContent);

        group.MapPost("content/{contentId:int}/publish", AquiferizationEndpoints.Publish)
            .RequireAuthorization(PermissionName.PublishContent);

        group.MapPost("content/{contentId:int}/unpublish", AquiferizationEndpoints.Unpublish)
            .RequireAuthorization(PermissionName.PublishContent);

        group.MapPost("content/{contentId:int}/assign-editor", AssignmentEndpoints.AssignEditor)
            .RequireAuthorization([PermissionName.AssignOverride, PermissionName.AssignContent]);

        group.MapPost("content/{contentId:int}/send-review", ResourceReviewEndpoints.SendToReview)
            .RequireAuthorization(PermissionName.SendReviewContent);

        group.MapPost("content/{contentId:int}/review", ResourceReviewEndpoints.Review)
            .RequireAuthorization(PermissionName.ReviewContent);

        // could add RequireAuthorization() with no policy on these to force a JWT for at least readonly.
        group.MapGet("summary", GetResourcesSummaryEndpoints.Get)
            .CacheOutput(x => x.Expire(TimeSpan.FromHours(1)));

        group.MapGet("content/summary/{resourceContentId:int}",
            GetResourceContentSummaryEndpoints.GetByResourceContentId);

        group.MapPut("content/summary/{resourceContentId:int}",
                UpdateResourcesSummaryEndpoints.UpdateResourceContentSummaryItem)
            .RequireAuthorization(PermissionName.EditContent);

        group.MapGet("content/statuses", ResourceContentStatusEndpoints.GetList)
            .CacheOutput(x => x.Expire(TimeSpan.FromHours(1)));

        group.MapGet("list", ResourcesListEndpoints.Get);

        group.MapGet("list/count", ResourcesListEndpoints.GetCount);

        return endpoints;
    }

    public IServiceCollection RegisterModule(IServiceCollection services)
    {
        return services.AddScoped<IAdminResourceHistoryService, AdminResourceHistoryService>();
    }
}