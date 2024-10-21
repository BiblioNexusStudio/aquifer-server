using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aquifer.Common.Jobs;

public static class QueueServicesRegistry
{
    public static IServiceCollection AddQueueServices(
        this IServiceCollection services,
        IConfiguration configuration,
        string azureQueueStorageConnectionStringName = "AzureStorageAccount")
    {
        var azureQueueStorageConnectionString = configuration.GetConnectionString(azureQueueStorageConnectionStringName)
            ?? throw new InvalidOperationException($"Connection string \"{azureQueueStorageConnectionStringName}\" was not found");
        services.AddSingleton(new QueueConfigurationOptions(azureQueueStorageConnectionString));

        services.AddSingleton<IQueueClientFactory, QueueClientFactory>();

        return services;
    }
}
