using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Dapper;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.TranslationPairs.List;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : Endpoint<Request, IEnumerable<Response>>
{
    public override void Configure()
    {
        Get("/translation-pairs");
        Permissions(PermissionName.GetTranslationPair);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var user = await userService.GetUserWithCompanyFromJwtAsync(ct);

        var query = """
                        SELECT TP.LanguageId,
                               L.EnglishDisplay AS LanguageEnglishDisplay,
                               TP.Id AS TranslationPairId,
                               TP.[Key] AS TranslationPairKey,
                               TP.Value AS TranslationPairValue
                        FROM CompanyLanguages CL
                        INNER JOIN Languages L ON CL.LanguageId = L.Id
                        INNER JOIN TranslationPairs TP ON L.Id = TP.LanguageId
                        WHERE CL.CompanyId = @CompanyId
                    """;

        var connection = dbContext.Database.GetDbConnection();
        var queryResults = await connection.QueryAsync<Response>(query, new { user.CompanyId });

        Response = queryResults.ToList();
    }
}