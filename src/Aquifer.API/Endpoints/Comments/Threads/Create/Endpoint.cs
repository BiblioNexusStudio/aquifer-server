using Aquifer.API.Helpers;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Comments.Threads.Create;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Post("/comments/thread/create");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);
        var newThreadId = 0;
        if (req.ThreadType == CommentThreadType.ResourceContentVersion)
        {
            newThreadId = await CreateResourceContentVersionCommentThread(req, user, ct);
        }

        Response response = new() { ThreadId = newThreadId };
        await SendOkAsync(response, ct);
    }

    private async Task<int> CreateResourceContentVersionCommentThread(Request req, UserEntity user, CancellationToken ct)
    {
        var resourceContentVersion = await dbContext.ResourceContentVersions
            .Include(x => x.CommentThreads).SingleOrDefaultAsync(x => x.Id == req.TypeId, ct);

        EndpointHelpers.ThrowErrorIfNull<Request>(resourceContentVersion, x => x.TypeId, "No type found for given id.", 404);

        CommentThreadEntity newThread = new()
        {
            ResourceComments =
            [
                new CommentEntity
                {
                    Comment = req.Comment,
                    UserId = user.Id
                }
            ]
        };

        resourceContentVersion!.CommentThreads.Add(new ResourceContentVersionCommentThreadEntity { CommentThread = newThread });
        await dbContext.SaveChangesAsync(ct);

        return newThread.Id;
    }
}