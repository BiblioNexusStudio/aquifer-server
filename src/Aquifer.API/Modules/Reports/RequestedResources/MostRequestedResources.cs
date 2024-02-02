using Aquifer.Data;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Reports.RequestedResources;

public class MostRequestedResources
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

    public static async Task<IResult> HandleAsync(
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var mostRequestedResources = await dbContext.Database
            .SqlQuery<MostRequestedResourcesResponse>($"exec ({MostRequestedResourcesQuery})")
            .ToListAsync(cancellationToken);

        return TypedResults.Ok(mostRequestedResources);
    }
}