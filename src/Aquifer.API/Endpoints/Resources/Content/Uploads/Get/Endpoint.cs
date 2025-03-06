using Aquifer.API.Common;
using Aquifer.Data;
using FastEndpoints;

namespace Aquifer.API.Endpoints.Resources.Content.Uploads.Get;

public class Endpoint(AquiferDbContext _dbContext) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/resources/content/{resourceContentId}/uploads/{uploadId}");
        Permissions(PermissionName.CreateContent, PermissionName.CreateCommunityContent);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var response = new Response
        {
            Id = 1,
            Status = Response.UploadStatus.Pending,
        };

        await SendOkAsync(response, ct);
    }
}