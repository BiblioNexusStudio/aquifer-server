using System.Text.Json.Serialization;

namespace Aquifer.Common.Clients.Http.IpAddressLookup;

public class IpAddressLookupResponse
{
    public string? City { get; set; }
    public string? Region { get; set; }

    [JsonPropertyName("country_name")]
    public string? Country { get; set; }
}