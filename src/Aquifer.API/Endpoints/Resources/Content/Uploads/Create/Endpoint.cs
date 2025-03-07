using Aquifer.API.Common;
using Aquifer.Common.Messages.Models;
using Aquifer.Common.Messages.Publishers;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

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
        var resourceContent = await _dbContext.ResourceContents
            .FirstOrDefaultAsync(rc => rc.Id == request.ResourceContentId, ct);

        if (resourceContent is null || resourceContent.MediaType != ResourceContentMediaType.Audio)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        // TODO ensure resource content has steps


        var uploadEntity = new UploadEntity
        {
            Status = UploadStatus.Pending,
        };
        _dbContext.Add(uploadEntity);
        await _dbContext.SaveChangesAsync(ct);

        // TODO upload file to temp storage
        var tempBlobName = $"uploads/{Guid.NewGuid()}";


        await _uploadResourceContentAudioMessagePublisher.PublishUploadResourceContentAudioMessageAsync(
            new UploadResourceContentAudioMessage(
                uploadEntity.Id,
                request.ResourceContentId,
                request.StepNumber,
                tempBlobName),
            ct);

        var response = new Response
        {
            ResourceContentId = request.ResourceContentId,
            StepNumber = request.StepNumber,
            UploadId = uploadEntity.Id,
        };

        await SendAsync(response, StatusCodes.Status202Accepted, ct);
    }
}