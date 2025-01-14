using System.Data.Common;
using System.Web;
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

    private const string SendResourceCommentCreatedNotificationMessageSubscriberFunctionName =
        "SendResourceCommentCreatedNotificationMessageSubscriber";

    [Function(SendResourceCommentCreatedNotificationMessageSubscriberFunctionName)]
    public async Task SendResourceCommentCreatedNotificationAsync(
        [QueueTrigger(Queues.SendResourceCommentCreatedNotification)] QueueMessage queueMessage,
        CancellationToken ct)
    {
        await queueMessage.ProcessAsync<SendResourceCommentCreatedNotificationMessage, NotificationMessageSubscriber>(
            _logger,
            SendResourceCommentCreatedNotificationMessageSubscriberFunctionName,
            ProcessAsync,
            ct);
    }

    private async Task ProcessAsync(QueueMessage queueMessage, SendResourceCommentCreatedNotificationMessage message, CancellationToken ct)
    {
        var dbConnection = _dbContext.Database.GetDbConnection();
        var resourceCommentData = await GetResourceCommentDataAsync(dbConnection, message.CommentId, ct);

        // Get users who are assigned or have previously been assigned to this resource
        // and who are in the same company as the user who made the comment,
        // excluding the user who made the comment (who should not get a notification).
        var previouslyAssignedUsers = await _dbContext.ResourceContentVersionAssignedUserHistory
            .Where(rcvauh =>
                rcvauh.ResourceContentVersion.ResourceContentId == resourceCommentData.ResourceContentId &&
                rcvauh.AssignedUserId != null &&
                rcvauh.AssignedUserId.Value != resourceCommentData.UserId)
            .Select(rcvauh => rcvauh.AssignedUserId)
            .Distinct()
            .Join(
                _dbContext.Users
                    .Where(u => u.Id == resourceCommentData.UserId && u.Enabled && u.AquiferNotificationsEnabled)
                    .SelectMany(u => u.Company.Users),
                userId => userId,
                u => u.Id,
                (_, u) => u)
            .ToListAsync(ct);

        if (previouslyAssignedUsers.Count == 0)
        {
            _logger.LogInformation(
                "Comment ID {CommentId} was added to Resource Content ID {ResourceContentId} by User ID {CommenterUserId} but no email was sent because only the commenter has previous resource assignment.",
                resourceCommentData.CommentId,
                resourceCommentData.ResourceContentId,
                resourceCommentData.UserId);
            return;
        }

        var recipientUsers = previouslyAssignedUsers
            .Where(u => u.Id != resourceCommentData.UserId)
            .ToList();

        var templatedEmail = new SendTemplatedEmailMessage(
            From: NotificationsHelper.NotificationSenderEmailAddress,
            TemplateId: _configurationOptions.Value.Notifications.SendResourceCommentCreatedNotificationTemplateId,
            Tos: recipientUsers
                .Select(NotificationsHelper.GetEmailAddress)
                .ToList(),
            DynamicTemplateData: new Dictionary<string, object>
            {
                [EmailMessagePublisher.DynamicTemplateDataSubjectPropertyName] = "Aquifer Notifications: New Resource Comment",
                ["aquiferAdminBaseUri"] = _configurationOptions.Value.AquiferAdminBaseUri,
                ["commenterUserName"] = NotificationsHelper.GetUserFullName(
                    resourceCommentData.UserFirstName,
                    resourceCommentData.UserLastName),
                ["commentHtml"] = HttpUtility.HtmlEncode(resourceCommentData.Comment).Replace("\n", "<br>"),
                ["parentResourceName"] = resourceCommentData.ParentResourceDisplayName,
                ["resourceContentId"] = resourceCommentData.ResourceContentId,
                ["resourceName"] = resourceCommentData.ResourceEnglishLabel,
            },
            EmailSpecificDynamicTemplateDataByToEmailAddressMap: recipientUsers.ToDictionary(
                user => user.Email,
                (user) => new Dictionary<string, object>
                {
                    ["recipientName"] = NotificationsHelper.GetUserFullName(user),
                }),
            ReplyTos: [NotificationsHelper.NotificationNoReplyEmailAddress]);

        await _emailMessagePublisher.PublishSendTemplatedEmailMessageAsync(templatedEmail, ct);

        _logger.LogInformation(
            "Comment created notification was sent for Comment ID {CommentId} on Resource Content ID {ResourceContentId} by User ID {CommenterUserId}.",
            resourceCommentData.CommentId,
            resourceCommentData.ResourceContentId,
            resourceCommentData.UserId);
    }

    private sealed record ResourceCommentData(
        int CommentId,
        string Comment,
        int UserId,
        string UserFirstName,
        string UserLastName,
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
                u.FirstName AS UserFirstName,
                u.LastName AS UserLastName,
                rc.Id AS ResourceContentId,
                r.EnglishLabel AS ResourceEnglishLabel,
                pr.DisplayName AS ParentResourceDisplayName
            FROM
                Comments c
                JOIN Users u ON u.Id = c.UserId
                JOIN CommentThreads ct ON ct.Id = c.ThreadId
                JOIN ResourceContentVersionCommentThreads rcvct ON rcvct.CommentThreadId = ct.Id
                JOIN ResourceContentVersions rcv ON rcv.Id = rcvct.ResourceContentVersionId
                JOIN ResourceContents rc ON rc.Id = rcv.ResourceContentId
                JOIN Resources r ON r.Id = rc.ResourceId
                JOIN ParentResources pr ON pr.Id = r.ParentResourceId
            WHERE
                c.Id = @commentId;
            """;

        return await dbConnection.QuerySingleWithRetriesAsync<ResourceCommentData>(
            query,
            new { commentId },
            cancellationToken: ct);
    }
}