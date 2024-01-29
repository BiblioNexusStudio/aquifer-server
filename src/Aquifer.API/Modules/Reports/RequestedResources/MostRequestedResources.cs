using Aquifer.Data;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Reports.RequestedResources;

/// <summary>
///     Handles the retrieval of the most requested resources.
/// </summary>
public class MostRequestedResources
{
    private const string MostRequestedResourcesQuery =
        """
        SELECT TOP 100 RCR.ResourceContentId, COUNT(RCR.ResourceContentId) AS Count, L.EnglishDisplay, R.EnglishLabel
        FROM ResourceContentRequests RCR
            INNER JOIN ResourceContents RC ON RC.Id = RCR.ResourceContentId
            INNER JOIN Resources R ON R.Id = RC.ResourceId
            INNER JOIN Languages L ON L.Id = RC.LanguageId
        WHERE RCR.Created >= DATEADD(DAY, -30, GETDATE())
        GROUP BY RCR.ResourceContentId, L.EnglishDisplay, R.EnglishLabel
        ORDER BY Count DESC;
        """;

    /// <summary>
    ///     Handles the asynchronous operation of retrieving the most requested resources.
    /// </summary>
    /// <param name="dbContext">The database context to use for the operation.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the most requested resources.</returns>
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