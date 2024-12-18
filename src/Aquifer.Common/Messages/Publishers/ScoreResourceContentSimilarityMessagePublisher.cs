using Aquifer.Common.Messages.Models;

namespace Aquifer.Common.Messages.Publishers;

public interface IScoreResourceContentVersionSimilarityMessagePublisher
{
    Task PublishScoreResourceContentVersionSimilarityMessageAsync(ScoreResourceContentVersionSimilarityMessage message, CancellationToken ct);
}

public sealed class ScoreResourceContentVersionSimilarityMessagePublisher(
    IQueueClientFactory _queueClientFactory) : IScoreResourceContentVersionSimilarityMessagePublisher
{
    public async Task PublishScoreResourceContentVersionSimilarityMessageAsync(ScoreResourceContentVersionSimilarityMessage message,
        CancellationToken ct)
    {
        var queueClient = await _queueClientFactory.GetQueueClientAsync(Queues.GenerateResourceContentSimilarityScore, ct);
        await queueClient.SendMessageAsync(message, cancellationToken: ct);
    }
}