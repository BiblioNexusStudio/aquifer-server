using Aquifer.Common.Services;
using Aquifer.Data;
using Aquifer.Data.Enums;
using Aquifer.Jobs.Configuration;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Aquifer.Jobs.Managers;

public class SendResourceAssignmentNotifications(
    IOptions<ConfigurationOptions> _configurationOptions,
    AquiferDbContext _dbContext,
    IEmailService _emailService)
{
    private const string _everyTenMinutesCronSchedule = "0 */10 * * * *";
    private const string _tenSecondDelayInterval = "00:00:10";

    [Function(nameof(SendResourceAssignmentNotifications))]
    [FixedDelayRetry(maxRetryCount: 1, delayInterval: _tenSecondDelayInterval)]
#pragma warning disable IDE0060 // Remove unused parameter: A (non-discard) TimerInfo parameter is required for correct Azure bindings.
    public async Task RunAsync([TimerTrigger(_everyTenMinutesCronSchedule)] TimerInfo timerInfo, CancellationToken ct)
#pragma warning restore IDE0060 // Remove unused parameter
    {
        var jobHistory = await _dbContext.JobHistory
            .AsTracking()
            .SingleAsync(jh => jh.JobId == JobId.SendResourceAssignmentNotifications, ct);

        // Possible Improvement: Only send notifications when a user is assigned to the *most recent* ResourceContentVersion.
        var userHistories = await _dbContext.ResourceContentVersionAssignedUserHistory
                .Where(rcvauh =>
                    rcvauh.AssignedUser != null &&
                    rcvauh.AssignedUser.AquiferNotificationsEnabled &&
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
            .ToDictionary(au => au.Id);

        var templatedEmails = userHistories
            .GroupBy(uh => uh.AssignedUser.Id)
            .Select(userGrouping => new TemplatedEmail(
                From: NotificationsHelper.NotificationSenderEmailAddress,
                Subject: "Aquifer Notification: Resources Assigned",
                // Template Designer: https://mc.sendgrid.com/dynamic-templates/d-d85f76c6b4d344f5bc8b90b27cc40cc3/version/b6955fec-6f2e-41f7-a9f5-fb695a5b8ed7/editor
                TemplateId: "d-d85f76c6b4d344f5bc8b90b27cc40cc3",
                DynamicTemplateData: new
                {
                    _configurationOptions.Value.AquiferAdminBaseUri,
                    ResourceCount = userGrouping.Count(),
                    ParentResources = userGrouping
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
        foreach (var templatedEmail in templatedEmails)
        {
            await _emailService.SendEmailAsync(templatedEmail, CancellationToken.None);
        }

        if (userHistories.Count > 0)
        {
            jobHistory.LastProcessed = userHistories.Select(uh => uh.Created).Max();
            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }
    }
}