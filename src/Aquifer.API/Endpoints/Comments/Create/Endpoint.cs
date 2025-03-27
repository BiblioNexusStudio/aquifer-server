using Aquifer.API.Helpers;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Aquifer.Data.Services;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Comments.Create;

public class Endpoint(AquiferDbContext dbContext, IUserService userService)
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
            UserId = user.Id,
            Mentions = CommentMentionsUtility.ParseMentionedUserIdsFromCommentText(req.Comment)
                .Select(userId => new CommentMentionEntity
                {
                    UserId = userId,
                })
                .ToList(),
        };

        thread!.Comments.Add(newComment);

        await dbContext.SaveChangesAsync(ct);

        await SendOkAsync(new Response { CommentId = newComment.Id }, ct);
    }
}