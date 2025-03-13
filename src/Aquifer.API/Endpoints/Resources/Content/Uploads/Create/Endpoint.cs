using System.Collections.ObjectModel;
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
    // we could potentially add more to this list if needed as ffmpeg supports most audio types
    private static readonly ReadOnlySet<string> s_supportedAudioMimeTypes = new(
        new HashSet<string>(StringComparer.InvariantCultureIgnoreCase)
        {
            "audio/aac",
            "audio/flac",
            "audio/mp3",
            "audio/mpeg",
            "audio/ogg",
            "audio/opus",
            "audio/speex",
            "audio/vorbis",
            "audio/wav",
            "audio/webm",
            "audio/x-aac",
            "audio/x-flac",
            "audio/x-mp3",
            "audio/x-mpeg",
            "audio/x-ms-wma",
            "audio/x-ogg",
            "audio/x-ogg-flac",
            "audio/x-ogg-pcm",
            "audio/x-oggflac",
            "audio/x-oggpcm",
            "audio/x-speex",
            "audio/x-wav",
        });

    public override void Configure()
    {
        Post("/resources/content/{resourceContentId}/uploads");
        AllowFileUploads();
        Permissions(PermissionName.CreateContent);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var user = await _userService.GetUserFromJwtAsync(ct);

        // only audio is supported for now
        var resourceContentVersion = await _dbContext.ResourceContentVersions
            .Where(rcv =>
                rcv.ResourceContentId == request.ResourceContentId &&
                rcv.ResourceContent.MediaType == ResourceContentMediaType.Audio)
            .OrderByDescending(rcv => rcv.Version)
            .FirstOrDefaultAsync(ct);

        if (resourceContentVersion is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        // we could change this in the future if desired and support additional audio types
        if (!s_supportedAudioMimeTypes.Contains(request.File.ContentType))
        {
            ThrowError(x => x.File, $"Unsupported MIME type: {request.File.ContentType}", StatusCodes.Status400BadRequest);
        }

        ValidateContentReferencesStep(request.StepNumber, resourceContentVersion.Content);

        _logger.LogInformation(
            "Creating upload for Resource Content ID {ResourceContentId} and Step Number {StepNumber} from file \"{fileName}\".",
            request.ResourceContentId,
            request.StepNumber,
            request.File.FileName);

        // Note: FastEndpoints also supports unbuffered stream processing
        // (see https://fast-endpoints.com/docs/file-handling#handling-large-file-uploads).
        // We could switch to that if buffering consumes too many server resources.
        var tempBlobName = $"uploads/{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}";
        await using (var fileStream = request.File.OpenReadStream())
        {
            await _blobStorageService.UploadStreamAsync(_uploadOptions.TempStorageContainerName, tempBlobName, fileStream, ct);
        }

        var uploadEntity = new UploadEntity
        {
            BlobName = tempBlobName,
            FileName = request.File.FileName,
            FileSize = request.File.Length,
            ResourceContentId = resourceContentVersion.ResourceContentId,
            StartedByUserId = user.Id,
            StepNumber = request.StepNumber,
            Status = UploadStatus.Pending,
        };
        _dbContext.Uploads.Add(uploadEntity);
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

    private void ValidateContentReferencesStep(int? stepNumber, string content)
    {
        var audioContent = JsonUtilities.DefaultDeserialize<ResourceContentAudioJsonSchema>(content);
        var audioContentHasSteps = audioContent.Mp3?.Steps?.Any() == true && audioContent.Webm?.Steps?.Any() == true;

        if (stepNumber.HasValue)
        {
            if (!audioContentHasSteps)
            {
                ThrowError(
                    x => x.StepNumber,
                    "The current Resource Content Version's Content does not have steps but a step number was passed.",
                    StatusCodes.Status400BadRequest);
            }
            else if (audioContent.Mp3!.Steps!.All(s => s.StepNumber != stepNumber) ||
                audioContent.Webm!.Steps!.All(s => s.StepNumber != stepNumber))
            {
                // in the future we could support new uploads but for now we only support replacing existing steps
                ThrowError(
                    x => x.StepNumber,
                    $"The current Resource Content Version's Content does not include step number {stepNumber}.",
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
    }
}