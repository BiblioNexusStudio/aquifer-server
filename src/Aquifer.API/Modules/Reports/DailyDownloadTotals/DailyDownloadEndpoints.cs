using Aquifer.API.Utilities;
using Aquifer.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Reports.DailyDownloadTotals;

public static class DailyDownloadEndpoints
{
    private const string DailyDownloadTotalQuery =
        """
        SELECT DATEADD(DD, 0, DATEADD(DD, DATEDIFF(D, 0, Created), 0)) AS Date,
                COUNT(*) AS Amount FROM ResourceContentRequests
        WHERE [Created] >= DATEADD(DAY, -30, GETUTCDATE())
        GROUP BY DATEADD(DD, 0, DATEADD(DD, DATEDIFF(D, 0, Created), 0));
        """;

    public static async Task<Ok<DailyDownloadTotalsResponse>> DailyResourceDownloadTotals(
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var dailyDownloadTotals = await dbContext.Database
            .SqlQuery<AmountPerDay>($"exec ({DailyDownloadTotalQuery})")
            .ToListAsync(cancellationToken);

        var lastSixMonthDates = ReportUtilities.GetLastDays(30);
        foreach (var date in lastSixMonthDates)
        {
            var zeroCountMonth = new AmountPerDay(date, 0);
            if (dailyDownloadTotals.All(x => x.Date.Day != date.Day))
            {
                dailyDownloadTotals.Add(zeroCountMonth);
            }
        }

        return TypedResults.Ok(new DailyDownloadTotalsResponse(dailyDownloadTotals.OrderBy(x => x.Date)));
    }
}