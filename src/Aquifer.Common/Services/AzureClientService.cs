using Azure.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Aquifer.Common.Services;

public interface IAzureClientService
{
    DefaultAzureCredential GetCredential();
}

public class AzureClientService(bool useCliCredential) : IAzureClientService
{
    private readonly DefaultAzureCredential _credential = new(new DefaultAzureCredentialOptions
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
        ExcludeSharedTokenCacheCredential = true
    });

    public DefaultAzureCredential GetCredential()
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