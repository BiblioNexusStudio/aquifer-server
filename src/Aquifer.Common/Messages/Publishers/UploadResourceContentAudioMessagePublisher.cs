using Aquifer.Common.Messages.Models;

namespace Aquifer.Common.Messages.Publishers;

public interface IUploadResourceContentAudioMessagePublisher
{
    Task PublishUploadResourceContentAudioMessageAsync(
        UploadResourceContentAudioMessage message,
        CancellationToken cancellationToken);
}

public sealed class UploadResourceContentAudioMessagePublisher(IQueueClientFactory _queueClientFactory)
    : IUploadResourceContentAudioMessagePublisher
{
    public async Task PublishUploadResourceContentAudioMessageAsync(
        UploadResourceContentAudioMessage message,
        CancellationToken cancellationToken)
    {
        var queueClient = await _queueClientFactory.GetQueueClientAsync(Queues.UploadResourceContentAudio, cancellationToken);
        await queueClient.SendMessageAsync(message, cancellationToken);
    }
}