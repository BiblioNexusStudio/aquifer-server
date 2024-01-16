using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Public.API.Endpoints.Resources.Search.GetResources;

public class Endpoint(AquiferDbContext _dbContext) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/resources/search");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var items = await GetResourcesAsync(req, ct);

        await SendAsync(new Response
            {
                Items = items,
                ItemCount = items.Count
            },
            200,
            ct);
    }

    private async Task<List<ResponseContent>> GetResourcesAsync(Request req, CancellationToken ct)
    {
        var resources = await _dbContext.ResourceContents.Where(x =>
                x.Resource.EnglishLabel.Contains(req.Query) &&
                (req.ResourceType == default || x.Resource.ParentResource.ResourceType == req.ResourceType) &&
                (x.LanguageId == req.LanguageId || x.Language.ISO6393Code == req.LanguageCode) &&
                x.Versions.Any(v => v.IsPublished))
            .OrderBy(x => x.Resource.EnglishLabel)
            .Skip(req.Skip)
            .Take(req.Take)
            .Select(x => new ResponseContent
            {
                Id = x.Id,
                Name = x.Resource.EnglishLabel,
                LanguageCode = x.Language.ISO6393Code,
                Grouping = new ResourceTypeMetadata
                {
                    Name = x.Resource.ParentResource.DisplayName,
                    Type = x.Resource.ParentResource.ResourceType
                }
            })
            .ToListAsync(ct);

        if (resources.Count is 0)
        {
            ThrowError("No records found for the given request", 404);
        }

        return resources;
    }
}