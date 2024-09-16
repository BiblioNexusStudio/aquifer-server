using System.Text.Json.Serialization;
using Aquifer.Common.Utilities;

namespace Aquifer.Public.API.Endpoints.Bibles.List;

public record Response
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Abbreviation { get; set; }
    public required int LanguageId { get; set; }
    public required bool IsLanguageDefault { get; set; }
    public required bool HasGreekAlignment { get; set; }
    public required bool HasAudio { get; set; }

    public object? LicenseInfo =>
        SerializedLicenseInfo == null ? null : JsonUtilities.DefaultDeserialize(SerializedLicenseInfo);

    [JsonIgnore]
    public string? SerializedLicenseInfo { get; init; }
}