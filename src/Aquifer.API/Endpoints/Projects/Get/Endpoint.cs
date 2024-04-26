using Aquifer.API.Common;
using Aquifer.API.Common.Dtos;
using Aquifer.API.Helpers;
using Aquifer.Common.Extensions;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Projects.Get;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/projects/{ProjectId}");
        Permissions(PermissionName.ReadProject);
        Options(EndpointHelpers.SetCacheOption(1));
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var project = await GetProjectAsync(req, ct);

        if (project is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(project, ct);
    }

    private async Task<Response?> GetProjectAsync(Request req, CancellationToken ct)
    {
        List<ResourceContentStatus> notStartedStatuses = [ResourceContentStatus.New, ResourceContentStatus.TranslationNotStarted];

        List<ResourceContentStatus> inProgressStatuses =
        [
            ResourceContentStatus.AquiferizeInProgress,
            ResourceContentStatus.TranslationInProgress
        ];

        List<ResourceContentStatus> inReviewStatuses =
        [
            ResourceContentStatus.AquiferizeInReview,
            ResourceContentStatus.AquiferizeReviewPending,
            ResourceContentStatus.TranslationInReview,
            ResourceContentStatus.TranslationReviewPending
        ];

        return await dbContext.Projects.Where(x => x.Id == req.ProjectId).Include(x => x.CompanyLeadUser).Select(x => new Response
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
                    AssignedUserName = rcv.AssignedUser == null ? null : $"{rcv.AssignedUser.FirstName} {rcv.AssignedUser.LastName}"
                })),
            Counts = new ProjectResourceStatusCounts
            {
                NotStarted = x.ResourceContents.Count(rc => notStartedStatuses.Contains(rc.Status)),
                InProgress = x.ResourceContents.Count(rc => inProgressStatuses.Contains(rc.Status)),
                InReview = x.ResourceContents.Count(rc => inReviewStatuses.Contains(rc.Status)),
                Completed = x.ResourceContents.Count(rc => rc.Status == ResourceContentStatus.Complete)
            }
        }).SingleOrDefaultAsync(ct);
    }
}