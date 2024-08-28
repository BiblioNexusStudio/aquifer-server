using Aquifer.Common.Services;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;

namespace Aquifer.Common.Clients;

public interface IAzureKeyVaultClient
{
    Task<string> GetSecretAsync(string secretName);
}

public class AzureKeyVaultClient : IAzureKeyVaultClient
{
    private readonly SecretClient _client;

    public AzureKeyVaultClient(IConfiguration configuration, IAzureClientService azureClientService)
    {
        var kvUri = GetKeyVaultUri(configuration);
        _client = new SecretClient(new Uri(kvUri), azureClientService.GetCredential());
    }

    public async Task<string> GetSecretAsync(string secretName)
    {
        var secret = await _client.GetSecretAsync(secretName);
        return secret.Value.Value;
    }

    private static string GetKeyVaultUri(IConfiguration configuration)
    {
        var keyVaultUri = configuration.GetValue<string>("KeyVaultUri");
        ArgumentException.ThrowIfNullOrEmpty(keyVaultUri);
        return keyVaultUri;
    }
}