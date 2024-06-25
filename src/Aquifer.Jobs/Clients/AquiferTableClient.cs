using Aquifer.Common.Services;
using Aquifer.Jobs.Configuration;
using Azure;
using Azure.Data.Tables;
using Microsoft.Extensions.Options;

namespace Aquifer.Jobs.Clients;

public class AquiferTableClient(
    string _tableName,
    string _partitionKey,
    IAzureClientService _azureClientService,
    IOptions<ConfigurationOptions> _options)
{
    private readonly TableClient _tableClient =
        new(new Uri(_options.Value.Analytics.StorageAccountUri), _tableName, _azureClientService.GetCredential());

    public async Task<DateTime?> GetLastTimestampAsync(string timestampName, CancellationToken ct)
    {
        var result = _tableClient.QueryAsync<TableEntity>(
            $"PartitionKey eq '{_partitionKey}'",
            select: [timestampName],
            maxPerPage: 1,
            cancellationToken: ct);

        TableEntity? lastEntity;

        await using (var enumerator = result.GetAsyncEnumerator(ct))
        {
            await enumerator.MoveNextAsync();
            lastEntity = enumerator.Current;
        }

        return lastEntity?.GetDateTime(timestampName);
    }

    public async Task<Response> AddEntityAsync(ITableEntity entity, CancellationToken ct)
    {
        return await _tableClient.AddEntityAsync(entity, ct);
    }
}