using Aquifer.API.Services;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.AssignedToSelf.List;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : EndpointWithoutRequest<List<Response>>
{
    private const string Query = """
                                 SELECT RCV.ResourceContentId AS Id, R.EnglishLabel, PR.DisplayName AS ParentResourceName,
                                        L.EnglishDisplay AS LanguageEnglishDisplay, RCV.WordCount, RC.Status AS StatusValue,
                                        P.Name AS ProjectName, P.ProjectedDeliveryDate, History.Created AS HistoryCreated
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
                                 WHERE RCV.AssignedUserId = {0}
                                 """;

    public override void Configure()
    {
        Get("/resources/content/assigned-to-self");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);
        var resourceContents = (await dbContext.Database.SqlQueryRaw<Response>(Query, user.Id).ToListAsync(ct))
            .OrderByDescending(x => x.DaysSinceAssignment).ThenBy(x => x.ProjectName).ThenBy(x => x.EnglishLabel).ToList();

        await SendOkAsync(resourceContents, ct);
    }
}