using Aquifer.Data;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Reports.EditedResources;

public class EditedResourcesLastThirtyDays
{
    private const string EditedResourcesLastThirtyDaysQuery =
        """
        SELECT DISTINCT PR.DisplayName AS Resource, R.EnglishLabel AS Label, L.EnglishDisplay AS Language
        FROM ResourceContentVersionStatusHistory RCVSH
            INNER JOIN ResourceContentVersions RCV ON RCV.Id = RCVSH.ResourceContentVersionId
            INNER JOIN ResourceContents RC ON RC.Id = RCV.ResourceContentId
            INNER JOIN Resources R ON R.Id = RC.ResourceId
            INNER JOIN ParentResources PR ON PR.Id = R.ParentResourceId
            INNER JOIN Languages L ON L.Id = RC.LanguageId
        WHERE RCVSH.Status IN (2, 4, 5, 7, 8, 9) -- in progress, in review, review pending
        AND RCVSH.Created >= DATEADD(DAY, -30, GETUTCDATE())
        """;

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