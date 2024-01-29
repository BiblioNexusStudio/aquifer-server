using Aquifer.API.Modules.Reports.RequestedResources;
using Aquifer.API.Modules.Reports.ResourceItemTotals;

namespace Aquifer.API.Modules.Reports;

public class ReportsModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("reports");
        group.MapGet("aquiferizations/monthly",
                MonthlyReportsEndpoints.AquiferizationCompleteAndStart)
            .CacheOutput(x => x.Expire(TimeSpan.FromMinutes(5)))
            .RequireAuthorization();
        group.MapGet("resources/item-totals", ResourceItemTotalsEndpoint.HandleAsync)
            .CacheOutput(x => x.Expire(TimeSpan.FromMinutes(5))).RequireAuthorization();

        group.MapGet("translations/monthly",
                MonthlyReportsEndpoints.TranslationCompleteAndStart)
            .CacheOutput(x => x.Expire(TimeSpan.FromMinutes(5)))
            .RequireAuthorization();

        group.MapGet("resources/most-requested-resources",
                MostRequestedResources.HandleAsync)
            .CacheOutput(x => x.Expire(TimeSpan.FromMinutes(5)))
            .RequireAuthorization();
        return endpoints;
    }
}