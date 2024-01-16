using Aquifer.Data.Entities;

namespace Aquifer.Public.API.Endpoints.Resources.Search.GetResources;

public record Request
{
    public string Query { get; init; } = null!;
    public int LanguageId { get; set; }
    public string? LanguageCode { get; set; }
    public ResourceType ResourceType { get; init; }
    public int Take { get; init; } = 10;
    public int Skip { get; init; }
}