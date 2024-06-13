namespace Aquifer.API.Endpoints.Resources.Content.GroupedByVerse.List;

public record Response
{
    public required IEnumerable<ResourcesForVerseResponse> Verses { get; set; }
}

public record ResourcesForVerseResponse
{
    public required int Number { get; set; }
    public required IEnumerable<ResourceContentResponse> ResourceContents { get; set; }
}

public record ResourceContentResponse
{
    public required int Id { get; set; }
    public required string MediaType { get; set; }
    public required int ParentResourceId { get; set; }
    public required int Version { get; set; }
    public required string ResourceType { get; set; }
}