using System.Diagnostics;
using System.Text;
using Aquifer.Common.Configuration;
using Aquifer.Common.Messages;
using Aquifer.Common.Messages.Models;
using Aquifer.Common.Services;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Aquifer.Data.Enums;
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
}

public sealed class FfmpegOptions
{
    public required string FfmpegFilePath { get; init; }
}

public sealed class FfmpegException(string _message, Exception? _innerException = null) : Exception(_message, _innerException);

public sealed class UploadResourceContentAudioMessageSubscriber
{
    public const string AzureCdnStorageAccountServiceKey = nameof(AzureCdnStorageAccountServiceKey);

    private readonly CdnOptions _cdnOptions;
    private readonly UploadOptions _uploadOptions;
    private readonly AquiferDbContext _dbContext;
    private readonly IBlobStorageService _blobStorageService;
    private readonly IBlobStorageService _cdnBlobStorageService;
    private readonly ILogger<UploadResourceContentAudioMessageSubscriber> _logger;
    private readonly string _ffmpegFilePath;

    private const string FiaLegacyCode = "CBBTER";
    private const string FiaCode = "FIA";

    public UploadResourceContentAudioMessageSubscriber(
        FfmpegOptions ffmpegOptions,
        CdnOptions cdnOptions,
        UploadOptions uploadOptions,
        AquiferDbContext dbContext,
        IBlobStorageService blobStorageService,
        [FromKeyedServices(AzureCdnStorageAccountServiceKey)] IBlobStorageService cdnBlobStorageService,
        ILogger<UploadResourceContentAudioMessageSubscriber> logger)
    {
        _cdnOptions = cdnOptions;
        _uploadOptions = uploadOptions;
        _dbContext = dbContext;
        _blobStorageService = blobStorageService;
        _cdnBlobStorageService = cdnBlobStorageService;
        _logger = logger;

        // Allow developers to specify a local workstation path but if running in Azure treat the passed setting as relative to
        // the Azure Functions App deployment directory.
        var functionAppRootDirectory = $@"{Environment.GetEnvironmentVariable("HOME")}\site\wwwroot";
        _ffmpegFilePath = !Path.IsPathFullyQualified(ffmpegOptions.FfmpegFilePath) && Directory.Exists(functionAppRootDirectory)
            ? Path.Combine(functionAppRootDirectory, ffmpegOptions.FfmpegFilePath)
            : ffmpegOptions.FfmpegFilePath;
    }

    private const string UploadResourceContentAudioMessageSubscriberName = "UploadResourceContentAudioMessageSubscriber";

