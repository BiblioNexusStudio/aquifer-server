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

public class SendResourceAssignmentNotifications(
    IOptions<ConfigurationOptions> _configurationOptions,
    AquiferDbContext _dbContext,
    IEmailMessagePublisher _emailMessagePublisher,
    ILogger<SendResourceAssignmentNotifications> _logger)
{
    private const string _tenSecondDelayInterval = "00:00:10";

    [Function(nameof(SendResourceAssignmentNotifications))]
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
            .Select(userGrouping => new SendTemplatedEmailMessage(
                From: NotificationsHelper.NotificationSenderEmailAddress,
                // Template Designer: https://mc.sendgrid.com/dynamic-templates/d-d85f76c6b4d344f5bc8b90b27cc40cc3/version/b6955fec-6f2e-41f7-a9f5-fb695a5b8ed7/editor
                TemplateId: _configurationOptions.Value.Notifications.SendResourceAssignmentNotificationTemplateId,
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
                Tos: [NotificationsHelper.NotificationToEmailAddress],
                Bccs: [NotificationsHelper.GetEmailAddress(usersByIdMap[userGrouping.Key])]))
            .ToList();

        // Note: If execution fails during this loop then it's possible that we will send multiple emails to a single user;
        // first during the initial failed run and again when the job retries (possibly with new data).
        foreach (var sendTemplatedEmailMessage in sendTemplatedEmailMessages)
        {
            await _emailMessagePublisher.SendEmailAsync(sendTemplatedEmailMessage, CancellationToken.None);
        }

        if (userHistories.Count > 0)
        {
            jobHistory.LastProcessed = userHistories.Select(uh => uh.Created).Max();
            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        _logger.LogInformation("Resource assignment notifications sent to {UserCount} users.", sendTemplatedEmailMessages.Count);
    }
}