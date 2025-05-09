﻿using System.Diagnostics;
using System.Text;
using Aquifer.Common.Configuration;
using Aquifer.Common.Messages;
using Aquifer.Common.Messages.Models;
using Aquifer.Common.Services;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Aquifer.Data.Enums;
using Aquifer.Data.Schemas;
using Aquifer.Data.Services;
using Azure.Storage.Queues.Models;
using CaseConverter;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Aquifer.Jobs.Subscribers;

public sealed class CdnOptions
{
    public required string AquiferContentContainerName { get; init; }
    public required string CdnBaseUri { get; init; }
}

public sealed class FfmpegOptions
{
    public required string FfmpegFilePath { get; init; }
}

public sealed class FfmpegException(string _message, Exception? _innerException = null) : Exception(_message, _innerException);

public sealed class UploadResourceContentAudioMessageSubscriber
{
    public const string AzureCdnStorageAccountServiceKey = nameof(AzureCdnStorageAccountServiceKey);

    private const string FiaLegacyCode = "CBBTER";
    private const string FiaCode = "FIA";

    private const string UploadResourceContentAudioMessageSubscriberName = "UploadResourceContentAudioMessageSubscriber";
    private readonly IBlobStorageService _blobStorageService;
    private readonly IBlobStorageService _cdnBlobStorageService;

    private readonly CdnOptions _cdnOptions;
    private readonly AquiferDbContext _dbContext;
    private readonly string _ffmpegFilePath;
    private readonly ILogger<UploadResourceContentAudioMessageSubscriber> _logger;
    private readonly IResourceHistoryService _resourceHistoryService;
    private readonly UploadOptions _uploadOptions;

    public UploadResourceContentAudioMessageSubscriber(
        FfmpegOptions ffmpegOptions,
        CdnOptions cdnOptions,
        UploadOptions uploadOptions,
        AquiferDbContext dbContext,
        IBlobStorageService blobStorageService,
        [FromKeyedServices(AzureCdnStorageAccountServiceKey)]
        IBlobStorageService cdnBlobStorageService,
        IResourceHistoryService resourceHistoryService,
        ILogger<UploadResourceContentAudioMessageSubscriber> logger)
    {
        _cdnOptions = cdnOptions;
        _uploadOptions = uploadOptions;
        _dbContext = dbContext;
        _blobStorageService = blobStorageService;
        _cdnBlobStorageService = cdnBlobStorageService;
        _resourceHistoryService = resourceHistoryService;
        _logger = logger;

        // Allow developers to specify a local workstation path but if running in Azure treat the passed setting as relative to
        // the Azure Functions App deployment directory.
        var functionAppRootDirectory = $@"{Environment.GetEnvironmentVariable("HOME")}\site\wwwroot";
        _ffmpegFilePath = !Path.IsPathFullyQualified(ffmpegOptions.FfmpegFilePath) && Directory.Exists(functionAppRootDirectory)
            ? Path.Combine(functionAppRootDirectory, ffmpegOptions.FfmpegFilePath)
            : ffmpegOptions.FfmpegFilePath;
    }

    [Function(UploadResourceContentAudioMessageSubscriberName)]
    public async Task UploadResourceContentAudioAsync(
        [QueueTrigger(Queues.UploadResourceContentAudio)]
        QueueMessage queueMessage,
        CancellationToken ct)
    {
        await queueMessage.ProcessAsync<UploadResourceContentAudioMessage, UploadResourceContentAudioMessageSubscriber>(
            _logger,
            UploadResourceContentAudioMessageSubscriberName,
            ProcessAsync,
            ct);
    }

