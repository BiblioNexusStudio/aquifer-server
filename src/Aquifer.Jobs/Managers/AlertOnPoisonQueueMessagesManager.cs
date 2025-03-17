using Aquifer.Common.Messages;
using Aquifer.Jobs.Common;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Aquifer.Jobs.Managers;

public sealed class AlertOnPoisonQueueMessagesManager(
    IQueueClientFactory _queueClientFactory,
    ILogger<AlertOnPoisonQueueMessagesManager> logger)
    : ManagerBase<AlertOnPoisonQueueMessagesManager>(logger)
{
    private readonly ILogger<AlertOnPoisonQueueMessagesManager> _logger = logger;

    [Function(nameof(AlertOnPoisonQueueMessagesManager))]
    [FixedDelayRetry(maxRetryCount: 1, Timings.TenSecondDelayInterval)]
    public override async Task RunAsync([TimerTrigger(CronSchedules.EveryTenMinutes)] TimerInfo timerInfo, CancellationToken ct)
    {
        await base.RunAsync(timerInfo, ct);
    }

    protected override async Task RunCoreAsync(CancellationToken ct)
    {
        foreach (var poisonQueue in Queues.AllPoisonQueues)
        {
            var client = await _queueClientFactory.GetQueueClientAsync(poisonQueue, ct);
            var poisonQueueMessages = await client.PeekMessagesAsync(maxMessages: 32, ct);
            if (poisonQueueMessages?.Value.Length > 0)
            {
                _logger.LogError(
                    "Poison Queue has {NumberOfMessages} messages: \"{PoisonQueue}\".",
                    poisonQueueMessages.Value.Length,
                    poisonQueue);
            }
        }
    }
}