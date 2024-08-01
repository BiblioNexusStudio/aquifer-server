using Aquifer.API.Helpers;
using Aquifer.Common.Extensions;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.ParentResources.Statuses.List;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, List<Response>>
{
    private const int EnglishLanguageId = 1;

    private const string Query = """
                                 SELECT PR.DisplayName AS Title,
                                        PR.Id AS ResourceId,
                                        COUNT(RC.Id) AS TotalResources,
                                        OtherLanguage.Total AS TotalLanguageResources,
                                        OtherLanguage.LastPublished AS LastPublished,
                                        PR.LicenseInfo AS LicenseInfoValue,
                                        PR.ResourceType AS ResourceTypeValue
                                 FROM ResourceContents RC
                                          INNER JOIN Resources R ON R.Id = RC.ResourceId
                                          INNER JOIN ParentResources PR ON PR.Id = R.ParentResourceId
                                          CROSS APPLY (SELECT COUNT(RC2.Id) AS Total, MAX(RC2.Updated) AS LastPublished
                                                       FROM ResourceContents RC2
                                                                INNER JOIN ResourceContentVersions RCV ON RCV.ResourceContentId = RC2.Id AND RCV.IsPublished = 1
                                                                INNER JOIN Resources R2 ON R2.Id = RC2.ResourceId
                                                       WHERE R2.ParentResourceId = PR.Id
                                                         AND RC2.LanguageId = @LanguageId) OtherLanguage
                                 WHERE RC.LanguageId = @EnglishLanguageId
                                 GROUP BY PR.DisplayName, OtherLanguage.Total, OtherLanguage.LastPublished, PR.LicenseInfo, PR.ResourceType, PR.Id
                                 ORDER BY PR.DisplayName
                                 """;

    private static int _languageId;
    public override void Configure()
    {
        Get("/resources/parent-resources/statuses");
        Options(EndpointHelpers.SetCacheOption(60));
        ResponseCache(EndpointHelpers.OneDayInSeconds);
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        _languageId = request.LanguageId;
        var languageExists = dbContext.ResourceContents.Any(x => x.Language.Id == request.LanguageId);
        if (!languageExists)
        {
            await SendOkAsync([], ct);
        }

        var rows = await dbContext.Database
            .SqlQueryRaw<ParentAndLanguageRow>(Query,
                new SqlParameter("LanguageId", request.LanguageId),
                new SqlParameter("EnglishLanguageId", EnglishLanguageId))
            .ToListAsync(ct);

        Response = rows.Select(x => new Response
        {
            ResourceId = x.ResourceId,
            ResourceType = x.ResourceType,
            Title = x.Title,
            LicenseInfo = x.LicenseInfo,
            Status = x.Status
        }).ToList();

    }
    
    
    private record ParentAndLanguageRow {
        public ResourceType ResourceTypeValue { get; set; }

        public string ResourceType => ResourceTypeValue.GetDisplayName();
        public required string Title { get; set; }
        public required int ResourceId { get; set; }
        
        public string? LicenseInfoValue { get; set; }
        
        public object? LicenseInfo => LicenseInfoValue is null ? null : JsonUtilities.DefaultDeserialize(LicenseInfoValue);
        
        public int TotalResources { get; set; }
        
        public int TotalLanguageResources { get; set; }
        
        public DateTime? LastPublished { get; set; }

        public ParentResourceStatus Status => GetStatus(TotalResources, TotalLanguageResources, LastPublished);
    
        private ParentResourceStatus GetStatus(int totalCount, int totalLanguageCount, DateTime? lastPublished)
        {
            
            if (totalLanguageCount == 0)
            {
                return ParentResourceStatus.ComingSoon;
            }

            var today = DateTime.Today;
            var thirtyDaysAgo = today.AddDays(-30);
            
            // for English no need to check counts, we're assuming they're always complete
            if (_languageId == EnglishLanguageId)
            {
                return lastPublished > thirtyDaysAgo ? ParentResourceStatus.RecentlyCompleted : ParentResourceStatus.Complete;

            }

            if (totalLanguageCount < totalCount)
            {
                return lastPublished > thirtyDaysAgo ? ParentResourceStatus.RecentlyUpdated : ParentResourceStatus.Partial;
            }

            if (totalLanguageCount == totalCount && lastPublished > thirtyDaysAgo)
            {
                return ParentResourceStatus.RecentlyCompleted;
            }

            return ParentResourceStatus.Complete;
        }
    }
}