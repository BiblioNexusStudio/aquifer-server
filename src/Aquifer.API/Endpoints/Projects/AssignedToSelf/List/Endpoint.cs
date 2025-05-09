﻿using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Projects.AssignedToSelf.List;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : EndpointWithoutRequest<List<Response>>
{
    public override void Configure()
    {
        Get("/projects/assigned-to-self");
        Permissions(PermissionName.ReadProject);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var projects = await GetProjectsAsync(ct);
        await SendOkAsync(projects, ct);
    }

    private async Task<List<Response>> GetProjectsAsync(CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);

        var projects = await dbContext.Projects
            .Where(p => p.ProjectManagerUserId == user.Id && p.ActualPublishDate == null)
            .Select(x => new Response
            {
                Id = x.Id,
                Name = x.Name,
                Company = x.Company.Name,
                Language = x.Language.EnglishDisplay,
                ProjectPlatform = x.ProjectPlatform.Name,
                IsStarted = x.Started != null,
                Days =
                    x.ProjectedDeliveryDate.HasValue
                        ? x.ProjectedDeliveryDate.Value.DayNumber - DateOnly.FromDateTime(DateTime.UtcNow).DayNumber
                        : null,
            })
            .ToListAsync(ct);

        var countsPerProject =
            await ProjectResourceStatusCountHelper.GetResourceStatusCountsPerProjectAsync(
                projects.Select(p => p.Id).ToList(),
                dbContext,
                ct);
        foreach (var p in projects)
        {
            p.Counts = countsPerProject.TryGetValue(p.Id, out var projectResourceStatusCounts)
                ? projectResourceStatusCounts
                : new ProjectResourceStatusCounts();
        }

        return projects;
    }
}