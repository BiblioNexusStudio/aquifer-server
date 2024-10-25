using Aquifer.Common.Jobs;
using Aquifer.Common.Services;
using Aquifer.Jobs.Configuration;
using Aquifer.Jobs.Services;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Aquifer.Jobs.Subscribers;

public sealed class EmailSubscriber(
    [FromKeyedServices(nameof(SendGridEmailService))] IEmailService _emailService,
    IOptions<ConfigurationOptions> _configurationOptions,
    ILogger<EmailSubscriber> _logger)
{
    [Function(nameof(SendEmail))]
    public async Task SendEmail(
        [QueueTrigger(Queues.SendEmail)]
        QueueMessage message,
        CancellationToken ct)
    {
        var email = message.Deserialize<Email, EmailSubscriber>(_logger);

        try
        {
            await _emailService.SendEmailAsync(
                email with { Subject = $"{_configurationOptions.Value.Email.SubjectPrefix}{email.Subject}" },
                ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unable to send email: \"{Email}\"", message.MessageText);
            throw;
        }

        _logger.LogInformation("Email sent: {Email}", message.MessageText);
    }

    [Function(nameof(SendTemplatedEmail))]
    public async Task SendTemplatedEmail(
        [QueueTrigger(Queues.SendTemplatedEmail)]
        QueueMessage message,
        CancellationToken ct)
    {
        var templatedEmail = message.Deserialize<TemplatedEmail, EmailSubscriber>(_logger);

        // if `Subject` is present in the dynamic template data then add an environment specific prefix to it
        if (templatedEmail.DynamicTemplateData.TryGetValue(EmailService.DynamicTemplateDataSubjectPropertyName, out var subject) &&
            subject is string subjectString)
        {
            templatedEmail.DynamicTemplateData[EmailService.DynamicTemplateDataSubjectPropertyName]
                = $"{_configurationOptions.Value.Email.SubjectPrefix}{subjectString}";
        }

        try
        {
            await _emailService.SendEmailAsync(templatedEmail, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unable to send templated email: \"{TemplatedEmail}\"", message.MessageText);
            throw;
        }

        _logger.LogInformation("Templated email sent: {TemplatedEmail}", message.MessageText);
    }
}