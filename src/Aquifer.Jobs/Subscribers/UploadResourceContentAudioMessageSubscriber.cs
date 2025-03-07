using System.Diagnostics;
using Aquifer.Common.Messages;
using Aquifer.Common.Messages.Models;
using Aquifer.Jobs.Configuration;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Aquifer.Jobs.Subscribers;

public sealed class UploadResourceContentAudioMessageSubscriber
{
    private readonly ILogger<UploadResourceContentAudioMessageSubscriber> _logger;
    private readonly string _ffmpegFilePath;

    public UploadResourceContentAudioMessageSubscriber(
        FfmpegOptions ffmpegOptions,
        ILogger<UploadResourceContentAudioMessageSubscriber> logger)
    {
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

        _logger.LogInformation("UploadResourceContentAudioMessageSubscriber: UploadResourceContentAudioAsync: Finish");
    }
}