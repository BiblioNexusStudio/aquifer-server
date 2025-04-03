namespace Aquifer.API.Endpoints.Resources.Unaquiferized.List;

public record Request
{
    public int LanguageId { get; set; }
    public int ParentResourceId { get; set; }
    public string? BookCode { get; set; }
    public int[]? Chapters { get; set; }
    public string? SearchQuery { get; set; }
}