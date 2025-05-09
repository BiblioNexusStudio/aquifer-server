using Aquifer.API.Common;
using Aquifer.API.Helpers;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Companies.List;

public class Endpoint(AquiferDbContext dbContext) : EndpointWithoutRequest<List<Response>>
{
    public override void Configure()
    {
        Get("/companies");
        ResponseCache(EndpointHelpers.OneHourInSeconds);
        Permissions(PermissionName.ReadUsers);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var companies = await dbContext.Companies
            .OrderBy(x => x.Name)
            .Select(company => new Response
            {
                Id = company.Id,
                Name = company.Name,
            })
            .ToListAsync(ct);

        await SendOkAsync(companies, ct);
    }
}