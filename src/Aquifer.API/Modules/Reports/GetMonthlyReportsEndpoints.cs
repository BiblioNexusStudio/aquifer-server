using Aquifer.API.Utilities;
using Aquifer.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Reports;

public static class MonthlyReportsEndpoints
{
    private const string MonthlyCountQuery =
        """
        SELECT DATEADD(mm, 0, DATEADD(mm, DATEDIFF(m, 0, Created),0)) AS Date,
                Count(Status) AS StatusCount from ResourceContentVersionStatusHistory
            WHERE Created >= DATEADD(MONTH, -6, GETDATE())
                AND [Status] = 2
            GROUP By DATEADD(mm, 0,DATEADD(mm, DATEDIFF(m, 0, Created), 0));
        """;

    private const string MonthlyCountForEnglishQuery =
        """
        SELECT DATEADD(mm, 0, DATEADD(mm, DATEDIFF(m, 0, rcvsh.[Created]), 0)) AS Date,
               Count(rcvsh.[Status])                                           AS StatusCount
        FROM ResourceContentVersionStatusHistory rcvsh
                 JOIN ResourceContentVersions rcv on rcvsh.ResourceContentVersionId = rcv.Id
                 JOIN ResourceContents rc on rcv.ResourceContentId = rc.Id
        WHERE rc.LanguageId = 1
          AND rcvsh.Created >= DATEADD(MONTH, -6, GETDATE())
          AND rcvsh.[Status] = 3
        GROUP BY DATEADD(mm, 0, DATEADD(mm, DATEDIFF(m, 0, rcvsh.[Created]), 0))
        """;

    public static async Task<Ok<MonthlyAquiferiationStartsAndCompletionsResponse>> AquiferizationCompleteAndStart(
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var monthlyStartedResources = await dbContext.Database
            .SqlQuery<StatusCountPerMonth>($"exec ({MonthlyCountQuery})")
            .ToListAsync(cancellationToken);

        var monthlyCompletedResources = await dbContext.Database
            .SqlQuery<StatusCountPerMonth>($"exec ({MonthlyCountForEnglishQuery})")
            .ToListAsync(cancellationToken);

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

        return TypedResults.Ok(new MonthlyAquiferiationStartsAndCompletionsResponse(
            monthlyStartedResources.OrderBy(x => x.Date),
            monthlyCompletedResources.OrderBy(x => x.Date)));
    }
}