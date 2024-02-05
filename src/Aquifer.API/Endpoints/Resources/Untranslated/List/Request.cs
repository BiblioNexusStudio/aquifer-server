namespace Aquifer.API.Endpoints.Resources.Untranslated.List;

public record Request
{
    public required int LanguageId { get; set; }
    public required int ParentResourceId { get; set; }
    public string? BookCode { get; set; }
    public int[]? Chapters { get; set; }
    public string? SearchQuery { get; set; }
}