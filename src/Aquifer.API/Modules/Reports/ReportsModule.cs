namespace Aquifer.API.Modules.Reports;

public class ReportsModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("reports");
        group.MapGet("monthly/aquiferization", GetMonthlyReportsEndpoints.GetAquiferizationCompleteAndStart).RequireAuthorization();

        return endpoints;
    }
}