using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Reports.Dynamic.List;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : EndpointWithoutRequest<IEnumerable<Response>>
{
    public override void Configure()
    {
        Get("/reports/dynamic");
        Permissions(PermissionName.ReadReports, PermissionName.ReadReportsInCompany);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var reportsWithAllowedRoles = await dbContext.Reports
            .Where(r => r.Enabled)
            .Select(
                r => new
                {
                    r.Slug,
                    r.Name,
                    r.Description,
                    r.Type,
                    r.AllowedRoles,
                    r.ShowInDropdown
                })
            .ToListAsync(ct);

        var reports = reportsWithAllowedRoles.Where(
                r => ReportRoleHelper.RoleIsAllowedForReport(r.AllowedRoles, userService.GetUserRoleFromJwt()))
            .Select(
                r => new Response
                {
                    Name = r.Name,
                    Description = r.Description,
                    Type = r.Type,
                    Slug = r.Slug,
                    ShowInDropdown = r.ShowInDropdown
                }
            );

        await SendOkAsync(reports, ct);
    }
}