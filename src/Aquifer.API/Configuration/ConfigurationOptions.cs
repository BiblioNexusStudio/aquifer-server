using System.ComponentModel.DataAnnotations;

namespace Aquifer.API.Configuration;

public class ConfigurationOptions
{
    public required ConnectionStringOptions ConnectionStrings { get; init; }
    public JwtSettingOptions? JwtSettings { get; init; }
}

public class ConnectionStringOptions
{
    public required string BiblioNexusDb { get; init; }
}

public class JwtSettingOptions
{
    // Auth isn't needed right now, make required as needed
    [Url]
    public required string Authority { get; init; }

    public required string Audience { get; init; }
}