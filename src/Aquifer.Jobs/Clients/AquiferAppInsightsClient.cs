using Aquifer.Common.Services;
using Aquifer.Jobs.Configuration;
using Azure;
using Azure.Core;
using Azure.Monitor.Query;
using Microsoft.Extensions.Options;

namespace Aquifer.Jobs.Clients;

public interface IAquiferAppInsightsClient
{
    public Task<Response<IReadOnlyList<T>>> QueryAsyncSinceTimestampAsync<T>(string query, DateTime? timestamp, CancellationToken ct);
}

public class AquiferAppInsightsClient(IAzureClientService _azureClientService, IOptions<ConfigurationOptions> _options)
    : IAquiferAppInsightsClient
{
    private static readonly LogsQueryClientOptions s_logsQueryClientOptions = new()
    {
        Retry = {
            Delay = TimeSpan.FromMilliseconds(500),
            MaxRetries = 5,
            Mode = RetryMode.Exponential,
            MaxDelay = TimeSpan.FromSeconds(10),
            NetworkTimeout = TimeSpan.FromSeconds(100),
        },
    };

    private readonly LogsQueryClient _logsClient = new(_azureClientService.GetCredential(), s_logsQueryClientOptions);

    public async Task<Response<IReadOnlyList<T>>> QueryAsyncSinceTimestampAsync<T>(string query, DateTime? timestamp, CancellationToken ct)
    {
        var appInsightsResourceId = new ResourceIdentifier(_options.Value.Analytics.AppInsightsResourceId);
        var endTime = DateTime.UtcNow;
        var startTime = timestamp ?? endTime.AddHours(-_options.Value.Analytics.HoursBetweenRuns);
        startTime = startTime.AddMilliseconds(1);
        var queryTimeRange = new QueryTimeRange(startTime, endTime);
        return await _logsClient.QueryResourceAsync<T>(appInsightsResourceId, query, queryTimeRange, null, ct);
    }
}