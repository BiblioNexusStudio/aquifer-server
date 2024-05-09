using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.ToAssign.List;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : EndpointWithoutRequest<List<Response>>
{
    private const string Query = """
                                 SELECT RCV.ResourceContentId AS Id, R.EnglishLabel, PR.DisplayName AS ParentResourceName,
                                        L.EnglishDisplay AS LanguageEnglishDisplay, RCV.WordCount, P.Name AS ProjectName,
                                        P.ProjectedDeliveryDate AS ProjectProjectedDeliveryDate
                                 FROM ResourceContentVersions AS RCV
                                          INNER JOIN ResourceContents AS RC ON RCV.ResourceContentId = RC.Id
                                          INNER JOIN Resources AS R ON RC.ResourceId = R.Id
                                          INNER JOIN ParentResources AS PR ON R.ParentResourceId = PR.Id
                                          INNER JOIN Languages AS L ON RC.LanguageId = L.Id
                                          INNER JOIN ProjectResourceContents PRC ON PRC.ResourceContentId = RC.Id
                                          INNER JOIN Projects P ON P.Id = PRC.ProjectId
                                 WHERE RCV.IsDraft = 1 AND RCV.AssignedUserId = @UserId AND RC.Status IN (1, 6)
                                 """;

    public override void Configure()
    {
        Get("/resources/content/to-assign");
        Permissions(PermissionName.AssignOverride);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);
        var resources = await dbContext.Database.SqlQueryRaw<Response>(Query, new SqlParameter("UserId", user.Id)).ToListAsync(ct);

        await SendOkAsync(resources, ct);
    }
}