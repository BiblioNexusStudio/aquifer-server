using Aquifer.API.Utilities;
using Aquifer.Data.Entities;

namespace Aquifer.API.Modules.Resources.ResourceTypes;

public record ResourceTypeResponse(int Id, string DisplayName, object? LicenseInfo, ResourceTypeComplexityLevel
    ComplexityLevel)
{
            public ResourceTypeResponse(int id, string displayName, string? licenseInfo, ResourceTypeComplexityLevel complexityLevel)
            : this(id, displayName, licenseInfo == null ? null : JsonUtilities.DefaultDeserialize(licenseInfo), complexityLevel)
        {
        }
}