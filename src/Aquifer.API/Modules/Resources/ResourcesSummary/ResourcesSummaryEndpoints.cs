using Aquifer.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Resources.ResourcesSummary;

public static class ResourcesSummaryEndpoints
{
    private const string GetResourcesByTypeQuery =
        """
        SELECT RT.DisplayName AS ResourceType, YEAR(R.Created) AS Year,
            MONTH(R.Created) AS Month, COUNT(DISTINCT R.Id) AS ResourceCount
        FROM Resources R
            INNER JOIN ResourceContents RC ON R.Id = RC.ResourceId
            INNER JOIN ResourceTypes RT on R.TypeId = RT.Id
        WHERE RC.Status = 3
        GROUP BY RT.DisplayName, YEAR(r.Created), MONTH(r.Created)
        """;

    private const string GetResourcesByLanguageQuery =
        """
        SELECT L.EnglishDisplay AS LanguageName, RT.DisplayName AS ResourceType, YEAR(RC.Created) AS Year,
               MONTH(RC.Created) AS Month, COUNT(DISTINCT R.Id) AS ResourceCount
        FROM Resources R
                 INNER JOIN ResourceContents RC ON R.Id = RC.ResourceId
                 INNER JOIN Languages L ON RC.LanguageId = L.Id
                 INNER JOIN ResourceTypes RT on R.TypeId = RT.Id
        WHERE RC.Status = 3 AND RC.MediaType != 2
        GROUP BY L.EnglishDisplay, RT.DisplayName, YEAR(RC.Created), MONTH(RC.Created)
        """;

    private const string GetMultiLanguageResourcesCountQuery =
        """
        SELECT COUNT(*) AS Count
        FROM (
            SELECT ResourceId, COUNT(DISTINCT LanguageId) AS Count
            FROM ResourceContents
            GROUP BY ResourceId
            HAVING COUNT(DISTINCT LanguageId) > 1
        ) AS Subquery;
        """;

    public static async Task<Ok<ResourcesSummaryResponse>> Get(AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var resourcesByType = new List<ResourcesSummaryByTypeDto>();
        await using (var sqlConnection = new SqlConnection(dbContext.Database.GetConnectionString()))
        {
            await sqlConnection.OpenAsync(cancellationToken);
            await using var command = new SqlCommand(GetResourcesByTypeQuery, sqlConnection);
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                resourcesByType.Add(new ResourcesSummaryByTypeDto(reader.GetString(0),
                    reader.GetInt32(1),
                    reader.GetInt32(2),
                    reader.GetInt32(3)));
            }
        }

        // var resourcesByType = await dbContext.Database
        //     .SqlQuery<ResourcesSummaryByTypeDto>($"{GetResourcesByTypeQuery}")
        //     .ToListAsync(cancellationToken);

        // var resourcesByLanguage = await dbContext.Database
        //     .SqlQuery<ResourcesSummaryByLanguageDto>($"{GetResourcesByLanguageQuery}")
        //     .ToListAsync(cancellationToken);

        int multiLanguageResourcesCount = 1;
        // await dbContext.Database
        //     .SqlQuery<int>($"{GetMultiLanguageResourcesCountQuery}")
        //     .SingleAsync(cancellationToken);

        return TypedResults.Ok(new ResourcesSummaryResponse(resourcesByType,
            new List<ResourcesSummaryByLanguageDto>(),
            multiLanguageResourcesCount));
    }
}