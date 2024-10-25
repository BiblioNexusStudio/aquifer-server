using Aquifer.Common.Services;
using Aquifer.Jobs.Clients;
using Aquifer.Jobs.Configuration;
using Azure;
using Azure.Data.Tables;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Aquifer.Jobs;

public class SyncCustomEventsToStorageTable(
    IAquiferAppInsightsClient _appInsightsClient,
    ILogger<SyncCustomEventsToStorageTable> _logger,
    IAzureClientService _azureClientService,
    IOptions<ConfigurationOptions> _options)
{
    [Function(nameof(SyncCustomEventsToStorageTable))]
#pragma warning disable IDE0060 // Remove unused parameter: A (non-discard) TimerInfo parameter is required for correct Azure bindings
    public async Task Run([TimerTrigger("%Analytics:CronSchedule%")] TimerInfo timerInfo, CancellationToken ct)
#pragma warning restore IDE0060 // Remove unused parameter
    {
        await SyncSourceToPartitionKey("content-manager-web", "AquiferAdminCustomEvents", ct);
        await SyncSourceToPartitionKey("well-web", "BibleWellCustomEvents", ct);
    }

    private async Task SyncSourceToPartitionKey(string source, string partitionKey, CancellationToken ct)
    {
        var tableClient =
            new AquiferTableClient(_options.Value.Analytics.CustomEventsTableName, partitionKey, _azureClientService, _options);

        try
        {
            var query = $"""
                         customEvents
                         | where customDimensions.source == "{source}"
                         | project ItemId = itemId, Name = name, UserId = user_Id, Url = operation_Name,
                                   City = client_City, State = client_StateOrProvince,
                                   Country = client_CountryOrRegion, Browser = client_Browser,
                                   OS = client_OS, Timestamp = timestamp,
                                   UserName = customDimensions.userName
                         | order by Timestamp asc
                         """;

            // Note: we're using RealTimestamp here instead of the built-in table's Timestamp, which is set to the time the row is inserted.
            var lastTimestamp = await tableClient.GetLastTimestampAsync("RealTimestamp", ct);

            var queryResult = await _appInsightsClient.QueryAsyncSinceTimestamp<CustomEventRow>(query, lastTimestamp, ct);

            foreach (var row in queryResult.Value)
            {
                var timestamp = DateTime.Parse(row.Timestamp).ToUniversalTime();
                var invertedTimestamp = DateTime.MaxValue.Ticks - timestamp.Ticks;
                var itemId = row.ItemId;

                var customEventEntity = new TableEntity(
                    partitionKey,
                    $"{invertedTimestamp}_{itemId}")
                {
                    {
                        "RealTimestamp", timestamp
                    }, // Storage Tables include a non-configurable Timestamp attribute set to the time of row insertion so this lets us have our own
                    { "Name", row.Name },
                    { "Url", row.Url },
                    { "UserId", row.UserId },
                    { "Location", $"{row.City}, {row.State}, {row.Country}" },
                    { "Browser", row.Browser },
                    { "OS", row.OS },
                    { "UserName", row.UserName },
                    { "ItemId", itemId }
                };

                try
                {
                    await tableClient.AddEntityAsync(customEventEntity, ct);
                }
                catch (RequestFailedException error)
                {
                    if (error.Message.Contains("already exists"))
                    {
                        _logger.LogError(
                            "Tried to insert an entity that already exists. This could be the result of items with identical timestamps but most likely indicates an error with filtering logs to the correct time range. Source: {source}. Partition Key: {partitionKey} ",
                            source, partitionKey);
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
                "An error occurred while syncing customEvents to the Azure Storage Table. Source: {source}. Partition Key: {partitionKey} ",
                source, partitionKey);
            throw;
        }
    }
}

public record CustomEventRow
{
    public string ItemId { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string City { get; set; } = null!;
    public string State { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string Browser { get; set; } = null!;
    public string OS { get; set; } = null!;
    public string? UserName { get; set; }
    public string Timestamp { get; set; } = null!;
}