using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.ToAssign.List;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : EndpointWithoutRequest<List<Response>>
{
    public override void Configure()
    {
        Get("/resources/content/to-assign");
        Permissions(PermissionName.AssignOverride);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);
        var query = $"""
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
                     WHERE RCV.IsDraft = 1 AND RCV.AssignedUserId = {user.Id}
                       AND RC.Status IN ({(int)ResourceContentStatus.New}, {(int)ResourceContentStatus.TranslationNotStarted})
                     """;

        var resources = await dbContext.Database.SqlQueryRaw<Response>(query).ToListAsync(ct);

        await SendOkAsync(resources, ct);
    }
}