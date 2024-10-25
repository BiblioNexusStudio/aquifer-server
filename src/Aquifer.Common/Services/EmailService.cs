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

/// <summary>
/// Note: The email Subject is specified in the templating system.  It is possible, however, to specify it via Dynamic Template Data
/// and to reference that value in the templating engine.
/// If this dynamic Subject strategy is used then the Subject can also be modified to include an environment specific subject prefix:
/// If the <paramref name="DynamicTemplateData"/> includes a <see cref="EmailService.DynamicTemplateDataSubjectPropertyName"/> key then that value
/// will be automatically updated during email send to include an environment specific Subject prefix like `[Test Email - Local] `.
/// </summary>
public sealed record TemplatedEmail(
    EmailAddress From,
    string TemplateId,
    IDictionary<string, object> DynamicTemplateData,
    IReadOnlyList<EmailAddress> Tos,
    IReadOnlyList<EmailAddress>? Ccs = null,
    IReadOnlyList<EmailAddress>? Bccs = null);

public sealed record EmailAddress(
    string Email,
    string? Name = null);

public sealed class EmailService(IQueueClientFactory _queueClientFactory) : IEmailService
{
    public const string DynamicTemplateDataSubjectPropertyName = "subject";

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