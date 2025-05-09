using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Aquifer.Common.Services;

public interface IAzureClientService
{
    TokenCredential GetCredential();
}

public class AzureClientService : IAzureClientService
{
    private readonly TokenCredential _credential;

    public AzureClientService(bool useCliCredential)
    {
        _credential = new DefaultAzureCredential(
            new DefaultAzureCredentialOptions
            {
                ExcludeManagedIdentityCredential = useCliCredential,
                ExcludeAzureCliCredential = !useCliCredential,
                ExcludeVisualStudioCodeCredential = true,
                ExcludeVisualStudioCredential = true,
                ExcludeEnvironmentCredential = true,
                ExcludeInteractiveBrowserCredential = true,
                ExcludeWorkloadIdentityCredential = true,
                ExcludeAzureDeveloperCliCredential = true,
                ExcludeAzurePowerShellCredential = true,
                ExcludeSharedTokenCacheCredential = true,
            });
    }

    public AzureClientService(TokenCredential credential)
    {
        _credential = credential;
    }

    public TokenCredential GetCredential()
    {
        return _credential;
    }
}

public static class AzureClientServiceExtensions
{
    public static IServiceCollection AddAzureClient(this IServiceCollection services, bool useCliCredential)
    {
        return services.AddSingleton<IAzureClientService>(new AzureClientService(useCliCredential));
    }
}