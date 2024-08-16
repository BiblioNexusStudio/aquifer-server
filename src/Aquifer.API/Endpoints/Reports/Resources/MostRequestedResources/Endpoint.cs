using Aquifer.API.Helpers;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Reports.Resources.MostRequestedResources;

public class Endpoint(AquiferDbContext dbContext) : EndpointWithoutRequest<IEnumerable<Response>>
{
    private const string MostRequestedResourcesQuery =
        """
        SELECT TOP 100 PR.DisplayName AS Resource, R.EnglishLabel AS Label, L.EnglishDisplay AS Language, COUNT(RCR.ResourceContentId) AS Count
        FROM ResourceContentRequests RCR
                 INNER JOIN ResourceContents RC ON RC.Id = RCR.ResourceContentId
                 INNER JOIN Resources R ON R.Id = RC.ResourceId
                 INNER JOIN ParentResources PR ON PR.Id = R.ParentResourceId
                 INNER JOIN Languages L ON L.Id = RC.LanguageId
        WHERE RCR.Created >= DATEADD(DAY, -30, GETUTCDATE())
        GROUP BY RCR.ResourceContentId, L.EnglishDisplay, R.EnglishLabel, PR.DisplayName
        ORDER BY Count DESC;
        """;

    public override void Configure()
    {
        Get("/reports/resources/most-requested-resources");
        EndpointHelpers.ServerCacheInSeconds(EndpointHelpers.TenMinutesInSeconds);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var mostRequestedResources = await dbContext.Database
            .SqlQuery<Response>($"exec ({MostRequestedResourcesQuery})")
            .ToListAsync(ct);

        await SendOkAsync(mostRequestedResources, ct);
    }
}