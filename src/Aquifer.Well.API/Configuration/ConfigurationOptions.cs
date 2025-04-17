﻿using Aquifer.Common.Configuration;

namespace Aquifer.Well.API.Configuration;

public class ConfigurationOptions
{
    public required AzureStorageAccountOptions AzureStorageAccount { get; init; }
    public required ConnectionStringOptions ConnectionStrings { get; init; }
}

public class ConnectionStringOptions
{
    public required string BiblioNexusReadOnlyDb { get; init; }
}