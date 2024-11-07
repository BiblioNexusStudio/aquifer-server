using Aquifer.API.Helpers;
using Aquifer.API.Services;
using Aquifer.Common.Messages.Models;
using Aquifer.Common.Messages.Publishers;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Comments.Create;

public class Endpoint(AquiferDbContext dbContext, IUserService userService, INotificationMessagePublisher notificationMessagePublisher)
    : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Post("/comments");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var thread = await dbContext.CommentThreads
            .AsTracking()
            .SingleOrDefaultAsync(x => x.Id == req.ThreadId && !x.Resolved, ct);

        EndpointHelpers.ThrowErrorIfNull<Request>(thread, x => x.ThreadId, "No open thread found for id", 404);

        var user = await userService.GetUserFromJwtAsync(ct);
        var newComment = new CommentEntity
        {
            Comment = req.Comment,
            UserId = user.Id
        };

        thread!.Comments.Add(newComment);

        await dbContext.SaveChangesAsync(ct);

        await notificationMessagePublisher.SendResourceCommentCreatedNotificationAsync(
            new SendResourceCommentCreatedNotificationMessage(newComment.Id),
            ct);

        await SendOkAsync(new Response { CommentId = newComment.Id }, ct);
    }
}