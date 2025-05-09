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

    // If the resource is a non-text media type (like Audio) this id helps link it to its text counterpart
    public required int? DependentOnId { get; set; }
    public required string MediaType { get; set; }
    public required int ParentResourceId { get; set; }
    public required int Version { get; set; }
    public required string ResourceType { get; set; }
}