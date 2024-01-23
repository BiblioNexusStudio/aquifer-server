using Aquifer.Common.Utilities;
using Aquifer.Data.Entities;
using System.Text.Json.Serialization;

namespace Aquifer.API.Modules.Resources.ParentResources;

public class ParentResourceResponse
{
    public int Id { get; init; }
    public string DisplayName { get; init; } = null!;

    public object? LicenseInfo =>
        SerializedLicenseInfo == null ? null : JsonUtilities.DefaultDeserialize(SerializedLicenseInfo);

    public ResourceTypeComplexityLevel ComplexityLevel { get; init; }

    [JsonIgnore]
    public string? SerializedLicenseInfo { get; init; }
}