    private async Task ProcessAsync(QueueMessage queueMessage, UploadResourceContentAudioMessage message, CancellationToken ct)
    {
        _logger.LogInformation("Beginning processing of {Message}.", message);

        var resourceContent = await _dbContext.ResourceContents
                .AsTracking()
                .Include(rc => rc.Language)
                .Include(rc => rc.Resource)
                .ThenInclude(r => r.ParentResource)
                .FirstOrDefaultAsync(rc => rc.Id == message.ResourceContentId, ct) ??
            throw new InvalidOperationException($"ResourceContent with ID {message.ResourceContentId} not found.");

        var upload = await _dbContext.Uploads
                .AsTracking()
                .FirstOrDefaultAsync(u => u.Id == message.UploadId, ct) ??
            throw new InvalidOperationException($"Upload with ID {message.UploadId} not found.");

        if (upload.Status == UploadStatus.Completed)
        {
            _logger.LogInformation(
                "Upload status is already {UploadStatus}. Gracefully skipping processing. Message: {Message}.",
                upload.Status,
                message);
            return;
        }

        var resourceContentVersion = await _dbContext.ResourceContentVersions
            .AsTracking()
            .Where(rcv => rcv.ResourceContentId == resourceContent.Id)
            .OrderByDescending(rcv => rcv.Version)
            .FirstAsync(ct);

        var audioContent = JsonUtilities.DefaultDeserialize<ResourceContentAudioJsonSchema>(resourceContentVersion.Content);

        upload.Status = UploadStatus.Processing;
        await _dbContext.SaveChangesAsync(ct);

        try
        {
            // Calculate CDN blob name format string. Full examples after formatting with audio file extension:
            // * "mp3": "resources/FIA/HIN/audio/mp3/HIN_FIA_043_LUK_001_001_1_v003.mp3"
            // * "webm": "resources/FIAKeyTerms/ENG/audio/webm/ENG_FIAKeyTerms_angel-of-the-lord_v011.webm"
            var code = resourceContent.Resource.ParentResource.Code != FiaLegacyCode
                ? resourceContent.Resource.ParentResource.Code
                : FiaCode;
            var language = resourceContent.Language.ISO6393Code.ToUpper();
            var newVersionNumber = resourceContentVersion.Version + 1;

            var blobifiedResourceName = GetBlobifiedResourceName(resourceContent.Resource.EnglishLabel);

            var cdnBlobDirectoryFormatString = $"resources/{code}/{language}/audio/{{0}}";
            var cdnBlobFileNameFormatString
                = $"{language}_{code}_{blobifiedResourceName}{(message.StepNumber != null ? $"_{message.StepNumber}" : "")}_v{newVersionNumber:D3}.{{0}}";

            var cdnBlobFormatString = $"{cdnBlobDirectoryFormatString}/{cdnBlobFileNameFormatString}";

            // Process audio file.
            string tempAudioFilePath = null!;
            string normalizedAudioFilePath = null!;
            string mp3FilePath = null!;
            int mp3FileSize;
            string webmFilePath = null!;
            int webmFileSize;
            try
            {
                // download temp blob (keeping the same file name and extension)
                tempAudioFilePath = GetTempFilePath(Path.GetFileName(message.TempUploadBlobName));
                await _blobStorageService.DownloadFileAsync(
                    _uploadOptions.TempStorageContainerName,
                    message.TempUploadBlobName,
                    tempAudioFilePath,
                    ct);

                // normalize audio file
                normalizedAudioFilePath = await NormalizeAudioFileAsync(tempAudioFilePath, ct);

                // compress to output formats
                mp3FilePath = await CompressToMp3Async(normalizedAudioFilePath, ct);
                mp3FileSize = (int)new FileInfo(mp3FilePath).Length;

                webmFilePath = await CompressToWebmAsync(normalizedAudioFilePath, ct);
                webmFileSize = (int)new FileInfo(webmFilePath).Length;

                // upload files to CDN
                await _cdnBlobStorageService.UploadFilesInParallelAsync(
                    _cdnOptions.AquiferContentContainerName,
                    [
                        (string.Format(cdnBlobFormatString, "mp3"), mp3FilePath),
                        (string.Format(cdnBlobFormatString, "webm"), webmFilePath),
                    ],
                    true, // overwrite existing files in case this job has to be replayed
                    ct);
            }
            finally
            {
                // ensure we delete all the temp files
                SafeDelete(tempAudioFilePath);
                SafeDelete(normalizedAudioFilePath);
                SafeDelete(mp3FilePath);
                SafeDelete(webmFilePath);

                static void SafeDelete(string? filePath)
                {
                    if (filePath == null)
                    {
                        return;
                    }

                    try
                    {
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                    }
                    catch
                    {
                        // ignore errors deleting temp files
                    }
                }
            }

            var cdnUrlFormatString = $"{_cdnOptions.CdnBaseUri}{_cdnOptions.AquiferContentContainerName}/{cdnBlobFormatString}";

            // Update ResourceContentVersion.Content with new CDN URLs.
            var hasSteps = audioContent.Mp3?.Steps?.Any() == true && audioContent.Webm?.Steps?.Any() == true;
            if (message.StepNumber.HasValue)
            {
                var mp3Step = audioContent.Mp3?.Steps?.FirstOrDefault(s => s.StepNumber == message.StepNumber);
                var webmStep = audioContent.Webm?.Steps?.FirstOrDefault(s => s.StepNumber == message.StepNumber);

                if (mp3Step == null || webmStep == null)
                {
                    throw new InvalidOperationException(
                        $"Step number {message.StepNumber.Value} is missing in audio content for Resource Content ID {message.ResourceContentId} and Upload ID {message.UploadId}.");
                }

                mp3Step.Url = string.Format(cdnUrlFormatString, "mp3");
                webmStep.Url = string.Format(cdnUrlFormatString, "webm");

                // Zip files are ignored for now because they are being retired in favor of individual step audio files.
                //audioContent.Mp3!.Size = mp3ZipFileSize;
                //audioContent.Webm!.Size = webmZipFileSize;
            }
            else
            {
                if (hasSteps)
                {
                    throw new InvalidOperationException(
                        $"Audio content has steps but no step number was passed for Resource Content ID {message.ResourceContentId} and Upload ID {message.UploadId}.");
                }

                audioContent.Mp3!.Url = string.Format(cdnUrlFormatString, "mp3");
                audioContent.Mp3!.Size = mp3FileSize;

                audioContent.Webm!.Url = string.Format(cdnUrlFormatString, "webm");
                audioContent.Webm!.Size = webmFileSize;
            }

            var newContent = JsonUtilities.DefaultSerialize(audioContent);

            // The content text size was updated but the content size is based upon either the size of the individual webm file (if no steps)
            // or the size of the zip file (if steps).  We are ignoring the zip file for now and thus will use the previous content size
            // for content with steps as the zip file has not changed.
            var newContentSize = hasSteps ? resourceContentVersion.ContentSize : webmFileSize;

            // create new resource content version and status history
            var newResourceContentVersion = new ResourceContentVersionEntity
            {
                AssignedReviewerUserId = resourceContentVersion.AssignedReviewerUserId,
                AssignedUserId = resourceContentVersion.AssignedUserId,
                Content = newContent,
                ContentSize = newContentSize,
                DisplayName = resourceContentVersion.DisplayName,
                InlineMediaSize = resourceContentVersion.InlineMediaSize,
                IsDraft = false,
                IsPublished = resourceContentVersion.IsPublished,
                ReviewLevel = resourceContentVersion.ReviewLevel,
                SourceWordCount = resourceContentVersion.SourceWordCount,
                Version = newVersionNumber,
                WordCount = resourceContentVersion.WordCount,
            };

            resourceContent.Versions.Add(newResourceContentVersion);

            // this is a somewhat redundant status history as it's the same as the previous status, but it records who uploaded the file
            await _resourceHistoryService.AddStatusHistoryAsync(
                newResourceContentVersion,
                resourceContent.Status,
                message.StartedByUserId,
                ct);

            // unpublish the previous version
            resourceContentVersion.IsDraft = false;
            resourceContentVersion.IsPublished = false;

            upload.Status = UploadStatus.Completed;

            await _dbContext.SaveChangesAsync(ct);
        }
        catch
        {
            const UploadStatus failedStatus = UploadStatus.Failed;

            _logger.LogWarning("Error during processing of {Message}. Marking upload as \"{Status}\".", message, failedStatus);

            try
            {
                upload.Status = failedStatus;
                await _dbContext.SaveChangesAsync(ct);
            }
            catch (Exception ex)
            {
                // don't allow a failure in this catch block to prevent the original exception from being thrown
                _logger.LogError(
                    ex,
                    "Error during processing of {Message} when attempting to mark upload as \"{Status}\". This error will be gracefully ignored and the upload will remain in its previous status.",
                    message,
                    failedStatus);
            }

            throw;
        }

        // delete temp blob (this is done last because replays are not possible if this file is missing)
        await _blobStorageService.DeleteFileAsync(
            _uploadOptions.TempStorageContainerName,
            message.TempUploadBlobName,
            CancellationToken.None);

        // Future improvement: re-zip for Bible Well consumption (probably in another subscriber).
        // 1. Download all step audio files from CDN.
        // 2. Create zip file with correct naming including Version suffix.
        // 3. Upload zip file to CDN (will be unique per version); use `overwrite: false`.
        // 4. Update resource content version content to include the new ZIP file CDN URL.
        // 5. Update resource content version content size to be the size of the ZIP file.

        _logger.LogInformation("Finished processing of {Message}.", message);
    }

