namespace Aquifer.Jobs.Configuration;

public class ConfigurationOptions
{
    public bool IsDevelopment { get; init; }
    public required string BaseUrl { get; init; }
    public required string KeyVaultUri { get; init; }
    public required AnalyticsOptions Analytics { get; init; }
    public required MarketingEmailOptions MarketingEmail { get; init; }
}

public class AnalyticsOptions
{
    public required string StorageAccountUri { get; init; }
    public required string CustomEventsTableName { get; init; }
    public required string ApiRequestStatsTableName { get; init; }
    public required string PageViewsTableName { get; init; }
    public required string AppInsightsResourceId { get; init; }
    public required string ApiManagementResourceId { get; init; }
    public required string CronSchedule { get; init; }
    public required int HoursBetweenRuns { get; init; }
}

public class MarketingEmailOptions
{
    public required string Address { get; init; }
    public required string Name { get; init; }
    public required string ResourceLink { get; init; }
}