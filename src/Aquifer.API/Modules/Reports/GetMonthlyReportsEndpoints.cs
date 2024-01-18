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
        var startedSqlStr = GetMonthlyCountSqlStringForStatus((int)ResourceContentStatus.AquiferizeInProgress);
        var monthlyStartedResources = await dbContext.Database
            .SqlQuery<StatusCountPerMonth>($"exec ({startedSqlStr})")
            .ToListAsync(cancellationToken);

         int englishLanguageId = (await dbContext.Languages.Where(language => language.ISO6393Code.ToLower() == "eng")
            .FirstOrDefaultAsync(cancellationToken))?.Id ?? -1;

        var completedSqlStr = GetMonthlyCountSqlStringForStatusByLanguage((int)ResourceContentStatus.Complete, englishLanguageId);
        var monthlyCompletedResources = await dbContext.Database
            .SqlQuery<StatusCountPerMonth>($"exec ({completedSqlStr})")
            .ToListAsync(cancellationToken);
        

        return TypedResults.Ok(new MonthlyAquiferiationStartsAndCompletionsResponse(monthlyStartedResources, monthlyCompletedResources));
    }  

    private static string GetMonthlyCountSqlStringForStatus(int status) {
        return 
        $@"
        select DATEADD(mm,0,DATEADD(mm, DATEDIFF(m,0,Created),0)) AS Date, 
                Count(Status) as StatusCount from ResourceContentVersionStatusHistory 
            WHERE Created >= DATEADD(MONTH, -6, GETDATE()) 
                and [Status] = {status} 
            GROUP By DATEADD(mm,0,DATEADD(mm, DATEDIFF(m,0,Created),0));
        ";
    }

    private static string GetMonthlyCountSqlStringForStatusByLanguage(int status, int languageId) {
        return 
        $@"
        select DATEADD(mm,0,DATEADD(mm, DATEDIFF(m,0,rcvsh.[Created]),0)) AS Date, Count(rcvsh.[Status]) as StatusCount from ResourceContentVersionStatusHistory rcvsh 
                JOIN ResourceContentVersions rcv on rcvsh.ResourceContentVersionId = rcv.Id
                JOIN ResourceContents rc on rcv.ResourceContentId = rc.Id
            WHERE  rc.LanguageId = {languageId} AND
                rcvsh.Created >= DATEADD(MONTH, -6, GETDATE()) AND
                rcvsh.[Status] = {status}
        GROUP BY DATEADD(mm,0,DATEADD(mm, DATEDIFF(m,0,rcvsh.[Created]),0));
        ";
    }
}