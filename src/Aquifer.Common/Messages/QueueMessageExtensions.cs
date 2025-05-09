using System.Text.Json;
using Azure.Storage.Queues.Models;
using Microsoft.Extensions.Logging;

namespace Aquifer.Common.Messages;

public static class QueueMessageExtensions
{
    public static TMessage Deserialize<TMessage, TLogger>(this QueueMessage queueMessage, ILogger<TLogger> logger)
    {
        TMessage? message;
        try
        {
            message = MessagesJsonSerializer.Deserialize<TMessage>(queueMessage.MessageText);
        }
        catch (JsonException ex)
        {
            logger.LogError(
                ex,
                "An error occurred while deserializing JSON message text (ID: {MessageId}): {MessageText}",
                queueMessage.MessageId,
                queueMessage.MessageText);
            throw;
        }

        if (message == null)
        {
            logger.LogError(
                "Message (ID: {MessageId}) unexpectedly deserialized to null: {MessageText}",
                queueMessage.MessageId,
                queueMessage.MessageText);
            throw new InvalidOperationException($"Message (ID: {queueMessage.MessageId}) unexpectedly deserialized to null.");
        }

        return message;
    }

    public static async Task ProcessAsync<TMessage, TLogger>(
        this QueueMessage queueMessage,
        ILogger<TLogger> logger,
        string functionName,
        Func<QueueMessage, TMessage, CancellationToken, Task> processAsync,
        CancellationToken cancellationToken)
    {
        try
        {
            var message = Deserialize<TMessage, TLogger>(queueMessage, logger);
            await processAsync(queueMessage, message, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "An error occurred during queue processing in {FunctionName}. Message ID: {MessageId}; Retry Count: {RetryCount}; Message Text: {MessageText}",
                functionName,
                queueMessage.MessageId,
                queueMessage.DequeueCount,
                queueMessage.MessageText);
            throw;
        }
    }
}