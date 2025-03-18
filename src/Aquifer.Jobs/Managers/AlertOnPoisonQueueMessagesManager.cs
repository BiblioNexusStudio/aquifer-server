using Aquifer.Common.Messages;
using Aquifer.Data;
using Aquifer.Data.Enums;
using Aquifer.Jobs.Common;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Aquifer.Jobs.Managers;

public sealed class AlertOnPoisonQueueMessagesManager(
    AquiferDbContext _dbContext,
    IQueueClientFactory _queueClientFactory,
    ILogger<AlertOnPoisonQueueMessagesManager> logger)
    : ManagerBase<AlertOnPoisonQueueMessagesManager>(logger)
{
    private const int MaxMessagesToFetch = 11;

    private readonly ILogger<AlertOnPoisonQueueMessagesManager> _logger = logger;

    [Function(nameof(AlertOnPoisonQueueMessagesManager))]
    [FixedDelayRetry(maxRetryCount: 1, Timings.TenSecondDelayInterval)]
    public override async Task RunAsync([TimerTrigger(CronSchedules.EveryTenMinutes)] TimerInfo timerInfo, CancellationToken ct)
    {
        await base.RunAsync(timerInfo, ct);
    }

    protected override async Task RunCoreAsync(CancellationToken ct)
    {
        var jobHistory = await _dbContext.JobHistory
            .AsTracking()
            .SingleAsync(jh => jh.JobId == JobId.AlertOnPoisonQueueMessages, ct);

        var now = DateTime.UtcNow;

        var isHealthy = true;
        foreach (var poisonQueue in Queues.AllPoisonQueues)
        {
            var client = await _queueClientFactory.GetQueueClientAsync(poisonQueue, ct);
            var poisonQueueMessages = await client.PeekMessagesAsync(MaxMessagesToFetch, ct);
            var poisonQueueMessagesSinceLastJobRun = poisonQueueMessages
                    ?.Value
                    .Where(pqm =>
                        !pqm.InsertedOn.HasValue ||
                        (pqm.InsertedOn.Value.UtcDateTime > jobHistory.LastProcessed && pqm.InsertedOn.Value.UtcDateTime <= now))
                    .ToList()
                ?? [];

            if (poisonQueueMessagesSinceLastJobRun.Count > 0)
            {
                isHealthy = false;

                _logger.LogError(
                    "Poison Queue \"{PoisonQueue}\" has {NumberOfMessages} new message(s) since {LastJobRun}.",
                    poisonQueue,
                    poisonQueueMessagesSinceLastJobRun.Count < MaxMessagesToFetch
                        ? poisonQueueMessagesSinceLastJobRun.Count
                        : "more than 10",
                    jobHistory.LastProcessed);
            }
        }

        if (isHealthy)
        {
            _logger.LogInformation($"{nameof(AlertOnPoisonQueueMessagesManager)}: No poison queue messages detected since {{LastJobRun}}.",
                jobHistory.LastProcessed);
        }

        jobHistory.LastProcessed = now;
        await _dbContext.SaveChangesAsync(CancellationToken.None);
    }
}