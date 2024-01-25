using Aquifer.API.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Reports;

public static class MonthlyReportsEndpoints
{
    public static async Task<Ok<MonthlyStartsAndCompletionsResponse>> AquiferizationCompleteAndStart(
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var monthlyStartedResources = await dbContext.Database
            .SqlQuery<StatusCountPerMonth>($"exec ({MonthlyStartedQuery(ResourceContentStatus.AquiferizeInProgress)})")
            .ToListAsync(cancellationToken);

        var monthlyCompletedResources = await dbContext.Database
            .SqlQuery<StatusCountPerMonth>($"exec ({MonthlyCompletedQuery(true)})")
            .ToListAsync(cancellationToken);

        return TypedResults.Ok(PopulateZeroCountMonths(monthlyStartedResources, monthlyCompletedResources));
    }

    public static async Task<Ok<MonthlyStartsAndCompletionsResponse>> TranslationCompleteAndStart(
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var monthlyStartedResources = await dbContext.Database
            .SqlQuery<StatusCountPerMonth>($"exec ({MonthlyStartedQuery(ResourceContentStatus.TranslationInProgress)})")
            .ToListAsync(cancellationToken);

        var monthlyCompletedResources = await dbContext.Database
            .SqlQuery<StatusCountPerMonth>($"exec ({MonthlyCompletedQuery(false)})")
            .ToListAsync(cancellationToken);

        return TypedResults.Ok(PopulateZeroCountMonths(monthlyStartedResources, monthlyCompletedResources));
    }

    private static MonthlyStartsAndCompletionsResponse PopulateZeroCountMonths(List<StatusCountPerMonth> monthlyStartedResources,
        List<StatusCountPerMonth> monthlyCompletedResources)
    {
        var lastSixMonthDates = ReportUtilities.GetLastMonths(6);
        foreach (var date in lastSixMonthDates)
        {
            var zeroCountMonth = new StatusCountPerMonth(date, 0);
            if (monthlyStartedResources.All(x => x.Date.Month != date.Month))
            {
                monthlyStartedResources.Add(zeroCountMonth);
            }

            if (monthlyCompletedResources.All(x => x.Date.Month != date.Month))
            {
                monthlyCompletedResources.Add(zeroCountMonth);
            }
        }

        return new MonthlyStartsAndCompletionsResponse(
            monthlyStartedResources.OrderBy(x => x.Date),
            monthlyCompletedResources.OrderBy(x => x.Date));
    }

    private static string MonthlyStartedQuery(ResourceContentStatus status)
    {
        return
            $"""
              SELECT DATEADD(mm, 0, DATEADD(mm, DATEDIFF(m, 0, Created),0)) AS Date,
              Count(Status) AS StatusCount from ResourceContentVersionStatusHistory
                  WHERE Created >= DATEADD(MONTH, -6, GETDATE())
              AND [Status] = {(int)status}
              GROUP By DATEADD(mm, 0,DATEADD(mm, DATEDIFF(m, 0, Created), 0));
              """;
    }

    private static string MonthlyCompletedQuery(bool isEnglish)
    {
        string whereLanguage = isEnglish ? "WHERE rc.LanguageId = 1" : "WHERE rc.LanguageId != 1";
        return
            $"""
            SELECT DATEADD(mm, 0, DATEADD(mm, DATEDIFF(m, 0, rcvsh.[Created]), 0)) AS Date,
                   Count(rcvsh.[Status]) AS StatusCount
            FROM ResourceContentVersionStatusHistory rcvsh
                     JOIN ResourceContentVersions rcv on rcvsh.ResourceContentVersionId = rcv.Id
                     JOIN ResourceContents rc on rcv.ResourceContentId = rc.Id
            {whereLanguage}
              AND rcvsh.Created >= DATEADD(MONTH, -6, GETDATE())
              AND rcvsh.[Status] = 3
            GROUP BY DATEADD(mm, 0, DATEADD(mm, DATEDIFF(m, 0, rcvsh.[Created]), 0))
            """;
    }
}