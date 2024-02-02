using Aquifer.API.Utilities;
using Aquifer.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Reports.DailyDownloadTotals;

public static class DailyDownloadEndpoints
{
    private const string DailyDownloadTotalsQuery =
        """
        SELECT DATEADD(DD, 0, DATEADD(DD, DATEDIFF(D, 0, Created), 0)) AS Date,
                COUNT(*) AS Amount FROM ResourceContentRequests
        WHERE [Created] >= DATEADD(DAY, -30, GETUTCDATE())
        GROUP BY DATEADD(DD, 0, DATEADD(DD, DATEDIFF(D, 0, Created), 0));
        """;

    public static async Task<Ok<IOrderedEnumerable<AmountPerDay>>> DailyResourceDownloadTotals(
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var dailyDownloadTotals = await dbContext.Database
            .SqlQuery<AmountPerDay>($"exec ({DailyDownloadTotalsQuery})")
            .ToListAsync(cancellationToken);

        var lastThirtyDays = ReportUtilities.GetLastDays(30);
        foreach (var date in lastThirtyDays)
        {
            if (dailyDownloadTotals.All(x => x.Date != date))
            {
                dailyDownloadTotals.Add(new AmountPerDay
                {
                    Date = date,
                    Amount = 0
                });
            }
        }

        return TypedResults.Ok(dailyDownloadTotals.OrderBy(x => x.Date));
    }
}