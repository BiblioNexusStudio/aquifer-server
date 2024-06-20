using Aquifer.Common.Services;
using Aquifer.Jobs.Configuration;
using Azure;
using Azure.Core;
using Azure.Monitor.Query;
using Microsoft.Extensions.Options;

namespace Aquifer.Jobs.Clients;

public interface IAquiferAppInsightsClient
{
    public Task<Response<IReadOnlyList<T>>> QueryAsyncSinceTimestamp<T>(string query, DateTime? timestamp, CancellationToken ct);
}

public class AquiferAppInsightsClient(IAzureClientService azureClientService, IOptions<ConfigurationOptions> options) : IAquiferAppInsightsClient
{
    private readonly LogsQueryClient _logsClient = new LogsQueryClient(azureClientService.GetCredential());
    private readonly IOptions<ConfigurationOptions> _options = options;

    public async Task<Response<IReadOnlyList<T>>> QueryAsyncSinceTimestamp<T>(string query, DateTime? timestamp, CancellationToken ct)
    {
        var appInsightsResourceId = new ResourceIdentifier(_options.Value.Analytics.AppInsightsResourceId);
        var endTime = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(1));
        var startTime = timestamp ?? endTime.Subtract(TimeSpan.FromDays(1));
        var queryTimeRange = new QueryTimeRange(startTime, endTime);
        return await _logsClient.QueryResourceAsync<T>(appInsightsResourceId, query, queryTimeRange, null, ct);
    }
}