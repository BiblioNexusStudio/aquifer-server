namespace Aquifer.API.Endpoints.Resources.Content.NeedsTranslation.List;

public record Request
{
    public int Limit { get; init; } = 10;
    public int Offset { get; init; } = 0;

    public int? ParentResourceId { get; set; }
    public string? BookCode { get; set; }
    public int? StartChapter { get; set; }
    public int? EndChapter { get; set; }
    public string? SearchQuery { get; set; }
}