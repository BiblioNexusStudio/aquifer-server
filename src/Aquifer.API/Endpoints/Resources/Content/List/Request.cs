namespace Aquifer.API.Endpoints.Resources.Content.List;

public record Request
{
    public int Limit { get; init; } = 10;
    public int Offset { get; init; } = 0;

    public int? LanguageId { get; set; }
    public int? ParentResourceId { get; set; }
    public string? BookCode { get; set; }
    public int? StartChapter { get; set; }
    public int? EndChapter { get; set; }
    public bool? IsNotApplicable { get; set; }
    public bool? IsPublished { get; set; }
    public bool? HasAudio { get; set; }
    public bool? HasUnresolvedCommentThreads { get; set; }
    public string? SearchQuery { get; set; }
}