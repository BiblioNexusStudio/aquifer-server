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
        SELECT TOP 100 PR.DisplayName, R.EnglishLabel, L.EnglishDisplay, COUNT(RCR.ResourceContentId) AS Count
        FROM ResourceContentRequests RCR
                 INNER JOIN ResourceContents RC ON RC.Id = RCR.ResourceContentId
                 INNER JOIN Resources R ON R.Id = RC.ResourceId
                 INNER JOIN ParentResources PR ON PR.Id = R.ParentResourceId
                 INNER JOIN Languages L ON L.Id = RC.LanguageId
        WHERE RCR.Created >= DATEADD(DAY, -30, GETUTCDATE())
        GROUP BY RCR.ResourceContentId, L.EnglishDisplay, R.EnglishLabel, PR.DisplayName
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