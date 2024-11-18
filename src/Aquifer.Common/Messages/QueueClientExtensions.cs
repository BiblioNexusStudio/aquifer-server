using Azure;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace Aquifer.Common.Messages;

public static class QueueClientExtensions
{
    public static readonly TimeSpan s_noExpirationTimeSpan = TimeSpan.FromSeconds(-1);

    public static async Task<Response<SendReceipt>> SendMessageAsync<T>(
        this QueueClient queueClient,
        T message,
        CancellationToken cancellationToken)
    {
        var serializedMessage = MessagesJsonSerializer.Serialize(message);
        return await queueClient.SendMessageAsync(
            serializedMessage,
            visibilityTimeout: null,
            timeToLive: s_noExpirationTimeSpan,
            cancellationToken);
    }
}