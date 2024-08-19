using Aquifer.API.Common;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Reports.Dynamic.List;

public class Endpoint(AquiferDbContext dbContext) : EndpointWithoutRequest<IEnumerable<Response>>
{
    public override void Configure()
    {
        Get("/reports/dynamic");
        Permissions(PermissionName.ReadReports);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var reports = await dbContext.Reports
            .Where(r => r.Enabled)
            .Select(r => new Response { Id = r.Id, Name = r.Name, Description = r.Description, Type = r.Type })
            .ToListAsync(ct);

        await SendOkAsync(reports, ct);
    }
}