using Aquifer.API.Helpers;
using Aquifer.API.Services;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Comments.Edit;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : Endpoint<Request>
{
    public override void Configure()
    {
        Patch("/comments/{commentId}/edit");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);
        var comment = await dbContext.Comments
            .SingleOrDefaultAsync(x => x.Id == req.CommentId && x.UserId == user.Id && !x.Thread.Resolved, ct);

        EndpointHelpers.ThrowErrorIfNull<Request>(comment, x => x.CommentId, "No owned, unresolved comment found", 404);

        comment!.Comment = req.Comment;
        await dbContext.SaveChangesAsync(ct);
        await SendNoContentAsync(ct);
    }
}