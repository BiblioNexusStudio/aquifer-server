using Aquifer.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Resources.ResourcesList;

public static class ResourcesListEndpoints
{
    public static async Task<Ok<List<ResourceListItemResponse>>> Get([AsParameters] ResourceListRequest request,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        string query = request.Query ?? string.Empty;
        var resourceTypes = await dbContext.Resources.Where(x => x.EnglishLabel.Contains(query)).OrderBy(x => x.Id)
            .Skip(request.Skip).Take(request.Take)
            .Select(x => new ResourceListItemResponse(x.Id,
                x.EnglishLabel,
                x.Type.DisplayName,
                x.ResourceContents.Select(rc => rc.Status)
                    .OrderBy(status => (int)status)
                    .First())).ToListAsync(cancellationToken);

        return TypedResults.Ok(resourceTypes);
    }
}