using Aquifer.Common.Jobs;
using Aquifer.Common.Jobs.Messages;

namespace Aquifer.Common.Services;

public interface INotificationService
{
    public Task SendProjectStartedNotificationAsync(int projectId, CancellationToken cancellationToken);
    public Task SendResourceCommentCreatedNotificationAsync(int commentId, CancellationToken cancellationToken);
}

public sealed class NotificationService(IQueueClientFactory _queueClientFactory) : INotificationService
{
    public async Task SendProjectStartedNotificationAsync(int projectId, CancellationToken cancellationToken)
    {
        var message = new SendProjectStartedNotificationMessage(projectId);

        var queueClient = await _queueClientFactory.GetQueueClientAsync(Queues.SendProjectStartedNotification, cancellationToken);
        await queueClient.SendMessageAsync(message, cancellationToken: cancellationToken);
    }

    public async Task SendResourceCommentCreatedNotificationAsync(int commentId, CancellationToken cancellationToken)
    {
        var message = new SendResourceCommentCreatedNotificationMessage(commentId);

        var queueClient = await _queueClientFactory.GetQueueClientAsync(Queues.SendResourceCommentCreatedNotification, cancellationToken);
        await queueClient.SendMessageAsync(message, cancellationToken: cancellationToken);
    }
}