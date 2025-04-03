namespace Aquifer.API.Endpoints.Resources.Untranslated.List;

public record Request
{
    public int SourceLanguageId { get; set; }
    public int TargetLanguageId { get; set; }
    public int ParentResourceId { get; set; }
    public string? BookCode { get; set; }
    public int[]? Chapters { get; set; }
    public string? SearchQuery { get; set; }
}