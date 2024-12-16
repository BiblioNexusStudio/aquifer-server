using Aquifer.Common.Messages.Models;

namespace Aquifer.Common.Messages.Publishers;

public interface ITranslationMessagePublisher
{
    public Task PublishTranslateLanguageResourcesMessageAsync(
        TranslateLanguageResourcesMessage message,
        CancellationToken cancellationToken);

    public Task PublishTranslateProjectResourcesMessageAsync(
        TranslateProjectResourcesMessage message,
        CancellationToken cancellationToken);

    public Task PublishTranslateResourceMessageAsync(
        TranslateResourceMessage message,
        CancellationToken cancellationToken);
}

public sealed class TranslationMessagePublisher(IQueueClientFactory _queueClientFactory) : ITranslationMessagePublisher
{
    public async Task PublishTranslateLanguageResourcesMessageAsync(
        TranslateLanguageResourcesMessage message,
        CancellationToken cancellationToken)
    {
        var queueClient = await _queueClientFactory.GetQueueClientAsync(Queues.TranslateLanguageResources, cancellationToken);
        await queueClient.SendMessageAsync(message, cancellationToken: cancellationToken);
    }

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