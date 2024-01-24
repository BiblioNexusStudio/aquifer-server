using Aquifer.API.Modules.Reports.ResourceItemTotals;

namespace Aquifer.API.Modules.Reports;

public class ReportsModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("reports");
        //group.MapGet("monthly/aquiferization", MonthlyReportsEndpoints.AquiferizationCompleteAndStart).RequireAuthorization();
        group.MapGet("resources/item-totals", ResourceItemTotalsEndpoint.HandleAsync);

        return endpoints;
    }
}