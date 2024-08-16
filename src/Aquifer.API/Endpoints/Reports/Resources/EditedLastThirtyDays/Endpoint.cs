using Aquifer.API.Helpers;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Reports.Resources.EditedLastThirtyDays;

public class Endpoint(AquiferDbContext dbContext) : EndpointWithoutRequest<IEnumerable<Response>>
{
    private readonly string EditedResourcesLastThirtyDaysQuery =
        $"""
         SELECT DISTINCT PR.DisplayName AS Resource, R.EnglishLabel AS Label, L.EnglishDisplay AS Language
         FROM ResourceContentVersionStatusHistory RCVSH
             INNER JOIN ResourceContentVersions RCV ON RCV.Id = RCVSH.ResourceContentVersionId
             INNER JOIN ResourceContents RC ON RC.Id = RCV.ResourceContentId
             INNER JOIN Resources R ON R.Id = RC.ResourceId
             INNER JOIN ParentResources PR ON PR.Id = R.ParentResourceId
             INNER JOIN Languages L ON L.Id = RC.LanguageId
         WHERE RCVSH.Status IN (
                 {(int)ResourceContentStatus.AquiferizeInProgress},
                 {(int)ResourceContentStatus.AquiferizeReviewPending},
                 {(int)ResourceContentStatus.AquiferizePublisherReview},
                 {(int)ResourceContentStatus.TranslationInProgress},
                 {(int)ResourceContentStatus.TranslationReviewPending},
                 {(int)ResourceContentStatus.TranslationPublisherReview})
         AND RCVSH.Created >= DATEADD(DAY, -30, GETUTCDATE())
         """;

    public override void Configure()
    {
        Get("/reports/resources/edited-last-thirty-days");
        EndpointHelpers.ServerCacheInSeconds(EndpointHelpers.TenMinutesInSeconds);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var editedResources = await dbContext.Database
            .SqlQuery<Response>($"exec ({EditedResourcesLastThirtyDaysQuery})")
            .ToListAsync(ct);

        await SendOkAsync(editedResources, ct);
    }
}