using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.NextUp.Get;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : Endpoint<Request, Response>
{
    private const string NextUpForEditorQuery = """
                                                WITH UserAssignedResources AS (
                                                   SELECT RCV.ResourceContentId,
                                                          ROW_NUMBER() OVER (ORDER BY DATEDIFF(DAY, History.Created, GETDATE()) DESC, R.SortOrder ASC, R.EnglishLabel ASC) AS RowNum
                                                   FROM ResourceContentVersions AS RCV
                                                       INNER JOIN ResourceContents AS RC ON RCV.ResourceContentId = RC.Id
                                                       INNER JOIN Resources AS R ON RC.ResourceId = R.Id
                                                       CROSS APPLY (
                                                           SELECT TOP 1 RCVAUH.Created AS Created
                                                           FROM ResourceContentVersionAssignedUserHistory AS RCVAUH
                                                           WHERE RCV.Id = RCVAUH.ResourceContentVersionId AND RCVAUH.AssignedUserId = {0}
                                                           ORDER BY RCVAUH.Id DESC
                                                       ) AS History
                                                   WHERE RCV.AssignedUserId = {0}
                                                )
                                                SELECT ResourceContentId AS Value
                                                FROM UserAssignedResources
                                                WHERE RowNum = (
                                                    SELECT RowNum + 1
                                                    FROM UserAssignedResources
                                                    WHERE ResourceContentId = {1}
                                                );
                                                """;

    private const string NextUpForManagerQuery = """
                                                 WITH UserAssignedResources AS (
                                                    SELECT RCV.ResourceContentId,
                                                           ROW_NUMBER() OVER (ORDER BY DATEDIFF(DAY, GETDATE(), COALESCE(P.ProjectedDeliveryDate, '2100-12-31')) ASC,
                                                                              P.Name ASC, R.SortOrder ASC, R.EnglishLabel ASC) AS RowNum
                                                    FROM ResourceContentVersions AS RCV
                                                        INNER JOIN ResourceContents AS RC ON RCV.ResourceContentId = RC.Id
                                                        INNER JOIN Resources AS R ON RC.ResourceId = R.Id
                                                        LEFT JOIN ProjectResourceContents PRC ON RC.Id = PRC.ResourceContentId
                                                        LEFT JOIN Projects P ON PRC.ProjectId = P.Id
                                                    WHERE RCV.AssignedUserId = {0}
                                                 )
                                                 SELECT ResourceContentId AS Value
                                                 FROM UserAssignedResources
                                                 WHERE RowNum = (
                                                     SELECT RowNum + 1
                                                     FROM UserAssignedResources
                                                     WHERE ResourceContentId = {1}
                                                 );
                                                 """;

    public override void Configure()
    {
        Get("/resources/content/{Id}/next-up");
        Permissions(PermissionName.EditContent);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);
        var response = new Response
        {
            NextUpResourceContentId =
                (await dbContext.Database
                    .SqlQueryRaw<int?>(user.Role == UserRole.Manager ? NextUpForManagerQuery : NextUpForEditorQuery, user.Id,
                        request.Id).ToListAsync(ct)).FirstOrDefault()
        };
        await SendOkAsync(response, ct);
    }
}