using System.Diagnostics;
using Aquifer.API.Common;
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
        Post("/comments/threads");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var response = req.ThreadType switch
        {
            CommentThreadType.ResourceContentVersion => await CreateResourceContentVersionCommentThreadAsync(req, ct),
            _ => throw new UnreachableException()
        };

        await SendOkAsync(response, ct);
    }

    private async Task<Response> CreateResourceContentVersionCommentThreadAsync(Request req, CancellationToken ct)
    {
        var resourceContentVersion = await dbContext.ResourceContentVersions
            .AsTracking()
            .Include(x => x.CommentThreads)
            .SingleOrDefaultAsync(x => x.Id == req.TypeId, ct);

        EndpointHelpers.ThrowErrorIfNull<Request>(resourceContentVersion, x => x.TypeId, "No type found for given id.", 404);

        var user = await userService.GetUserFromJwtAsync(ct);

        CommentEntity newComment = new()
        {
            Comment = req.Comment,
            UserId = user.Id
        };

        CommentThreadEntity newThread = new() { Comments = [newComment] };

        resourceContentVersion!.CommentThreads.Add(new ResourceContentVersionCommentThreadEntity { CommentThread = newThread });
        await dbContext.SaveChangesAsync(ct);

        return new Response
        {
            ThreadId = newThread.Id,
            CommentId = newComment.Id
        };
    }
}