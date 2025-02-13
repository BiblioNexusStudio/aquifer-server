using System.Text.RegularExpressions;
using Aquifer.Common.Clients;
using SendGrid;
using SendGrid.Helpers.Mail;
using SendGrid.Helpers.Reliability;

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
    private const int MaximumNumberOfTosPerApiCall = 1_000;
    private static readonly Regex s_removePlusAddressing = new("\\+.*@", RegexOptions.Compiled);

    private readonly SendGridClient _sendGridClient;

    public SendGridEmailService(IAzureKeyVaultClient keyVaultClient)
    {
        const string apiKeySecretName = "SendGridMarketingApiKey";

        // TODO Inject a SendGridClient instead of building one here. This will require changing how we fetch key vault secrets.
#pragma warning disable VSTHRD002
        var apiToken = keyVaultClient.GetSecretAsync(apiKeySecretName).GetAwaiter().GetResult();
#pragma warning restore VSTHRD002

        _sendGridClient = new SendGridClient(
            new SendGridClientOptions
            {
                ApiKey = apiToken,
                ReliabilitySettings = new ReliabilitySettings(
                    maximumNumberOfRetries: 5,
                    minimumBackoff: TimeSpan.FromMilliseconds(200),
                    maximumBackOff: TimeSpan.FromSeconds(5),
                    deltaBackOff: TimeSpan.FromMilliseconds(50)),
            });
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
        return GetToEmailAddressBatches(email.Tos)
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
        return GetToEmailAddressBatches(email.Tos)
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

    // Email providers (like Gmail) will not deliver all simultaneously sent emails from SendGrid
    // if those emails are in the same batch with the same "Message-ID" (see https://mtsknn.fi/blog/tricky-email-aliases/).
    // Thus, we need to send plus-aliased emails (e.g. "john.doe+testing@example.com") in separate batches
    // when there is more than one plus-aliased email for the same email address in order for the provider to actually deliver them.
    // This method will send most emails in the first batch but any duplicate email base email addresses will be sent in subsequent
    // batches in order to force a different "Message-ID" for those plus-aliased email addresses.
    private static IEnumerable<EmailAddress[]> GetToEmailAddressBatches(IEnumerable<EmailAddress> toEmailAddresses)
    {
        var plusAliasGroups = toEmailAddresses
            .DistinctBy(to => to.Email)
            .GroupBy(to => s_removePlusAddressing.Replace(to.Email, "@"))
            .Select(grp => grp.ToList())
            .ToList();

        return RecursivelyChunkAndZipGroupedItems(plusAliasGroups);

        // This method takes multiple lists and returns batches of items, each batch containing all items at index N in the lists.
        // It then also batches the output batch if there are too many in a single batch.
        static IEnumerable<EmailAddress[]> RecursivelyChunkAndZipGroupedItems(List<List<EmailAddress>> groups)
        {
            if (groups.Count == 0)
            {
                return [];
            }

            return groups
                .Select(grp => grp.First())
                .Chunk(MaximumNumberOfTosPerApiCall)
                .Concat(
                    RecursivelyChunkAndZipGroupedItems(
                        groups
                            .Select(grp => grp.Skip(1).ToList())
                            .Where(grp => grp.Count > 0)
                            .ToList()));
        }
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