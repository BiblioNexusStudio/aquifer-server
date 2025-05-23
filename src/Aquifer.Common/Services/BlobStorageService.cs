﻿using Aquifer.Common.Configuration;
using Azure;
using Azure.Core;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;

namespace Aquifer.Common.Services;

public interface IBlobStorageService
{
    Task DeleteFileAsync(string containerName, string blobName, CancellationToken ct);
    Task DownloadFileAsync(string containerName, string blobName, string filePath, CancellationToken ct);
    Task DownloadStreamAsync(string containerName, string blobName, Stream stream, CancellationToken ct);
    Task UploadFileAsync(string containerName, string blobName, string filePath, bool overwrite, CancellationToken ct);

    Task UploadFilesInParallelAsync(
        string containerName,
        IEnumerable<(string BlobName, string FilePath)> files,
        bool overwrite,
        CancellationToken ct);

    Task UploadStreamAsync(string containerName, string blobName, Stream fileStream, CancellationToken ct);
}

public sealed class BlobStorageService(
    AzureStorageAccountOptions _azureStorageAccountOptions,
    IAzureClientService _azureClientService,
    ILogger<BlobStorageService> _logger)
    : IBlobStorageService
{
    private static readonly BlobClientOptions s_blobClientOptions = new()
    {
        Retry =
        {
            Delay = TimeSpan.FromMilliseconds(50),
            MaxRetries = 5,
            Mode = RetryMode.Exponential,
            MaxDelay = TimeSpan.FromSeconds(10),
            NetworkTimeout = TimeSpan.FromSeconds(100),
        },
    };

    private readonly BlobServiceClient _blobServiceClient = GetBlobServiceClient(
        _azureStorageAccountOptions.BlobEndpoint,
        _azureStorageAccountOptions.ConnectionStringOverride,
        _azureClientService);

    public async Task DeleteFileAsync(string containerName, string blobName, CancellationToken ct)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(blobName);

        try
        {
            await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots, cancellationToken: ct);
        }
        catch (RequestFailedException rfe)
        {
            _logger.LogError(
                rfe,
                "Failed to delete blob \"{BlobName}\" from container \"{ContainerName}\" in storage account \"{StorageAccountName}\".",
                blobName,
                containerName,
                blobClient.AccountName);
            throw;
        }
    }

    public async Task DownloadFileAsync(string containerName, string blobName, string filePath, CancellationToken ct)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(blobName);

        try
        {
            await blobClient.DownloadToAsync(filePath, ct);
        }
        catch (RequestFailedException rfe)
        {
            _logger.LogError(
                rfe,
                "Failed to download blob \"{BlobName}\" from container \"{ContainerName}\" in storage account \"{StorageAccountName}\" to file \"{FilePath}\".",
                blobName,
                containerName,
                blobClient.AccountName,
                filePath);
            throw;
        }
    }

    public async Task DownloadStreamAsync(string containerName, string blobName, Stream stream, CancellationToken ct)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(blobName);

        try
        {
            await blobClient.DownloadToAsync(stream, ct);
        }
        catch (RequestFailedException rfe)
        {
            _logger.LogError(
                rfe,
                "Failed to download blob \"{BlobName}\" from container \"{ContainerName}\" in storage account \"{StorageAccountName}\" to stream.",
                blobName,
                containerName,
                blobClient.AccountName);
            throw;
        }
    }

    public async Task UploadFileAsync(string containerName, string blobName, string filePath, bool overwrite, CancellationToken ct)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(blobName);

        await UploadFileCoreAsync(blobClient, filePath, overwrite, ct);
    }

    public async Task UploadFilesInParallelAsync(
        string containerName,
        IEnumerable<(string BlobName, string FilePath)> files,
        bool overwrite,
        CancellationToken ct)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        await Task.WhenAll(
            files.Select(f =>
                UploadFileCoreAsync(containerClient.GetBlobClient(f.BlobName), f.FilePath, overwrite, ct)));
    }

    public async Task UploadStreamAsync(string containerName, string blobName, Stream fileStream, CancellationToken ct)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(blobName);

        try
        {
            await blobClient.UploadAsync(fileStream, true, ct);
        }
        catch (RequestFailedException rfe)
        {
            _logger.LogError(
                rfe,
                "Failed to upload stream to blob \"{BlobName}\" in container \"{ContainerName}\" in storage account \"{StorageAccountName}\".",
                blobName,
                containerName,
                blobClient.AccountName);
            throw;
        }
    }

    private async Task UploadFileCoreAsync(
        BlobClient blobClient,
        string filePath,
        bool overwrite,
        CancellationToken cancellationToken)
    {
        await using var fileStream = File.OpenRead(filePath);
        try
        {
            await blobClient.UploadAsync(fileStream, overwrite, cancellationToken);
        }
        catch (RequestFailedException rfe)
        {
            _logger.LogError(
                rfe,
                "Failed to upload file \"{FilePath}\" to blob \"{BlobName}\" in container \"{ContainerName}\" in storage account \"{StorageAccountName}\".",
                filePath,
                blobClient.Name,
                blobClient.BlobContainerName,
                blobClient.AccountName);
            throw;
        }
    }

    private static BlobServiceClient GetBlobServiceClient(
        Uri? blobEndpoint,
        string? connectionStringOverride,
        IAzureClientService azureClientService)
    {
        return string.IsNullOrEmpty(connectionStringOverride)
            ? new BlobServiceClient(
                blobEndpoint ??
                throw new InvalidOperationException(
                    $"The \"{nameof(AzureStorageAccountOptions.BlobEndpoint)}\" setting must be provided when \"{nameof(AzureStorageAccountOptions.ConnectionStringOverride)}\" is empty."),
                azureClientService.GetCredential(),
                s_blobClientOptions)
            : new BlobServiceClient(
                connectionStringOverride,
                s_blobClientOptions);
    }
}