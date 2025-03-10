namespace Aquifer.Common.Configuration;

public sealed class AzureStorageAccountOptions
{
    public required Uri? BlobEndpoint { get; init; }
    public required Uri? QueueEndpoint { get; init; }
    public required string? ConnectionStringOverride { get; init; }
}