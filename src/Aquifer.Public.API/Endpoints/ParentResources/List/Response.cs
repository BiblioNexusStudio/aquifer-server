using System.Text.Json.Serialization;
using Aquifer.Common.Utilities;
using Aquifer.Data.Entities;

namespace Aquifer.Public.API.Endpoints.ParentResources.List;

public sealed class Response
{
    public required int Id { get; init; }
    public required string ShortName { get; init; }
    public required string DisplayName { get; init; }
    public required string Code { get; init; }
    public required int ResourceCountForLanguage { get; init; }

    [JsonIgnore]
    public string? LicenseInfoValue { get; init; }

    public object? LicenseInfo => LicenseInfoValue != null
        ? JsonUtilities.DefaultDeserialize(LicenseInfoValue)
        : null;

    public required ResourceType ResourceType { get; init; }
}