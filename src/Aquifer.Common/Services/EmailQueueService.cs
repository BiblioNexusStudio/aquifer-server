using Aquifer.Common.Clients;

namespace Aquifer.Common.Services;

public interface IEmailQueueService
{
    Task QueueEmailAsync(Email email, CancellationToken ct);
    Task QueueEmailAsync(TemplatedEmail email, CancellationToken ct);
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

public sealed class EmailQueueService : IEmailQueueService
{
    public Task QueueEmailAsync(Email email, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task QueueEmailAsync(TemplatedEmail email, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}