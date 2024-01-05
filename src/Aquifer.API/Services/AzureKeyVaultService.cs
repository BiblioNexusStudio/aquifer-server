using Aquifer.API.Configuration;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Options;

namespace Aquifer.API.Services;

public interface IAzureKeyVaultService
{
    Task<string> GetSecretAsync(string secretName);
}

public class AzureKeyVaultService : IAzureKeyVaultService
{
    private readonly SecretClient _client;

    public AzureKeyVaultService(IOptions<ConfigurationOptions> options)
    {
        string kvUri = options.Value.KeyVaultUri;
        _client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
    }

    public async Task<string> GetSecretAsync(string secretName)
    {
        var secret = await _client.GetSecretAsync(secretName);
        return secret.Value.Value;
    }
}