using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Notifications.Update;

public class Endpoint(AquiferDbContext _dbContext, IUserService _userService, ILogger<Endpoint> _logger) : Endpoint<Request>
{
    public override void Configure()
    {
        Patch("/notifications/{notificationKind}/{notificationEntityId}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var currentUserId = (await _userService.GetUserFromJwtAsync(ct)).Id;

        if (req.NotificationKind == NotificationKind.Comment)
        {
            var comment = await _dbContext.Comments
                .Where(c => c.Id == req.NotificationEntityId)
                .FirstOrDefaultAsync(ct);

            if (comment is null)
            {
                await SendNotFoundAsync(ct);
                return;
            }
        }
        else if (req.NotificationKind == NotificationKind.HelpDocument)
        {
            var helpDocument = await _dbContext.HelpDocuments
                .Where(d => d.Id == req.NotificationEntityId)
                .FirstOrDefaultAsync(ct);

            if (helpDocument is null)
            {
                await SendNotFoundAsync(ct);
                return;
            }
        }
        else
        {
            ThrowError(
                x => x.NotificationKind,
                $"Invalid {nameof(req.NotificationKind)}: \"{req.NotificationKind}\".",
                StatusCodes.Status400BadRequest);
        }

        if (req.IsRead.HasValue)
        {
            // Only read notifications are stored in the DB.
            var existingNotification = await _dbContext.Notifications
                .AsTracking()
                .Where(n => n.Kind == req.NotificationKind && n.NotificationEntityId == req.NotificationEntityId)
                .FirstOrDefaultAsync(ct);

            if (existingNotification is not null && !req.IsRead.Value)
            {
                // instead of marking as unread, delete the notification altogether
                _dbContext.Notifications.Remove(existingNotification);
                await _dbContext.SaveChangesAsync(ct);
            }
            else if (req.IsRead.Value)
            {
                var newNotification = new NotificationEntity
                {
                    UserId = currentUserId,
                    Kind = req.NotificationKind,
                    NotificationEntityId = req.NotificationEntityId,
                    Created = DateTime.UtcNow,
                    IsRead = true,
                };

                _dbContext.Notifications.Add(newNotification);
                await _dbContext.SaveChangesAsync(ct);
            }
            else
            {
                _logger.LogInformation(
                    "An already unread notification (Kind: {NotificationKind}; NotificationEntityId: {NotificationEntityId}) was marked as unread, resulting in a no-op.",
                    req.NotificationKind,
                    req.NotificationEntityId);
            }
        }

        await SendNoContentAsync(ct);
    }
}