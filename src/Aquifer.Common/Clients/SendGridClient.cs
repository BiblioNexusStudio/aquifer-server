using Aquifer.Data;
using Microsoft.Extensions.Configuration;
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
    private const string ApiKeySecretName = "SendGridMarketingApiKey";
    private readonly string _apiToken;
    private readonly SendGrid.SendGridClient _client;

    public SendGridClient(IAzureKeyVaultClient keyVaultClient)
    {
        _apiToken = keyVaultClient.GetSecretAsync(ApiKeySecretName).GetAwaiter().GetResult();
        _client = new SendGrid.SendGridClient(_apiToken);
    }

    public async Task<Response> SendEmail(SendGridEmailConfiguration emailConfiguration, CancellationToken ct)
    {
        var from = new EmailAddress(emailConfiguration.FromEmail, emailConfiguration.FromName);
        var msg = new SendGridMessage();
        msg.SetFrom(from);
        msg.SetSubject(emailConfiguration.Subject);
        msg.AddTos(emailConfiguration.ToAddresses);
        msg.HtmlContent = emailConfiguration.HtmlContent;
        if (emailConfiguration.PlainTextContent is not null)
        {
            msg.PlainTextContent = emailConfiguration.PlainTextContent;
        }

        return await _client.SendEmailAsync(msg, ct);
    }
}