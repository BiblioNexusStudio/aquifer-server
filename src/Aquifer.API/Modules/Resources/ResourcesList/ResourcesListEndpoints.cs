using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Aquifer.API.Modules.Resources.ResourcesList;

public static class ResourcesListEndpoints
{
    public static async Task<Ok<List<ResourceListItemResponse>>> Get([AsParameters] ResourceListRequest request,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var resourceFilter = CreateResourceFilterExpression(request);
        var resourceTypes = await dbContext.Resources.Where(resourceFilter)
            .OrderBy(x => x.Id)
            .Skip(request.Skip).Take(request.Take)
            .Select(x => new ResourceListItemResponse(x.Id,
                x.EnglishLabel,
                x.Type.DisplayName,
                x.ResourceContents.Select(rc => rc.Status)
                    .OrderBy(status => (int)status)
                    .First())).ToListAsync(cancellationToken);

        return TypedResults.Ok(resourceTypes);
    }

    public static async Task<Ok<int>> GetCount([AsParameters] ResourceListCountRequest request,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var resourceFilter = CreateResourceFilterExpression(request);
        int count = await dbContext.Resources.CountAsync(resourceFilter, cancellationToken);

        return TypedResults.Ok(count);
    }

    private static Expression<Func<ResourceEntity, bool>> CreateResourceFilterExpression(
        ResourceListCountRequest request)
    {
        string query = request.Query ?? string.Empty;
        return x =>
            x.EnglishLabel.Contains(query) &&
            (request.ResourceTypeId == default || x.TypeId == request.ResourceTypeId) &&
            (request.LanguageId == default || x.ResourceContents.Any(rc => rc.LanguageId == request.LanguageId));
    }
}