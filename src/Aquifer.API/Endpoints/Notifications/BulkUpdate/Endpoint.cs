﻿using System.Data;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Notifications.BulkUpdate;

public class Endpoint(AquiferDbContext _dbContext, IUserService _userService) : Endpoint<Request>
{
    public override void Configure()
    {
        Post("/notifications/update");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var currentUserId = (await _userService.GetUserFromJwtAsync(ct)).Id;

        ThrowErrorOnInvalidNotificationKinds(req);

        await _dbContext.Database
            .CreateExecutionStrategy()
            .ExecuteAsync(async () =>
            {
                await using var transaction = await _dbContext.Database.BeginTransactionAsync(IsolationLevel.Serializable, ct);

                var requestedCommentIds = await ThrowErrorOnInvalidCommentIdsAsync(req, ct);
                var requestedHelpDocumentIds = await ThrowErrorOnInvalidHelpDocumentIdsAsync(req, ct);

                var created = DateTime.UtcNow;

                // Note that only notifications that a user has marked as read/unread are stored in the DB.
                var existingNotifications = await _dbContext.Notifications
                    .AsTracking()
                    .Where(n => n.UserId == currentUserId &&
                        ((n.Kind == NotificationKind.Comment && requestedCommentIds.Contains(n.NotificationKindId)) ||
                            (n.Kind == NotificationKind.HelpDocument && requestedHelpDocumentIds.Contains(n.NotificationKindId))))
                    .ToListAsync(ct);

                var existingNotificationByIdMapByKindMap = existingNotifications
                    .GroupBy(n => n.Kind)
                    .ToDictionary(
                        n => n.Key,
                        grp => grp.ToDictionary(n => n.NotificationKindId));

                foreach (var update in req.Updates)
                {
                    if (update.IsRead.HasValue)
                    {
                        var existingNotification = existingNotificationByIdMapByKindMap
                            .GetValueOrDefault(update.NotificationKind)
                            ?.GetValueOrDefault(update.NotificationKindId);

                        if (existingNotification is not null)
                        {
                            existingNotification.IsRead = update.IsRead.Value;
                        }
                        else
                        {
                            var newNotification = new NotificationEntity
                            {
                                UserId = currentUserId,
                                Kind = update.NotificationKind,
                                NotificationKindId = update.NotificationKindId,
                                Created = created,
                                IsRead = update.IsRead.Value,
                            };

                            _dbContext.Notifications.Add(newNotification);
                        }
                    }
                }

                await _dbContext.SaveChangesAsync(ct);
                await transaction.CommitAsync(ct);
            });

        await SendNoContentAsync(ct);
    }

    private void ThrowErrorOnInvalidNotificationKinds(Request req)
    {
        var invalidNotificationKinds = req.Updates
            .Select(u => u.NotificationKind)
            .Distinct()
            .Except([NotificationKind.Comment, NotificationKind.HelpDocument])
            .ToList();

        if (invalidNotificationKinds.Count > 0)
        {
            ThrowError(
                x => x.Updates,
                $"Invalid {nameof(NotificationUpdate.NotificationKind)}: \"{string.Join("\", \"", invalidNotificationKinds)}\".",
                StatusCodes.Status400BadRequest);
        }
    }

    private async Task<IReadOnlyList<int>> ThrowErrorOnInvalidCommentIdsAsync(Request req, CancellationToken ct)
    {
        var requestedCommentIds = req.Updates
            .Where(u => u.NotificationKind == NotificationKind.Comment)
            .Select(u => u.NotificationKindId)
            .Distinct()
            .ToList();

        var existingCommentIds = await _dbContext.Comments
            .Where(c => requestedCommentIds.Contains(c.Id))
            .Select(c => c.Id)
            .ToListAsync(ct);

        var nonExistentRequestedCommentIds = requestedCommentIds
            .Except(existingCommentIds)
            .ToList();

        if (nonExistentRequestedCommentIds.Count > 0)
        {
            ThrowError(
                x => x.Updates,
                $"The following Comment IDs do not exist: {string.Join(", ", nonExistentRequestedCommentIds)}.",
                StatusCodes.Status400BadRequest);
        }

        return requestedCommentIds;
    }

    private async Task<IReadOnlyList<int>> ThrowErrorOnInvalidHelpDocumentIdsAsync(Request req, CancellationToken ct)
    {
        var requestedHelpDocumentIds = req.Updates
            .Where(u => u.NotificationKind == NotificationKind.HelpDocument)
            .Select(u => u.NotificationKindId)
            .Distinct()
            .ToList();

        var existingHelpDocumentIds = await _dbContext.HelpDocuments
            .Where(d => requestedHelpDocumentIds.Contains(d.Id))
            .Select(d => d.Id)
            .ToListAsync(ct);

        var nonExistentHelpDocumentIds = requestedHelpDocumentIds
            .Except(existingHelpDocumentIds)
            .ToList();

        if (nonExistentHelpDocumentIds.Count > 0)
        {
            ThrowError(
                x => x.Updates,
                $"The following Help Document IDs do not exist: {string.Join(", ", nonExistentHelpDocumentIds)}.",
                StatusCodes.Status400BadRequest);
        }

        return requestedHelpDocumentIds;
    }
}