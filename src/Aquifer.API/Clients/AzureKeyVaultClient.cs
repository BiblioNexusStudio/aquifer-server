using Aquifer.API.Configuration;
using Aquifer.Common.Services;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Options;

namespace Aquifer.API.Clients;

public interface IAzureKeyVaultClient
{
    Task<string> GetSecretAsync(string secretName);
}

public class AzureKeyVaultClient : IAzureKeyVaultClient
{
    private readonly SecretClient _client;

    public AzureKeyVaultClient(IOptions<ConfigurationOptions> options, IAzureClientService azureClientService)
    {
        var kvUri = options.Value.KeyVaultUri;
        _client = new SecretClient(new Uri(kvUri), azureClientService.GetCredential());
    }

    public async Task<string> GetSecretAsync(string secretName)
    {
        var secret = await _client.GetSecretAsync(secretName);
        return secret.Value.Value;
    }
}