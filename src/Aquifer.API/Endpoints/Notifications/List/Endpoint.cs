using System.Data.Common;
using Aquifer.API.Common.Dtos;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Notifications.List;

public class Endpoint(
    AquiferDbContext _dbContext,
    IUserService _userService,
    IConfiguration _configuration) : Endpoint<Request, IReadOnlyList<Response>>
{
    public override void Configure()
    {
        Get("/notifications");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var currentUserId = (await _userService.GetUserFromJwtAsync(ct)).Id;

        // Only read notifications are stored in the DB.  This makes paging a little tricky because we'd need to page into each notification
        // entity kind separately.  Instead, we'll read all read notification entity IDs for the last 30 days and then filter and page the
        // notifications in-memory.  Notifications older than 30 days will not be returned.
        var oldestNotificationTimestamp = DateTime.UtcNow.AddDays(-30);

        var dbConnection = _dbContext.Database.GetDbConnection();

        var resourceCommentNotificationData = req.Kinds?.Contains(NotificationKind.Comment) ?? true
            ? await GetResourceCommentNotificationDataAsync(
                dbConnection,
                currentUserId,
                oldestNotificationTimestamp,
                _configuration,
                ct)
            : [];

        var helpDocumentNotificationData = req.Kinds?.Contains(NotificationKind.HelpDocument) ?? true
            ? await GetHelpDocumentNotificationDataAsync(
                dbConnection,
                oldestNotificationTimestamp,
                ct)
            : [];

        var readNotificationEntityIdsByNotificationKindMap = (await _dbContext.Notifications
                .Where(n => n.Created > oldestNotificationTimestamp && n.IsRead)
                .Select(n => new
                {
                    n.Kind,
                    n.NotificationKindId,
                })
                .ToListAsync(ct))
            .GroupBy(x => x.Kind)
            .ToDictionary(
                grp => grp.Key,
                grp => grp.Select(x => x.NotificationKindId).ToHashSet());

        var notifications = resourceCommentNotificationData
            .Select(c => new Response
            {
                Kind = NotificationKind.Comment,
                Comment = new CommentNotification
                {
                    Id = c.CommentId,
                    Created = c.Created,
                    Text = c.Comment,
                    User = new UserDto(c.UserId, c.UserFirstName, c.UserLastName),
                    ResourceContentId = c.ResourceContentId,
                    ResourceEnglishLabel = c.ResourceEnglishLabel,
                    ParentResourceDisplayName = c.ParentResourceDisplayName,
                },
                HelpDocument = null,
                IsRead = readNotificationEntityIdsByNotificationKindMap.GetValueOrDefault(NotificationKind.Comment)
                    ?.Contains(c.CommentId)
                         ?? false,
            })
            .Concat(helpDocumentNotificationData
                .Select(h => new Response
                {
                    Kind = NotificationKind.HelpDocument,
                    Comment = null,
                    HelpDocument = new HelpDocumentNotification
                    {
                        Id = h.Id,
                        Created = h.Created,
                        Title = h.Title,
                        Type = h.Type,
                        Url = h.Url,
                        ThumbnailUrl = h.ThumbnailUrl,
                    },
                    IsRead = readNotificationEntityIdsByNotificationKindMap.GetValueOrDefault(NotificationKind.HelpDocument)
                        ?.Contains(h.Id)
                            ?? false,
                }))
            .Where(x => !req.IsRead.HasValue || x.IsRead == req.IsRead.Value)
            .OrderByDescending(n => n.Comment?.Created ?? n.HelpDocument!.Created)
            .Skip(req.Offset)
            .Take(req.Limit)
            .ToList();

        await SendOkAsync(notifications, ct);
    }

    private static int[]? GetNotifyUserIdsOnCommunityReviewerComment(IConfiguration configuration)
    {
        var userIds = configuration.GetValue<string>("NotifyIdsOnCommunityReviewerComment");
        return userIds is not null ? Array.ConvertAll(userIds.Split(","), int.Parse) : null;
    }

    private sealed record HelpDocumentNotificationData(
        int Id,
        HelpDocumentType Type,
        DateTime Created,
        string Title,
        string Url,
        string? ThumbnailUrl);

    private static async Task<IReadOnlyList<HelpDocumentNotificationData>> GetHelpDocumentNotificationDataAsync(
        DbConnection dbConnection,
        DateTime minimumHelpDocumentCreatedDate,
        CancellationToken ct)
    {
        const string query = """
            SELECT
                d.Id,
                d.Type,
                d.Created,
                d.Title,
                d.Url,
                d.ThumbnailUrl
            FROM HelpDocuments d
            WHERE
                d.Created > @minimumHelpDocumentCreatedDate
            ORDER BY
                d.Created DESC,
                d.Id DESC
            """;

        return (await dbConnection.QueryWithRetriesAsync<HelpDocumentNotificationData>(
                query,
                new { minimumHelpDocumentCreatedDate },
                cancellationToken: ct))
            .ToList();
    }

    private sealed record ResourceCommentNotificationData(
        int CommentId,
        string Comment,
        DateTime Created,
        int UserId,
        string UserFirstName,
        string UserLastName,
        int ResourceContentId,
        string ResourceEnglishLabel,
        string ParentResourceDisplayName);

    private static async Task<IReadOnlyList<ResourceCommentNotificationData>> GetResourceCommentNotificationDataAsync(
        DbConnection dbConnection,
        int userId,
        DateTime minimumCommentCreatedDate,
        IConfiguration cofiguration,
        CancellationToken ct)
    {
        var userIds = GetNotifyUserIdsOnCommunityReviewerComment(cofiguration);
        var isContainingCommunityReviewerComments = userIds is not null && userIds.Contains(userId);
        const string communityReviewerCommentsSubquery = """
                                                         OR EXISTS (
                                                             SELECT NULL
                                                             FROM CommentThreads ct2
                                                             JOIN Comments c2 ON c2.ThreadId = ct2.Id
                                                             WHERE  
                                                                 c2.UserId IN (
                                                                     SELECT Id FROM Users WHERE Role = 6
                                                                 ) AND
                                                                 c.ThreadId = ct2.Id
                                                         )
                                                         """;

        // Get comments for the current user where:
        // 1. The user was assigned to the resource content on which the comment was made at the time the comment was made.
        // 2. The user has previously interacted in the comment thread for the comment.
        // 3. The user was at-mentioned on the comment.
        // The user who made the comment should not get a notification.
        var query = $"""
                     SELECT
                         c.Id AS CommentId,
                         c.Comment,
                         c.Created,
                         c.UserId,
                         u.FirstName AS UserFirstName,
                         u.LastName AS UserLastName,
                         rc.Id AS ResourceContentId,
                         r.EnglishLabel AS ResourceEnglishLabel,
                         pr.DisplayName AS ParentResourceDisplayName
                     FROM Comments c
                         JOIN Users u ON u.Id = c.UserId
                         JOIN CommentThreads ct ON ct.Id = c.ThreadId
                         JOIN ResourceContentVersionCommentThreads rcvct ON rcvct.CommentThreadId = ct.Id
                         JOIN ResourceContentVersions rcv ON rcv.Id = rcvct.ResourceContentVersionId
                         JOIN ResourceContents rc ON rc.Id = rcv.ResourceContentId
                         JOIN Resources r ON r.Id = rc.ResourceId
                         JOIN ParentResources pr ON pr.Id = r.ParentResourceId
                     WHERE
                         (
                             EXISTS
                             (
                                 SELECT NULL
                                 FROM
                                 (
                                     SELECT rcvauh.AssignedUserId,
                                     ROW_NUMBER() OVER
                                     (
                                         PARTITION BY rcvauh.ResourceContentVersionId
                                         ORDER BY rcvauh.Created DESC
                                     ) AS Rank
                                     FROM ResourceContentVersionAssignedUserHistory rcvauh
                                     WHERE
                                         rcvauh.ResourceContentVersionId = rcv.Id AND
                                         rcvauh.Created < c.Created
                                 ) mostRecentHistory
                                 WHERE
                                     mostRecentHistory.Rank = 1 AND
                                     rcv.IsDraft = 1 AND
                                     mostRecentHistory.AssignedUserId = @userId
                             ) {(isContainingCommunityReviewerComments ? communityReviewerCommentsSubquery : "")}
                              OR 
                             EXISTS
                             (
                                 SELECT NULL
                                 FROM CommentThreads ct2
                                 JOIN Comments c2 ON c2.ThreadId = ct2.Id
                                 WHERE
                                     c.ThreadId = ct2.Id AND
                                     c2.UserId = @userId AND
                                     rcv.IsDraft = 1 AND
                                     c.Created > c2.Created
                             ) OR
                             EXISTS
                             (
                                 SELECT NULL
                                 FROM CommentMentions cm
                                 WHERE
                                     cm.CommentId = c.Id AND
                                     cm.UserId <> c.UserId AND
                                     rcv.IsDraft = 1 AND
                                     cm.UserId = @userId
                             )
                         ) AND
                         c.UserId <> @userId AND
                         c.Created > @minimumCommentCreatedDate AND
                         ct.Resolved = 0
                     ORDER BY
                         c.Created DESC,
                         c.Id DESC
                     """;

        return (await dbConnection.QueryWithRetriesAsync<ResourceCommentNotificationData>(
                query,
                new { userId, minimumCommentCreatedDate },
                cancellationToken: ct))
            .ToList();
    }
}