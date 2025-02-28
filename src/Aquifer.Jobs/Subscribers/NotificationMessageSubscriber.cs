using Aquifer.Common.Messages;
using Aquifer.Common.Messages.Models;
using Aquifer.Common.Messages.Publishers;
using Aquifer.Data;
using Aquifer.Jobs.Common;
using Aquifer.Jobs.Configuration;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Aquifer.Jobs.Subscribers;

public sealed class NotificationMessageSubscriber(
    IOptions<ConfigurationOptions> _configurationOptions,
    AquiferDbContext _dbContext,
    IEmailMessagePublisher _emailMessagePublisher,
    ILogger<NotificationMessageSubscriber> _logger)
{
    private const string SendProjectStartedNotificationMessageSubscriberFunctionName = "SendProjectStartedNotificationMessageSubscriber";

    [Function(SendProjectStartedNotificationMessageSubscriberFunctionName)]
    public async Task SendProjectStartedNotificationAsync(
        [QueueTrigger(Queues.SendProjectStartedNotification)] QueueMessage queueMessage,
        CancellationToken ct)
    {
        await queueMessage.ProcessAsync<SendProjectStartedNotificationMessage, NotificationMessageSubscriber>(
            _logger,
            SendProjectStartedNotificationMessageSubscriberFunctionName,
            ProcessAsync,
            ct);
    }

    private async Task ProcessAsync(QueueMessage queueMessage, SendProjectStartedNotificationMessage message, CancellationToken ct)
    {
        var project = await _dbContext.Projects
            .Include(projectEntity => projectEntity.CompanyLeadUser)
            .FirstAsync(p => p.Id == message.ProjectId, ct);

        var companyLeadUser = project.CompanyLeadUser;
        if (companyLeadUser is { Enabled: true, AquiferNotificationsEnabled: true })
        {
            var templatedEmail = new SendTemplatedEmailMessage(
                From: NotificationsHelper.NotificationSenderEmailAddress,
                TemplateId: _configurationOptions.Value.Notifications.SendProjectStartedNotificationTemplateId,
                Tos: [NotificationsHelper.GetEmailAddress(companyLeadUser)],
                DynamicTemplateData: new Dictionary<string, object>
                {
                    [EmailMessagePublisher.DynamicTemplateDataSubjectPropertyName] = "Aquifer Notifications: Project Started",
                    ["aquiferAdminBaseUri"] = _configurationOptions.Value.AquiferAdminBaseUri,
                    ["projectId"] = project.Id,
                    ["projectName"] = project.Name,
                },
                EmailSpecificDynamicTemplateDataByToEmailAddressMap: new Dictionary<string, Dictionary<string, object>>
                {
                    [companyLeadUser.Email] = new()
                    {
                        ["recipientName"] = NotificationsHelper.GetUserFullName(companyLeadUser),
                    },
                },
                ReplyTos: [NotificationsHelper.NotificationNoReplyEmailAddress]);

            await _emailMessagePublisher.PublishSendTemplatedEmailMessageAsync(templatedEmail, ct);

            _logger.LogInformation(
                "Project started notification sent for Project ID {ProjectId} to User ID {UserId}.",
                project.Id,
                companyLeadUser.Id);
        }
        else
        {
            throw new InvalidOperationException(
                $"Unable to send project started notification for Project ID {project.Id} because there is no assigned Company or Company Lead.");
        }
    }
}