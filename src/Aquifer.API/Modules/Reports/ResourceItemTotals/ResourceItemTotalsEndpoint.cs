using Aquifer.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Reports.ResourceItemTotals;

public static class ResourceItemTotalsEndpoint
{
    private const string TotalsQuery =
        """
        SELECT (SELECT COUNT(*)
         FROM Resources) AS TotalResources,
        
        (SELECT COUNT(*)
         FROM Resources
         WHERE Created > DATEADD(MM, DATEDIFF(MM, 0, GETUTCDATE()), 0)) AS TotalResourcesThisMonth,
        
        (SELECT COUNT(DISTINCT RES.Id)
         FROM Resources RES
                  INNER JOIN ResourceContents RC ON RC.ResourceId = RES.Id
         WHERE RC.LanguageId != 1) AS TotalNonEnglishResources,
        
        (SELECT COUNT(DISTINCT RES.Id)
         FROM Resources RES
                  INNER JOIN ResourceContents RC ON RC.ResourceId = RES.Id
         WHERE RC.LanguageId != 1
           AND RC.Created > DATEADD(MM, DATEDIFF(MM, 0, GETUTCDATE()), 0)) AS TotalNonEnglishResourcesThisMonth,
        
        (SELECT COUNT(*)
         FROM (SELECT RES.Id
               FROM Resources RES
                        INNER JOIN ResourceContents RC ON RC.ResourceId = RES.Id
               WHERE RC.MediaType != 2
               GROUP BY RES.Id
               HAVING COUNT(DISTINCT RC.LanguageId) > 1) TwoPlusResources) AS TotalResourcesTwoPlusLanguages,
        
        (SELECT COUNT(*)
         FROM (SELECT RES.Id
               FROM Resources RES
                        INNER JOIN ResourceContents RC ON RC.ResourceId = RES.Id
               WHERE RC.MediaType != 2 --  This is leaving out audio types from an earlier decision.
               GROUP BY RES.Id
               HAVING COUNT(DISTINCT RC.LanguageId) > 1
                  AND MAX(RC.Created) > DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0)) TwoPlusResources) AS TotalResourcesTwoPlusLanguagesThisMonth,
        
        (SELECT COUNT(DISTINCT RES.Id)
         FROM Resources RES
                  INNER JOIN ResourceContents RC ON RC.ResourceId = RES.Id
         WHERE RC.Status IN (2, 4, 5)) AS AquiferizedResources,
        
        (SELECT COUNT(DISTINCT RES.Id)
         FROM Resources RES
                  INNER JOIN ResourceContents RC ON RC.ResourceId = RES.Id
                  INNER JOIN ResourceContentVersions RCV ON RCV.ResourceContentId = RC.Id
                  INNER JOIN ResourceContentVersionStatusHistory RCVSH ON RCVSH.ResourceContentVersionId = RCV.Id
         WHERE RCVSH.Created > DATEADD(MM, DATEDIFF(MM, 0, GETUTCDATE()), 0)
           AND RCVSH.Status = 2) AS AquiferizedResourcesThisMonth,
        
        (SELECT COUNT(DISTINCT RES.Id)
         FROM Resources RES
                  INNER JOIN ResourceContents RC ON RC.ResourceId = RES.Id
         WHERE RC.Status IN (7, 8, 9)) AS TotalResourceBeingTranslated,
        
        (SELECT COUNT(DISTINCT RES.Id)
         FROM Resources RES
                  INNER JOIN ResourceContents RC ON RC.ResourceId = RES.Id
                  INNER JOIN ResourceContentVersions RCV ON RCV.ResourceContentId = RC.Id
                  INNER JOIN ResourceContentVersionStatusHistory RCVSH ON RCVSH.ResourceContentVersionId = RCV.Id
         WHERE RCVSH.Created > DATEADD(MM, DATEDIFF(MM, 0, GETUTCDATE()), 0)
           AND RCVSH.Status = 7) AS TotalResourceBeingTranslatedThisMonth;
        """;

    public static async Task<Ok<ResourceItemTotalsResponse>>
        HandleAsync(
            AquiferDbContext dbContext,
            CancellationToken cancellationToken)
    {
        var totals = await dbContext.Database
            .SqlQuery<ResourceItemTotalsResponse>($"exec ({TotalsQuery})")
            .ToListAsync(cancellationToken);

        return TypedResults.Ok(totals.Single());
    }
}