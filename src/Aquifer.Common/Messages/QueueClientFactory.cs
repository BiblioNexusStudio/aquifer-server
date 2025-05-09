using System.Collections.Concurrent;
using Aquifer.Common.Configuration;
using Aquifer.Common.Services;
using Azure.Core;
using Azure.Storage.Queues;

namespace Aquifer.Common.Messages;

public interface IQueueClientFactory
{
    Task<QueueClient> GetQueueClientAsync(string queueName, CancellationToken ct);
}

public sealed class QueueClientFactory(AzureStorageAccountOptions _azureStorageAccountOptions, IAzureClientService _azureClientService)
    : IQueueClientFactory
{
    private static readonly QueueClientOptions s_clientOptions = new()
    {
        MessageEncoding = QueueMessageEncoding.Base64,
        Retry =
        {
            Delay = TimeSpan.FromMilliseconds(50),
            MaxRetries = 5,
            Mode = RetryMode.Exponential,
            MaxDelay = TimeSpan.FromSeconds(10),
            NetworkTimeout = TimeSpan.FromSeconds(100),
        },
    };

    private readonly ConcurrentDictionary<string, Lazy<Task<QueueClient>>> _queueClientByQueueNameMap = new();

    public async Task<QueueClient> GetQueueClientAsync(string queueName, CancellationToken ct)
    {
        // Using a thread safe Lazy<Task<T>> along with a concurrent dictionary ensures that the operation to create the client
        // and create the queue in Azure Storage (if it doesn't exist yet) only happens once.
        var queueClient = _queueClientByQueueNameMap.GetOrAdd(
            queueName,
#pragma warning disable VSTHRD011 // Lazy<Task<T>> is fine here because we don't have a UI thread.
            qn => new Lazy<Task<QueueClient>>(() => GetQueueClientCoreAsync(qn, ct), LazyThreadSafetyMode.ExecutionAndPublication));
#pragma warning restore VSTHRD011

        try
        {
            return await queueClient.Value;
        }
        catch (Exception)
        {
            // ensure the dictionary doesn't get stuck with a faulted task
            _queueClientByQueueNameMap.TryRemove(queueName, out _);
            throw;
        }
    }

    // Note: This method creates the queue if it doesn't yet exist.
    private async Task<QueueClient> GetQueueClientCoreAsync(string queueName, CancellationToken ct)
    {
        var client = string.IsNullOrEmpty(_azureStorageAccountOptions.ConnectionStringOverride)
            ? new QueueClient(
                new Uri(
                    _azureStorageAccountOptions.QueueEndpoint ??
                    throw new InvalidOperationException(
                        $"The \"{nameof(AzureStorageAccountOptions.QueueEndpoint)}\" setting must be provided when \"{nameof(AzureStorageAccountOptions.ConnectionStringOverride)}\" is empty."),
                    queueName),
                _azureClientService.GetCredential(),
                s_clientOptions)
            : new QueueClient(
                _azureStorageAccountOptions.ConnectionStringOverride,
                queueName,
                s_clientOptions);

        await client.CreateIfNotExistsAsync(cancellationToken: ct);

        return client;
    }
}