using System.Text.Json.Serialization;
using Aquifer.Common.Utilities;

namespace Aquifer.Public.API.Endpoints.Resources.Types.List;

public class Response
{
    public required string Type { get; set; }
    public List<AvailableResourceCollection> Collections { get; set; } = [];
}

public class AvailableResourceCollection
{
    public required string Code { get; set; }
    public required string Title { get; set; }

    [JsonConverter(typeof(JsonUtilities.RawJsonConverter))]
    public required object? LicenseInformation { get; init; }
}