using Aquifer.Common.Configuration;

namespace Aquifer.Public.API.Configuration;

public class ConfigurationOptions
{
    public required AzureStorageAccountOptions AzureStorageAccount { get; init; }
    public required ConnectionStringOptions ConnectionStrings { get; init; }
}

public class ConnectionStringOptions
{
    public required string BiblioNexusDb { get; init; }
    public required string BiblioNexusReadOnlyDb { get; init; }
}