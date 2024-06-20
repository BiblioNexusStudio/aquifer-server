using Aquifer.Common.Services;
using Aquifer.Jobs.Configuration;
using Azure;
using Azure.Data.Tables;
using Microsoft.Extensions.Options;

namespace Aquifer.Jobs.Clients;

public class AquiferTableClient(string tableName, string partitionKey, IAzureClientService azureClientService,
    IOptions<ConfigurationOptions> options)
{
    private readonly string _partitionKey = partitionKey;
    private readonly TableClient _tableClient = new TableClient(new Uri(options.Value.Analytics.StorageAccountUri), tableName, azureClientService.GetCredential());

    public async Task<DateTime?> GetLastTimestampAsync(CancellationToken ct)
    {
        // Note: we're using RealTimestamp here instead of the built-in table's Timestamp
        var result = _tableClient.QueryAsync<TableEntity>(
            $"PartitionKey eq '{_partitionKey}'",
            select: new[] { "RealTimestamp" },
            maxPerPage: 1,
            cancellationToken: ct);

        TableEntity? lastEntity;

        await using (var enumerator = result.GetAsyncEnumerator(ct))
        {
            await enumerator.MoveNextAsync();
            lastEntity = enumerator.Current;
        }

        return lastEntity?.GetDateTime("RealTimestamp");
    }

    public async Task<Response> AddEntityAsync(ITableEntity entity, CancellationToken ct)
    {
        return await _tableClient.AddEntityAsync(entity, ct);
    }
}