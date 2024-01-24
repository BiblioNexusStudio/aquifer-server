using Aquifer.API.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Reports;

public static class MonthlyReportsEndpoints 
{

     public static async Task<Results<Ok<MonthlyAquiferiationStartsAndCompletionsResponse>, NotFound>> AquiferizationCompleteAndStart(
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var startedSql = MonthlyCountQuery(ResourceContentStatus.AquiferizeInProgress);
        var monthlyStartedResources = await dbContext.Database
            .SqlQuery<StatusCountPerMonth>($"exec ({startedSql})")
            .ToListAsync(cancellationToken);

        var completedSql = MonthlyCountForEnglishQuery(ResourceContentStatus.Complete);
        var monthlyCompletedResources = await dbContext.Database
            .SqlQuery<StatusCountPerMonth>($"exec ({completedSql})")
            .ToListAsync(cancellationToken);

        var months = ReportUtilities.GetLastMonths(6);
        int monthlyResourceIdx = 0;
        // fill in 'empty' months with a StatusCount of zero
        for (int i = months.Count - 1; i >= 0; i--) {
            // processing completed resource
            if (monthlyCompletedResources.Count - 1  < monthlyResourceIdx){
                monthlyCompletedResources.Insert(monthlyResourceIdx, new StatusCountPerMonth(months[i], 0));
            }  

            var completedDate = monthlyCompletedResources[monthlyResourceIdx].Date;

            if (completedDate.Month != months[i].Month) {
                monthlyCompletedResources.Insert(monthlyResourceIdx, new StatusCountPerMonth(months[i], 0));
            }

            // processing started resources
            if (monthlyStartedResources.Count - 1  < monthlyResourceIdx){
                monthlyStartedResources.Insert(monthlyResourceIdx, new StatusCountPerMonth(months[i], 0));
            }
            var startedDate = monthlyStartedResources[monthlyResourceIdx].Date;
            if(startedDate.Month != months[i].Month) {
                monthlyStartedResources.Insert(monthlyResourceIdx, new StatusCountPerMonth(months[i], 0));
            }
            
            monthlyResourceIdx++;
        }
        
        return TypedResults.Ok(new MonthlyAquiferiationStartsAndCompletionsResponse(monthlyStartedResources, monthlyCompletedResources));
    }  

    private static string MonthlyCountQuery(ResourceContentStatus status) {
        return 
        $"""
        select DATEADD(mm,0,DATEADD(mm, DATEDIFF(m,0,Created),0)) AS Date, 
                Count(Status) as StatusCount from ResourceContentVersionStatusHistory 
            WHERE Created >= DATEADD(MONTH, -6, GETDATE()) 
                and [Status] = {(int)status} 
            GROUP By DATEADD(mm,0,DATEADD(mm, DATEDIFF(m,0,Created),0));
        """;
    }

    private static string MonthlyCountForEnglishQuery(ResourceContentStatus status) {
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