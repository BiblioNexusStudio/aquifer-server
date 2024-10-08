using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Resources.Content.Metadata;

public record Response
{
    public required int Id { get; set; }
    public required string? DisplayName { get; set; }
    public required object? Metadata { get; set; }
    public required ResourceContentVersionReviewLevel ReviewLevel { get; set; }
    public required IEnumerable<AssociatedResourceResponse> AssociatedResources { get; set; }
}

public record AssociatedResourceResponse
{
    public required string? ExternalId { get; set; }
    public required int ResourceId { get; set; }
    public required int ContentId { get; set; }
    public required ResourceContentMediaType MediaType { get; set; }
}