using Aquifer.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Reports.ResourceItemTotals;

public static class ResourceItemTotalsEndpoint
{
    private const string TotalsQuery =
        """
        SELECT (SELECT COUNT(DISTINCT RES.Id)
        FROM Resources RES
             INNER JOIN ResourceContents RC ON RC.ResourceId = RES.Id
        WHERE RC.Status IN (2, 4, 5)) AS AquiferizedResources,

        (SELECT COUNT(DISTINCT RES.Id)
        FROM Resources RES
             INNER JOIN ResourceContents RC ON RC.ResourceId = RES.Id
             INNER JOIN ResourceContentVersions RCV ON RCV.ResourceContentId = RC.Id
             INNER JOIN ResourceContentVersionStatusHistory RCVSH ON RCVSH.ResourceContentVersionId = RCV.Id
        WHERE RCVSH.Created > DATEADD(mm, DATEDIFF(mm, 0, GETDATE()), 0)
        AND RCVSH.Status = 2)       AS AquiferizedResourcesThisMonth;
        """;

    public static async Task<Ok<ResourceItemTotalsResponse>>
        HandleAsync(
            AquiferDbContext dbContext,
            CancellationToken cancellationToken)
    {
        var totals = await dbContext.Database
            .SqlQuery<ResourceItemTotalsResponse>($"exec ({TotalsQuery})")
            .ToListAsync(cancellationToken);

        return TypedResults.Ok(totals.Single());
    }
}