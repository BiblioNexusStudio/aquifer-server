using System.Text.Json;
using Azure;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace Aquifer.Common.Jobs;

public static class QueueClientExtensions
{
    public static async Task<Response<SendReceipt>> SendMessageAsync<T>(
        this QueueClient queueClient,
        T message,
        TimeSpan? visibilityTimeout = null,
        TimeSpan? timeToLive = null,
        CancellationToken cancellationToken = default)
    {
        var serializedMessage = JsonSerializer.Serialize(message);
        return await queueClient.SendMessageAsync(serializedMessage, visibilityTimeout, timeToLive, cancellationToken);
    }
}