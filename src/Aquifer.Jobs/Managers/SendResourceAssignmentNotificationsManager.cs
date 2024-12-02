using Aquifer.Common.Messages;
using Aquifer.Common.Messages.Models;
using Aquifer.Common.Messages.Publishers;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Aquifer.Data.Enums;
using Aquifer.Jobs.Common;
using Aquifer.Jobs.Configuration;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Aquifer.Jobs.Managers;

public class SendResourceAssignmentNotificationsManager(
    IOptions<ConfigurationOptions> _configurationOptions,
    AquiferDbContext _dbContext,
    IEmailMessagePublisher _emailMessagePublisher,
    ILogger<SendResourceAssignmentNotificationsManager> _logger)
{
    private const int _maxNumberOfResourcesToDisplayInNotificationContent = 25;
    private const int _maxAgeOfResourceAssignmentInMinutes = 120;
    private const string _tenSecondDelayInterval = "00:00:10";

    [Function(nameof(SendResourceAssignmentNotificationsManager))]
    [FixedDelayRetry(maxRetryCount: 1, delayInterval: _tenSecondDelayInterval)]
#pragma warning disable IDE0060 // Remove unused parameter: A (non-discard) TimerInfo parameter is required for correct Azure bindings.
    public async Task RunAsync([TimerTrigger(CronSchedules.EveryTenMinutes)] TimerInfo timerInfo, CancellationToken ct)
#pragma warning restore IDE0060 // Remove unused parameter
    {
        var jobHistory = await _dbContext.JobHistory
            .AsTracking()
            .SingleAsync(jh => jh.JobId == JobId.SendResourceAssignmentNotifications, ct);

        // Don't send notifications for very old resource assignments.
        // Normally we expect to find only recent resource assignments but if there are errors during processing
        // or the Azure Function is temporarily disabled then we don't want to send out-of-date notification content.
        var includeResourcesAssignedSince = new[]
        {
            jobHistory.LastProcessed,
            DateTime.UtcNow.AddMinutes(-_maxAgeOfResourceAssignmentInMinutes),
        }
        .Max();

        // Possible Improvement: Only send notifications when a user is assigned to the *most recent* ResourceContentVersion.
        var userHistories = await _dbContext.ResourceContentVersionAssignedUserHistory
                .Where(rcvauh =>
                    rcvauh.AssignedUser != null &&
                    rcvauh.AssignedUser.Enabled &&
                    rcvauh.AssignedUser.AquiferNotificationsEnabled &&
                    (rcvauh.AssignedUser.Role == UserRole.Editor || rcvauh.AssignedUser.Role == UserRole.Reviewer) &&
                    rcvauh.AssignedUserId == rcvauh.ResourceContentVersion.AssignedUserId &&
                    (rcvauh.Created > includeResourcesAssignedSince))
                .Select(rcvauh => new
                {
                    AssignedUser = rcvauh.AssignedUser!,
                    rcvauh.Created,
                    ParentResourceName = rcvauh.ResourceContentVersion.ResourceContent.Resource.ParentResource.DisplayName,
                    rcvauh.ResourceContentVersion.ResourceContentId,
                    ResourceName = rcvauh.ResourceContentVersion.ResourceContent.Resource.EnglishLabel,
                })
                .ToListAsync(ct);

        var usersByIdMap = userHistories
            .Select(uh => uh.AssignedUser)
            .DistinctBy(u => u.Id)
            .ToDictionary(au => au.Id);

        var sendTemplatedEmailMessages = userHistories
            .GroupBy(uh => uh.AssignedUser.Id)
            .Select(userGrouping =>
            {
                var user = usersByIdMap[userGrouping.Key];
                var countOfResourcesAssignedToUser = userGrouping.Count();

                return new SendTemplatedEmailMessage(
                    From: NotificationsHelper.NotificationSenderEmailAddress,
                    TemplateId: _configurationOptions.Value.Notifications.SendResourceAssignmentNotificationTemplateId,
                    Tos: [NotificationsHelper.GetEmailAddress(user)],
                    DynamicTemplateData: new Dictionary<string, object>
                    {
                        [EmailMessagePublisher.DynamicTemplateDataSubjectPropertyName] = "Aquifer Notification: Resources Assigned",
                        ["aquiferAdminBaseUri"] = _configurationOptions.Value.AquiferAdminBaseUri,
                        ["resourceCount"] = countOfResourcesAssignedToUser,
                        ["additionalResourceCount"] = Math.Max(countOfResourcesAssignedToUser - _maxNumberOfResourcesToDisplayInNotificationContent, 0),
                        ["parentResources"] = userGrouping
                            .Take(_maxNumberOfResourcesToDisplayInNotificationContent)
                            .GroupBy(uh => uh.ParentResourceName)
                            .OrderBy(parentResourceGrouping => parentResourceGrouping.Key)
                            .Select(parentResourceGrouping => new
                            {
                                ParentResourceName = parentResourceGrouping.Key,
                                Resources = parentResourceGrouping
                                    .DistinctBy(uh => uh.ResourceContentId)
                                    .OrderBy(uh => uh.ResourceName)
                                    .Select(uh => new
                                    {
                                        uh.ResourceContentId,
                                        uh.ResourceName,
                                    })
                                    .ToArray(),
                            })
                            .ToArray(),
                    },
                    EmailSpecificDynamicTemplateDataByToEmailAddressMap: new Dictionary<string, Dictionary<string, object>>
                    {
                        [user.Email] = new()
                        {
                            ["recipientName"] = NotificationsHelper.GetUserFullName(user),
                        },
                    },
                    ReplyTos: [NotificationsHelper.NotificationNoReplyEmailAddress]);
            })
            .ToList();

        // If execution fails during this loop without exception handling then it's possible that we will send multiple emails to a single
        // user, first during the initial failed run and then again when the job retries (possibly with new data). We can prevent this
        // by skipping any failed message publishing in order to ensure that the majority of the batch succeeds.
        // Any failed messages will be logged and must be replayed manually by a developer.
        // The entire batch will be retried only if all messages failed to publish.
        var successCount = 0;
        foreach (var sendTemplatedEmailMessage in sendTemplatedEmailMessages)
        {
            try
            {
                await _emailMessagePublisher.PublishSendTemplatedEmailMessageAsync(sendTemplatedEmailMessage, CancellationToken.None);
                successCount++;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex, 
                    "Unable to publish resource assignment notification message for \"{EmailAddress}\". Skipping notification. Manual developer replay is required: {MessageContent}",
                    sendTemplatedEmailMessage.Tos[0].Email,
                    MessagesJsonSerializer.Serialize(sendTemplatedEmailMessage, shouldAllowInvalidMessageLength: true));
            }
        }

        if (successCount > 0)
        {
            jobHistory.LastProcessed = userHistories.Select(uh => uh.Created).Max();
            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        _logger.LogInformation("Resource assignment notifications successfully sent to {UserCount} users.", successCount);

        var skippedCount = sendTemplatedEmailMessages.Count - successCount;
        if (skippedCount > 0)
        {
            _logger.LogWarning("Skipped resource assignment notifications for {UserCount} users.", skippedCount);
        }
    }
}