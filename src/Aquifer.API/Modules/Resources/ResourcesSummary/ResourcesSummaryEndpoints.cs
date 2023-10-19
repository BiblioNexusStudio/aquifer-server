using Aquifer.API.Utilities;
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
            COUNT(DISTINCT R.Id) AS ResourceCount
        FROM Resources R
            INNER JOIN ResourceTypes RT on R.TypeId = RT.Id
        GROUP BY RT.DisplayName, DATEADD(MONTH, DATEDIFF(MONTH, 0, R.Created), 0)
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
        (var resourcesByType, var resourcesByLanguage, int multiLanguageResourcesCount) =
            await GetDataAsync(dbContext, cancellationToken);

        // resourcesByType sum will be changed later, so keep this at the top
        int allResourcesCount = resourcesByType.Select(x => x.ResourceCount).Sum();
        var months = GetMonthsForSummary();
        var languages = resourcesByLanguage.Select(x => x.LanguageName).Distinct().ToList();
        var resourceTypes = resourcesByLanguage.Select(x => x.ResourceType).Distinct().ToList();

        var resourcesByTypeResponse = GetResourcesByTypeResponse(resourcesByType, months);
        var resourcesByLanguageResponse = GetResourcesByLanguageResponse(resourcesByLanguage,
            months,
            languages,
            resourceTypes);

        var typeTotalsByMonth = resourcesByTypeResponse.GroupBy(x => x.Date)
            .Select(x =>
                new ResourcesSummaryTypeTotalsByMonth(x.Key,
                    x.First().MonthAbbreviation,
                    x.Sum(rc => rc.ResourceCount))).ToList();

        return TypedResults.Ok(new ResourcesSummaryResponse(resourcesByTypeResponse,
            resourcesByLanguageResponse,
            typeTotalsByMonth,
            allResourcesCount,
            multiLanguageResourcesCount,
            languages,
            resourceTypes));
    }

    public static async Task<Ok<ResourcesSummaryById>> GetByResourceId(int resourceId,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var resource = await dbContext.Resources.Where(x => x.Id == resourceId).Select(r => new ResourcesSummaryById
        {
            Label = r.EnglishLabel,
            Type = r.Type.DisplayName,
            Resources = r.ResourceContents.Select(rc => new ResourcesSummaryContentById
            {
                Language = new ResourcesSummaryLanguageById
                {
                    DisplayName = rc.Language.EnglishDisplay,
                    Id = rc.LanguageId
                },
                DisplayName = rc.DisplayName,
                Status = rc.Status,
                ContentSize = rc.ContentSize,
                Content = JsonUtilities.DefaultDeserialize(rc.Content),
                MediaType = rc.MediaType
            }),
            AssociatedResources =
                r.AssociatedResourceChildren.Select(ar => new ResourcesSummaryAssociatedContentById
                {
                    Label = ar.EnglishLabel,
                    Type = ar.Type.DisplayName,
                    MediaTypes = ar.ResourceContents.Select(arrc => arrc.MediaType)
                }),
            PassageReferences =
                r.PassageResources.Select(pr => new ResourcesSummaryPassageById
                {
                    StartVerseId = pr.Passage.StartVerseId,
                    EndVerseId = pr.Passage.EndVerseId
                }),
            VerseReferences = r.VerseResources.Select(vr => new ResourcesSummaryVerseById { VerseId = vr.VerseId })
        }).FirstOrDefaultAsync(cancellationToken);

        return TypedResults.Ok(resource);
    }

    private static List<DateTime> GetMonthsForSummary()
    {
        var currentDate = DateTime.UtcNow;
        return Enumerable.Range(0, 5).Select(i => new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(-i))
            .ToList();
    }

    private static List<ResourcesSummaryByType> GetResourcesByTypeResponse(
        List<ResourcesSummaryByTypeDto> resourcesByType,
        List<DateTime> lastFiveMonths)
    {
        // Must iterate twice with current setup (i.e. don't call ToList)
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

        return resourcesByType.Where(x => x.Date >= lastFiveMonths.Last()).OrderBy(x => x.Date)
            .Select(x => new ResourcesSummaryByType(x.ResourceCount, x.ResourceType, x.Date))
            .ToList();
    }

    private static List<ResourcesSummaryByLanguage> GetResourcesByLanguageResponse(
        List<ResourcesSummaryByLanguageDto> resourcesByLanguage,
        List<DateTime> lastFiveMonths,
        List<string> languages,
        List<string> resources)
    {
        // Must iterate twice with current setup (i.e. don't call ToList)
        var resourceLanguagesByGroup = resourcesByLanguage.GroupBy(x => (x.LanguageName, x.ResourceType));

        foreach (string language in languages)
        {
            foreach (string resource in resources)
            {
                if (!resourcesByLanguage.Any(x => x.ResourceType == resource && x.LanguageName == language))
                {
                    resourcesByLanguage.Add(new ResourcesSummaryByLanguageDto
                    {
                        Date = lastFiveMonths.Last(),
                        LanguageName = language,
                        ResourceCount = 0,
                        ResourceType = resource
                    });
                }
            }
        }

        foreach (var resourceGroup in resourceLanguagesByGroup)
        {
            foreach (var date in lastFiveMonths)
            {
                if (resourceGroup.SingleOrDefault(x => x.Date == date) == null)
                {
                    resourcesByLanguage.Add(new ResourcesSummaryByLanguageDto
                    {
                        LanguageName = resourceGroup.Key.LanguageName,
                        ResourceType = resourceGroup.Key.ResourceType,
                        Date = date,
                        ResourceCount = 0
                    });
                }
            }
        }

        foreach (var resourceGroup in resourceLanguagesByGroup)
        {
            var orderedGroup = resourceGroup.OrderBy(x => x.Date).ToList();
            for (int i = 1; i < orderedGroup.Count; i++)
            {
                orderedGroup[i].ResourceCount += orderedGroup[i - 1].ResourceCount;
            }
        }

        return resourcesByLanguage.Where(x => x.Date >= lastFiveMonths.Last()).OrderBy(x => x.Date)
            .Select(x => new ResourcesSummaryByLanguage(x.LanguageName,
                x.ResourceCount,
                x.ResourceType,
                x.Date))
            .ToList();
    }

    private static async
        Task<(List<ResourcesSummaryByTypeDto> resourcesByType,
            List<ResourcesSummaryByLanguageDto> resourcesByLanguage,
            int multiLanguageResourcesCount)>
        GetDataAsync(AquiferDbContext dbContext, CancellationToken cancellationToken)
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
                        ResourceCount = reader.GetInt32(2)
                    });
                }
            }

            await using (var command = new SqlCommand(GetResourcesByLanguageQuery, sqlConnection))
            {
                await using var reader = await command.ExecuteReaderAsync(cancellationToken);
                while (await reader.ReadAsync(cancellationToken))
                {
                    resourcesByLanguage.Add(new ResourcesSummaryByLanguageDto
                    {
                        LanguageName = reader.GetString(0),
                        ResourceType = reader.GetString(1),
                        Date = reader.GetDateTime(2),
                        ResourceCount = reader.GetInt32(3)
                    });
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

        return (resourcesByType, resourcesByLanguage, multiLanguageResourcesCount);
    }
}