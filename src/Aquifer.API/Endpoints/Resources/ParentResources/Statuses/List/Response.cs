using System.Text.Json.Serialization;
using Aquifer.Common.Utilities;

namespace Aquifer.API.Endpoints.Resources.ParentResources.Statuses.List;

public class Response
{
    public required string ResourceType { get; set; }
    public required string Title { get; set; }
    public required int ResourceId { get; set; }

    [JsonIgnore]
    public string? LicenseInfoValue { get; set; } = null!;

    public object? LicenseInfo => LicenseInfoValue is null ? null : JsonUtilities.DefaultDeserialize(LicenseInfoValue);
    public object? LicenseInformation { get; set; }
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