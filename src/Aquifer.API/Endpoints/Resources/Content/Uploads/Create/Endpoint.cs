using Aquifer.API.Common;
using Aquifer.Common.Messages.Publishers;
using Aquifer.Data;
using FastEndpoints;

namespace Aquifer.API.Endpoints.Resources.Content.Uploads.Create;

public class Endpoint(
    AquiferDbContext _dbContext,
    ICompressAudioMessagePublisher _compressAudioMessagePublisher)
    : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Post("/resources/content/{resourceContentId}/uploads");
        AllowFileUploads();
        Permissions(PermissionName.CreateContent, PermissionName.CreateCommunityContent);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var response = new Response
        {
            ResourceContentId = request.ResourceContentId,
            UploadId = 1,
        };

        await SendAsync(response, StatusCodes.Status202Accepted, ct);
    }
}