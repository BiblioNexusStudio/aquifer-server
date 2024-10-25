using Aquifer.Common.Services;
using Aquifer.Jobs.Clients;
using Aquifer.Jobs.Configuration;
using Azure;
using Azure.Data.Tables;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Aquifer.Jobs;

public class SyncPageViewsToStorageTable(
    IAquiferAppInsightsClient _appInsightsClient,
    ILogger<SyncPageViewsToStorageTable> _logger,
    IAzureClientService _azureClientService,
    IOptions<ConfigurationOptions> _options)
{
    [Function(nameof(SyncPageViewsToStorageTable))]
#pragma warning disable IDE0060 // Remove unused parameter: A (non-discard) TimerInfo parameter is required for correct Azure bindings
    public async Task Run([TimerTrigger("%Analytics:CronSchedule%")] TimerInfo timerInfo, CancellationToken ct)
#pragma warning restore IDE0060 // Remove unused parameter
    {
        await SyncSourceToPartitionKey("content-manager-web", "AquiferAdminPageViews", ct);
        await SyncSourceToPartitionKey("well-web", "BibleWellPageViews", ct);
    }

    private async Task SyncSourceToPartitionKey(string source, string partitionKey, CancellationToken ct)
    {
        var tableClient = new AquiferTableClient(_options.Value.Analytics.PageViewsTableName, partitionKey, _azureClientService, _options);

        try
        {
            var query = $"""
                         pageViews
                         | where customDimensions.source == "{source}"
                         | project PageViewId = itemId, Name = name, UserId = user_Id, Url = url,
                                   City = client_City, State = client_StateOrProvince,
                                   Country = client_CountryOrRegion, Browser = client_Browser,
                                   OS = client_OS, Timestamp = timestamp,
                                   UserName = customDimensions.userName
                         | order by Timestamp asc
                         """;

            // Note: we're using RealTimestamp here instead of the built-in table's Timestamp, which is set to the time the row is inserted.
            var lastTimestamp = await tableClient.GetLastTimestampAsync("RealTimestamp", ct);

            var queryResult = await _appInsightsClient.QueryAsyncSinceTimestamp<PageViewRow>(query, lastTimestamp, ct);

            foreach (var row in queryResult.Value)
            {
                var timestamp = DateTime.Parse(row.Timestamp).ToUniversalTime();
                var invertedTimestamp = DateTime.MaxValue.Ticks - timestamp.Ticks;
                var pageViewId = row.PageViewId;

                var pageViewEntity = new TableEntity(
                    partitionKey,
                    $"{invertedTimestamp}_{pageViewId}")
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
                    { "PageViewId", pageViewId }
                };

                try
                {
                    await tableClient.AddEntityAsync(pageViewEntity, ct);
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
                "An error occurred while syncing pageViews to the Azure Storage Table. Source: {source}. Partition Key: {partitionKey} ",
                source, partitionKey);
            throw;
        }
    }
}

public record PageViewRow
{
    public string PageViewId { get; set; } = null!;
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