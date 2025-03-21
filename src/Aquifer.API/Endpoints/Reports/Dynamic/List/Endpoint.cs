using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Reports.Dynamic.List;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : EndpointWithoutRequest<IEnumerable<Response>>
{
    public override void Configure()
    {
        Get("/reports/dynamic");
        Permissions(PermissionName.ReadReports);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);

        var reportsWithAllowedRoles = await dbContext.Reports
            .Where(r => r.Enabled)
            .Select(
                r => new Report
                {
                    Slug = r.Slug,
                    Name = r.Name,
                    Description = r.Description,
                    Type = r.Type,
                    AllowedRoles = r.AllowedRoles
                })
            .ToListAsync(ct);

        var reports = reportsWithAllowedRoles.Where(
                r => ReportRoleHelper.RoleIsAllowedForReport(r.AllowedRoles, user.Role))
            .Select(
                r => new Response
                {
                    Name = r.Name,
                    Description = r.Description,
                    Type = r.Type,
                    Slug = r.Slug
                }
            );

        await SendOkAsync(reports, ct);
    }

    private record Report
    {
        public required string Slug { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public ReportType Type { get; set; }
        public string? AllowedRoles { get; set; }
    }
}