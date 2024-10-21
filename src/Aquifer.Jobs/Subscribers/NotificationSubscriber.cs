using System.Data.Common;
using System.Web;
using Aquifer.Common.Jobs;
using Aquifer.Common.Jobs.Messages;
using Aquifer.Common.Services;
using Aquifer.Data;
using Aquifer.Jobs.Configuration;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Aquifer.Jobs.Subscribers;

public sealed class NotificationSubscriber(
    IOptions<ConfigurationOptions> _configurationOptions,
    AquiferDbContext _dbContext,
    IEmailService _emailService,
    ILogger<NotificationSubscriber> _logger)
{
    [Function(nameof(SendProjectStartedNotification))]
    public async Task SendProjectStartedNotification(
        [QueueTrigger(Queues.SendProjectStartedNotification)] QueueMessage queueMessage,
        CancellationToken ct)
    {
        var message = queueMessage.Deserialize<SendProjectStartedNotificationMessage, NotificationSubscriber>(_logger);

        var project = await _dbContext.Projects
            .Include(projectEntity => projectEntity.CompanyLeadUser)
            .FirstAsync(p => p.Id == message.ProjectId, ct);

        var companyLead = project.CompanyLeadUser;
        if (companyLead is not null && companyLead.AquiferNotificationsEnabled)
        {
            var templatedEmail = new TemplatedEmail(
                From: NotificationsHelper.NotificationSenderEmailAddress,
                Subject: "Aquifer Notifications: Project Started",
                // Template Designer: https://mc.sendgrid.com/dynamic-templates/d-7760ec3b5ce34b179384d4783cc1bd81/version/e83075a3-ba61-4d42-922f-9fd5df4ee45c/editor
                TemplateId: "d-7760ec3b5ce34b179384d4783cc1bd81",
                DynamicTemplateData: new
                {
                    AquiferAdminBaseUri = _configurationOptions.Value.AquiferAdminBaseUri,
                    ProjectId = project.Id,
                    ProjectName = project.Name,
                },
                Tos: [NotificationsHelper.NotificationToEmailAddress],
                Bccs: [NotificationsHelper.GetEmailAddress(companyLead)]);

            await _emailService.SendEmailAsync(templatedEmail, ct);
        }
    }

    [Function(nameof(SendResourceCommentCreatedNotification))]
    public async Task SendResourceCommentCreatedNotification(
        [QueueTrigger(Queues.SendResourceCommentCreatedNotification)] QueueMessage queueMessage,
        CancellationToken ct)
    {
        var message = queueMessage.Deserialize<SendResourceCommentCreatedNotificationMessage, NotificationSubscriber>(_logger);

        var dbConnection = _dbContext.Database.GetDbConnection();
        var resourceCommentData = await GetResourceCommentDataAsync(dbConnection, message.CommentId, ct);

        var companyUsers = await _dbContext.Users
            .Where(u => u.Id == resourceCommentData.UserId && u.AquiferNotificationsEnabled)
            .SelectMany(u => u.Company.Users)
            .ToListAsync(ct);

        var templatedEmail = new TemplatedEmail(
            From: NotificationsHelper.NotificationSenderEmailAddress,
            Subject: "Aquifer Notifications: New Resource Comment",
            // Template Designer: https://mc.sendgrid.com/dynamic-templates/d-ea84b461ed0f439589098053f8810189/version/26393e82-7d17-4b8f-a37c-cb20f62d8802/editor
            TemplateId: "d-ea84b461ed0f439589098053f8810189",
            DynamicTemplateData: new
            {
                AquiferAdminBaseUri = _configurationOptions.Value.AquiferAdminBaseUri,
                CommenterUserName = NotificationsHelper.GetUserFullName(companyUsers.Single(u => u.Id == resourceCommentData.UserId)),
                CommentHtml = HttpUtility.HtmlEncode(resourceCommentData.Comment).Replace("\n", "<br>"),
                ParentResourceName = resourceCommentData.ParentResourceDisplayName,
                ResourceContentId = resourceCommentData.ResourceContentId,
                ResourceName = resourceCommentData.ResourceEnglishLabel,
            },
            Tos: [NotificationsHelper.NotificationToEmailAddress],
            Bccs: companyUsers.Select(NotificationsHelper.GetEmailAddress).ToList());

        await _emailService.SendEmailAsync(templatedEmail, ct);
    }

    private sealed record ResourceCommentData(
        int CommentId,
        string Comment,
        int UserId,
        int ResourceContentId,
        string ResourceEnglishLabel,
        string ParentResourceDisplayName);

    private static async Task<ResourceCommentData> GetResourceCommentDataAsync(
        DbConnection dbConnection,
        int commentId,
        CancellationToken ct)
    {
        const string query = """
            SELECT
                c.Id AS CommentId,
                c.Comment,
                c.UserId,
                rc.Id AS ResourceContentId,
                r.EnglishLabel AS ResourceEnglishLabel,
                pr.DisplayName AS ParentResourceDisplayName
            FROM
                Comments c
                JOIN CommentThreads ct ON ct.Id = c.ThreadId
                JOIN ResourceContentVersionCommentThreads rcvct ON rcvct.CommentThreadId = ct.Id
                JOIN ResourceContentVersions rcv ON rcv.Id = rcvct.ResourceContentVersionId
                JOIN ResourceContents rc ON rc.Id = rcv.ResourceContentId
                JOIN Resources r ON r.Id = rc.ResourceId
                JOIN ParentResource pr ON pr.Id = r.ParentResourceId
            WHERE
                c.Id = @commentId;
            """;

        return await dbConnection.QuerySingleWithRetriesAsync<ResourceCommentData>(
            query,
            new { commentId },
            cancellationToken: ct);
    }
}