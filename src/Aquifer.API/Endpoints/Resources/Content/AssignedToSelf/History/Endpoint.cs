using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.AssignedToSelf.History;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : EndpointWithoutRequest<IEnumerable<Response>>
{
    public override void Configure()
    {
        Get("/resources/content/assigned-to-self/history");
        Permissions(PermissionName.ReadResources);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);
        var queryResults = await dbContext.ResourceContentVersionAssignedUserHistory
            .Where(x => x.ChangedByUserId == user.Id &&
                x.Created > DateTime.UtcNow.AddDays(-45) &&
                x.ResourceContentVersion.AssignedUserId != user.Id)
            .Select(x => new Response
            {
                Id = x.ResourceContentVersion.ResourceContentId,
                EnglishLabel = x.ResourceContentVersion.ResourceContent.Resource.EnglishLabel,
                LastActionTime = x.Created,
                ParentResourceName = x.ResourceContentVersion.ResourceContent.Resource.ParentResource.DisplayName,
                SortOrder = x.ResourceContentVersion.ResourceContent.Resource.SortOrder,
                SourceWords = x.ResourceContentVersion.SourceWordCount,
            })
            .ToListAsync(ct);

        Response = queryResults.GroupBy(g => g.Id).Select(g => g.OrderByDescending(r => r.LastActionTime).First());
    }
}