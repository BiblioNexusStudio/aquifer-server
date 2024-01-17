using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Reports;

public static class GetMonthlyReportsEndpoints 
{

     public static async Task<Results<Ok<MonthlyAquiferiationStartsAndCompletionsResponse>, NotFound>> GetPassagesByLanguageAndResource(
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var startedSqlStr = GetMonthlyCountSqlStringForStatus(ResourceContentStatus.AquiferizeInProgress);
        var monthlyStartedResources = await dbContext.Database
            .SqlQuery<StatusCountPerMonth>($"exec ({startedSqlStr})")
            .ToListAsync(cancellationToken);

        var completedSqlStr = GetMonthlyCountSqlStringForStatus(ResourceContentStatus.Complete);
        var monthlyCompletedResources = await dbContext.Database
            .SqlQuery<StatusCountPerMonth>($"exec ({completedSqlStr})")
            .ToListAsync(cancellationToken);
        

        return TypedResults.Ok(new MonthlyAquiferiationStartsAndCompletionsResponse(monthlyStartedResources, monthlyCompletedResources));
    }  

    private static string GetMonthlyCountSqlStringForStatus(ResourceContentStatus status) {
        return 
        $@"
        SELECT Month(Created) AS Month, Count(Status) as StatusAmount 
            FROM ResourceContentVersionStatusHistory 
            WHERE Created >= DATEADD(MONTH, -6, GETDATE()) and [Status] = {status}
            GROUP By MONTH(Created);
        ";
    }
}