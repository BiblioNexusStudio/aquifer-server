namespace Aquifer.API.Modules.Reports;

public class ReportsModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("reports");
        group.MapGet("monthly/aquiferization", MonthlyReportsEndpoints.AquiferizationCompleteAndStart).RequireAuthorization();

        return endpoints;
    }
}