using Aquifer.API.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Reports;

public static class GetMonthlyReportsEndpoints 
{

     public static async Task<Results<Ok<MonthlyAquiferiationStartsAndCompletionsResponse>, NotFound>> GetAquiferizationCompleteAndStart(
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var startedSql = GetMonthlyCountQuery(ResourceContentStatus.AquiferizeInProgress);
        var monthlyStartedResources = await dbContext.Database
            .SqlQuery<StatusCountPerMonth>($"exec ({startedSql})")
            .ToListAsync(cancellationToken);

        var completedSql = GetMonthlyCountForEnglishQuery(ResourceContentStatus.Complete);
        var monthlyCompletedResources = await dbContext.Database
            .SqlQuery<StatusCountPerMonth>($"exec ({completedSql})")
            .ToListAsync(cancellationToken);

        var months = ReportUtilities.GetLastMonths(6);
        int monthlyResourceIdx = 0;
        // fill in 'empty' months with a StatusCount of zero
        for (int i = months.Count - 1; i >= 0; i--) {
            if (monthlyCompletedResources.Count - 1  < monthlyResourceIdx){
                monthlyCompletedResources.Insert(monthlyResourceIdx, new StatusCountPerMonth(months[i], 0));
            }  else if (monthlyCompletedResources[monthlyResourceIdx] != null) {
                DateTime completedMonth = monthlyCompletedResources[monthlyResourceIdx].Date;
                if(completedMonth.Month != months[i].Month) {
                    monthlyCompletedResources.Insert(monthlyResourceIdx, new StatusCountPerMonth(months[i], 0));
                }
            } 

            if (monthlyStartedResources.Count - 1  < monthlyResourceIdx){
                monthlyStartedResources.Insert(monthlyResourceIdx, new StatusCountPerMonth(months[i], 0));
            } else if (monthlyStartedResources[monthlyResourceIdx] != null) {
                DateTime completedMonth = monthlyStartedResources[monthlyResourceIdx].Date;
                if(completedMonth.Month != months[i].Month) {
                    monthlyStartedResources.Insert(monthlyResourceIdx, new StatusCountPerMonth(months[i], 0));
                }
            } 

            monthlyResourceIdx++;
        }
        
        return TypedResults.Ok(new MonthlyAquiferiationStartsAndCompletionsResponse(monthlyStartedResources, monthlyCompletedResources));
    }  

    private static string GetMonthlyCountQuery(ResourceContentStatus status) {
        return 
        $"""
        select DATEADD(mm,0,DATEADD(mm, DATEDIFF(m,0,Created),0)) AS Date, 
                Count(Status) as StatusCount from ResourceContentVersionStatusHistory 
            WHERE Created >= DATEADD(MONTH, -6, GETDATE()) 
                and [Status] = {(int)status} 
            GROUP By DATEADD(mm,0,DATEADD(mm, DATEDIFF(m,0,Created),0));
        """;
    }

    private static string GetMonthlyCountForEnglishQuery(ResourceContentStatus status) {
        return 
        $"""
        select DATEADD(mm,0,DATEADD(mm, DATEDIFF(m,0,rcvsh.[Created]),0)) AS Date, Count(rcvsh.[Status]) as StatusCount from ResourceContentVersionStatusHistory rcvsh 
                JOIN ResourceContentVersions rcv on rcvsh.ResourceContentVersionId = rcv.Id
                JOIN ResourceContents rc on rcv.ResourceContentId = rc.Id
                JOIN Languages l on rc.LanguageId = l.Id
            WHERE  l.ISO6393Code = 'eng' AND
                rcvsh.Created >= DATEADD(MONTH, -6, GETDATE()) AND
                rcvsh.[Status] = {(int)status}
        GROUP BY DATEADD(mm,0,DATEADD(mm, DATEDIFF(m,0,rcvsh.[Created]),0));
        """;
    }
}