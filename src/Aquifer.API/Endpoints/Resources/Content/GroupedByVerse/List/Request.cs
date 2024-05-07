namespace Aquifer.API.Endpoints.Resources.Content.GroupedByVerse.List;

public record Request
{
    public bool FirstTest { get; set; }
    public int LanguageId { get; set; }
    public string? BookCode { get; set; }
    public int Chapter { get; set; }
}