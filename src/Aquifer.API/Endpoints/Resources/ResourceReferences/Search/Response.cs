namespace Aquifer.API.Endpoints.Resources.ResourceReferences.Search;

public record Response
{
    public required int ResourceId { get; set; }
    public required string EnglishLabel { get; set; }
}