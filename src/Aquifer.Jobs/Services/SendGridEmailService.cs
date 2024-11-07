using Aquifer.Common.Clients;
using Aquifer.Common.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using EmailAddress = Aquifer.Common.Services.EmailAddress;

namespace Aquifer.Jobs.Services;

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
            Personalizations =
            [
                new Personalization
                {
                    Tos = email.Tos.Select(MapToSendGridEmailAddress).ToList(),
                    Ccs = email.Ccs?.Select(MapToSendGridEmailAddress).ToList(),
                    Bccs = email.Bccs?.Select(MapToSendGridEmailAddress).ToList(),
                },
            ],
            Subject = email.Subject,
        };
    }

    private static SendGridMessage MapToSendGridMessage(TemplatedEmail email)
    {
        var sendGridMessage = new SendGridMessage
        {
            From = MapToSendGridEmailAddress(email.From),
            Personalizations =
            [
                new Personalization
                {
                    Tos = email.Tos.Select(MapToSendGridEmailAddress).ToList(),
                    Ccs = email.Ccs?.Select(MapToSendGridEmailAddress).ToList(),
                    Bccs = email.Bccs?.Select(MapToSendGridEmailAddress).ToList(),
                },
            ],
            TemplateId = email.TemplateId,
        };

        sendGridMessage.SetTemplateData(email.DynamicTemplateData);

        return sendGridMessage;
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