using System.Data;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Notifications.Update;

public class Endpoint(AquiferDbContext _dbContext, IUserService _userService) : Endpoint<Request>
{
    public override void Configure()
    {
        Patch("/notifications/{notificationKind}/{notificationKindId}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var currentUserId = (await _userService.GetUserFromJwtAsync(ct)).Id;

        await _dbContext.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync(IsolationLevel.Serializable, ct);

            if (req.NotificationKind == NotificationKind.Comment)
            {
                var comment = await _dbContext.Comments
                    .Where(c => c.Id == req.NotificationKindId)
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
                    .Where(d => d.Id == req.NotificationKindId)
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
                // Note that only notifications that a user has marked as read/unread are stored in the DB.
                var existingNotification = await _dbContext.Notifications
                    .AsTracking()
                    .Where(
                        n =>
                            n.UserId == currentUserId &&
                            n.Kind == req.NotificationKind &&
                            n.NotificationKindId == req.NotificationKindId)
                    .SingleOrDefaultAsync(ct);

                if (existingNotification is not null)
                {
                    existingNotification.IsRead = req.IsRead.Value;
                }
                else
                {
                    var newNotification = new NotificationEntity
                    {
                        UserId = currentUserId,
                        Kind = req.NotificationKind,
                        NotificationKindId = req.NotificationKindId,
                        Created = DateTime.UtcNow,
                        IsRead = req.IsRead.Value,
                    };

                    _dbContext.Notifications.Add(newNotification);
                }

                await _dbContext.SaveChangesAsync(ct);
                await transaction.CommitAsync(ct);
            }
        });

        await SendNoContentAsync(ct);
    }
}