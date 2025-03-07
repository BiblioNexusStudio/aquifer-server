using System.Diagnostics;
using Aquifer.Common.Messages;
using Aquifer.Common.Messages.Models;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Aquifer.Data.Enums;
using Aquifer.Jobs.Configuration;
using Azure.Storage.Queues.Models;
using CaseConverter;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Aquifer.Jobs.Subscribers;

public sealed class UploadResourceContentAudioMessageSubscriber
{
    private readonly AquiferDbContext _dbContext;
    private readonly ILogger<UploadResourceContentAudioMessageSubscriber> _logger;
    private readonly string _ffmpegFilePath;

    private const string CdnContainerName = "aquifer-content";
    private const string TempContainerName = "temp";

    private const string FiaLegacyCode = "CBBTER";
    private const string FiaCode = "FIA";

    public UploadResourceContentAudioMessageSubscriber(
        FfmpegOptions ffmpegOptions,
        AquiferDbContext dbContext,
        ILogger<UploadResourceContentAudioMessageSubscriber> logger)
    {
        _dbContext = dbContext;
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
        _logger.LogInformation("UploadResourceContentAudioMessageSubscriber: UploadResourceContentAudioAsync: Start");

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



        var code = resourceContent.Resource.ParentResource.Code != FiaLegacyCode
            ? resourceContent.Resource.ParentResource.Code
            : FiaCode;
        var language = resourceContent.Language.ISO6393Code.ToUpper();

        // attempt to get the verse reference from English labels like "Fia Luke 1:1-4"
        (BookId BookId, int ChapterNumber, int VerseNumber)? bibleReference = null;
        var possibleBibleReferenceStrings = resourceContent.Resource.EnglishLabel.Split(' ').TakeLast(2).ToList();
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
            : resourceContent.Resource.EnglishLabel.ToKebabCase();

        var targetBlobDirectoryFormatString = $"resources/{code}/{language}/audio/{{0}}";

        var targetBlobFileNameFormatString
            = $"{language}_{code}_{blobifiedResourceName}{(message.StepNumber != null ? $"_{message.StepNumber}" : "")}.{{0}}";

        // Full examples after formatting with extension:
        // * "mp3": "resources/FIA/HIN/audio/mp3/HIN_FIA_043_LUK_001_001_1.mp3"
        // * "webm": "resources/FIAKeyTerms/ENG/audio/webm/ENG_FIAKeyTerms_angel-of-the-lord.webm"
        var targetBlobFormatString = $"{targetBlobDirectoryFormatString}/{targetBlobFileNameFormatString}";

        using var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = _ffmpegFilePath,
                Arguments = "-version",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            },
        };

        process.Start();
        var ffmpegVersionOutput = await process.StandardOutput.ReadToEndAsync(ct);
        await process.WaitForExitAsync(ct);

        _logger.LogInformation("UploadResourceContentAudioMessageSubscriber: UploadResourceContentAudioAsync: ffmpegVersion: {ffmpegVersion}", ffmpegVersionOutput);

        // delete temp blob

        // upload to CDN

        upload.Status = UploadStatus.Completed;
        await _dbContext.SaveChangesAsync(ct);

        _logger.LogInformation("UploadResourceContentAudioMessageSubscriber: UploadResourceContentAudioAsync: Finish");
    }

    private async Task CompressToMp3Async(string inputFilePath, string outputMp3FilePath, CancellationToken ct)
    {
        using var process = new Process();
        process.StartInfo = new ProcessStartInfo
        {
            FileName = _ffmpegFilePath,
            Arguments = $"-y -i \"{inputFilePath}\" -b:a 32k \"{outputMp3FilePath}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        process.Start();
        await process.WaitForExitAsync(ct);
    }

    private async Task CompressToWebmAsync(string inputFilePath, string outputWebmFilePath, CancellationToken ct)
    {
        using var process = new Process();
        process.StartInfo = new ProcessStartInfo
        {
            FileName = _ffmpegFilePath,
            Arguments = $"-y -i \"{inputFilePath}\" -b:a 16k -c:a libopus \"{outputWebmFilePath}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        process.Start();
        await process.WaitForExitAsync(ct);
    }
}