using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Resources.Search;

public record Request
{
    public int LanguageId { get; set; }
    public string? Query { get; set; }
    public List<ResourceType> ResourceTypes { get; set; } = [];
    public string? BookCode { get; init; }
    public int? StartChapter { get; init; }
    public int? EndChapter { get; init; }
    public int? StartVerse { get; init; }
    public int? EndVerse { get; init; }
    public int Limit { get; init; } = 100;
    public int Offset { get; init; } = 0;
    public int? ParentResourceId { get; init; }
}