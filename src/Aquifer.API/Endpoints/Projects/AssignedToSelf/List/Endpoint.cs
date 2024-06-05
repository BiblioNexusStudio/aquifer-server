using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
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
        List<ResourceContentStatus> inProgressStatuses =
        [
            ResourceContentStatus.AquiferizeInProgress,
            ResourceContentStatus.TranslationInProgress
        ];

        List<ResourceContentStatus> inReviewStatuses =
        [
            ResourceContentStatus.AquiferizePublisherReview,
            ResourceContentStatus.AquiferizeReviewPending,
            ResourceContentStatus.TranslationPublisherReview,
            ResourceContentStatus.TranslationReviewPending
        ];
        var user = await userService.GetUserFromJwtAsync(ct);

        return await dbContext.Projects.Where(p => p.ProjectManagerUserId == user.Id && p.ActualPublishDate == null).Select(x =>
            new Response
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
                Counts = new ProjectResourceStatusCounts
                {
                    InProgress = x.ResourceContents.Count(rc => inProgressStatuses.Contains(rc.Status)),
                    InReview = x.ResourceContents.Count(rc => inReviewStatuses.Contains(rc.Status)),
                    Completed = x.ResourceContents.Count(rc => rc.Status == ResourceContentStatus.Complete)
                }
            }).ToListAsync(ct);
    }
}