namespace Aquifer.Jobs.Configuration;

public class ConfigurationOptions
{
    public bool IsDevelopment { get; init; }
    public required string AquiferAdminBaseUri { get; init; }
    public required string AquiferApiBaseUri { get; init; }
    public required string KeyVaultUri { get; init; }
    public required ConnectionStringOptions ConnectionStrings { get; init; }
    public required AnalyticsOptions Analytics { get; init; }
    public required EmailOptions Email { get; init; }
}

public class ConnectionStringOptions
{
    public required string BiblioNexusDb { get; init; }
    public required string AzureStorageAccount { get; init; }
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

public class EmailOptions
{
    public required string SubjectPrefix { get; init; }
    public required MarketingEmailOptions Marketing { get; init; }
    public required TransactionalEmailOptions Transactional { get; init; }
}

public class MarketingEmailOptions
{
    public required string Address { get; init; }
    public required string Name { get; init; }
    public required string ResourceLink { get; init; }
}

public class TransactionalEmailOptions
{
    public required SendResourceAssignmentNotificationOptions SendResourceAssignmentNotification { get; init; }
    public required SendProjectStartedNotificationOptions SendProjectStartedNotification { get; init; }
    public required SendResourceCommentCreatedNotificationOptions SendResourceCommentCreatedNotification { get; init; }
}

public class SendResourceAssignmentNotificationOptions
{
    public required string TemplateId { get; init; }
}

public class SendProjectStartedNotificationOptions
{
    public required string TemplateId { get; init; }
}

public class SendResourceCommentCreatedNotificationOptions
{
    public required string TemplateId { get; init; }
}