using System.Text.Json.Serialization;
using Aquifer.Common.Utilities;
using Aquifer.Data.Entities;

namespace Aquifer.Public.API.Endpoints.Resources.Collections.Get;

public sealed class Response
{
    public required string Code { get; init; }
    public required string DisplayName { get; init; }
    public required string ShortName { get; init; }
    public required ResourceType ResourceType { get; init; }

    [JsonConverter(typeof(JsonUtilities.RawJsonConverter))]
    public required string? LicenseInfo { get; init; }

    public required IReadOnlyList<AvailableLanguageResponse> AvailableLanguages { get; init; }
}

public sealed class AvailableLanguageResponse
{
    public required int LanguageId { get; init; }
    public required string LanguageCode { get; init; }
    public required string DisplayName { get; init; }
    public required int ResourceItemCount { get; init; }
}