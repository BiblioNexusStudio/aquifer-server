using Aquifer.API.Helpers;
using Aquifer.Common;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.ParentResources.List;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, List<Response>>
{
    public override void Configure()
    {
        Get("/resources/parent-resources");
        AllowAnonymous();
        ResponseCache(EndpointHelpers.OneHourInSeconds);
        Options(EndpointHelpers.UnauthenticatedServerCacheInSeconds(EndpointHelpers.TenMinutesInSeconds));
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var fallbackMediaTypesSqlArray = string.Join(',', Constants.FallbackToEnglishForMediaTypes.Select(t => (int)t));

        var query = $"""
                         SELECT
                             COALESCE(prl.DisplayName, pr.DisplayName) AS DisplayName,
                             (
                                 SELECT
                                     COUNT(rcv.Id)
                                 FROM
                                     Resources r
                                     LEFT JOIN ResourceContents rc ON rc.ResourceId = r.Id AND rc.LanguageId = @LanguageId
                                     LEFT JOIN ResourceContents rce ON rce.ResourceId = r.Id AND rce.LanguageId = 1 AND rce.MediaType IN ({fallbackMediaTypesSqlArray})
                                     INNER JOIN ResourceContentVersions rcv ON rcv.ResourceContentId = COALESCE(rc.Id, rce.Id)
                                 WHERE
                                     r.ParentResourceId = pr.Id AND rcv.IsPublished = 1
                             ) AS ResourceCountForLanguage,
                             pr.Enabled,
                             pr.ComplexityLevel,
                             pr.LicenseInfo AS LicenseInfoValue,
                             pr.ShortName,
                             pr.Code,
                             pr.Id,
                             pr.ResourceType
                         FROM
                             ParentResources pr
                         LEFT JOIN ParentResourceLocalizations prl ON prl.ParentResourceId = pr.Id AND prl.LanguageId = @LanguageId
                         WHERE pr.Enabled = 1
                         ORDER BY COALESCE(prl.DisplayName, pr.DisplayName)
                     """;

        var response = await dbContext.Database
            .SqlQueryRaw<Response>(query,
                new SqlParameter("LanguageId", req.LanguageId))
            .ToListAsync(ct);

        await SendOkAsync(response, ct);
    }
}