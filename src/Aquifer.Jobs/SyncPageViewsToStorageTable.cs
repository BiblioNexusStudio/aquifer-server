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
    // Run at 7am UTC which is 2-3am ET (depending on DST)
    [Function(nameof(SyncPageViewsToStorageTable))]
    public async Task Run([TimerTrigger("0 7 * * *")] TimerInfo timerInfo, CancellationToken ct)
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
                                   OS = client_OS, Duration = duration, Timestamp = timestamp,
                                   UserName = customDimensions.userName
                         | order by Timestamp asc
                         """;

            var lastTimestamp = await tableClient.GetLastTimestampAsync(ct);

            var queryResult = await _appInsightsClient.QueryAsyncSinceTimestamp<PageViewRow>(query, lastTimestamp, ct);

            foreach (var row in queryResult.Value)
            {
                var timestamp = DateTime.Parse(row.Timestamp);
                var invertedTimestamp = DateTime.MaxValue.Ticks - timestamp.Ticks;
                var pageViewId = row.PageViewId;

                var pageViewEntity = new TableEntity(
                    partitionKey,
                    $"{invertedTimestamp}_{pageViewId}")
                {
                    { "RealTimestamp", timestamp }, // Storage Tables include a non-configurable Timestamp attribute so this lets us have our own
                    { "Name", row.Name },
                    { "Url", row.Url },
                    { "UserId", row.UserId },
                    { "Location", $"{row.City}, {row.State}, {row.Country}" },
                    { "Browser", row.Browser },
                    { "OS", row.OS },
                    { "UserName", row.UserName },
                    { "Duration", row.Duration },
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
                            "Tried to insert an entity that already exists. This could be the result of items with identical timestamps but most likely indicates an error with filtering logs to the correct time range.");
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
            _logger.LogError(ex, "An error occurred while syncing pageViews to the Azure Storage Table. Source: {0}. Partition Key: {1} ",
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
    public int Duration { get; set; }
    public string Timestamp { get; set; } = null!;
}