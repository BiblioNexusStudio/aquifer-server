using System.Text.Json.Serialization;
using Aquifer.Common.Utilities;
using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Resources.ParentResources.List;

public class Response
{
    public required int Id { get; set; }
    public required bool Enabled { get; set; }
    public required string ShortName { get; set; }
    public required string DisplayName { get; set; }
    public required string Code { get; set; }
    public required int ResourceCountForLanguage { get; set; }

    [JsonIgnore]
    public required string LicenseInfoValue { get; set; }

    public ResourceLicenseInfo LicenseInfo => JsonUtilities.DefaultDeserialize<ResourceLicenseInfo>(LicenseInfoValue);

    public required ResourceTypeComplexityLevel ComplexityLevel { get; set; }
    public required ResourceType ResourceType { get; set; }
}