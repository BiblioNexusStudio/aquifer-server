using Aquifer.Common.Configuration;
using Azure.Core;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Aquifer.Common.Services;

public interface IBlobStorageService
{
    Task DeleteFileAsync(string containerName, string blobName, CancellationToken ct);
    Task DownloadFileAsync(string containerName, string blobName, string filePath, CancellationToken ct);
    Task DownloadStreamAsync(string containerName, string blobName, Stream stream, CancellationToken ct);
    Task UploadFileAsync(string containerName, string blobName, string filePath, CancellationToken ct);
    Task UploadFilesInParallelAsync(string containerName, IEnumerable<(string BlobName, string FilePath)> files, CancellationToken ct);
    Task UploadStreamAsync(string containerName, string blobName, Stream fileStream, CancellationToken ct);
}

public sealed class BlobStorageService(AzureStorageAccountOptions _azureStorageAccountOptions, IAzureClientService _azureClientService)
    : IBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient = GetBlobServiceClient(
        _azureStorageAccountOptions.BlobEndpoint,
        _azureStorageAccountOptions.ConnectionStringOverride,
        _azureClientService);

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

    public async Task DeleteFileAsync(string containerName, string blobName, CancellationToken ct)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(blobName);

        await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots, cancellationToken: ct);
    }

    public async Task DownloadFileAsync(string containerName, string blobName, string filePath, CancellationToken ct)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(blobName);

        await blobClient.DownloadToAsync(filePath, cancellationToken: ct);
    }

    public async Task DownloadStreamAsync(string containerName, string blobName, Stream stream, CancellationToken ct)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(blobName);

        await blobClient.DownloadToAsync(stream, cancellationToken: ct);
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

    public async Task UploadStreamAsync(string containerName, string blobName, Stream fileStream, CancellationToken ct)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(blobName);

        await blobClient.UploadAsync(fileStream, overwrite: true, ct);
    }

    private static async Task UploadFileCoreAsync(BlobClient blobClient, string filePath, CancellationToken cancellationToken)
    {
        await using var fileStream = File.OpenRead(filePath);
        await blobClient.UploadAsync(fileStream, overwrite: true, cancellationToken);
    }

    private static BlobServiceClient GetBlobServiceClient(
        Uri? blobEndpoint,
        string? connectionStringOverride,
        IAzureClientService azureClientService)
    {
        return string.IsNullOrEmpty(connectionStringOverride)
            ? new BlobServiceClient(
                blobEndpoint
                    ?? throw new InvalidOperationException(
                        $"The \"{nameof(AzureStorageAccountOptions.BlobEndpoint)}\" setting must be provided when \"{nameof(AzureStorageAccountOptions.ConnectionStringOverride)}\" is empty."),
                azureClientService.GetCredential(),
                s_blobClientOptions)
            : new BlobServiceClient(
                connectionStringOverride,
                s_blobClientOptions);
    }
}