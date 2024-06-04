using Aquifer.API.Common;
using Aquifer.API.Common.Dtos;
using Aquifer.API.Services;
using Aquifer.Common.Extensions;
using Aquifer.Data;
using Aquifer.Data.Entities;
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

        var wordCounts = await GetWordCounts(req.ProjectId, ct);
        foreach (var item in project.Items)
        {
            item.WordCount = wordCounts.SingleOrDefault(x => x.Id == item.ResourceContentId)?.WordCount ?? 0;
        }

        project.Items = project.Items.OrderBy(x => x.SortOrder).ThenBy(x => x.EnglishLabel);
        await SendOkAsync(project, ct);
    }

    private async Task<Response?> GetProjectAsync(Request req, CancellationToken ct)
    {
        List<ResourceContentStatus> notStartedStatuses = [ResourceContentStatus.New, ResourceContentStatus.TranslationNotStarted];

        List<ResourceContentStatus> inProgressStatuses =
        [
            ResourceContentStatus.AquiferizeInProgress, ResourceContentStatus.TranslationInProgress
        ];

        List<ResourceContentStatus> inManagerReviewStatuses =
        [
            ResourceContentStatus.AquiferizeManagerReview, ResourceContentStatus.TranslationManagerReview
        ];

        List<ResourceContentStatus> inPublisherReviewStatuses =
        [
            ResourceContentStatus.AquiferizeInReview,
            ResourceContentStatus.AquiferizeReviewPending,
            ResourceContentStatus.TranslationInReview,
            ResourceContentStatus.TranslationReviewPending
        ];

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
                Items = x.ResourceContents.SelectMany(rc => rc.Versions.OrderByDescending(v => v.Created).Take(1).Select(rcv =>
                    new ProjectResourceItem
                    {
                        ResourceContentId = rc.Id,
                        EnglishLabel = rc.Resource.EnglishLabel,
                        ParentResourceName = rc.Resource.ParentResource.DisplayName,
                        StatusDisplayName = rc.Status.GetDisplayName(),
                        AssignedUserName = rcv.AssignedUser == null ? null : $"{rcv.AssignedUser.FirstName} {rcv.AssignedUser.LastName}",
                        SortOrder = rc.Resource.SortOrder
                    })),
                Counts = new ProjectResourceStatusCounts
                {
                    NotStarted = x.ResourceContents.Count(rc => notStartedStatuses.Contains(rc.Status)),
                    InProgress = x.ResourceContents.Count(rc => inProgressStatuses.Contains(rc.Status)),
                    InManagerReview = x.ResourceContents.Count(rc => inManagerReviewStatuses.Contains(rc.Status)),
                    InPublisherReview = x.ResourceContents.Count(rc => inPublisherReviewStatuses.Contains(rc.Status)),
                    Completed = x.ResourceContents.Count(rc => rc.Status == ResourceContentStatus.Complete)
                }
            }).SingleOrDefaultAsync(ct);
    }

    private async Task<bool> HasSameCompanyAsProject(int projectId, CancellationToken ct)
    {
        var self = await userService.GetUserWithCompanyFromJwtAsync(ct);
        return dbContext.Projects.Any(x => x.Id == projectId && self.CompanyId == x.CompanyId);
    }

    private async Task<List<ResourceContentWordCount>> GetWordCounts(int projectId, CancellationToken ct)
    {
        const string query = """
                             SELECT
                                 RC.Id, Snapshots.WordCount
                             FROM ResourceContents RC
                                 INNER JOIN ResourceContentVersions RCV ON RCV.ResourceContentId = RC.Id
                                 CROSS APPLY (
                                     SELECT TOP 1 WordCount
                                     FROM ResourceContentVersionSnapshots
                                     WHERE ResourceContentVersionId = RCV.Id
                                     ORDER BY Created ASC
                                 ) Snapshots
                             WHERE RC.Id IN (SELECT ResourceContentId FROM ProjectResourceContents WHERE ProjectId = {0})
                             """;

        return await dbContext.Database.SqlQueryRaw<ResourceContentWordCount>(query, projectId).ToListAsync(ct);
    }
}

public class ResourceContentWordCount
{
    public required int Id { get; set; }
    public required int WordCount { get; set; }
}