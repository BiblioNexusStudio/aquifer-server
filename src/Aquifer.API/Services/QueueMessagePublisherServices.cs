using Aquifer.Common.Extensions;
using Aquifer.Common.Messages.Publishers;

namespace Aquifer.API.Services;

public static class QueueMessagePublisherServices
{
    public static IServiceCollection AddQueueMessagePublisherServices(this IServiceCollection services)
    {
        services
            .AddTrackResourceContentRequestServices()
            .AddSingleton<IUploadResourceContentAudioMessagePublisher, UploadResourceContentAudioMessagePublisher>()
            .AddSingleton<INotificationMessagePublisher, NotificationMessagePublisher>()
            .AddSingleton<IResourceContentVersionSimilarityMessagePublisher, ResourceContentVersionSimilarityMessagePublisher>()
            .AddSingleton<ITranslationMessagePublisher, TranslationMessagePublisher>();

        return services;
    }
}