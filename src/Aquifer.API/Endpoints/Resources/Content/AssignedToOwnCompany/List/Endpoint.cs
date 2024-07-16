using Aquifer.API.Common;
using Aquifer.API.Common.Dtos;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

namespace Aquifer.API.Endpoints.Resources.Content.AssignedToOwnCompany.List;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : EndpointWithoutRequest<IEnumerable<Response>>
{
    public override void Configure()
    {
        Get("/resources/content/assigned-to-own-company");
        Permissions(PermissionName.ReadCompanyContentAssignments);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);
        List<ResourceContentStatus> statuses =
        [
            ResourceContentStatus.New,
            ResourceContentStatus.AquiferizeInProgress,
            ResourceContentStatus.TranslationNotStarted,
            ResourceContentStatus.TranslationInProgress,
            ResourceContentStatus.AquiferizeManagerReview,
            ResourceContentStatus.TranslationManagerReview
        ];

        var query = $"""
                     SELECT RCV.ResourceContentId AS ResourceContentId, RCV.Id AS ResourceContentVersionId, R.EnglishLabel, PR.DisplayName AS ParentResourceName, U.Id AS UserId,
                            U.FirstName AS UserFirstName, U.LastName AS UserLastName, RC.Status AS StatusValue,
                            L.EnglishDisplay AS LanguageEnglishDisplay, RCV.WordCount, P.Name AS ProjectName,
                            P.ProjectedDeliveryDate AS ProjectProjectedDeliveryDate, R.SortOrder, RC.ContentUpdated
                     FROM ResourceContentVersions AS RCV
                              INNER JOIN Users AS U ON RCV.AssignedUserId = U.Id
                              INNER JOIN ResourceContents AS RC ON RCV.ResourceContentId = RC.Id
                              INNER JOIN Resources AS R ON RC.ResourceId = R.Id
                              INNER JOIN ParentResources AS PR ON R.ParentResourceId = PR.Id
                              INNER JOIN Languages AS L ON RC.LanguageId = L.Id
                              INNER JOIN ProjectResourceContents PRC ON PRC.ResourceContentId = RC.Id
                              INNER JOIN Projects P ON P.Id = PRC.ProjectId
                     WHERE RCV.IsDraft = 1 AND U.CompanyId = {user.CompanyId} AND RC.Status IN ({string.Join(',', statuses.Select(x => (int)x))})
                     """;

        var queryResponse = await dbContext.Database.SqlQueryRaw<SqlQueryResult>(query).ToListAsync(ct);

        var resourceContentVersionIds = queryResponse.Select(x => x.ResourceContentVersionId);

        List<(int resourceContentVersionIds, UserDto? user)> lastAssignments =
            await Helpers.GetLastAssignmentsAsync(resourceContentVersionIds, dbContext, ct);

        Response = queryResponse.Select(x => new Response
        {
            Id = x.ResourceContentId,
            EnglishLabel = x.EnglishLabel,
            ParentResourceName = x.ParentResourceName,
            LanguageEnglishDisplay = x.LanguageEnglishDisplay,
            WordCount = x.WordCount,
            ProjectName = x.ProjectName,
            SortOrder = x.SortOrder,
            StatusValue = x.StatusValue,
            StatusDisplayName = x.StatusValue.GetDisplayName(),
            AssignedUser = new UserDto
            {
                Id = x.UserId,
                Name = $"{x.UserFirstName} {x.UserLastName}"
            },
            DaysSinceContentUpdated = x.ContentUpdated == null ? null : (DateTime.UtcNow - (DateTime)x.ContentUpdated).Days,
            DaysUntilProjectDeadline =
                x.ProjectProjectedDeliveryDate == null
                    ? null
                    : (x.ProjectProjectedDeliveryDate.Value.ToDateTime(new TimeOnly(23, 59)) - DateTime.UtcNow).Days,
            LastAssignedUser = lastAssignments.FirstOrDefault(a => a.resourceContentVersionIds == x.ResourceContentVersionId).user
        }).OrderBy(x => x.DaysUntilProjectDeadline)
            .ThenBy(x => x.ProjectName)
            .ThenBy(x => x.ParentResourceName)
            .ThenBy(x => x.SortOrder)
            .ThenBy(x => x.EnglishLabel)
            .ToList();
    }
}

internal class SqlQueryResult
{
    public required int ResourceContentId { get; set; }
    public required int ResourceContentVersionId { get; set; }
    public required string EnglishLabel { get; set; }
    public required string ParentResourceName { get; set; }
    public required int UserId { get; set; }
    public required string UserFirstName { get; set; }
    public required string UserLastName { get; set; }
    public required ResourceContentStatus StatusValue { get; set; }
    public required string LanguageEnglishDisplay { get; set; }
    public required int? WordCount { get; set; }
    public required string ProjectName { get; set; }
    public required DateOnly? ProjectProjectedDeliveryDate { get; set; }
    public required int SortOrder { get; set; }
    public required DateTime? ContentUpdated { get; set; }
}