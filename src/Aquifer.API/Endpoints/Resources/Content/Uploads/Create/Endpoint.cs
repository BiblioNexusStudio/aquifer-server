using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Common.Configuration;
using Aquifer.Common.Messages.Models;
using Aquifer.Common.Messages.Publishers;
using Aquifer.Common.Services;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Aquifer.Data.Schemas;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.Uploads.Create;

public class Endpoint(
    UploadOptions _uploadOptions,
    AquiferDbContext _dbContext,
    IBlobStorageService _blobStorageService,
    IUploadResourceContentAudioMessagePublisher _uploadResourceContentAudioMessagePublisher,
    IUserService _userService,
    ILogger<Endpoint> _logger)
    : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Post("/resources/content/{resourceContentId}/uploads");
        AllowFileUploads();
        Permissions(PermissionName.CreateContent);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var user = await _userService.GetUserFromJwtAsync(ct);

        var resourceContent = await _dbContext.ResourceContents
            .FirstOrDefaultAsync(rc => rc.Id == request.ResourceContentId, ct);

        if (resourceContent is null || resourceContent.MediaType != ResourceContentMediaType.Audio)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        // we could change this in the future if desired and support additional audio types
        if (request.File.ContentType != "audio/mpeg")
        {
            ThrowError(x => x.File, "File must be an mp3.", StatusCodes.Status400BadRequest);
        }

        var resourceContentVersion = await _dbContext.ResourceContentVersions
            .Where(rcv => rcv.ResourceContentId == request.ResourceContentId)
            .OrderByDescending(rcv => rcv.Version)
            .FirstAsync(ct);

        var audioContent = JsonUtilities.DefaultDeserialize<ResourceContentAudioJsonSchema>(resourceContentVersion.Content);
        var audioContentHasSteps = audioContent.Mp3?.Steps?.Any() == true && audioContent.Webm?.Steps?.Any() == true;

        if (request.StepNumber.HasValue)
        {
            if (!audioContentHasSteps)
            {
                ThrowError(
                    x => x.StepNumber,
                    "The current Resource Content Version's Content does not have steps but a step number was passed.",
                    StatusCodes.Status400BadRequest);
            }
            else if (audioContent.Mp3!.Steps!.All(s => s.StepNumber != request.StepNumber) ||
                audioContent.Webm!.Steps!.All(s => s.StepNumber != request.StepNumber))
            {
                // in the future we could support new uploads but for now we only support replacing existing steps
                ThrowError(
                    x => x.StepNumber,
                    $"The current Resource Content Version's Content does not include step number {request.StepNumber}.",
                    StatusCodes.Status400BadRequest);
            }
        }
        else if (audioContentHasSteps)
        {
            ThrowError(
                x => x.StepNumber,
                "The current Resource Content Version's Content has steps but no step number was passed.",
                StatusCodes.Status400BadRequest);
        }

        _logger.LogInformation(
            "Creating upload for Resource Content ID {ResourceContentId} and Step Number {StepNumber} from file \"{fileName}\".",
            request.ResourceContentId,
            request.StepNumber,
            request.File.FileName);

        // Note: FastEndpoints also supports unbuffered stream processing
        // (see https://fast-endpoints.com/docs/file-handling#handling-large-file-uploads).
        // We could switch to that if buffering consumes too many server resources.
        var tempBlobName = $"uploads/{Guid.NewGuid()}.mp3";
        await using (var fileStream = request.File.OpenReadStream())
        {
            await _blobStorageService.UploadStreamAsync(_uploadOptions.TempStorageContainerName, tempBlobName, fileStream, ct);
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
                tempBlobName,
                user.Id),
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