using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.AssignedToOwnCompany.List;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : EndpointWithoutRequest<List<Response>>
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
                     SELECT RCV.ResourceContentId AS Id, R.EnglishLabel, PR.DisplayName AS ParentResourceName, U.Id AS UserId,
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

        var queryResponse = (await dbContext.Database.SqlQueryRaw<Response>(query).ToListAsync(ct)).OrderBy(x => x.DaysUntilProjectDeadline)
            .ThenBy(x => x.ProjectName)
            .ThenBy(x => x.ParentResourceName)
            .ThenBy(x => x.SortOrder)
            .ThenBy(x => x.EnglishLabel)
            .ToList();

        await SendOkAsync(queryResponse, ct);
    }
}