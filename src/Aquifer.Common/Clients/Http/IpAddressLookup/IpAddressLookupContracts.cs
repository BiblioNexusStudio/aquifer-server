using System.Text.Json.Serialization;

namespace Aquifer.Common.Clients.Http.IpAddressLookup;

public class IpAddressLookupResponse
{
    public string City { get; set; } = null!;
    public string Region { get; set; } = null!;

    [JsonPropertyName("country_name")]
    public string Country { get; set; } = null!;
}