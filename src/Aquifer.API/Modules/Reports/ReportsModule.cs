using Aquifer.API.Modules.Resources.LanguageResources;
using Aquifer.API.Modules.Resources.ParentResources;
using Aquifer.API.Modules.Resources.ResourceContentBatch;
using Aquifer.API.Modules.Resources.ResourceContentItem;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Aquifer.API.Modules.Reports;

public class ReportsModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("reports").WithTags("Reports");
        group.MapGet("monthly/aquiferization", ResourceContentItemEndpoints.GetResourceContentById);

        return endpoints;
    }
}