namespace Aquifer.API.Endpoints.Resources.Unaquiferized.List;

public record Request
{
    public required int ParentResourceId { get; set; }
    public string? BookCode { get; set; }
    public int[]? Chapters { get; set; }
    public string? SearchQuery { get; set; }
}