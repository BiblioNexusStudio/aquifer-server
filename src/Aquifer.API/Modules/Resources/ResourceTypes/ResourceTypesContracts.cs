using Aquifer.API.Utilities;
using Aquifer.Data.Entities;
using System.Text.Json.Serialization;

namespace Aquifer.API.Modules.Resources.ResourceTypes;

public class ResourceTypeResponse
{
    public int Id { get; init; }
    public string DisplayName { get; init; } = null!;

    public object? LicenseInfo =>
        SerializedLicenseInfo == null ? null : JsonUtilities.DefaultDeserialize(SerializedLicenseInfo);

    public ResourceTypeComplexityLevel ComplexityLevel { get; init; }

    [JsonIgnore]
    public string? SerializedLicenseInfo { get; init; }
}