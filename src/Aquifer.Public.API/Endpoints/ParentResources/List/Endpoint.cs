using Aquifer.Common.Services.Caching;
using Aquifer.Data;
using Aquifer.Public.API.Helpers;
using FastEndpoints;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Public.API.Endpoints.ParentResources.List;

public sealed class Endpoint(AquiferDbContext _dbContext, ICachingLanguageService _cachingLanguageService)
    : Endpoint<Request, List<Response>>
{
    public override void Configure()
    {
        Get("/resources/parent-resources");
        Options(EndpointHelpers.UnauthenticatedServerCacheInSeconds(EndpointHelpers.TenMinutesInSeconds));
        Summary(s =>
        {
            s.Summary = "Gets a list of parent resources for a given language.";
            s.Description =
                "Returns all parent resources for a given language including the count of resources associated with each parent.";
        });
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var languageId = await GetLanguageIdAsync(req.LanguageId, req.LanguageCode, ct);

        var query = $"""
            SELECT
                COALESCE(prl.DisplayName, pr.DisplayName) AS DisplayName,
                (
                    SELECT
                        COUNT(rcv.Id)
                    FROM Resources r
                        LEFT JOIN ResourceContents rc ON rc.ResourceId = r.Id AND rc.LanguageId = @LanguageId
                        INNER JOIN ResourceContentVersions rcv ON rcv.ResourceContentId = rc.Id
                    WHERE
                        r.ParentResourceId = pr.Id AND
                        rcv.IsPublished = 1
                ) AS ResourceCountForLanguage,
                pr.LicenseInfo AS LicenseInfoValue,
                pr.ShortName,
                pr.Code,
                pr.Id,
                pr.ResourceType
            FROM
                ParentResources pr
                LEFT JOIN ParentResourceLocalizations prl ON prl.ParentResourceId = pr.Id AND prl.LanguageId = @LanguageId
            WHERE
                pr.Enabled = 1
            ORDER BY pr.Id
            """;

        var response = await _dbContext.Database
            .SqlQueryRaw<Response>(
                query,
                new SqlParameter("LanguageId", languageId))
            .ToListAsync(ct);

        await SendOkAsync(response, ct);
    }

    private async Task<int> GetLanguageIdAsync(int? languageId, string? languageCode, CancellationToken ct)
    {
        if (languageId is not null)
        {
            if (await _cachingLanguageService.GetLanguageCodeAsync(languageId.Value, ct) is null)
            {
                ThrowError(r => r.LanguageId, $"Invalid 'Language Id': \"{languageId.Value}\"");
            }

            return languageId.Value;
        }

        if (languageCode is not null)
        {
            var languageIdForCode = await _cachingLanguageService.GetLanguageIdAsync(languageCode, ct);
            if (languageIdForCode is null)
            {
                ThrowError(r => r.LanguageCode, $"Invalid 'Language Code': \"{languageCode}\"");
            }

            return languageIdForCode.Value;
        }

        throw new ArgumentException($"One of {nameof(languageId)} or {nameof(languageCode)} must be non-null.");
    }
}