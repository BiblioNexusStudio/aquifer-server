using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Projects.List;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : EndpointWithoutRequest<List<Response>>
{
    public override void Configure()
    {
        Get("/projects");
        Permissions(PermissionName.ReadProject, PermissionName.ReadProjectsInCompany);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var projects = await GetProjectsAsync(ct);

        await SendOkAsync(projects, ct);
    }

    private async Task<List<Response>> GetProjectsAsync(CancellationToken ct)
    {
        var self = await userService.GetUserFromJwtAsync(ct);

        var projects = await dbContext.Projects
            .Where(
                x =>
                    (x.ActualPublishDate == null ||
                     x.ActualPublishDate.Value > DateOnly.FromDateTime(DateTime.UtcNow).AddDays(-30)) &&
                    (userService.HasPermission(PermissionName.ReadProject) ||
                     userService.HasPermission(PermissionName.ReadProjectsInCompany) && self.CompanyId == x.CompanyId))
            .Include(x => x.ProjectResourceContents)
            .Select(
                p => new Response
                {
                    Id = p.Id,
                    Name = p.Name,
                    Company = p.Company.Name,
                    Language = p.Language.EnglishDisplay,
                    ProjectPlatform = p.ProjectPlatform.Name,
                    ProjectLead = $"{p.ProjectManagerUser.FirstName} {p.ProjectManagerUser.LastName}",
                    Manager = p.CompanyLeadUser != null
                        ? $"{p.CompanyLeadUser.FirstName} {p.CompanyLeadUser.LastName}"
                        : null,
                    Resource = p.ProjectResourceContents.First().ResourceContent.Resource.ParentResource.DisplayName,
                    ItemCount = p.ProjectResourceContents.Count,
                    WordCount = p.SourceWordCount,
                    IsStarted = p.Started != null,
                    IsCompleted = p.ActualPublishDate != null,
                    Days =
                        p.ProjectedDeliveryDate.HasValue
                            ? p.ProjectedDeliveryDate.Value.DayNumber - DateOnly.FromDateTime(DateTime.UtcNow).DayNumber
                            : null
                })
            .ToListAsync(ct);

        var statusCountsPerProject =
            await ProjectResourceStatusCountHelper.GetResourceStatusCountsPerProjectAsync(
                projects.Select(x => x.Id).ToList(),
                dbContext,
                ct);

        foreach (var p in projects)
        {
            p.Counts = statusCountsPerProject[p.Id];
        }

        return projects;
    }
}