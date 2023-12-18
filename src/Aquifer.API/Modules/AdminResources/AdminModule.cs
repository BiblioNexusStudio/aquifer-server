using Aquifer.API.Common;
using Aquifer.API.Modules.AdminResources.Aquiferization;
using Aquifer.API.Modules.AdminResources.Assignment;
using Aquifer.API.Modules.AdminResources.ResourceContent;
using Aquifer.API.Modules.AdminResources.ResourceContentSummary;
using Aquifer.API.Modules.AdminResources.ResourceReview;
using Aquifer.API.Modules.AdminResources.ResourcesList;
using Aquifer.API.Modules.AdminResources.ResourcesSummary;

namespace Aquifer.API.Modules.AdminResources;

public class AdminModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("admin/resources").WithTags("Resources (Admin)");

        group.MapPost("content/{contentId:int}/aquiferize", AquiferizationEndpoints.Aquiferize)
            .RequireAuthorization(PermissionName.AquiferizeContent);

        group.MapPost("content/{contentId:int}/publish", AquiferizationEndpoints.Publish)
            .RequireAuthorization(PermissionName.PublishContent);

        group.MapPost("content/{contentId:int}/unpublish", AquiferizationEndpoints.UnPublish)
            .RequireAuthorization(PermissionName.PublishContent);

        group.MapPost("content/{contentId:int}/assign-editor", AssignmentEndpoints.AssignEditor)
            .RequireAuthorization([PermissionName.AssignOverride, PermissionName.AssignContent]);

        group.MapPost("content/{contentId:int}/send-review", AquiferizationEndpoints.SendReview)
            .RequireAuthorization([PermissionName.SendReviewOverride, PermissionName.SendReviewContent]);

        group.MapPost("content/{contentId:int}/review", ResourceReviewEndpoints.Review)
            .RequireAuthorization(PermissionName.ReviewContent);

        group.MapGet("summary", GetResourcesSummaryEndpoints.Get)
            .CacheOutput(x => x.Expire(TimeSpan.FromHours(1))).RequireAuthorization();

        group.MapGet("content/summary/{resourceContentId:int}",
            GetResourceContentSummaryEndpoints.GetByResourceContentId).RequireAuthorization();

        group.MapPut("content/summary/{resourceContentId:int}",
                UpdateResourcesSummaryEndpoints.UpdateResourceContentSummaryItem)
            .RequireAuthorization(PermissionName.EditContent);

        group.MapGet("content/statuses", ResourceContentStatusEndpoints.GetList)
            .CacheOutput(x => x.Expire(TimeSpan.FromHours(1))).RequireAuthorization();

        group.MapGet("list", ResourcesListEndpoints.Get).RequireAuthorization();

        group.MapGet("list/count", ResourcesListEndpoints.GetCount).RequireAuthorization();

        return endpoints;
    }
}