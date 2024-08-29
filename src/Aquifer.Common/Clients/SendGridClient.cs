using SendGrid;
using SendGrid.Helpers.Mail;

namespace Aquifer.Common.Clients;

public interface ISendGridClient
{
    Task<Response> SendEmail(SendGridEmailConfiguration emailConfiguration, CancellationToken ct);
}

public class SendGridEmailConfiguration
{
    public required string FromEmail { get; set; }
    public required string FromName { get; set; }
    public required string Subject { get; set; }
    public required string HtmlContent { get; set; }
    public string? PlainTextContent { get; set; } = null!;
    public required List<EmailAddress> ToAddresses { get; set; }
}

public class SendGridClient : ISendGridClient
{
    private readonly SendGrid.SendGridClient _client;

    public SendGridClient(IAzureKeyVaultClient keyVaultClient)
    {
        const string apiKeySecretName = "SendGridMarketingApiKey";
        var apiToken = keyVaultClient.GetSecretAsync(apiKeySecretName).GetAwaiter().GetResult();
        _client = new SendGrid.SendGridClient(apiToken);
    }

    public async Task<Response> SendEmail(SendGridEmailConfiguration emailConfiguration, CancellationToken ct)
    {
        var message = new SendGridMessage
        {
            From = new EmailAddress(emailConfiguration.FromEmail, emailConfiguration.FromName),
            Subject = emailConfiguration.Subject,
            Personalizations =
            [
                new Personalization
                {
                    Tos = emailConfiguration.ToAddresses
                }
            ],
            HtmlContent = emailConfiguration.HtmlContent,
            PlainTextContent = emailConfiguration.PlainTextContent,
        };

        return await _client.SendEmailAsync(message, ct);
    }
}