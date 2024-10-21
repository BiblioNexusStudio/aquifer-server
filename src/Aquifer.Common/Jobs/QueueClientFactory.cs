using System.Collections.Concurrent;
using Aquifer.Common.Services;
using Azure.Storage.Queues;

namespace Aquifer.Common.Jobs;

public interface IQueueClientFactory
{
    public Task<QueueClient> GetQueueClientAsync(string queueName, CancellationToken ct);
}

public sealed class QueueClientFactory(QueueConfigurationOptions _queueConfigurationOptions, IAzureClientService _azureClientService)
    : IQueueClientFactory
{
    private static readonly QueueClientOptions s_clientOptions = new() { MessageEncoding = QueueMessageEncoding.Base64 };

    private readonly ConcurrentDictionary<string, Lazy<Task<QueueClient>>> _queueClientByQueueNameMap = new();

    public async Task<QueueClient> GetQueueClientAsync(string queueName, CancellationToken ct)
    {
        // Using a thread safe Lazy<Task<T>> along with a concurrent dictionary ensures that the operation to create the client
        // and create the queue in Azure Storage (if it doesn't exist yet) only happens once.
        var queueClient = _queueClientByQueueNameMap.GetOrAdd(
            queueName,
            (qn) =>
            {
                return new Lazy<Task<QueueClient>>(() => GetQueueClientCoreAsync(qn, ct), LazyThreadSafetyMode.ExecutionAndPublication);
            });

        try
        {
            return await queueClient.Value;
        }
        catch (Exception e)
        {
            // ensure the dictionary doesn't get stuck with a faulted task
            _queueClientByQueueNameMap.TryRemove(queueName, out _);
            throw;
        }
    }

    // Note: This method creates the queue if it doesn't yet exist.
    private async Task<QueueClient> GetQueueClientCoreAsync(string queueName, CancellationToken ct)
    {
        var client = _queueConfigurationOptions.AzureQueueStorageConnectionString.StartsWith("http")
            ? new QueueClient(
                new Uri($"{_queueConfigurationOptions.AzureQueueStorageConnectionString}/{queueName}"),
                _azureClientService.GetCredential(),
                s_clientOptions)
            : new QueueClient(
                _queueConfigurationOptions.AzureQueueStorageConnectionString,
                queueName,
                s_clientOptions);

        await client.CreateIfNotExistsAsync(cancellationToken: ct);

        return client;
    }
}