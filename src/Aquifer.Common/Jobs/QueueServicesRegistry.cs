using Microsoft.Extensions.DependencyInjection;

namespace Aquifer.Common.Jobs;

public static class QueueServicesRegistry
{
    public static IServiceCollection AddQueueServices(this IServiceCollection services, string azureQueueStorageConnectionString)
    {
        services.AddSingleton(new QueueConfigurationOptions(azureQueueStorageConnectionString));
        services.AddSingleton<IQueueClientFactory, QueueClientFactory>();

        return services;
    }
}