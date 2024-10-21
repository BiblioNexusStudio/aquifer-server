using Aquifer.Common.Jobs;

namespace Aquifer.Common.Services;

public interface IEmailService
{
    Task SendEmailAsync(Email email, CancellationToken ct);
    Task SendEmailAsync(TemplatedEmail templatedEmail, CancellationToken ct);
}

public sealed record Email(
    EmailAddress From,
    string Subject,
    string HtmlContent,
    IReadOnlyList<EmailAddress> Tos,
    IReadOnlyList<EmailAddress>? Ccs = null,
    IReadOnlyList<EmailAddress>? Bccs = null);

public sealed record TemplatedEmail(
    EmailAddress From,
    string Subject,
    string TemplateId,
    object DynamicTemplateData,
    IReadOnlyList<EmailAddress> Tos,
    IReadOnlyList<EmailAddress>? Ccs = null,
    IReadOnlyList<EmailAddress>? Bccs = null);

public sealed record EmailAddress(
    string Email,
    string? Name = null);

public sealed class EmailService(IQueueClientFactory _queueClientFactory) : IEmailService
{
    public async Task SendEmailAsync(Email email, CancellationToken cancellationToken)
    {
        var queueClient = await _queueClientFactory.GetQueueClientAsync(Queues.SendEmail, cancellationToken);
        await queueClient.SendMessageAsync(email, cancellationToken: cancellationToken);
    }

    public async Task SendEmailAsync(TemplatedEmail templatedEmail, CancellationToken cancellationToken)
    {
        var queueClient = await _queueClientFactory.GetQueueClientAsync(Queues.SendTemplatedEmail, cancellationToken);
        await queueClient.SendMessageAsync(templatedEmail, cancellationToken: cancellationToken);
    }
}