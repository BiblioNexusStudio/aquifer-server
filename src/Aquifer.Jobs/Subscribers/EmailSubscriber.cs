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

public sealed class EmailSubscriber(
    IEmailService _emailService,
    IOptions<ConfigurationOptions> _configurationOptions,
    ILogger<EmailSubscriber> _logger)
{
    [Function(nameof(SendEmail))]
    public async Task SendEmail(
        [QueueTrigger(Queues.SendEmail)]
        QueueMessage queueMessage,
        CancellationToken ct)
    {
        var message = queueMessage.Deserialize<SendEmailMessage, EmailSubscriber>(_logger);

        try
        {
            await _emailService.SendEmailAsync(
                MapToEmail(message with { Subject = $"{_configurationOptions.Value.Email.SubjectPrefix}{message.Subject}" }),
                ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unable to send email: \"{SendEmailMessage}\"", queueMessage.MessageText);
            throw;
        }

        _logger.LogInformation("Email sent: {SendEmailMessage}", queueMessage.MessageText);
    }

    [Function(nameof(SendTemplatedEmail))]
    public async Task SendTemplatedEmail(
        [QueueTrigger(Queues.SendTemplatedEmail)]
        QueueMessage queueMessage,
        CancellationToken ct)
    {
        var message = queueMessage.Deserialize<SendTemplatedEmailMessage, EmailSubscriber>(_logger);

        // if `Subject` is present in the dynamic template data then add an environment specific prefix to it
        if (message.DynamicTemplateData.TryGetValue(EmailMessagePublisher.DynamicTemplateDataSubjectPropertyName, out var subject) &&
            subject is string subjectString)
        {
            message.DynamicTemplateData[EmailMessagePublisher.DynamicTemplateDataSubjectPropertyName]
                = $"{_configurationOptions.Value.Email.SubjectPrefix}{subjectString}";
        }

        try
        {
            await _emailService.SendEmailAsync(MapToTemplatedEmail(message), ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unable to send templated email: \"{SendTemplatedEmailMessage}\"", queueMessage.MessageText);
            throw;
        }

        _logger.LogInformation("Templated email sent: {SendTemplatedEmailMessage}", queueMessage.MessageText);
    }

    private static Email MapToEmail(SendEmailMessage source)
    {
        return new Email(
            MapToEmailAddress(source.From),
            source.Subject,
            source.HtmlContent,
            source.Tos.Select(MapToEmailAddress).ToList(),
            source.Ccs?.Select(MapToEmailAddress).ToList(),
            source.Bccs?.Select(MapToEmailAddress).ToList());
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
            source.DynamicTemplateData,
            source.Tos.Select(MapToEmailAddress).ToList(),
            source.Ccs?.Select(MapToEmailAddress).ToList(),
            source.Bccs?.Select(MapToEmailAddress).ToList());
    }
}