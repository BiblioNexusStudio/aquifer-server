using Aquifer.Data;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Reports.EditedResources;

/// <summary>
///     Handles the retrieval of edited resources
/// </summary>
public class EditedResourcesLastThirtyDays
{
    private const string EditedResourcesLastThirtyDaysQuery =
        """
        SELECT DISTINCT PR.DisplayName, R.EnglishLabel, L.EnglishDisplay
        FROM ResourceContentVersionStatusHistory RCVSH
            INNER JOIN ResourceContentVersions RCV ON RCV.Id = RCVSH.ResourceContentVersionId
            INNER JOIN ResourceContents RC ON RC.Id = RCV.ResourceContentId
            INNER JOIN Resources R ON R.Id = RC.ResourceId
            INNER JOIN ParentResources PR ON PR.Id = R.ParentResourceId
            INNER JOIN Languages L ON L.Id = RC.LanguageId
        WHERE RCVSH.Status = 3 -- status 3 is complete
        AND RCVSH.Created >= DATEADD(DAY, -30, GETDATE())
        """;

    /// <summary>
    ///     Handles the retrieval of resources edited in the last thirty days.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the HTTP result.</returns>
    public static async Task<IResult> HandleAsync(
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var editedResources = await dbContext.Database
            .SqlQuery<EditedResourcesLastThirtyDaysResponse>($"exec ({EditedResourcesLastThirtyDaysQuery})")
            .ToListAsync(cancellationToken);

        return Results.Ok(editedResources);
    }
}