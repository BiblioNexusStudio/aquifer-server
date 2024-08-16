namespace Aquifer.API.Endpoints.Reports.Resources.MostRequestedResources;

public record Response
{
    public required string Resource { get; init; }
    public required int Count { get; init; }
    public required string Language { get; init; }
    public required string Label { get; init; }
}