﻿using System.Data.Common;
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

        var companyLeadUser = project.CompanyLeadUser;
        if (companyLeadUser is not null && companyLeadUser.AquiferNotificationsEnabled)
        {
            var templatedEmail = new TemplatedEmail(
                From: NotificationsHelper.NotificationSenderEmailAddress,
                // Template Designer: https://mc.sendgrid.com/dynamic-templates/d-7760ec3b5ce34b179384d4783cc1bd81/version/e83075a3-ba61-4d42-922f-9fd5df4ee45c/editor
                TemplateId: "d-7760ec3b5ce34b179384d4783cc1bd81",
                DynamicTemplateData: new Dictionary<string, object>
                {
                    [EmailService.DynamicTemplateDataSubjectPropertyName] = "Aquifer Notifications: Project Started",
                    ["aquiferAdminBaseUri"] = _configurationOptions.Value.AquiferAdminBaseUri,
                    ["projectId"] = project.Id,
                    ["projectName"] = project.Name,
                },
                Tos: [NotificationsHelper.NotificationToEmailAddress],
                Bccs: [NotificationsHelper.GetEmailAddress(companyLeadUser)]);

            await _emailService.SendEmailAsync(templatedEmail, ct);

            _logger.LogInformation(
                "Project started notification sent for Project ID {ProjectId} to User ID {UserId}.",
                project.Id,
                companyLeadUser.Id);
        }
        else
        {
            _logger.LogWarning(
                "Unable to send project started notification for Project ID {ProjectId} because there is no assigned Company or Company Lead.",
                project.Id);
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
                    .Where(u => u.Id == resourceCommentData.UserId && u.AquiferNotificationsEnabled)
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

        var commenterUser = await _dbContext.Users
            .SingleAsync(u => u.Id == resourceCommentData.UserId, ct);

        var templatedEmail = new TemplatedEmail(
            From: NotificationsHelper.NotificationSenderEmailAddress,
            // Template Designer: https://mc.sendgrid.com/dynamic-templates/d-ea84b461ed0f439589098053f8810189/version/26393e82-7d17-4b8f-a37c-cb20f62d8802/editor
            TemplateId: "d-ea84b461ed0f439589098053f8810189",
            DynamicTemplateData: new Dictionary<string, object>
            {
                [EmailService.DynamicTemplateDataSubjectPropertyName] = "Aquifer Notifications: New Resource Comment",
                ["aquiferAdminBaseUri"] = _configurationOptions.Value.AquiferAdminBaseUri,
                ["commenterUserName"] = NotificationsHelper.GetUserFullName(commenterUser),
                ["commentHtml"] = HttpUtility.HtmlEncode(resourceCommentData.Comment).Replace("\n", "<br>"),
                ["parentResourceName"] = resourceCommentData.ParentResourceDisplayName,
                ["resourceContentId"] = resourceCommentData.ResourceContentId,
                ["resourceName"] = resourceCommentData.ResourceEnglishLabel,
            },
            Tos: [NotificationsHelper.NotificationToEmailAddress],
            Bccs: previouslyAssignedUsers
                .Where(u => u.Id != resourceCommentData.UserId)
                .Select(NotificationsHelper.GetEmailAddress)
                .ToList());

        await _emailService.SendEmailAsync(templatedEmail, ct);

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