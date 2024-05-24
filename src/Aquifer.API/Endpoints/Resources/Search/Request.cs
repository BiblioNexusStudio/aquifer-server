using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Resources.Search;

public record Request
{
    public int LanguageId { get; set; }
    public string Query { get; set; } = null!;
    public List<ResourceType> ResourceTypes { get; set; } = [];
}