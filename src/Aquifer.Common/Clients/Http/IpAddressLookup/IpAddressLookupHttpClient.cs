using System.Net.Http.Json;

namespace Aquifer.Common.Clients.Http.IpAddressLookup;

public interface IIpAddressLookupHttpClient
{
    Task<IpAddressLookupResponse?> LookupIpAddressAsync(string ipAddress);
}

public class IpAddressLookupHttpClient : IIpAddressLookupHttpClient
{
    private const string BaseUri = "https://ipapi.co";
    private readonly HttpClient _httpClient;

    public IpAddressLookupHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(BaseUri);
    }

    public async Task<IpAddressLookupResponse?> LookupIpAddressAsync(string ipAddress)
    {
        return await _httpClient.GetFromJsonAsync<IpAddressLookupResponse>($"{ipAddress}/json");
    }
}