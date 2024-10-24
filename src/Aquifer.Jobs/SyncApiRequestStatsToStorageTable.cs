using Aquifer.Common.Services;
using Aquifer.Jobs.Clients;
using Aquifer.Jobs.Configuration;
using Azure.Data.Tables;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Aquifer.Jobs;

public class SyncApiRequestStatsToStorageTable(
    IAquiferApiManagementClient _apiManagementClient,
    ILogger<SyncApiRequestStatsToStorageTable> _logger,
    IAzureClientService _azureClientService,
    IOptions<ConfigurationOptions> _options)
{
    [Function(nameof(SyncApiRequestStatsToStorageTable))]
#pragma warning disable IDE0060 // Remove unused parameter: A (non-discard) TimerInfo parameter is required for correct Azure bindings
    public async Task Run([TimerTrigger("%Analytics:CronSchedule%")] TimerInfo timerInfo, CancellationToken ct)
#pragma warning restore IDE0060 // Remove unused parameter
    {
        const string partitionKey = "ApiRequestStats";
        var tableClient = new AquiferTableClient(_options.Value.Analytics.ApiRequestStatsTableName, partitionKey, _azureClientService,
            _options);

        try
        {
            // Note: we're using EndTimestamp here instead of the built-in table's Timestamp, which is set to the time the row is inserted.
            var lastTimestamp = await tableClient.GetLastTimestampAsync("EndTimestamp", ct);

            var startTime = lastTimestamp ?? DateTime.UtcNow.AddHours(-_options.Value.Analytics.HoursBetweenRuns);
            var endTime = DateTime.UtcNow;
            var queryResults = _apiManagementClient.QuerySinceTimestamp(startTime);

            foreach (var singleResult in queryResults)
            {
                var invertedTimestamp = DateTime.MaxValue.Ticks - endTime.Ticks;

                var apiRequestStatsEntity = new TableEntity(
                    partitionKey,
                    $"{invertedTimestamp}_{singleResult.SubscriptionName}")
                {
                    { "StartTimestamp", startTime },
                    { "EndTimestamp", endTime },
                    { "SubscriptionName", singleResult.SubscriptionName },
                    { "SuccessCount", singleResult.SuccessCount },
                    { "BlockCount", singleResult.BlockCount },
                    { "FailCount", singleResult.FailCount },
                    { "TotalCount", singleResult.TotalCount },
                    { "AverageTime", singleResult.AverageTime }
                };

                try
                {
                    await tableClient.AddEntityAsync(apiRequestStatsEntity, ct);
                }
                catch (Exception error)
                {
                    if (error.Message.Contains("already exists"))
                    {
                        _logger.LogError(
                            "Tried to insert an entity that already exists. This could be the result of items with identical timestamps but most likely indicates an error with filtering logs to the correct time range. Partition Key: {partitionKey} ",
                            partitionKey);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An error occurred while syncing API Request Stats to the Azure Storage Table. Partition Key: {partitionKey} ",
                partitionKey);
            throw;
        }
    }
}