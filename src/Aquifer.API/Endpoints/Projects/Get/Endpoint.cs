﻿using Aquifer.API.Common;
using Aquifer.API.Common.Dtos;
using Aquifer.API.Services;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Projects.Get;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/projects/{ProjectId}");
        Permissions(PermissionName.ReadProject, PermissionName.ReadProjectsInCompany);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        if (userService.HasPermission(PermissionName.ReadProjectsInCompany) && !await HasSameCompanyAsProject(req.ProjectId, ct))
        {
            await SendForbiddenAsync(ct);
            return;
        }

        var project = await GetProjectAsync(req, ct);
        if (project is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var items = await GetProjectItems(req.ProjectId, ct);

        project.Items = items.OrderBy(x => x.SortOrder).ThenBy(x => x.EnglishLabel);
        await SendOkAsync(project, ct);
    }

    private async Task<Response?> GetProjectAsync(Request req, CancellationToken ct)
    {
        return await dbContext.Projects
            .Where(x => x.Id == req.ProjectId).Include(x => x.CompanyLeadUser).Select(x => new Response
            {
                Id = x.Id,
                Name = x.Name,
                Company = x.Company.Name,
                Language = x.Language.EnglishDisplay,
                CompanyLead = x.CompanyLeadUser != null ? $"{x.CompanyLeadUser.FirstName} {x.CompanyLeadUser.LastName}" : null,
                CompanyLeadUser = UserDto.FromUserEntity(x.CompanyLeadUser),
                ProjectPlatform = x.ProjectPlatform.Name,
                ProjectManager = $"{x.ProjectManagerUser.FirstName} {x.ProjectManagerUser.LastName}",
                ProjectManagerUser = UserDto.FromUserEntity(x.ProjectManagerUser)!,
                SourceWordCount = x.SourceWordCount,
                EffectiveWordCount = x.EffectiveWordCount,
                QuotedCost = x.QuotedCost,
                Started = x.Started,
                ActualDeliveryDate = x.ActualDeliveryDate,
                ActualPublishDate = x.ActualPublishDate,
                ProjectedDeliveryDate = x.ProjectedDeliveryDate,
                ProjectedPublishDate = x.ProjectedPublishDate,
                Counts = new ProjectResourceStatusCounts(x.ResourceContents)
            }).SingleOrDefaultAsync(ct);
    }

    private async Task<bool> HasSameCompanyAsProject(int projectId, CancellationToken ct)
    {
        var self = await userService.GetUserWithCompanyFromJwtAsync(ct);
        return dbContext.Projects.Any(x => x.Id == projectId && self.CompanyId == x.CompanyId);
    }

    private async Task<List<ProjectResourceItem>> GetProjectItems(int projectId, CancellationToken ct)
    {
        const string query = """
                             SELECT
                                 RC.Id AS ResourceContentId,
                                 Snapshots.WordCount,
                                 R.EnglishLabel,
                                 PR.DisplayName AS ParentResourceName,
                                 RC.Status,
                                 CASE
                                    WHEN RCVD.AssignedUserId IS NULL THEN NULL
                                    ELSE U.FirstName + ' ' + U.LastName
                                 END AS AssignedUserName,
                                 R.SortOrder
                             FROM ResourceContents RC
                             INNER JOIN Resources R ON R.Id = RC.ResourceId
                             INNER JOIN ParentResources PR ON PR.Id = R.ParentResourceId
                             LEFT JOIN ResourceContentVersions RCVD ON RCVD.ResourceContentId = RC.Id AND RCVD.IsDraft = 1
                             LEFT JOIN Users U on U.Id = RCVD.AssignedUserId
                             CROSS APPLY (
                                 SELECT TOP 1 COALESCE(RCVS.WordCount, RCV.WordCount) AS WordCount
                                 FROM ResourceContentVersions RCV
                                 LEFT JOIN ResourceContentVersionSnapshots RCVS ON RCVS.ResourceContentVersionId = RCV.Id
                                 WHERE RCV.ResourceContentId = RC.Id
                                 ORDER BY RCVS.Created ASC, RCV.Version DESC
                             ) Snapshots
                             WHERE RC.Id IN (
                                 SELECT ResourceContentId
                                 FROM ProjectResourceContents
                                 WHERE ProjectId = {0}
                             )
                             """;

        return await dbContext.Database.SqlQueryRaw<ProjectResourceItem>(query, projectId).ToListAsync(ct);
    }
}