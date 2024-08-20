using Aquifer.API.Helpers;
using Aquifer.Common.Extensions;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Marketing.ParentResourceStatuses.List;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, IEnumerable<Response>>
{
    private const int EnglishLanguageId = 1;

    public override void Configure()
    {
        Get("/marketing/parent-resource-statuses");
        Options(EndpointHelpers.ServerCacheInSeconds(EndpointHelpers.OneHourInSeconds));
        ResponseCache(EndpointHelpers.OneHourInSeconds);
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var rows = await dbContext.Database.SqlQuery<ParentAndLanguageRow>($"""
                                                                            SELECT PR.DisplayName AS Title,
                                                                                   PR.Id AS ResourceId,
                                                                                   COUNT(RC.Id) AS TotalResources,
                                                                                   OtherLanguage.Total AS TotalLanguageResources,
                                                                                   OtherLanguage.LastPublished AS LastPublished,
                                                                                   PR.LicenseInfo AS LicenseInfoValue,
                                                                                   PR.ResourceType AS ResourceType
                                                                            FROM ResourceContents RC
                                                                                     INNER JOIN Resources R ON R.Id = RC.ResourceId
                                                                                     INNER JOIN ParentResources PR ON PR.Id = R.ParentResourceId AND PR.ForMarketing = 1
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

        var bibles = await dbContext.Bibles.Where(b => b.LanguageId == request.LanguageId).Select(b => new BibleParentAndLanguageRow
        {
            Title = b.Name, LicenseInfoValue = b.LicenseInfo
        }).ToListAsync(ct);

        var selectedBibles = bibles.Count > 0 ? bibles.Select(b => new Response
        {
            ResourceId = null,
            ResourceType = "Bible(s)",
            Title = b.Title,
            LicenseInfo = b.LicenseInfoValue is not null ? JsonUtilities.DefaultDeserialize<object>(b.LicenseInfoValue) : null,
            Status = ParentResourceStatus.Complete
        }) : [ new Response
        {
            ResourceId = null,
            Status = ParentResourceStatus.ComingSoon,
            Title = "Open License Needed",
            LicenseInfo = null,
            ResourceType = "Bible(s)"
        }];

        var selectedRows = rows.Select(x => new Response
        {
            ResourceId = x.ResourceId,
            ResourceType = x.ResourceType.GetDisplayName(),
            Title = x.Title,
            LicenseInfo = x.LicenseInfoValue is not null ? JsonUtilities.DefaultDeserialize<object>(x.LicenseInfoValue) : null,
            Status = ParentResourceStatusHelpers.GetStatus(x.TotalResources, x.TotalLanguageResources, x.LastPublished)
        });

        Response = selectedBibles.Concat(selectedRows);
    }
}

internal class BibleParentAndLanguageRow
{
    public string Title { get; set; } = null!;
    public string? LicenseInfoValue { get; set; }
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