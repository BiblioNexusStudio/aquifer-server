using Azure.Core;
using Azure.Storage.Blobs;

namespace Aquifer.Common.Services;

public interface IBlobStorageService
{
    Task UploadFileAsync(string containerName, string blobName, Stream fileStream, CancellationToken ct);
    Task UploadFileAsync(string containerName, string blobName, string filePath, CancellationToken ct);
    Task UploadFilesInParallelAsync(string containerName, IEnumerable<(string BlobName, string FilePath)> files, CancellationToken ct);
}

public sealed class BlobStorageService(string _connectionString, IAzureClientService _azureClientService): IBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient = GetBlobServiceClient(_connectionString, _azureClientService);

    private static readonly BlobClientOptions s_blobClientOptions = new()
    {
        Retry = {
            Delay = TimeSpan.FromMilliseconds(50),
            MaxRetries = 5,
            Mode = RetryMode.Exponential,
            MaxDelay = TimeSpan.FromSeconds(10),
            NetworkTimeout = TimeSpan.FromSeconds(100),
        },
    };

    public async Task UploadFileAsync(string containerName, string blobName, Stream fileStream, CancellationToken ct)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(blobName);

        await blobClient.UploadAsync(fileStream, overwrite: true, ct);
    }

    public async Task UploadFileAsync(string containerName, string blobName, string filePath, CancellationToken ct)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(blobName);

        await UploadFileCoreAsync(blobClient, filePath, ct);
    }

    public async Task UploadFilesInParallelAsync(
        string containerName,
        IEnumerable<(string BlobName, string FilePath)> files,
        CancellationToken ct)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        await Task.WhenAll(files.Select(f => UploadFileCoreAsync(containerClient.GetBlobClient(f.BlobName), f.FilePath, ct)));
    }

    private static async Task UploadFileCoreAsync(BlobClient blobClient, string filePath, CancellationToken cancellationToken)
    {
        await using var fileStream = File.OpenRead(filePath);
        await blobClient.UploadAsync(fileStream, overwrite: true, cancellationToken);
    }

    private static BlobServiceClient GetBlobServiceClient(string connectionString, IAzureClientService azureClientService)
    {
        return connectionString.StartsWith("http")
            ? new BlobServiceClient(
                new Uri(connectionString),
                azureClientService.GetCredential(),
                s_blobClientOptions)
            : new BlobServiceClient(connectionString);
    }
}