namespace Aquifer.API.Endpoints.Resources.ParentResources.Statuses.List;

public class Response
{
    public required int ResourceId { get; set; }
    public required string ResourceType { get; set; }
    public required string Title { get; set; }
    public object? LicenseInfo { get; set; }
    public ParentResourceStatus Status { get; set; }
}

public enum ParentResourceStatus
{
    Complete = 1,
    RecentlyCompleted = 2,
    Partial = 3,
    RecentlyUpdated = 4,
    ComingSoon = 5
}