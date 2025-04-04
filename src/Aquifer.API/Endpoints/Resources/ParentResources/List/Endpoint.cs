using Aquifer.API.Helpers;
using Aquifer.API.Services;
using Aquifer.Common;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.ParentResources.List;

public class Endpoint(AquiferDbContext _dbContext, IUserService _userService) : Endpoint<Request, List<Response>>
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
        // Note that the server never caches when an auth header is sent, so while Well users would continue to get a cached version, a
        // community reviewer will always hit this. Thus, there's no risk of the community reviewer getting a cached version with everything
        // or vice versa.
        var isRequestedByCommunityReviewer =
            _userService.GetAllJwtRoles().FirstOrDefault() == UserRole.CommunityReviewer.ToString().ToLower();
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
                                     LEFT JOIN ResourceContents rce ON
                                         rce.ResourceId = r.Id AND
                                         rce.LanguageId = {Constants.EnglishLanguageId} AND
                                         rce.MediaType IN ({fallbackMediaTypesSqlArray})
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
                         WHERE pr.Enabled = 1 {(isRequestedByCommunityReviewer ? "AND pr.AllowCommunityReview = 1" : "")}
                         ORDER BY COALESCE(prl.DisplayName, pr.DisplayName)
                     """;

        var response = await _dbContext.Database
            .SqlQueryRaw<Response>(query, new SqlParameter("LanguageId", req.LanguageId))
            .ToListAsync(ct);

        await SendOkAsync(response, ct);
    }
}