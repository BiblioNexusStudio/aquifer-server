using Aquifer.API.Helpers;
using Aquifer.API.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Reports.Monthly;

public class Endpoint(AquiferDbContext dbContext) : EndpointWithoutRequest<Response>
{
    public override void Configure()
    {
        Get("/reports/aquiferizations/monthly", "/reports/translations/monthly");
        EndpointHelpers.ServerCacheInSeconds(EndpointHelpers.TenMinutesInSeconds);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = HttpContext.Request.Path.Value switch
        {
            "/reports/aquiferizations/monthly" => await HandleAquiferizations(ct),
            "/reports/translations/monthly" => await HandleTranslations(ct),
            _ => throw new InvalidOperationException("Invalid endpoint")
        };

        await SendOkAsync(response, ct);
    }

    private async Task<Response> HandleAquiferizations(CancellationToken ct)
    {
        var monthlyStartedResources = await dbContext.Database
            .SqlQuery<StatusCountPerMonth>($"exec ({MonthlyStartedQuery(ResourceContentStatus.AquiferizeInProgress)})")
            .ToListAsync(ct);

        var monthlyCompletedResources = await dbContext.Database
            .SqlQuery<StatusCountPerMonth>($"exec ({MonthlyCompletedQuery(true)})")
            .ToListAsync(ct);

        return PopulateZeroCountMonths(monthlyStartedResources, monthlyCompletedResources);
    }

    private async Task<Response> HandleTranslations(CancellationToken ct)
    {
        var monthlyStartedResources = await dbContext.Database
            .SqlQuery<StatusCountPerMonth>($"exec ({MonthlyStartedQuery(ResourceContentStatus.TranslationInProgress)})")
            .ToListAsync(ct);

        var monthlyCompletedResources = await dbContext.Database
            .SqlQuery<StatusCountPerMonth>($"exec ({MonthlyCompletedQuery(false)})")
            .ToListAsync(ct);

        return PopulateZeroCountMonths(monthlyStartedResources, monthlyCompletedResources);
    }

    private static string MonthlyStartedQuery(ResourceContentStatus status)
    {
        return
            $"""
             SELECT DATEADD(mm, 0, DATEADD(mm, DATEDIFF(m, 0, Created),0)) AS Date,
             Count(Status) AS StatusCount from ResourceContentVersionStatusHistory
                 WHERE Created >= DATEADD(MONTH, -6, GETUTCDATE())
             AND [Status] = {(int)status}
             GROUP By DATEADD(mm, 0,DATEADD(mm, DATEDIFF(m, 0, Created), 0));
             """;
    }

    private static string MonthlyCompletedQuery(bool isEnglish)
    {
        var whereLanguage = isEnglish ? "WHERE rc.LanguageId = 1" : "WHERE rc.LanguageId != 1";
        return
            $"""
             SELECT DATEADD(mm, 0, DATEADD(mm, DATEDIFF(m, 0, rcvsh.[Created]), 0)) AS Date,
                    Count(rcvsh.[Status]) AS StatusCount
             FROM ResourceContentVersionStatusHistory rcvsh
                      JOIN ResourceContentVersions rcv on rcvsh.ResourceContentVersionId = rcv.Id
                      JOIN ResourceContents rc on rcv.ResourceContentId = rc.Id
             {whereLanguage}
               AND rcvsh.Created >= DATEADD(MONTH, -6, GETUTCDATE())
               AND rcvsh.[Status] = 3
             GROUP BY DATEADD(mm, 0, DATEADD(mm, DATEDIFF(m, 0, rcvsh.[Created]), 0))
             """;
    }

    private static Response PopulateZeroCountMonths(List<StatusCountPerMonth> monthlyStartedResources,
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

        return new Response(
            monthlyStartedResources.OrderBy(x => x.Date),
            monthlyCompletedResources.OrderBy(x => x.Date));
    }
}