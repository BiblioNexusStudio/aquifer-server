namespace Aquifer.API.Endpoints.Resources.Unaquiferized.List;

public record Response
{
    public required string Title { get; set; }
    public required int SortOrder { get; set; }
    public required int ResourceId { get; set; }
    public required int WordCount { get; set; }
}