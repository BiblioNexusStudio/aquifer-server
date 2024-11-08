using Aquifer.Common.Clients;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Aquifer.Jobs.Services;

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
    IReadOnlyList<EmailAddress>? ReplyTos = null);

public sealed record TemplatedEmail(
    EmailAddress From,
    string TemplateId,
    IReadOnlyList<EmailAddress> Tos,
    Dictionary<string, object> DynamicTemplateData,
    Dictionary<string, Dictionary<string, object>>? EmailSpecificDynamicTemplateDataByToEmailAddressMap = null,
    IReadOnlyList<EmailAddress>? ReplyTos = null);

public sealed record EmailAddress(
    string Email,
    string? Name = null);

public class SendGridEmailService : IEmailService
{
    private readonly SendGridClient _sendGridClient;

    public SendGridEmailService(IAzureKeyVaultClient keyVaultClient)
    {
        const string apiKeySecretName = "SendGridMarketingApiKey";
        var apiToken = keyVaultClient.GetSecretAsync(apiKeySecretName).GetAwaiter().GetResult();
        _sendGridClient = new SendGridClient(apiToken);
    }

    public async Task SendEmailAsync(Email email, CancellationToken ct)
    {
        var sendGridMessage = MapToSendGridMessage(email);
        var response = await _sendGridClient.SendEmailAsync(sendGridMessage, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new SendGridEmailException(
                "Unable to send email via SendGrid.",
                sendGridMessage, 
                await response.Body.ReadAsStringAsync(ct));
        }
    }

    public async Task SendEmailAsync(TemplatedEmail templatedEmail, CancellationToken ct)
    {
        var sendGridMessage = MapToSendGridMessage(templatedEmail);
        var response = await _sendGridClient.SendEmailAsync(sendGridMessage, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new SendGridEmailException(
                "Unable to send templated email via SendGrid.",
                sendGridMessage, 
                await response.Body.ReadAsStringAsync(ct));
        }
    }

    private static SendGridMessage MapToSendGridMessage(Email email)
    {
        return new SendGridMessage
        {
            From = MapToSendGridEmailAddress(email.From),
            HtmlContent = email.HtmlContent,
            Personalizations = email
                .Tos
                .Select(to =>
                    new Personalization
                    {
                        Tos = [MapToSendGridEmailAddress(to)],
                    })
                .ToList(),
            ReplyTos = email.ReplyTos?.Select(MapToSendGridEmailAddress).ToList(),
            Subject = email.Subject,
        };
    }

    private static SendGridMessage MapToSendGridMessage(TemplatedEmail email)
    {
        var sendGridMessage = new SendGridMessage
        {
            From = MapToSendGridEmailAddress(email.From),
            TemplateId = email.TemplateId,
            Personalizations = email
                .Tos
                .Select(to => new Personalization
                {
                    Tos = [MapToSendGridEmailAddress(to)],
                    TemplateData = MergeDynamicTemplateData(
                        email.DynamicTemplateData,
                        email.EmailSpecificDynamicTemplateDataByToEmailAddressMap,
                        to.Email),
                })
                .ToList(),
            ReplyTos = email.ReplyTos?.Select(MapToSendGridEmailAddress).ToList(),
        };

        return sendGridMessage;
    }

    private static Dictionary<string, object> MergeDynamicTemplateData(
        Dictionary<string, object> dynamicTemplateData,
        Dictionary<string, Dictionary<string, object>>? emailSpecificDynamicTemplateDataByToEmailAddressMap,
        string toEmailAddress)
    {
        var emailSpecificDynamicTemplateData = emailSpecificDynamicTemplateDataByToEmailAddressMap
            ?.GetValueOrDefault(toEmailAddress)
            ?? null;

        return new List<Dictionary<string, object>?> { dynamicTemplateData, emailSpecificDynamicTemplateData }
            .Where(d => d is not null)
            .SelectMany(d => d!)
            .ToDictionary();
    }

    private static SendGrid.Helpers.Mail.EmailAddress MapToSendGridEmailAddress(EmailAddress emailAddress)
    {
        return new SendGrid.Helpers.Mail.EmailAddress(emailAddress.Email, emailAddress.Name);
    }
}

public sealed class SendGridEmailException(string _message, SendGridMessage _email, string _response)
    : Exception(_message)
{
    public SendGridMessage Email { get; } = _email;
    public string? Response { get; } = _response;

    public override string ToString()
    {
        return $"""
                {Message}

                Email:
                {Email}

                Response:
                {Response}
                """;
    }
}