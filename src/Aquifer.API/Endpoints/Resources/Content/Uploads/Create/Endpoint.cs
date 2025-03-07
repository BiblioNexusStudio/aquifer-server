using Aquifer.API.Common;
using Aquifer.Common.Messages.Models;
using Aquifer.Common.Messages.Publishers;
using Aquifer.Common.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.Uploads.Create;

public class Endpoint(
    AquiferDbContext _dbContext,
    IBlobStorageService _blobStorageService,
    IUploadResourceContentAudioMessagePublisher _uploadResourceContentAudioMessagePublisher,
    ILogger<Endpoint> _logger)
    : Endpoint<Request, Response>
{
    // TODO move to app settings
    private const string TempContainerName = "temp";

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

        if (request.StepNumber.HasValue)
        {
            // TODO ensure ResourceContentVersion.Content has steps
            ThrowError(x => x.StepNumber, "Resource Content Version does not have steps.", StatusCodes.Status400BadRequest);
        }

        if (request.File.ContentType != "audio/mpeg")
        {
            ThrowError(x => x.File, "File must be an mp3.", StatusCodes.Status400BadRequest);
        }

        _logger.LogInformation(
            "Creating upload for Resource Content ID {ResourceContentId} and Step Number {StepNumber} from file \"{fileName}\".",
            request.ResourceContentId,
            request.StepNumber,
            request.File.FileName);

        // TODO refactor to unbuffered stream of request???
        var tempBlobName = $"uploads/{Guid.NewGuid()}.mp3";
        await using (var fileStream = request.File.OpenReadStream())
        {
            await _blobStorageService.UploadStreamAsync(TempContainerName, tempBlobName, fileStream, ct);
        }

        var uploadEntity = new UploadEntity
        {
            Status = UploadStatus.Pending,
        };
        _dbContext.Add(uploadEntity);
        await _dbContext.SaveChangesAsync(ct);

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