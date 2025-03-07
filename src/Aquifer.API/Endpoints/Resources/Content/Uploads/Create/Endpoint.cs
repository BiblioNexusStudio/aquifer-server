using Aquifer.API.Common;
using Aquifer.Common.Messages.Models;
using Aquifer.Common.Messages.Publishers;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;

namespace Aquifer.API.Endpoints.Resources.Content.Uploads.Create;

public class Endpoint(
    AquiferDbContext _dbContext,
    IUploadResourceContentAudioMessagePublisher _uploadResourceContentAudioMessagePublisher)
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
        var uploadEntity = new UploadEntity
        {
            Status = UploadStatus.Pending,
        };

        _dbContext.Add(uploadEntity);
        await _dbContext.SaveChangesAsync(ct);

        _uploadResourceContentAudioMessagePublisher.PublishUploadResourceContentAudioMessageAsync(
            new UploadResourceContentAudioMessage
            {
                UploadId = uploadEntity.Id,
            },
            ct);


        var response = new Response
        {
            ResourceContentId = request.ResourceContentId,
            UploadId = 1,
        };

        await SendAsync(response, StatusCodes.Status202Accepted, ct);
    }
}