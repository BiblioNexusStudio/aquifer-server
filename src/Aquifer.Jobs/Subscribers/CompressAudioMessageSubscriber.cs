using System.Diagnostics;
using Aquifer.Common.Messages;
using Aquifer.Common.Messages.Models;
using Aquifer.Jobs.Configuration;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Aquifer.Jobs.Subscribers;

public sealed class CompressAudioMessageSubscriber(FfmpegOptions _ffmpegOptions, ILogger<CompressAudioMessageSubscriber> _logger)
{
    private const string CompressAudioMessageSubscriberFunctionName = "CompressAudioMessageSubscriber";

    [Function(CompressAudioMessageSubscriberFunctionName)]
    public async Task CompressAudioAsync(
        [QueueTrigger(Queues.CompressAudio)] QueueMessage queueMessage,
        CancellationToken ct)
    {
        await queueMessage.ProcessAsync<CompressAudioMessage, CompressAudioMessageSubscriber>(
            _logger,
            CompressAudioMessageSubscriberFunctionName,
            ProcessAsync,
            ct);
    }

    private async Task ProcessAsync(QueueMessage queueMessage, CompressAudioMessage message, CancellationToken ct)
    {
        _logger.LogInformation("CompressAudioMessageSubscriber: CompressAudioAsync: Start");

        using var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = _ffmpegOptions.FfmpegFilePath,
                Arguments = "-version",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            },
        };

        process.Start();
        var ffmpegVersionOutput = await process.StandardOutput.ReadToEndAsync(ct);
        await process.WaitForExitAsync(ct);

        _logger.LogInformation("CompressAudioMessageSubscriber: CompressAudioAsync: ffmpegVersion: {ffmpegVersion}", ffmpegVersionOutput);

        _logger.LogInformation("CompressAudioMessageSubscriber: CompressAudioAsync: Finish");
    }
}