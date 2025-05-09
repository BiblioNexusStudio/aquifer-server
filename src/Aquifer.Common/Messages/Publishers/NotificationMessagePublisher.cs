using Aquifer.Common.Messages.Models;

namespace Aquifer.Common.Messages.Publishers;

public interface INotificationMessagePublisher
{
    Task PublishSendProjectStartedNotificationMessageAsync(
        SendProjectStartedNotificationMessage message,
        CancellationToken cancellationToken);
}

public sealed class NotificationMessagePublisher(IQueueClientFactory _queueClientFactory)
    : INotificationMessagePublisher
{
    public async Task PublishSendProjectStartedNotificationMessageAsync(
        SendProjectStartedNotificationMessage message,
        CancellationToken cancellationToken)
    {
        var queueClient = await _queueClientFactory.GetQueueClientAsync(Queues.SendProjectStartedNotification, cancellationToken);
        await queueClient.SendMessageAsync(message, cancellationToken);
    }
}