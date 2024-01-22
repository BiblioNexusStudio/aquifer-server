namespace Aquifer.Public.API.Configuration;

public class ConfigurationOptions
{
    public required ConnectionStringOptions ConnectionStrings { get; init; }
    public required JobSettings JobSettings { get; init; }
}

public class ConnectionStringOptions
{
    public required string BiblioNexusDb { get; init; }
    public required string AzureStorageAccount { get; init; }
}

public class JobSettings
{
    public required string JobQueueName { get; init; }
}