using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Common.Extensions;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.AssignedToSelf.List;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : EndpointWithoutRequest<IEnumerable<Response>>
{
    private const string Query = """
                                 SELECT RCV.ResourceContentId AS ResourceContentId, RCV.Id AS ResourceContentVersionId, R.EnglishLabel, PR.DisplayName AS ParentResourceName,
                                        L.EnglishDisplay AS LanguageEnglishDisplay, RCV.SourceWordCount AS WordCount, RC.Status AS StatusValue,
                                        P.Name AS ProjectName, P.ProjectedDeliveryDate, History.Created AS HistoryCreated, R.SortOrder, RC.ContentUpdated
                                 FROM ResourceContentVersions AS RCV
                                     INNER JOIN ResourceContents AS RC ON RCV.ResourceContentId = RC.Id
                                     INNER JOIN Resources AS R ON RC.ResourceId = R.Id
                                     INNER JOIN ParentResources AS PR ON R.ParentResourceId = PR.Id
                                     INNER JOIN Languages AS L ON RC.LanguageId = L.Id
                                     LEFT JOIN ProjectResourceContents PRC ON RC.Id = PRC.ResourceContentId
                                     LEFT JOIN Projects P ON PRC.ProjectId = P.Id
                                     CROSS APPLY (
                                         SELECT TOP 1 RCVAUH.Created AS Created
                                         FROM ResourceContentVersionAssignedUserHistory AS RCVAUH
                                         WHERE RCV.Id = RCVAUH.ResourceContentVersionId AND RCVAUH.AssignedUserId = {0}
                                         ORDER BY RCVAUH.Id DESC
                                     ) AS History
                                 WHERE RCV.AssignedUserId = {0} AND RC.Status != {1}
                                 """;

    public override void Configure()
    {
        Get("/resources/content/assigned-to-self");
        Permissions(PermissionName.ReadResources);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);
        var queryResults = await dbContext.Database
            .SqlQueryRaw<SqlQueryResult>(Query, user.Id, (int)ResourceContentStatus.TranslationNotStarted).ToListAsync(ct);
        var resourceContentVersionIds = queryResults.Select(x => x.ResourceContentVersionId);
        var lastAssignments =
            await Helpers.GetLastAssignmentsAsync(resourceContentVersionIds, dbContext, ct);

        Response = queryResults.Select(x => new Response
        {
            Id = x.ResourceContentId,
            EnglishLabel = x.EnglishLabel,
            ParentResourceName = x.ParentResourceName,
            LanguageEnglishDisplay = x.LanguageEnglishDisplay,
            WordCount = x.WordCount,
            StatusValue = x.StatusValue,
            SortOrder = x.SortOrder,
            ProjectName = x.ProjectName,
            Status = x.StatusValue.GetDisplayName(),
            StatusDisplayName = x.StatusValue.GetDisplayName(),
            DaysSinceAssignment = (DateTime.UtcNow - x.HistoryCreated).Days,
            DaysUntilProjectDeadline = x.ProjectedDeliveryDate == null
                ? null
                : (x.ProjectedDeliveryDate.Value.ToDateTime(new TimeOnly(23, 59)) - DateTime.UtcNow).Days,
            DaysSinceContentUpdated = x.ContentUpdated == null ? null : (DateTime.UtcNow - (DateTime)x.ContentUpdated).Days,
            LastAssignedUser = lastAssignments.FirstOrDefault(a => a.resourceContentVersionId == x.ResourceContentVersionId).user
        }).OrderByDescending(x => x.DaysSinceAssignment).ThenBy(x => x.ProjectName).ThenBy(x => x.EnglishLabel);
    }
}

internal class SqlQueryResult
{
    public required int ResourceContentId { get; set; }
    public required int ResourceContentVersionId { get; set; }
    public required string EnglishLabel { get; set; }
    public required string ParentResourceName { get; set; }
    public required string LanguageEnglishDisplay { get; set; }
    public required int? WordCount { get; set; }
    public required ResourceContentStatus StatusValue { get; set; }
    public required int SortOrder { get; set; }
    public string? ProjectName { get; set; }
    public DateTime? ContentUpdated { get; set; }
    public DateOnly? ProjectedDeliveryDate { get; set; }
    public DateTime HistoryCreated { get; set; }
}