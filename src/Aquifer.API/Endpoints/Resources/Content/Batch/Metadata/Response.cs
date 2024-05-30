namespace Aquifer.API.Endpoints.Resources.Content.Batch.Metadata;

public record Response
{
    public required int Id { get; set; }
    public required string? DisplayName { get; set; }
    public required object? Metadata { get; set; }
    public required IEnumerable<AssociatedResourceResponse> AssociatedResources { get; set; }
}

public record AssociatedResourceResponse
{
    public required string? ExternalId { get; set; }
    public required int ResourceId { get; set; }
    public required int ContentId { get; set; }
}