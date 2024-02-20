using Aquifer.API.Common;
using Aquifer.API.Helpers;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Projects.List;

public class Endpoint(AquiferDbContext dbContext) : EndpointWithoutRequest<List<Response>>
{
    public override void Configure()
    {
        Get("/projects");
        Permissions(PermissionName.ReadProject);
        Options(EndpointHelpers.SetCacheOption(1));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var projects = await GetProjectsAsync(ct);
        await SendOkAsync(projects, ct);
    }

    private async Task<List<Response>> GetProjectsAsync(CancellationToken ct)
    {
        List<ResourceContentStatus> inProgressStatuses =
        [
            ResourceContentStatus.AquiferizeInProgress,
            ResourceContentStatus.TranslationInProgress
        ];

        List<ResourceContentStatus> inReviewStatuses =
        [
            ResourceContentStatus.AquiferizeInReview,
            ResourceContentStatus.AquiferizeReviewPending,
            ResourceContentStatus.TranslationInProgress,
            ResourceContentStatus.TranslationReviewPending
        ];

        return await dbContext.Projects.Select(x => new Response
        {
            Id = x.Id,
            Name = x.Name,
            Company = x.Company.Name,
            Language = x.Language.EnglishDisplay,
            ProjectPlatform = x.ProjectPlatform.Name,
            ProjectLead = $"{x.ProjectManagerUser.FirstName} {x.ProjectManagerUser.LastName}",
            IsStarted = x.Started != null,
            Days =
                x.ProjectedDeliveryDate.HasValue
                    ? x.ProjectedDeliveryDate.Value.DayNumber - DateOnly.FromDateTime(DateTime.UtcNow).DayNumber
                    : null,
            Counts = new ProjectResourceStatusCounts
            {
                InProgress = x.ResourceContents.Count(rc => inProgressStatuses.Contains(rc.Status)),
                InReview = x.ResourceContents.Count(rc => inReviewStatuses.Contains(rc.Status)),
                Completed = x.ResourceContents.Count(rc => rc.Status == ResourceContentStatus.Complete)
            }
        }).ToListAsync(ct);
    }
}