using Aquifer.API.Helpers;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Reports.BarCharts.DailyResourceDownloads;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, IEnumerable<Response>>
{
    public override void Configure()
    {
        Get("/reports/bar-charts/daily-resource-downloads");
        EndpointHelpers.ServerCacheInSeconds(EndpointHelpers.TenMinutesInSeconds);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var dailyDownloadTotals = await dbContext.Database
            .SqlQuery<Response>($"""
                                     SELECT DATEADD(DD, 0, DATEADD(DD, DATEDIFF(D, 0, Created), 0)) AS Date, COUNT(*) AS Amount
                                     FROM ResourceContentRequests
                                     WHERE [Created] BETWEEN {request.StartDate} AND {request.EndDate}
                                     GROUP BY DATEADD(DD, 0, DATEADD(DD, DATEDIFF(D, 0, Created), 0));
                                 """)
            .ToListAsync(ct);

        var allDates = Enumerable.Range(0, request.EndDate.DayNumber - request.StartDate.DayNumber + 1)
            .Select(request.StartDate.AddDays);

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