using Aquifer.API.Common;
using Aquifer.Common;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.NextUp.Community;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/resources/content/{Id}/next-up/community");
        Permissions(PermissionName.SendReviewCommunityContent);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var resourceContentFromReq =
            await dbContext.ResourceContents.Include(x => x.Resource).SingleOrDefaultAsync(x => x.Id == request.Id, ct);

        if (resourceContentFromReq is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var query = $"""
                     SELECT TOP 1 RC.Id
                     FROM Resources R
                     INNER JOIN ResourceContents RC ON RC.ResourceId = R.Id AND RC.LanguageId = {Constants.EnglishLanguageId}
                     INNER JOIN ResourceContentVersions RCV ON RC.Id = RCV.ResourceContentId AND RCV.IsPublished = 1
                     LEFT OUTER JOIN ResourceContents RC2 ON RC2.ResourceId = R.Id AND RC2.LanguageId = {resourceContentFromReq.LanguageId}
                     WHERE R.ParentResourceId = {resourceContentFromReq.Resource.ParentResourceId}
                     AND R.SortOrder >= {resourceContentFromReq.Resource.SortOrder}
                     AND RC2.Id IS NULL
                     ORDER BY R.SortOrder, R.EnglishLabel
                     """;

        var result = await dbContext.Database.SqlQueryRaw<int>(query).ToListAsync(ct);

        var response = new Response { NextUpResourceContentId = result.SingleOrDefault() };

        await SendAsync(response, cancellation: ct);
    }
}