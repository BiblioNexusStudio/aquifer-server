using Aquifer.Common.Messages.Models;

namespace Aquifer.Common.Messages.Publishers;

public interface IEmailMessagePublisher
{
    Task PublishSendEmailMessageAsync(SendEmailMessage message, CancellationToken ct);

    Task PublishSendTemplatedEmailMessageAsync(SendTemplatedEmailMessage message, CancellationToken ct);
}

public sealed class EmailMessagePublisher(IQueueClientFactory _queueClientFactory) : IEmailMessagePublisher
{
    public const string DynamicTemplateDataSubjectPropertyName = "subject";

    public async Task PublishSendEmailMessageAsync(SendEmailMessage message, CancellationToken cancellationToken)
    {
        var queueClient = await _queueClientFactory.GetQueueClientAsync(Queues.SendEmail, cancellationToken);
        await queueClient.SendMessageAsync(message, cancellationToken: cancellationToken);
    }

    public async Task PublishSendTemplatedEmailMessageAsync(SendTemplatedEmailMessage message, CancellationToken cancellationToken)
    {
        var queueClient = await _queueClientFactory.GetQueueClientAsync(Queues.SendTemplatedEmail, cancellationToken);
        await queueClient.SendMessageAsync(message, cancellationToken: cancellationToken);
    }
}