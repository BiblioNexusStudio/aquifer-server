using Aquifer.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Resources.ResourcesSummary;

public static class ResourcesSummaryEndpoints
{
    private const string GetResourcesByTypeQuery =
        """
        SELECT RT.DisplayName AS ResourceType, DATEADD(MONTH, DATEDIFF(MONTH, 0, R.Created), 0) AS Date,
               RC.Status, COUNT(DISTINCT R.Id) AS ResourceCount
        FROM Resources R
                 INNER JOIN ResourceContents RC ON R.Id = RC.ResourceId
                 INNER JOIN ResourceTypes RT on R.TypeId = RT.Id
        GROUP BY RC.Status, RT.DisplayName, DATEADD(MONTH, DATEDIFF(MONTH, 0, R.Created), 0)
        """;

    private const string GetResourcesByLanguageQuery =
        """
        SELECT L.EnglishDisplay AS LanguageName, RT.DisplayName AS ResourceType,
               DATEADD(MONTH, DATEDIFF(MONTH, 0, RC.Created), 0) AS Date, COUNT(DISTINCT R.Id) AS ResourceCount
        FROM Resources R
                 INNER JOIN ResourceContents RC ON R.Id = RC.ResourceId
                 INNER JOIN Languages L ON RC.LanguageId = L.Id
                 INNER JOIN ResourceTypes RT on R.TypeId = RT.Id
        WHERE RC.MediaType != 2
        GROUP BY L.EnglishDisplay, RT.DisplayName, DATEADD(MONTH, DATEDIFF(MONTH, 0, RC.Created), 0)
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
        var resourcesByLanguage = new List<ResourcesSummaryByLanguageDto>();
        int multiLanguageResourcesCount = 0;

        await using (var sqlConnection = new SqlConnection(dbContext.Database.GetConnectionString()))
        {
            await sqlConnection.OpenAsync(cancellationToken);
            await using (var command = new SqlCommand(GetResourcesByTypeQuery, sqlConnection))
            {
                await using var reader = await command.ExecuteReaderAsync(cancellationToken);
                while (await reader.ReadAsync(cancellationToken))
                {
                    resourcesByType.Add(new ResourcesSummaryByTypeDto
                    {
                        ResourceType = reader.GetString(0),
                        Date = reader.GetDateTime(1),
                        Status = reader.GetInt32(2),
                        ResourceCount = reader.GetInt32(3)
                    });
                }
            }

            await using (var command = new SqlCommand(GetResourcesByLanguageQuery, sqlConnection))
            {
                await using var reader = await command.ExecuteReaderAsync(cancellationToken);
                while (await reader.ReadAsync(cancellationToken))
                {
                    resourcesByLanguage.Add(new ResourcesSummaryByLanguageDto(reader.GetString(0),
                        reader.GetString(1),
                        reader.GetDateTime(2),
                        reader.GetInt32(3)));
                }
            }

            await using (var command = new SqlCommand(GetMultiLanguageResourcesCountQuery, sqlConnection))
            {
                await using var reader = await command.ExecuteReaderAsync(cancellationToken);
                while (await reader.ReadAsync(cancellationToken))
                {
                    multiLanguageResourcesCount = reader.GetInt32(0);
                }
            }
        }

        // Apparently, without jumping over hurdles, Entity Framework no longer supports
        // mapping to types not in the DbSet. It will support it again with EF8 which
        // should release in November. So once we upgrade to .NET 8 we can replace the
        // above with the below.

        // var resourcesByType = await dbContext.Database
        //     .SqlQuery<ResourcesSummaryByTypeDto>($"{GetResourcesByTypeQuery}")
        //     .ToListAsync(cancellationToken);

        // var resourcesByLanguage = await dbContext.Database
        //     .SqlQuery<ResourcesSummaryByLanguageDto>($"{GetResourcesByLanguageQuery}")
        //     .ToListAsync(cancellationToken);

        // var multiLanguageResourcesCount = await dbContext.Database
        //     .SqlQuery<int>($"{GetMultiLanguageResourcesCountQuery}")
        //     .SingleAsync(cancellationToken);

        int allResourcesCount = resourcesByType.Select(x => x.ResourceCount).Sum();

        var currentDate = DateTime.UtcNow;
        var lastFiveMonths = Enumerable.Range(0, 5)
            .Select(i => new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(-i));
        var resourceTypesByGroup = resourcesByType.GroupBy(x => x.ResourceType);

        foreach (var resourceGroup in resourceTypesByGroup)
        {
            foreach (var date in lastFiveMonths)
            {
                if (resourceGroup.SingleOrDefault(x => x.Date == date) == null)
                {
                    resourcesByType.Add(new ResourcesSummaryByTypeDto
                    {
                        ResourceType = resourceGroup.Key,
                        Date = date,
                        Status = 3,
                        ResourceCount = 0
                    });
                }
            }
        }

        // The resourceGroup above doesn't have the new references until it
        // exits the foreach. So moving this into the one above does not work.
        // I'm sure there's some way to get it done in one loop, but not
        // worrying about it right now.
        foreach (var resourceGroup in resourceTypesByGroup)
        {
            var orderedGroup = resourceGroup.OrderBy(x => x.Date).ToList();
            for (int i = 1; i < orderedGroup.Count; i++)
            {
                orderedGroup[i].ResourceCount += orderedGroup[i - 1].ResourceCount;
            }
        }

        return TypedResults.Ok(new ResourcesSummaryResponse(resourcesByType,
            resourcesByLanguage,
            allResourcesCount,
            multiLanguageResourcesCount));
    }
}