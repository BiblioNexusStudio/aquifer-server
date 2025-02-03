namespace Aquifer.API.Endpoints.Resources.Content.List;

public record Response
{
    public required IReadOnlyList<ResourceContentResponse> ResourceContents { get; init; }
    public required int Total { get; init; }
}

public record ResourceContentResponse
{
    public required int Id { get; init; }
    public required string EnglishLabel { get; init; }
    public required string ParentResourceName { get; init; }
    public required string LanguageEnglishDisplay { get; init; }
    public required string Status { get; init; }
    public required bool IsPublished { get; init; }
    public required bool HasAudio { get; init; }
    public required bool HasUnresolvedCommentThreads { get; init; }
}