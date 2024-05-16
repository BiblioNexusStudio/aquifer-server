﻿using Aquifer.API.Common;
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
                NotStarted = x.ResourceContents.Count(rc => notStartedStatuses.Contains(rc.Status)),
                InProgress = x.ResourceContents.Count(rc => inProgressStatuses.Contains(rc.Status)),
                InManagerReview = x.ResourceContents.Count(rc => inManagerReviewStatuses.Contains(rc.Status)),
                InPublisherReview = x.ResourceContents.Count(rc => inPublisherReviewStatuses.Contains(rc.Status)),
                Completed = x.ResourceContents.Count(rc => rc.Status == ResourceContentStatus.Complete)
            }
        }).ToListAsync(ct);
    }
}