using System.ComponentModel.DataAnnotations;

namespace Aquifer.API.Configuration;

public class ConfigurationOptions
{
    public required ConnectionStringOptions ConnectionStrings { get; init; }
    public required JwtSettingOptions JwtSettings { get; init; }
    public required Auth0Settings Auth0Settings { get; init; }
    public required JobQueues JobQueues { get; init; }
    public required string KeyVaultUri { get; init; }
}

public class ConnectionStringOptions
{
    public required string BiblioNexusDb { get; init; }
    public required string AzureStorageAccount { get; init; }
}

public class JwtSettingOptions
{
    [Url]
    public required string Authority { get; init; }

    public required string Audience { get; init; }
}

public class Auth0Settings
{
    public required string ClientId { get; init; }
    public required string Audience { get; init; }

    [Url]
    public required string BaseUri { get; init; }
}

public class JobQueues
{
    public required string TrackResourceContentRequestQueue { get; init; }
}