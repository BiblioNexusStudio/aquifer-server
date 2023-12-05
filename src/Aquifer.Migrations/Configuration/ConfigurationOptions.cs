namespace Aquifer.Migrations.Configuration;

public class ConfigurationOptions
{
    public required ConnectionStringOptions ConnectionStrings { get; init; }
}

public class ConnectionStringOptions
{
    public required string BiblioNexusDb { get; init; }
}