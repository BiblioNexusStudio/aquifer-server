namespace Aquifer.API.Endpoints.Resources.Content.List;

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
    public required string LanguageEnglishDisplay { get; set; }
    public required string Status { get; set; }
    public required bool IsPublished { get; set; }
}