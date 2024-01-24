using Aquifer.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Aquifer.API.Utilities;

namespace Aquifer.API.Modules.AdminResources.ResourcesSummary;

public static class GetResourcesSummaryEndpoints
{
    private const string GetResourcesByParentResourceQuery =
        """
        SELECT PR.DisplayName AS ParentResourceName, DATEADD(MONTH, DATEDIFF(MONTH, 0, R.Created), 0) AS Date,
            COUNT(DISTINCT R.Id) AS ResourceCount
        FROM Resources R
            INNER JOIN ParentResources PR on R.ParentResourceId = PR.Id
        GROUP BY PR.DisplayName, DATEADD(MONTH, DATEDIFF(MONTH, 0, R.Created), 0)
        """;

    private const string GetResourcesByLanguageQuery =
        """
        SELECT L.EnglishDisplay AS LanguageName, PR.DisplayName AS ParentResourceName,
               DATEADD(MONTH, DATEDIFF(MONTH, 0, RC.Created), 0) AS Date, COUNT(DISTINCT R.Id) AS ResourceCount
        FROM Resources R
                 INNER JOIN ResourceContents RC ON R.Id = RC.ResourceId
                 INNER JOIN Languages L ON RC.LanguageId = L.Id
                 INNER JOIN ParentResources PR on R.ParentResourceId = PR.Id
        WHERE RC.MediaType != 2
        GROUP BY L.EnglishDisplay, PR.DisplayName, DATEADD(MONTH, DATEDIFF(MONTH, 0, RC.Created), 0)
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
        (var resourcesByParentResource, var resourcesByLanguage, int multiLanguageResourcesCount) =
            await GetDataAsync(dbContext, cancellationToken);

        // resourcesByParentResource sum will be changed later, so keep this at the top
        int allResourcesCount = resourcesByParentResource.Select(x => x.ResourceCount).Sum();
        var months = ReportUtilities.GetLastMonths(5);
        var languages = resourcesByLanguage.Select(x => x.LanguageName).Distinct().ToList();
        var parentResources = resourcesByLanguage.Select(x => x.ParentResourceName).Distinct().ToList();

        var resourcesByParentResourceResponse = GetResourcesByParentResourceResponse(resourcesByParentResource, months);
        var resourcesByLanguageResponse = GetResourcesByLanguageResponse(resourcesByLanguage,
            months,
            languages,
            parentResources);

        var typeTotalsByMonth = resourcesByParentResourceResponse.GroupBy(x => x.Date)
            .Select(x =>
                new ResourcesSummaryParentResourceTotalsByMonthResponse(x.Key,
                    x.First().MonthAbbreviation,
                    x.Sum(rc => rc.ResourceCount))).ToList();

        return TypedResults.Ok(new ResourcesSummaryResponse(resourcesByParentResourceResponse,
            resourcesByLanguageResponse,
            typeTotalsByMonth,
            allResourcesCount,
            multiLanguageResourcesCount,
            languages,
            parentResources));
    }

    private static List<DateTime> GetMonthsForSummary()
    {
        var currentDate = DateTime.UtcNow;
        return Enumerable.Range(0, 6).Select(i => new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(-i))
            .ToList();
    }

    private static List<ResourcesSummaryByParentResourceResponse> GetResourcesByParentResourceResponse(
        List<ResourcesSummaryCommon> resourcesByParentResource,
        List<DateTime> lastFiveMonths)
    {
        // Must iterate twice with current setup (i.e. don't call ToList)
        var parentResourcesByGroup = resourcesByParentResource.GroupBy(x => x.ParentResourceName);

        foreach (var resourceGroup in parentResourcesByGroup)
        {
            foreach (var date in lastFiveMonths)
            {
                if (resourceGroup.SingleOrDefault(x => x.Date == date) == null)
                {
                    resourcesByParentResource.Add(new ResourcesSummaryCommon
                    {
                        ParentResourceName = resourceGroup.Key,
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
        foreach (var resourceGroup in parentResourcesByGroup)
        {
            var orderedGroup = resourceGroup.OrderBy(x => x.Date).ToList();
            for (int i = 1; i < orderedGroup.Count; i++)
            {
                orderedGroup[i].ResourceCount += orderedGroup[i - 1].ResourceCount;
            }
        }

        return resourcesByParentResource.Where(x => x.Date >= lastFiveMonths.Last()).OrderBy(x => x.Date)
            .Select(x => new ResourcesSummaryByParentResourceResponse(x.ResourceCount, x.ParentResourceName, x.Date))
            .ToList();
    }

    private static List<ResourcesSummaryByLanguageResponse> GetResourcesByLanguageResponse(
        List<ResourcesSummaryByLanguage> resourcesByLanguage,
        List<DateTime> lastFiveMonths,
        List<string> languages,
        List<string> resources)
    {
        // Must iterate twice with current setup (i.e. don't call ToList)
        var resourceLanguagesByGroup = resourcesByLanguage.GroupBy(x => (x.LanguageName, x.ParentResourceName));

        foreach (string language in languages)
        {
            foreach (string resource in resources)
            {
                if (!resourcesByLanguage.Any(x => x.ParentResourceName == resource && x.LanguageName == language))
                {
                    resourcesByLanguage.Add(new ResourcesSummaryByLanguage
                    {
                        Date = lastFiveMonths.Last(),
                        LanguageName = language,
                        ResourceCount = 0,
                        ParentResourceName = resource
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
                    resourcesByLanguage.Add(new ResourcesSummaryByLanguage
                    {
                        LanguageName = resourceGroup.Key.LanguageName,
                        ParentResourceName = resourceGroup.Key.ParentResourceName,
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
            .Select(x => new ResourcesSummaryByLanguageResponse(x.LanguageName,
                x.ResourceCount,
                x.ParentResourceName,
                x.Date))
            .ToList();
    }

    private static async
        Task<(List<ResourcesSummaryCommon> resourcesByParentResource,
            List<ResourcesSummaryByLanguage> resourcesByLanguage,
            int multiLanguageResourcesCount)>
        GetDataAsync(AquiferDbContext dbContext, CancellationToken cancellationToken)
    {
        var resourcesByParentResource = await dbContext.Database
            .SqlQuery<ResourcesSummaryCommon>($"exec ({GetResourcesByParentResourceQuery})")
            .ToListAsync(cancellationToken);

        var resourcesByLanguage = await dbContext.Database
            .SqlQuery<ResourcesSummaryByLanguage>($"exec ({GetResourcesByLanguageQuery})")
            .ToListAsync(cancellationToken);

        int multiLanguageResourcesCount = (await dbContext.Database
            .SqlQuery<int>($"exec ({GetMultiLanguageResourcesCountQuery})").ToListAsync(cancellationToken)).Single();

        return (resourcesByParentResource, resourcesByLanguage, multiLanguageResourcesCount);
    }

    private class ResourcesSummaryByLanguage : ResourcesSummaryCommon
    {
        public string LanguageName { get; set; } = null!;
    }

    private class ResourcesSummaryCommon
    {
        public string ParentResourceName { get; set; } = null!;
        public DateTime Date { get; set; }
        public int ResourceCount { get; set; }
    }
}