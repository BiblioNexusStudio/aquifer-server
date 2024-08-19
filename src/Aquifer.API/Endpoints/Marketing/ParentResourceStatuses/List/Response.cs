namespace Aquifer.API.Endpoints.Marketing.ParentResourceStatuses.List;

public class Response
{
    public required int ResourceId { get; set; }
    public required string ResourceType { get; set; }
    public required string Title { get; set; }
    public required object? LicenseInfo { get; set; }
    public required ParentResourceStatus Status { get; set; }
}