    private static string GetBlobifiedResourceName(string resourceEnglishLabel)
    {
        // attempt to get the verse reference from English labels like "Fia Luke 1:1-4"
        (Data.Enums.BookId BookId, int ChapterNumber, int VerseNumber)? bibleReference = null;
        var possibleBibleReferenceStrings = resourceEnglishLabel.Split(' ').TakeLast(2).ToList();
        if (possibleBibleReferenceStrings.Count == 2)
        {
            var bookFullName = possibleBibleReferenceStrings[0];
            var bookId = BibleBookCodeUtilities.IdFromFullName(bookFullName);

            var chapterAndVerseParts = possibleBibleReferenceStrings[1].Split('-').First().Split(':');

            if (bookId != BookId.None &&
                chapterAndVerseParts.Length == 2 &&
                int.TryParse(chapterAndVerseParts[0], out var chapterNumber) &&
                int.TryParse(chapterAndVerseParts[1], out var verseNumber))
            {
                bibleReference = (BookId: bookId, ChapterNumber: chapterNumber, VerseNumber: verseNumber);
            }
        }

        // get a name like "043_LUK_001_001" or "angel-of-the-lord"
        var blobifiedResourceName = bibleReference.HasValue
            ? $"{(int)bibleReference.Value.BookId:D3}_{BibleBookCodeUtilities.CodeFromId(bibleReference.Value.BookId)}_{bibleReference.Value.ChapterNumber:D3}_{bibleReference.Value.VerseNumber:D3}"
            : resourceEnglishLabel.ToKebabCase();

        return blobifiedResourceName;
    }

