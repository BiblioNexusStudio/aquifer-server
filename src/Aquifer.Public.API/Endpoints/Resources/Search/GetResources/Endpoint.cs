using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Public.API.Endpoints.Resources.Search.GetResources;

public class Endpoint(AquiferDbContext _dbContext) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/resources/search");
        ResponseCache(60);
        Summary(s =>
        {
            s.Summary = "Search resources by keyword query.";
            s.Description =
                "For a given query, language, and content type, search for matching resources.";
        });
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
        var resources = await _dbContext.ResourceContentVersions.Where(x => x.IsPublished &&
                                                                            (x.DisplayName.Contains(req.Query) ||
                                                                             x.ResourceContent.Resource.EnglishLabel
                                                                                 .Contains(req.Query)) &&
                                                                            (req.ResourceType == default ||
                                                                             x.ResourceContent.Resource.ParentResource
                                                                                 .ResourceType ==
                                                                             req.ResourceType) &&
                                                                            (x.ResourceContent.LanguageId ==
                                                                             req.LanguageId ||
                                                                             x.ResourceContent.Language
                                                                                 .ISO6393Code ==
                                                                             req.LanguageCode))
            .OrderBy(x => x.ResourceContent.Resource.EnglishLabel)
            .Skip(req.Offset)
            .Take(req.Limit)
            .Select(x => new ResponseContent
            {
                Id = x.ResourceContent.Id,
                Name = x.ResourceContent.Resource.EnglishLabel,
                LocalizedName = x.DisplayName,
                LanguageCode = x.ResourceContent.Language.ISO6393Code,
                MediaType = x.ResourceContent.MediaType,
                Grouping = new ResourceTypeMetadata
                {
                    Name = x.ResourceContent.Resource.ParentResource.DisplayName,
                    Type = x.ResourceContent.Resource.ParentResource.ResourceType
                }
            })
            .ToListAsync(ct);

        if (resources.Count is 0)
        {
            ThrowError("No records found for the given request", 404);
        }

        return resources;
    }

    public override void OnValidationFailed()
    {
        var resourceTypeFailure = ValidationFailures.FirstOrDefault(x => x.PropertyName.ToLower() == "resourcetype");
        if (resourceTypeFailure is not null)
        {
            var validValues = string.Join(", ", Enum.GetValues(typeof(ResourceType)).Cast<ResourceType>().ToList());
            var errorMessage = $"{resourceTypeFailure.ErrorMessage} Valid values are {validValues}";
            resourceTypeFailure.ErrorMessage = errorMessage;
        }

        base.OnValidationFailed();
    }
}
