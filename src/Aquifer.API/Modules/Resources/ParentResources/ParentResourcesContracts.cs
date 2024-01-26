using System.Text.Json.Serialization;
using Aquifer.Common.Utilities;
using Aquifer.Data.Entities;

namespace Aquifer.API.Modules.Resources.ParentResources;

public class ParentResourceResponse
{
    public int Id { get; init; }
    public string ShortName { get; init; } = null!;
    public string DisplayName { get; init; } = null!;
    public ResourceType ResourceType { get; init; }

    public object? LicenseInfo =>
        SerializedLicenseInfo == null ? null : JsonUtilities.DefaultDeserialize(SerializedLicenseInfo);

    public ResourceTypeComplexityLevel ComplexityLevel { get; init; }

    [JsonIgnore]
    public string? SerializedLicenseInfo { get; init; }
}