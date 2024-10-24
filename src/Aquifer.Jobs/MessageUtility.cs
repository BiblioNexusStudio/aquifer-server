using System.Text.Json;
using Aquifer.Common.Utilities;
using Azure.Storage.Queues.Models;
using Microsoft.Extensions.Logging;

namespace Aquifer.Jobs;

internal static class MessageUtility
{
    public static TDto Deserialize<TDto, TLogger>(this QueueMessage message, ILogger<TLogger> logger)
    {
        TDto? dto;
        try
        {
            dto = JsonSerializer.Deserialize<TDto>(message.MessageText, JsonUtilities.QueueMessageOptions);
        }
        catch (JsonException ex)
        {
            logger.LogError(ex, "An error occurred while deserializing JSON messageText (ID: {MessageId}): {MessageText}", message.MessageId, message.MessageText);
            throw;
        }

        if (dto == null)
        {
            logger.LogError("Message (ID: {MessageId}) unexpectedly deserialized to null: {MessageText}", message.MessageId, message.MessageText);
            throw new InvalidOperationException($"Message (ID: {message.MessageId}) unexpectedly deserialized to null.");
        }

        return dto;
    }
}
