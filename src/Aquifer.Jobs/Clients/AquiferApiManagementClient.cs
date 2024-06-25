using Aquifer.Common.Services;
using Aquifer.Jobs.Configuration;
using Azure.Core;
using Azure.ResourceManager;
using Azure.ResourceManager.ApiManagement;
using Microsoft.Extensions.Options;

namespace Aquifer.Jobs.Clients;

public interface IAquiferApiManagementClient
{
    List<ApiManagementStats> QuerySinceTimestamp(DateTime timestamp);
}

public class AquiferApiManagementClient(IOptions<ConfigurationOptions> _options, IAzureClientService _azureClientService)
    : IAquiferApiManagementClient
{
    private readonly ArmClient _armClient = new(_azureClientService.GetCredential());

    public List<ApiManagementStats> QuerySinceTimestamp(DateTime timestamp)
    {
        var id = new ResourceIdentifier(_options.Value.Analytics.ApiManagementResourceId);
        var isoTimestamp = timestamp.ToString("o");
        var apiManagementServiceResource = _armClient.GetApiManagementServiceResource(id);
        var reportResults = apiManagementServiceResource.GetReportsBySubscription($"timestamp ge {isoTimestamp}");
        var apiManagementStats = new List<ApiManagementStats>();

        foreach (var result in reportResults)
        {
            var subscriptionName = result.SubscriptionResourceId.ToString().Replace("/subscriptions/", "");
            var stats = new ApiManagementStats
            {
                SubscriptionName = subscriptionName,
                SuccessCount = result.CallCountSuccess ?? 0,
                BlockCount = result.CallCountBlocked ?? 0,
                FailCount = result.CallCountFailed ?? 0,
                TotalCount = result.CallCountTotal ?? 0,
                AverageTime = result.ApiTimeAvg ?? 0
            };
            apiManagementStats.Add(stats);
        }

        return apiManagementStats;
    }
}

public record ApiManagementStats
{
    public string SubscriptionName { get; init; } = null!;
    public int SuccessCount { get; init; }
    public int BlockCount { get; init; }
    public int FailCount { get; init; }
    public int TotalCount { get; init; }
    public double AverageTime { get; init; }
}