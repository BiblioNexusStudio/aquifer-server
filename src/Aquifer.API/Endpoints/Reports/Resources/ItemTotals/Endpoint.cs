using Aquifer.API.Common;
using Aquifer.API.Helpers;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Reports.Resources.ItemTotals;

public class Endpoint(AquiferDbContext dbContext) : EndpointWithoutRequest<Response>
{
    private readonly string _totalsQuery =
        $"""
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
                WHERE RC.MediaType != 2 -- This is leaving out audio types from an earlier decision.
                GROUP BY RES.Id
                HAVING COUNT(DISTINCT RC.LanguageId) > 1) TwoPlusResources) AS TotalResourcesTwoPlusLanguages,

         (SELECT COUNT(*)
          FROM (SELECT RES.Id
                FROM Resources RES
                         INNER JOIN ResourceContents RC ON RC.ResourceId = RES.Id
                WHERE RC.MediaType != 2 -- This is leaving out audio types from an earlier decision.
                GROUP BY RES.Id
                HAVING COUNT(DISTINCT RC.LanguageId) > 1
                   AND MAX(RC.Created) > DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0)) TwoPlusResources) AS TotalResourcesTwoPlusLanguagesThisMonth,

         (SELECT COUNT(DISTINCT RES.Id)
          FROM Resources RES
                   INNER JOIN ResourceContents RC ON RC.ResourceId = RES.Id
          WHERE RC.Status IN (
              {(int)ResourceContentStatus.AquiferizeEditorReview},
              {(int)ResourceContentStatus.AquiferizeReviewPending},
              {(int)ResourceContentStatus.AquiferizePublisherReview})) AS AquiferizedResources,

         (SELECT COUNT(DISTINCT RES.Id)
          FROM Resources RES
                   INNER JOIN ResourceContents RC ON RC.ResourceId = RES.Id
                   INNER JOIN ResourceContentVersions RCV ON RCV.ResourceContentId = RC.Id
                   INNER JOIN ResourceContentVersionStatusHistory RCVSH ON RCVSH.ResourceContentVersionId = RCV.Id
          WHERE RCVSH.Created > DATEADD(MM, DATEDIFF(MM, 0, GETUTCDATE()), 0)
            AND RCVSH.Status = {(int)ResourceContentStatus.AquiferizeEditorReview}) AS AquiferizedResourcesThisMonth,

         (SELECT COUNT(DISTINCT RES.Id)
          FROM Resources RES
                   INNER JOIN ResourceContents RC ON RC.ResourceId = RES.Id
          WHERE RC.Status IN (
              {(int)ResourceContentStatus.TranslationEditorReview},
              {(int)ResourceContentStatus.TranslationReviewPending},
              {(int)ResourceContentStatus.TranslationPublisherReview}
              )) AS TotalResourceBeingTranslated,

         (SELECT COUNT(DISTINCT RES.Id)
          FROM Resources RES
                   INNER JOIN ResourceContents RC ON RC.ResourceId = RES.Id
                   INNER JOIN ResourceContentVersions RCV ON RCV.ResourceContentId = RC.Id
                   INNER JOIN ResourceContentVersionStatusHistory RCVSH ON RCVSH.ResourceContentVersionId = RCV.Id
          WHERE RCVSH.Created > DATEADD(MM, DATEDIFF(MM, 0, GETUTCDATE()), 0)
            AND RCVSH.Status = {(int)ResourceContentStatus.TranslationEditorReview}) AS TotalResourceBeingTranslatedThisMonth;
         """;

    public override void Configure()
    {
        Get("/reports/resources/item-totals");
        Permissions(PermissionName.ReadReports);
        ResponseCache(EndpointHelpers.OneHourInSeconds);
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var totals = await dbContext.Database
            .SqlQuery<Response>($"exec ({_totalsQuery})")
            .ToListAsync(cancellationToken);

        await SendAsync(totals.Single(), cancellation: cancellationToken);
    }
}