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

        // Possible Improvement: Only send notifications when a user is assigned to the *most recent* ResourceContentVersion.
        var userHistories = await _dbContext.ResourceContentVersionAssignedUserHistory
                .Where(rcvauh =>
                    rcvauh.AssignedUser != null &&
                    rcvauh.AssignedUser.Enabled &&
                    rcvauh.AssignedUser.AquiferNotificationsEnabled &&
                    (rcvauh.AssignedUser.Role == UserRole.Editor || rcvauh.AssignedUser.Role == UserRole.Reviewer) &&
                    rcvauh.AssignedUserId == rcvauh.ResourceContentVersion.AssignedUserId &&
                    (rcvauh.Created > jobHistory.LastProcessed))
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

                return new SendTemplatedEmailMessage(
                    From: NotificationsHelper.NotificationSenderEmailAddress,
                    TemplateId: _configurationOptions.Value.Notifications.SendResourceAssignmentNotificationTemplateId,
                    Tos: [NotificationsHelper.GetEmailAddress(user)],
                    DynamicTemplateData: new Dictionary<string, object>
                    {
                        [EmailMessagePublisher.DynamicTemplateDataSubjectPropertyName] = "Aquifer Notification: Resources Assigned",
                        ["aquiferAdminBaseUri"] = _configurationOptions.Value.AquiferAdminBaseUri,
                        ["resourceCount"] = userGrouping.Count(),
                        ["parentResources"] = userGrouping
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

        // Note: If execution fails during this loop then it's possible that we will send multiple emails to a single user;
        // first during the initial failed run and again when the job retries (possibly with new data).
        foreach (var sendTemplatedEmailMessage in sendTemplatedEmailMessages)
        {
            await _emailMessagePublisher.PublishSendTemplatedEmailMessageAsync(sendTemplatedEmailMessage, CancellationToken.None);
        }

        if (userHistories.Count > 0)
        {
            jobHistory.LastProcessed = userHistories.Select(uh => uh.Created).Max();
            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        _logger.LogInformation("Resource assignment notifications sent to {UserCount} users.", sendTemplatedEmailMessages.Count);
    }
}