using Aquifer.API.Helpers;
using Aquifer.Common.Extensions;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.ParentResources.Statuses.List;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, IEnumerable<Response>>
{
    private const int EnglishLanguageId = 1;

    public override void Configure()
    {
        Get("/resources/parent-resources/statuses");
        Options(EndpointHelpers.SetCacheOption(60));
        ResponseCache(EndpointHelpers.OneDayInSeconds, varyByQueryKeys: ["languageId"]);
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var rows = await dbContext.Database.SqlQueryRaw<ParentAndLanguageRow>($"""
                                                                               SELECT PR.DisplayName AS Title,
                                                                                      PR.Id AS ResourceId,
                                                                                      COUNT(RC.Id) AS TotalResources,
                                                                                      OtherLanguage.Total AS TotalLanguageResources,
                                                                                      OtherLanguage.LastPublished AS LastPublished,
                                                                                      PR.LicenseInfo AS LicenseInfoValue,
                                                                                      PR.ResourceType AS ResourceType
                                                                               FROM ResourceContents RC
                                                                                        INNER JOIN Resources R ON R.Id = RC.ResourceId
                                                                                        INNER JOIN ParentResources PR ON PR.Id = R.ParentResourceId
                                                                                        CROSS APPLY (SELECT COUNT(RC2.Id) AS Total, MAX(RC2.Updated) AS LastPublished
                                                                                                     FROM ResourceContents RC2
                                                                                                              INNER JOIN ResourceContentVersions RCV ON RCV.ResourceContentId = RC2.Id AND RCV.IsPublished = 1
                                                                                                              INNER JOIN Resources R2 ON R2.Id = RC2.ResourceId
                                                                                                     WHERE R2.ParentResourceId = PR.Id
                                                                                                       AND RC2.LanguageId = {request.LanguageId}) OtherLanguage
                                                                               WHERE RC.LanguageId = {EnglishLanguageId}
                                                                               GROUP BY PR.DisplayName, OtherLanguage.Total, OtherLanguage.LastPublished, PR.LicenseInfo, PR.ResourceType, PR.Id
                                                                               ORDER BY PR.DisplayName
                                                                               """)
            .ToListAsync(ct);

        Response = rows.Select(x => new Response
        {
            ResourceId = x.ResourceId,
            ResourceType = x.ResourceType.GetDisplayName(),
            Title = x.Title,
            LicenseInfo = x.LicenseInfoValue is not null ? JsonUtilities.DefaultDeserialize<object>(x.LicenseInfoValue) : null,
            Status = GetStatus(x.TotalResources, x.TotalLanguageResources, x.LastPublished)
        });
    }

    private ParentResourceStatus GetStatus(int totalCount, int totalLanguageCount, DateTime? lastPublished)
    {
        if (totalLanguageCount == 0)
        {
            return ParentResourceStatus.ComingSoon;
        }

        var thirtyDaysAgo = DateTime.UtcNow.AddDays(-30);
        if (totalLanguageCount < totalCount)
        {
            return lastPublished >= thirtyDaysAgo ? ParentResourceStatus.RecentlyUpdated : ParentResourceStatus.Partial;
        }

        if (totalLanguageCount == totalCount && lastPublished >= thirtyDaysAgo)
        {
            return ParentResourceStatus.RecentlyCompleted;
        }

        return ParentResourceStatus.Complete;
    }
}

internal class ParentAndLanguageRow
{
    public ResourceType ResourceType { get; set; }
    public string Title { get; set; } = null!;
    public int ResourceId { get; set; }
    public string? LicenseInfoValue { get; set; }
    public int TotalResources { get; set; }
    public int TotalLanguageResources { get; set; }
    public DateTime? LastPublished { get; set; }
}