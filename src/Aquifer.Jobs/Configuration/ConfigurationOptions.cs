namespace Aquifer.Jobs.Configuration;

public class ConfigurationOptions
{
    public bool IsDevelopment { get; set; }
    public required AnalyticsOptions Analytics { get; init; }
}

public class AnalyticsOptions
{
    public required string StorageAccountUri { get; init; }
    public required string CustomEventsTableName { get; init; }
    public required string ApiRequestsTableName { get; init; }
    public required string PageViewsTableName { get; init; }
    public required string AppInsightsResourceId { get; init; }
    public required string ApiManagementReportsUri { get; init; }
    public required string CronSchedule { get; init; }
    public required int HoursBetweenRuns { get; init; }
}