using Aquifer.API.Modules.Reports.DailyDownloadTotals;
using Aquifer.API.Modules.Reports.EditedResources;
using Aquifer.API.Modules.Reports.MonthlyReports;
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

        group.MapGet("bar-charts/daily-resource-downloads", DailyDownloadEndpoints.DailyResourceDownloadTotals)
            .CacheOutput(x => x.Expire(TimeSpan.FromMinutes(5))).RequireAuthorization();

        group.MapGet("translations/monthly",
                MonthlyReportsEndpoints.TranslationCompleteAndStart)
            .CacheOutput(x => x.Expire(TimeSpan.FromMinutes(5)))
            .RequireAuthorization();

        group.MapGet("resources/edited-last-thirty-days",
                EditedResourcesLastThirtyDays.HandleAsync)
            .CacheOutput(x => x.Expire(TimeSpan.FromMinutes(5)))
            .RequireAuthorization();

        group.MapGet("resources/most-requested-resources",
                MostRequestedResources.HandleAsync)
            .CacheOutput(x => x.Expire(TimeSpan.FromMinutes(5)))
            .RequireAuthorization();

        return endpoints;
    }
}