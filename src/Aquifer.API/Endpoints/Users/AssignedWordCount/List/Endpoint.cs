using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Users.AssignedWordCount.List;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : EndpointWithoutRequest<List<Response>>
{
    private const string Query = """
        WITH AssignedUserWordCounts AS (SELECT U.Id AS UserId
                                             , U.FirstName + ' ' + U.LastName AS UserName
                                             , COALESCE(RCV.SourceWordCount, 0) AS AssignedSourceWordCount
                                        FROM Companies C
                                        INNER JOIN Users U ON C.Id = U.CompanyId AND U.Enabled = 1
                                        LEFT JOIN ResourceContentVersions RCV ON U.Id = RCV.AssignedUserId
                                        WHERE C.Id = {0})
        SELECT UserId, UserName, SUM(AssignedSourceWordCount) AS AssignedSourceWordCount
        FROM AssignedUserWordCounts
        GROUP BY UserId, UserName
        ORDER BY UserName
        """;

    public override void Configure()
    {
        Get("/users/assigned-word-count");
        Permissions(PermissionName.ReadCompanyContentAssignments);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var self = await userService.GetUserFromJwtAsync(ct);
        Response = await dbContext.Database.SqlQueryRaw<Response>(Query, self.CompanyId).ToListAsync(ct);
    }
}