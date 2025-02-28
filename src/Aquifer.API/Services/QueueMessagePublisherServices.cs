using Aquifer.Common.Extensions;
using Aquifer.Common.Messages.Publishers;

namespace Aquifer.API.Services;

public static class QueueMessagePublisherServices
{
    public static IServiceCollection AddQueueMessagePublisherServices(this IServiceCollection services)
    {
        services
            .AddTrackResourceContentRequestServices()
            .AddSingleton<INotificationMessagePublisher, NotificationMessagePublisher>()
            .AddSingleton<ITranslationMessagePublisher, TranslationMessagePublisher>()
            .AddSingleton<IResourceContentVersionSimilarityMessagePublisher, ResourceContentVersionSimilarityMessagePublisher>();

        return services;
    }
}