    [Function(UploadResourceContentAudioMessageSubscriberName)]
    public async Task UploadResourceContentAudioAsync(
        [QueueTrigger(Queues.UploadResourceContentAudio)] QueueMessage queueMessage,
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
        _logger.LogInformation($"Beginning processing of {nameof(UploadResourceContentAudioMessage)}: {{Message}}", message);

        var resourceContent = await _dbContext.ResourceContents
            .Include(rc => rc.Language)
            .Include(rc => rc.Resource)
            .ThenInclude(r => r.ParentResource)
            .FirstOrDefaultAsync(rc => rc.Id == message.ResourceContentId, ct)
                ?? throw new InvalidOperationException($"ResourceContent with ID {message.ResourceContentId} not found.");

        var upload = await _dbContext.Uploads
            .AsTracking()
            .FirstOrDefaultAsync(u => u.Id == message.UploadId, ct)
                ?? throw new InvalidOperationException($"Upload with ID {message.UploadId} not found.");

        upload.Status = UploadStatus.Processing;
        await _dbContext.SaveChangesAsync(ct);

        // download temp blob
        var tempFilePath = Path.GetTempFileName();
        await _blobStorageService.DownloadFileAsync(_uploadOptions.TempStorageContainerName, message.TempUploadBlobName, tempFilePath, ct);

        // TODO add ResourceContentVersion.Version to blob name

        // Calculate CDN blob name format string. Full examples after formatting with audio file extension:
        // * "mp3": "resources/FIA/HIN/audio/mp3/HIN_FIA_043_LUK_001_001_1.mp3"
        // * "webm": "resources/FIAKeyTerms/ENG/audio/webm/ENG_FIAKeyTerms_angel-of-the-lord.webm"
        var code = resourceContent.Resource.ParentResource.Code != FiaLegacyCode
            ? resourceContent.Resource.ParentResource.Code
            : FiaCode;
        var language = resourceContent.Language.ISO6393Code.ToUpper();

        var blobifiedResourceName = GetBlobifiedResourceName(resourceContent.Resource.EnglishLabel);

        var cdnBlobDirectoryFormatString = $"resources/{code}/{language}/audio/{{0}}";
        var cdnBlobFileNameFormatString
            = $"{language}_{code}_{blobifiedResourceName}{(message.StepNumber != null ? $"_{message.StepNumber}" : "")}.{{0}}";

        var cdnBlobFormatString = $"{cdnBlobDirectoryFormatString}/{cdnBlobFileNameFormatString}";

        // compress to mp3
        var mp3FilePath = Path.GetTempFileName();
        await CompressToMp3Async(tempFilePath, mp3FilePath, ct);

        // compress to webm
        var webmFilePath = Path.GetTempFileName();
        await CompressToWebmAsync(tempFilePath, webmFilePath, ct);

        File.Delete(tempFilePath);

        // upload files to CDN
        await _cdnBlobStorageService.UploadFilesInParallelAsync(
            _cdnOptions.AquiferContentContainerName,
            [
                (string.Format(cdnBlobFormatString, "mp3"), mp3FilePath),
                (string.Format(cdnBlobFormatString, "webm"), webmFilePath),
            ],
            ct);

        File.Delete(mp3FilePath);
        File.Delete(webmFilePath);

        // TODO update ResourceContentVersion.Content and potentially publish a new version

        // update upload status
        upload.Status = UploadStatus.Completed;
        await _dbContext.SaveChangesAsync(ct);

        // delete temp blob (this is done last because replays are not possible if this file is missing)
        await _blobStorageService.DeleteFileAsync(
            _uploadOptions.TempStorageContainerName,
            message.TempUploadBlobName,
            CancellationToken.None);

        // TODO bonus: re-zip?
        // 1. Download all files from CDN.
        // 2. Create zip file with correct naming including Version suffix.
        // 3. Upload zip file to CDN (will be unique per version); use `overwrite: false`.
        // 4. Update content to include new ZIP file CDN URL.

        _logger.LogInformation($"Finished processing of {nameof(UploadResourceContentAudioMessage)}: {{Message}}", message);
    }

    private static string GetBlobifiedResourceName(string resourceEnglishLabel)
    {
        // attempt to get the verse reference from English labels like "Fia Luke 1:1-4"
        (BookId BookId, int ChapterNumber, int VerseNumber)? bibleReference = null;
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
            ? $"{(int) bibleReference.Value.BookId:D3}_{BibleBookCodeUtilities.CodeFromId(bibleReference.Value.BookId)}_{bibleReference.Value.ChapterNumber:D3}_{bibleReference.Value.VerseNumber:D3}"
            : resourceEnglishLabel.ToKebabCase();

        return blobifiedResourceName;
    }

    private async Task CompressToMp3Async(string inputFilePath, string outputMp3FilePath, CancellationToken ct)
    {
        // Compress source audio file to MP3 format at 32kbps.
        await RunFfmpegAsync($"-y -i \"{inputFilePath}\" -b:a 32k -f mp3 \"{outputMp3FilePath}\"", ct);
    }

    private async Task CompressToWebmAsync(string inputFilePath, string outputWebmFilePath, CancellationToken ct)
    {
        // Compress source audio file to WebM (Opus) format at 16kbps.
        await RunFfmpegAsync($"-y -i \"{inputFilePath}\" -b:a 16k -c:a libopus -f webm \"{outputWebmFilePath}\"", ct);
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
