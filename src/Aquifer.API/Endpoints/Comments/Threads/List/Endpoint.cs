using System.Diagnostics;
using Aquifer.API.Common;
using Aquifer.API.Common.Dtos;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Comments.Threads.List;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, List<Response>>
{
    public override void Configure()
    {
        Get("/comments/threads");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var response = req.ThreadType switch
        {
            CommentThreadType.ResourceContentVersion => await GetResourceContentVersionCommentThreadAsync(req, ct),
            _ => throw new UnreachableException()
        };

        await SendOkAsync(response, ct);
    }

    private async Task<List<Response>> GetResourceContentVersionCommentThreadAsync(Request req, CancellationToken ct)
    {
        return await dbContext.ResourceContentVersions.Where(x => x.Id == req.TypeId)
            .SelectMany(x => x.CommentThreads).Select(x => new Response
            {
                Id = x.CommentThreadId,
                Resolved = x.CommentThread.Resolved,
                Comments = x.CommentThread.Comments.Select(c => new CommentResponse
                {
                    Id = c.Id,
                    Comment = c.Comment,
                    User = UserDto.FromUserEntity(c.User)!
                }).ToList()
            }).ToListAsync(ct);
    }
}