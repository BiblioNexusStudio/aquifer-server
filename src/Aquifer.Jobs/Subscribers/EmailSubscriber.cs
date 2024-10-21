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

        if (!_configurationOptions.Value.IsDevelopment)
        {
            try
            {
                await _emailService.SendEmailAsync(email, ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to send email: \"{Subject}\"", email.Subject);
                throw;
            }
        }
        else
        {
            _logger.LogInformation("Email not sent in non-production environment: {Email}", message.MessageText);
        }
    }

    [Function(nameof(SendTemplatedEmail))]
    public async Task SendTemplatedEmail(
        [QueueTrigger(Queues.SendTemplatedEmail)]
        QueueMessage message,
        CancellationToken ct)
    {
        var email = message.Deserialize<TemplatedEmail, EmailSubscriber>(_logger);

        if (!_configurationOptions.Value.IsDevelopment)
        {
            try
            {
                await _emailService.SendEmailAsync(email, ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to send templated email: \"{Subject}\"", email.Subject);
                throw;
            }
        }
        else
        {
            _logger.LogInformation("Templated email not sent in non-production environment: {Email}", message.MessageText);
        }
    }
}