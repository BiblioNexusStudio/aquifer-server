using Aquifer.API.Common;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Uploads.Get;

public class Endpoint(AquiferDbContext _dbContext) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/uploads/{uploadId}");
        Permissions(PermissionName.CreateContent);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var upload = await _dbContext.Uploads
            .FirstOrDefaultAsync(u => u.Id == req.UploadId, ct);

        if (upload == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var response = new Response
        {
            UploadId = upload.Id,
            Status = upload.Status,
            Created = upload.Created,
        };

        await SendOkAsync(response, ct);
    }
}