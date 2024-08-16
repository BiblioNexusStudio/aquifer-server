using Aquifer.API.Helpers;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Reports.BarCharts.DailyResourceDownloads;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, IEnumerable<Response>>
{
    private const string DailyDownloadTotalsQuery =
        """
        SELECT DATEADD(DD, 0, DATEADD(DD, DATEDIFF(D, 0, Created), 0)) AS Date,
                COUNT(*) AS Amount FROM ResourceContentRequests
        WHERE [Created] BETWEEN @StartDate AND @EndDate
        GROUP BY DATEADD(DD, 0, DATEADD(DD, DATEDIFF(D, 0, Created), 0));
        """;

    public override void Configure()
    {
        Get("/reports/bar-charts/daily-resource-downloads");
        EndpointHelpers.ServerCacheInSeconds(EndpointHelpers.TenMinutesInSeconds);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var startDate = request.StartDate ?? DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-3));
        var endDate = request.EndDate ?? DateOnly.FromDateTime(DateTime.UtcNow);

        var dailyDownloadTotals = await dbContext.Database
            .SqlQueryRaw<Response>(DailyDownloadTotalsQuery, new SqlParameter("StartDate", startDate), new SqlParameter("EndDate", endDate))
            .ToListAsync(ct);

        var allDates = Enumerable.Range(0, endDate.DayNumber - startDate.DayNumber + 1)
            .Select(startDate.AddDays);

        var augmentedTotals = allDates
            .GroupJoin(dailyDownloadTotals,
                date => date,
                total => DateOnly.FromDateTime(total.Date),
                (date, totals) => new Response
                {
                    Date = date.ToDateTime(TimeOnly.MinValue),
                    Amount = totals.FirstOrDefault()?.Amount ?? 0
                });

        await SendOkAsync(augmentedTotals.OrderBy(x => x.Date), ct);
    }
}