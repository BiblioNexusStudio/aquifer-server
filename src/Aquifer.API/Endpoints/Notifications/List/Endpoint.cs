using System.Data.Common;
using Aquifer.API.Common.Dtos;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Notifications.List;

public class Endpoint(AquiferDbContext _dbContext, IUserService _userService) : Endpoint<Request, IReadOnlyList<Response>>
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
        CancellationToken ct)
    {
        // Get comment notifications for the current user where that user is assigned or has previously been assigned to the associated
        // resource and where the current user is in the same company as the user who made the comment.  The user who made the comment
        // should not get a notification.
        const string query = """
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
                EXISTS
                (
                    SELECT NULL
                    FROM ResourceContentVersionAssignedUserHistory rcvauh
                    WHERE
                        rcvauh.ResourceContentVersionId = rcv.Id AND
                        rcvauh.Created < c.Created AND
                        rcvauh.AssignedUserId = @userId
                ) AND
                c.UserId <> @userId AND
                c.Created > @minimumCommentCreatedDate
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