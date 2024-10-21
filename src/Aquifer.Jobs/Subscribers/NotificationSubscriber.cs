using System.Data.Common;
using Aquifer.Common.Jobs;
using Aquifer.Common.Jobs.Messages;
using Aquifer.Common.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Aquifer.Jobs.Subscribers;

public sealed class NotificationSubscriber(
    AquiferDbContext _dbContext,
    IEmailService _emailService,
    ILogger<NotificationSubscriber> _logger)
{
    private static readonly EmailAddress s_notificationSenderEmailAddress = new("notifications@aquifer.bible", "Aquifer Notifications");
    private static readonly EmailAddress s_notificationToEmailAddress = new("no-reply@aquifer.bible", "Aquifer");

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
        if (companyLead is not null)
        {
            var templatedEmail = new TemplatedEmail(
                From: s_notificationSenderEmailAddress,
                Subject: "Aquifer Notifications: Project Started",
                TemplateId: "", // TODO
                DynamicTemplateData: new
                {
                    ProjectId = project.Id,
                    ProjectName = project.Name,
                },
                Tos: [s_notificationToEmailAddress],
                Bccs: [GetEmailAddress(companyLead)]);

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
            .Where(u => u.Id == resourceCommentData.UserId)
            .SelectMany(u => u.Company.Users)
            .ToListAsync(ct);

        var templatedEmail = new TemplatedEmail(
            From: s_notificationSenderEmailAddress,
            Subject: "Aquifer Notifications: New Resource Comment",
            TemplateId: "", // TODO
            DynamicTemplateData: resourceCommentData,
            Tos: [s_notificationToEmailAddress],
            Bccs: companyUsers.Select(GetEmailAddress).ToList());

        await _emailService.SendEmailAsync(templatedEmail, ct);
    }

    private static EmailAddress GetEmailAddress(UserEntity userEntity)
    {
        return new EmailAddress(userEntity.Email, $"{userEntity.FirstName} {userEntity.LastName}");
    }

    private sealed record ResourceCommentData(
        int CommentId,
        string Comment,
        int UserId,
        int CommentThreadId,
        int ResourceContentVersionId,
        int ResourceContentId,
        int ResourceId,
        string ResourceEnglishLabel);

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
                ct.Id AS CommentThreadId,
                rcv.Id AS ResourceContentVersionId,
                rc.Id AS ResourceContentId,
                r.Id AS ResourceId,
                r.EnglishLabel AS ResourceEnglishLabel
            FROM
                Comments c
                JOIN CommentThreads ct ON ct.Id = c.ThreadId
                JOIN ResourceContentVersionCommentThreads rcvct ON rcvct.CommentThreadId = ct.Id
                JOIN ResourceContentVersions rcv ON rcv.Id = rcvct.ResourceContentVersionId
                JOIN ResourceContents rc ON rc.Id = rcv.ResourceContentId
                JOIN Resources r ON r.Id = rc.ResourceId
            WHERE
                c.Id = @commentId;
            """;

        return await dbConnection.QuerySingleWithRetriesAsync<ResourceCommentData>(
            query,
            new { commentId },
            cancellationToken: ct);
    }
}