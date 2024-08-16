using Aquifer.API.Helpers;
using Aquifer.API.Utilities;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Reports.BarCharts.DailyResourceDownloads;

public class Endpoint(AquiferDbContext dbContext) : EndpointWithoutRequest<IEnumerable<Response>>
{
    private const string DailyDownloadTotalsQuery =
        """
        SELECT DATEADD(DD, 0, DATEADD(DD, DATEDIFF(D, 0, Created), 0)) AS Date,
                COUNT(*) AS Amount FROM ResourceContentRequests
        WHERE [Created] >= DATEADD(DAY, -30, GETUTCDATE())
        GROUP BY DATEADD(DD, 0, DATEADD(DD, DATEDIFF(D, 0, Created), 0));
        """;

    public override void Configure()
    {
        Get("/reports/bar-charts/daily-resource-downloads");
        EndpointHelpers.ServerCacheInSeconds(EndpointHelpers.TenMinutesInSeconds);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var dailyDownloadTotals = await dbContext.Database
            .SqlQuery<Response>($"exec ({DailyDownloadTotalsQuery})")
            .ToListAsync(ct);

        var lastThirtyDays = ReportUtilities.GetLastDays(30);
        foreach (var date in lastThirtyDays)
        {
            if (dailyDownloadTotals.All(x => x.Date != date))
            {
                dailyDownloadTotals.Add(new Response { Date = date, Amount = 0 });
            }
        }

        await SendOkAsync(dailyDownloadTotals.OrderBy(x => x.Date), ct);
    }
}