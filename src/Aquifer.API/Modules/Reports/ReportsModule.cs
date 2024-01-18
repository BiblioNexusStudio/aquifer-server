namespace Aquifer.API.Modules.Reports;

public class ReportsModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("reports").WithTags("Reports");
        group.MapGet("monthly/aquiferization", GetMonthlyReportsEndpoints.GetAquiferizationCompleteAndStart);

        return endpoints;
    }
}