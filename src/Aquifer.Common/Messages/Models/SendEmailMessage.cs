namespace Aquifer.Common.Messages.Models;

public sealed record SendEmailMessage(
    EmailAddress From,
    string Subject,
    string HtmlContent,
    IReadOnlyList<EmailAddress> Tos,
    IReadOnlyList<EmailAddress>? ReplyTos = null);