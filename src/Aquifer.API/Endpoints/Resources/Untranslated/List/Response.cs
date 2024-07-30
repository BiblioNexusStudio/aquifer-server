namespace Aquifer.API.Endpoints.Resources.Untranslated.List;

public record Response
{
    public required string Title { get; set; }
    public required int SortOrder { get; set; }
    public required int ResourceId { get; set; }
    public required int WordCount { get; set; }
    public required bool IsBeingAquiferized { get; set; }
}