using Aquifer.Common.Messages.Models;

namespace Aquifer.Common.Messages.Publishers;

public interface IEmailMessagePublisher
{
    Task SendEmailAsync(SendEmailMessage message, CancellationToken ct);
    Task SendEmailAsync(SendTemplatedEmailMessage message, CancellationToken ct);
}

public sealed class EmailMessagePublisher(IQueueClientFactory _queueClientFactory) : IEmailMessagePublisher
{
    public const string DynamicTemplateDataSubjectPropertyName = "subject";

    public async Task SendEmailAsync(SendEmailMessage message, CancellationToken cancellationToken)
    {
        var queueClient = await _queueClientFactory.GetQueueClientAsync(Queues.SendEmail, cancellationToken);
        await queueClient.SendMessageAsync(message, cancellationToken: cancellationToken);
    }

    public async Task SendEmailAsync(SendTemplatedEmailMessage message, CancellationToken cancellationToken)
    {
        var queueClient = await _queueClientFactory.GetQueueClientAsync(Queues.SendTemplatedEmail, cancellationToken);
        await queueClient.SendMessageAsync(message, cancellationToken: cancellationToken);
    }
}