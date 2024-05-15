namespace Aquifer.API.Endpoints.Resources.Content.GroupedByVerse.List;

public record Request
{
    public int LanguageId { get; set; }
    public string BookCode { get; set; } = null!;
    public int Chapter { get; set; }
}