    private static string GetTempFilePath(string fileName)
    {
        // putting these into the temp path ensures that these get cleaned up eventually if needed.
        return Path.Combine(Path.GetTempPath(), fileName);
    }

    private async Task<string> NormalizeAudioFileAsync(string inputFilePath, CancellationToken ct)
    {
        // ffmpeg only supports writing to a different output file. It also requires either (a) a meaningful file extension or
        // (b) a format specifier like "mp3" or "webm" to determine the output format. In this case we want to be agnostic about the
        // input audio file's format so we need to make a temporary output file using the same extension as the input file.
        var normalizedAudioFilePath = GetTempFilePath(
            $"{Path.GetFileNameWithoutExtension(inputFilePath)}-normalized{Path.GetExtension(inputFilePath)}");
        await NormalizeAudioFileAsync(inputFilePath, normalizedAudioFilePath, ct);

        return normalizedAudioFilePath;
    }

    private async Task NormalizeAudioFileAsync(string inputFilePath, string outputFilePath, CancellationToken ct)
    {
        // Trim leading/trailing silence and apply speech normalization.
        await RunFfmpegAsync(
            $"-y -i \"{inputFilePath}\" -af \"speechnorm=e=3:r=0.00001:l=1,areverse,atrim=start=0.2,silenceremove=start_periods=0.75:start_silence=0.75:start_threshold=0.03,areverse,atrim=start=0.2,silenceremove=start_periods=1:start_silence=0.75:start_threshold=0.05\" \"{outputFilePath}\"",
            ct);
    }

    private async Task<string> CompressToMp3Async(string inputFilePath, CancellationToken ct)
    {
        var outputFilePath = GetTempFilePath($"{Path.GetFileNameWithoutExtension(inputFilePath)}-compressed.mp3");
        await CompressToMp3Async(inputFilePath, outputFilePath, ct);

        return outputFilePath;
    }

    private async Task CompressToMp3Async(string inputFilePath, string outputFilePath, CancellationToken ct)
    {
        // Compress source audio file to MP3 format at 32kbps.
        await RunFfmpegAsync($"-y -i \"{inputFilePath}\" -b:a 32k -f mp3 \"{outputFilePath}\"", ct);
    }

    private async Task<string> CompressToWebmAsync(string inputFilePath, CancellationToken ct)
    {
        var outputFilePath = GetTempFilePath($"{Path.GetFileNameWithoutExtension(inputFilePath)}-compressed.webm");
        await CompressToWebmAsync(inputFilePath, outputFilePath, ct);

        return outputFilePath;
    }

    private async Task CompressToWebmAsync(string inputFilePath, string outputFilePath, CancellationToken ct)
    {
        // Compress source audio file to WebM (Opus) format at 16kbps.
        await RunFfmpegAsync($"-y -i \"{inputFilePath}\" -b:a 16k -c:a libopus -f webm \"{outputFilePath}\"", ct);
    }

    private async Task RunFfmpegAsync(string arguments, CancellationToken ct)
    {
        try
        {
            var errors = new StringBuilder();

            using var process = new Process();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = _ffmpegFilePath,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            process.OutputDataReceived += (_, e) => _logger.LogInformation("ffmpeg output: \"{Data}\"", e.Data);
            process.ErrorDataReceived += (_, e) => errors.Append(e.Data);

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            await process.WaitForExitAsync(ct);

            if (process.ExitCode != 0)
            {
                throw new FfmpegException($"ffmpeg failed with exit code {process.ExitCode}. Errors: {errors}.");
            }
        }
        catch (Exception ex) when (ex is not FfmpegException or OperationCanceledException)
        {
            throw new FfmpegException("ffmpeg process failed to run successfully.", ex);
        }
    }
}