using Aquifer.Common.Messages.Publishers;

namespace Aquifer.Common.Messages.Models;

/// <summary>
/// Note: The email Subject is specified in the templating system.  It is possible, however, to specify it via Dynamic Template Data
/// and to reference that value in the templating engine.
/// If this dynamic Subject strategy is used then the Subject can also be modified to include an environment specific subject prefix:
/// If the <paramref name="DynamicTemplateData"/> includes a <see cref="EmailMessagePublisher.DynamicTemplateDataSubjectPropertyName"/> key then that value
/// will be automatically updated during email send to include an environment specific Subject prefix like `[Test SendEmailMessage - Local] `.
/// </summary>
public sealed record SendTemplatedEmailMessage(
    EmailAddress From,
    string TemplateId,
    IDictionary<string, object> DynamicTemplateData,
    IReadOnlyList<EmailAddress> Tos,
    IReadOnlyList<EmailAddress>? Ccs = null,
    IReadOnlyList<EmailAddress>? Bccs = null);