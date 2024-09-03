using Aquifer.Common.Utilities;

namespace Aquifer.Common.Clients.Http.IpAddressLookup;

public interface IIpAddressLookupHttpClient
{
    Task<IpAddressLookupResponse> LookupIpAddressAsync(string ipAddress, CancellationToken ct);
}

public class IpAddressLookupHttpClient : IIpAddressLookupHttpClient
{
    private const string BaseUri = "https://ipapi.co";
    private readonly HttpClient _httpClient;

    public IpAddressLookupHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(BaseUri);
        // request will fail without User-Agent
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "bn-user");
    }

    public async Task<IpAddressLookupResponse> LookupIpAddressAsync(string ipAddress, CancellationToken ct)
    {
        var response = await _httpClient.GetAsync($"{ipAddress}/json", ct);
        var responseContent = await response.Content.ReadAsStringAsync(ct);
        return JsonUtilities.DefaultDeserialize<IpAddressLookupResponse>(responseContent);
    }
}