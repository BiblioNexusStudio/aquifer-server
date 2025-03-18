namespace Aquifer.Common.Configuration;

public sealed class AzureStorageAccountOptions
{
    public Uri? BlobEndpoint { get; init; }
    public Uri? QueueEndpoint { get; init; }
    public string? ConnectionStringOverride { get; init; }
}