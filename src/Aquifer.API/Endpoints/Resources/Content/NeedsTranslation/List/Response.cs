namespace Aquifer.API.Endpoints.Resources.Content.NeedsTranslation.List;

public record Response
{
    public required List<ResourceContentResponse> ResourceContents { get; set; }
    public required int Total { get; set; }
}

public record ResourceContentResponse
{
    public required int Id { get; set; }
    public required string EnglishLabel { get; set; }
    public required string ParentResourceName { get; set; }
    public required int? WordCount { get; set; }
}