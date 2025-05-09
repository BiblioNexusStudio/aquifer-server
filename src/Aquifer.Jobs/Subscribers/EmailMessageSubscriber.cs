using Aquifer.Common.Messages;
using Aquifer.Common.Messages.Models;
using Aquifer.Common.Messages.Publishers;
using Aquifer.Jobs.Configuration;
using Aquifer.Jobs.Services;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using EmailAddress = Aquifer.Jobs.Services.EmailAddress;

namespace Aquifer.Jobs.Subscribers;

public sealed class EmailMessageSubscriber(
    IEmailService _emailService,
    IOptions<ConfigurationOptions> _configurationOptions,
    ILogger<EmailMessageSubscriber> _logger)
{
    private const string SendEmailFunctionName = "SendEmailMessageSubscriber";

    private const string SendTemplatedEmailFunctionName = "SendTemplatedEmailMessageSubscriber";

    [Function(SendEmailFunctionName)]
    public async Task SendEmailAsync(
        [QueueTrigger(Queues.SendEmail)] QueueMessage queueMessage,
        CancellationToken ct)
    {
        await queueMessage.ProcessAsync<SendEmailMessage, EmailMessageSubscriber>(
            _logger,
            SendEmailFunctionName,
            ProcessAsync,
            ct);
    }

    private async Task ProcessAsync(QueueMessage queueMessage, SendEmailMessage message, CancellationToken ct)
    {
        await _emailService.SendEmailAsync(
            MapToEmail(message with { Subject = $"{_configurationOptions.Value.Email.SubjectPrefix}{message.Subject}" }),
            ct);

        _logger.LogInformation("Email sent: {SendEmailMessage}", queueMessage.MessageText);
    }

    [Function(SendTemplatedEmailFunctionName)]
    public async Task SendTemplatedEmailAsync(
        [QueueTrigger(Queues.SendTemplatedEmail)]
        QueueMessage queueMessage,
        CancellationToken ct)
    {
        await queueMessage.ProcessAsync<SendTemplatedEmailMessage, EmailMessageSubscriber>(
            _logger,
            SendTemplatedEmailFunctionName,
            ProcessAsync,
            ct);
    }

    private async Task ProcessAsync(QueueMessage queueMessage, SendTemplatedEmailMessage message, CancellationToken ct)
    {
        // if `Subject` is present in the dynamic template data then add an environment specific prefix to it
        if (message.DynamicTemplateData
                .TryGetValue(EmailMessagePublisher.DynamicTemplateDataSubjectPropertyName, out var subject) &&
            subject is string subjectString)
        {
            message.DynamicTemplateData[EmailMessagePublisher.DynamicTemplateDataSubjectPropertyName]
                = $"{_configurationOptions.Value.Email.SubjectPrefix}{subjectString}";
        }

        await _emailService.SendEmailAsync(MapToTemplatedEmail(message), ct);

        _logger.LogInformation("Templated email sent: {SendTemplatedEmailMessage}", queueMessage.MessageText);
    }

    private static Email MapToEmail(SendEmailMessage source)
    {
        return new Email(
            MapToEmailAddress(source.From),
            source.Subject,
            source.HtmlContent,
            source.Tos.Select(MapToEmailAddress).ToList(),
            source.ReplyTos?.Select(MapToEmailAddress).ToList());
    }

    private static EmailAddress MapToEmailAddress(Aquifer.Common.Messages.Models.EmailAddress source)
    {
        return new EmailAddress(
            source.Email,
            source.Name);
    }

    private static TemplatedEmail MapToTemplatedEmail(SendTemplatedEmailMessage source)
    {
        return new TemplatedEmail(
            MapToEmailAddress(source.From),
            source.TemplateId,
            source.Tos.Select(MapToEmailAddress).ToList(),
            source.DynamicTemplateData,
            source.EmailSpecificDynamicTemplateDataByToEmailAddressMap,
            source.ReplyTos?.Select(MapToEmailAddress).ToList());
    }
}