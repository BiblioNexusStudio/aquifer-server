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
    // the SendGrid API limit is 1,000
    private const int _maximumNumberOfTosPerApiCall = 1_000;

    private readonly SendGridClient _sendGridClient;

    public SendGridEmailService(IAzureKeyVaultClient keyVaultClient)
    {
        const string apiKeySecretName = "SendGridMarketingApiKey";
        var apiToken = keyVaultClient.GetSecretAsync(apiKeySecretName).GetAwaiter().GetResult();
        _sendGridClient = new SendGridClient(apiToken);
    }

    public async Task SendEmailAsync(Email email, CancellationToken ct)
    {
        // Note: If one of these batch sends fails then we may send one or more batches but not all batches.
        // This could be improved with retry handling in the future.
        var sendGridMessages = MapToSendGridMessages(email);
        foreach (var sendGridMessage in sendGridMessages)
        {
            var response = await _sendGridClient.SendEmailAsync(sendGridMessage, ct);
            if (!response.IsSuccessStatusCode)
            {
                throw new SendGridEmailException(
                    "Unable to send email via SendGrid.",
                    sendGridMessage,
                    await response.Body.ReadAsStringAsync(ct));
            }
        }
    }

    public async Task SendEmailAsync(TemplatedEmail templatedEmail, CancellationToken ct)
    {
        // Note: If one of these batch sends fails then we may send one or more batches but not all batches.
        // This could be improved with retry handling in the future.
        var sendGridMessages = MapToSendGridMessages(templatedEmail);
        foreach (var sendGridMessage in sendGridMessages)
        {
            var response = await _sendGridClient.SendEmailAsync(sendGridMessage, ct);
            if (!response.IsSuccessStatusCode)
            {
                throw new SendGridEmailException(
                    "Unable to send templated email via SendGrid.",
                    sendGridMessage,
                    await response.Body.ReadAsStringAsync(ct));
            }
        }
    }

    private static IReadOnlyList<SendGridMessage> MapToSendGridMessages(Email email)
    {
        return email
            .Tos
            .DistinctBy(to => to.Email)
            .Chunk(_maximumNumberOfTosPerApiCall)
            .Select(tos => new SendGridMessage
            {
                From = MapToSendGridEmailAddress(email.From),
                HtmlContent = email.HtmlContent,
                Personalizations = tos
                    .Select(to =>
                        new Personalization
                        {
                            Tos = [MapToSendGridEmailAddress(to)],
                        })
                    .ToList(),
                ReplyTos = email.ReplyTos?.Select(MapToSendGridEmailAddress).ToList(),
                Subject = email.Subject,
            })
            .ToList();
    }

    private static IReadOnlyList<SendGridMessage> MapToSendGridMessages(TemplatedEmail email)
    {
        return email
            .Tos
            .DistinctBy(to => to.Email)
            .Chunk(_maximumNumberOfTosPerApiCall)
            .Select(tos => new SendGridMessage
            {
                From = MapToSendGridEmailAddress(email.From),
                TemplateId = email.TemplateId,
                Personalizations = tos
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
            })
            .ToList();
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