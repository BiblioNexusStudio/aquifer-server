using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aquifer.Common.Jobs;

public static class QueueServicesRegistry
{
    // The default connection string used by Azure is "AzureWebJobsStorage".
    // See https://learn.microsoft.com/en-us/azure/azure-functions/functions-bindings-storage-queue-trigger#connection-string for details.
    public static IServiceCollection AddQueueServices(
        this IServiceCollection services,
        IConfiguration configuration,
        string azureQueueStorageConnectionStringName = "AzureWebJobsStorage")
    {
        var azureQueueStorageConnectionString = configuration.GetConnectionString(azureQueueStorageConnectionStringName)
            ?? throw new InvalidOperationException($"Connection string \"{azureQueueStorageConnectionStringName}\" was not found");
        services.AddSingleton(new QueueConfigurationOptions(azureQueueStorageConnectionString));

        services.AddSingleton<IQueueClientFactory, QueueClientFactory>();

        return services;
    }
}
