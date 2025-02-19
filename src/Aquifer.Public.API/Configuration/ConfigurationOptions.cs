namespace Aquifer.Public.API.Configuration;

public class ConfigurationOptions
{
    public required ConnectionStringOptions ConnectionStrings { get; init; }
    public required JobQueues JobQueues { get; init; }
}

public class ConnectionStringOptions
{
    public required string BiblioNexusDb { get; init; }
    public required string BiblioNexusReadOnlyDb { get; init; }
    public required string AzureStorageAccount { get; init; }
}

public class JobQueues
{
    public required string TrackResourceContentRequestQueue { get; init; }
}