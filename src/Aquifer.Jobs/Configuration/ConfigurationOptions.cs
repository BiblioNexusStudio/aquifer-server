using Aquifer.AI;

namespace Aquifer.Jobs.Configuration;

public class ConfigurationOptions
{
    public required string AquiferAdminBaseUri { get; init; }
    public required string AquiferApiBaseUri { get; init; }
    public required string KeyVaultUri { get; init; }
    public required ConnectionStringOptions ConnectionStrings { get; init; }
    public required AnalyticsOptions Analytics { get; init; }
    public required EmailOptions Email { get; init; }
    public required MarketingEmailOptions MarketingEmail { get; init; }
    public required NotificationsOptions Notifications { get; init; }
    public required OpenAiOptions OpenAi { get; init; }
    public required OpenAiTranslationOptions OpenAiTranslation { get; init; }
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
}

public class MarketingEmailOptions
{
    public required string Address { get; init; }
    public required string Name { get; init; }
    public required string ResourceLink { get; init; }
}

public class NotificationsOptions
{
    public required string SendResourceAssignmentNotificationTemplateId { get; init; }
    public required string SendProjectStartedNotificationTemplateId { get; init; }
    public required string SendResourceCommentCreatedNotificationTemplateId { get; init; }
}