using Aquifer.API.Helpers;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Comments.Create;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Post("/comments/create");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var thread = await dbContext.CommentThreads.SingleOrDefaultAsync(x => x.Id == req.ThreadId && !x.Resolved, ct);
        EndpointHelpers.ThrowErrorIfNull<Request>(thread, x => x.ThreadId, "No open thread found for id", 404);

        var user = await userService.GetUserFromJwtAsync(ct);
        var newComment = new CommentEntity
        {
            Comment = req.Comment,
            UserId = user.Id
        };

        thread!.Comments.Add(newComment);

        await dbContext.SaveChangesAsync(ct);
        await SendOkAsync(new Response { CommentId = newComment.Id }, ct);
    }
}