using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Aquifer.API.Modules.AdminResources.ResourcesList;

public static class ResourcesListEndpoints
{
    public static async Task<Results<Ok<List<ResourceListItemResponse>>, ValidationProblem>> Get(
        [AsParameters] ResourceListRequest request,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        // There are probably better long term solutions for this sort of thing, something to explore.
        if (request.Take is > 100 or < 1 || request.Skip < 0)
        {
            return TypedResults.ValidationProblem(new Dictionary<string, string[]>
            {
                { "outOfRange", new[] { "Take must between 1 and 100. Skip must be greater than 0." } }
            });
        }

        var resourceFilter = CreateResourceFilterExpression(request);
        var resources = await dbContext.Resources.Where(resourceFilter)
            .OrderBy(x => x.EnglishLabel)
            .Skip(request.Skip).Take(request.Take)
            .Select(x => new ResourceListItemResponse
            {
                EnglishLabel = x.EnglishLabel,
                ParentResourceName = x.ParentResource.DisplayName,
                Status = x.ResourceContents.Select(rc => rc.Status).OrderBy(status => (int)status).First(),
                ContentIdsWithLanguageIds = x.ResourceContents
                    .Select(rc => new ResourceListItemForLanguageResponse
                    {
                        ContentId = rc.Id,
                        LanguageId = rc.LanguageId
                    })
            }).ToListAsync(cancellationToken);

        return TypedResults.Ok(resources);
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
            (request.ParentResourceId == default || x.ParentResourceId == request.ParentResourceId) &&
            (request.LanguageId == default || x.ResourceContents.Any(rc => rc.LanguageId == request.LanguageId));
    }
}