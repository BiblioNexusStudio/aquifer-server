using Aquifer.Data;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Aquifer.Common.Clients;

public interface ISendGridClient
{
    Task<Response> SendEmail(AquiferDbContext dbContext, SendGridEmailConfiguration emailConfiguration, CancellationToken ct);
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

    public SendGridClient(IAzureKeyVaultClient keyVaultClient, IConfiguration configuration)
    {
        _apiToken = keyVaultClient.GetSecretAsync(ApiKeySecretName).GetAwaiter().GetResult();
    }

    public async Task<Response> SendEmail(AquiferDbContext dbContext, SendGridEmailConfiguration emailConfiguration, CancellationToken ct)
    {
        var client = new SendGrid.SendGridClient(_apiToken);
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

        return await client.SendEmailAsync(msg, ct);
    }
}