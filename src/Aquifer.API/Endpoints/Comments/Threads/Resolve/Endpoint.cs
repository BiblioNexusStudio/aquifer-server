using Aquifer.API.Helpers;
using Aquifer.API.Services;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Comments.Threads.Resolve;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : Endpoint<Request>
{
    public override void Configure()
    {
        Patch("/comments/threads/{threadId}/resolve");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var thread = await dbContext.CommentThreads.SingleOrDefaultAsync(x => x.Id == req.ThreadId && !x.Resolved, ct);
        EndpointHelpers.ThrowErrorIfNull<Request>(thread, x => x.ThreadId, "No unresolved thread found for given id.", 404);

        var user = await userService.GetUserFromJwtAsync(ct);

        thread!.Resolved = true;
        thread.ResolvedByUserId = user.Id;

        await dbContext.SaveChangesAsync(ct);
        await SendNoContentAsync(ct);
    }
}