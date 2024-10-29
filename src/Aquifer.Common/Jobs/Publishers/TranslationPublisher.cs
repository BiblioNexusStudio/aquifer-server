using Aquifer.Common.Jobs.Messages;

namespace Aquifer.Common.Jobs.Publishers;

public interface ITranslationPublisher
{
    public Task PublishTranslateProjectResourcesMessageAsync(
        TranslateProjectResourcesMessage message,
        CancellationToken cancellationToken);
    public Task PublishTranslateResourceMessageAsync(
        TranslateResourceMessage message,
        CancellationToken cancellationToken);
}

public sealed class TranslationPublisher(IQueueClientFactory _queueClientFactory) : ITranslationPublisher
{
    public async Task PublishTranslateProjectResourcesMessageAsync(
        TranslateProjectResourcesMessage message,
        CancellationToken cancellationToken)
    {
        var queueClient = await _queueClientFactory.GetQueueClientAsync(Queues.TranslateProjectResources, cancellationToken);
        await queueClient.SendMessageAsync(message, cancellationToken: cancellationToken);
    }

    public async Task PublishTranslateResourceMessageAsync(
        TranslateResourceMessage message,
        CancellationToken cancellationToken)
    {
        var queueClient = await _queueClientFactory.GetQueueClientAsync(Queues.TranslateResource, cancellationToken);
        await queueClient.SendMessageAsync(message, cancellationToken: cancellationToken);
    }
}