using Aquifer.API.Helpers;
using Aquifer.Data;
using Aquifer.Data.Entities;
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
        Options(EndpointHelpers.SetCacheOption());
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var query = $"""
            SELECT
                COALESCE(prl.DisplayName, pr.DisplayName) AS DisplayName,
                (
                    SELECT
                        COUNT(rcv.Id)
                    FROM
                        ResourceContentVersions rcv
                        INNER JOIN ResourceContents rc ON rc.Id = rcv.ResourceContentId
                        INNER JOIN Resources r ON r.Id = rc.ResourceId
                    WHERE
                        r.ParentResourceId = pr.Id AND rc.LanguageId = @LanguageId AND rcv.IsPublished = 1
                ) AS ResourceCountForLanguage,
                pr.ComplexityLevel, pr.LicenseInfo AS LicenseInfoValue, pr.ShortName, pr.Id, pr.ResourceType
            FROM
                ParentResources pr
            LEFT JOIN ParentResourceLocalizations prl ON prl.ParentResourceId = pr.Id AND prl.LanguageId = @LanguageId
            WHERE @ResourceType = {(int)ResourceType.None} OR pr.ResourceType = @ResourceType
            ORDER BY COALESCE(prl.DisplayName, pr.DisplayName)
        """;

        var response = await dbContext.Database
            .SqlQueryRaw<Response>(query,
                new SqlParameter("LanguageId", req.LanguageId),
                new SqlParameter("ResourceType", req.ResourceType))
            .ToListAsync(ct);

        await SendOkAsync(response, ct);
    }
}