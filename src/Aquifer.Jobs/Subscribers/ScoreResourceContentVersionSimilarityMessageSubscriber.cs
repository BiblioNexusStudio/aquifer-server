using Aquifer.Common.Messages;
using Aquifer.Common.Messages.Models;
using Aquifer.Common.Tiptap;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Aquifer.Jobs.Subscribers;

public sealed class ScoreResourceContentVersionSimilarityMessageSubscriber(
    AquiferDbContext _dbContext,
    ILogger<ScoreResourceContentVersionSimilarityMessageSubscriber> _logger)
{
    [Function(nameof(ScoreResourceContentVersionSimilarity))]
    public async Task ScoreResourceContentVersionSimilarity(
        [QueueTrigger(Queues.GenerateResourceContentSimilarityScore)]
        QueueMessage queueMessage,
        CancellationToken ct)
    {
        await queueMessage.ProcessAsync<ScoreResourceContentVersionSimilarityMessage, ScoreResourceContentVersionSimilarityMessageSubscriber>(
            _logger,
            nameof(ScoreResourceContentVersionSimilarity),
            ProcessAsync,
            ct);
    }

    private async Task ProcessAsync(QueueMessage queueMessage, ScoreResourceContentVersionSimilarityMessage message, CancellationToken ct)
    {
        _logger.LogInformation("Scoring machine translation...");

        switch (message.ComparisonType)
        {
            case ResourceContentVersionSimilarityComparisonTypes.MachineTranslationToResourceContentVersion:
                await ScoreMachineTranslationToResourceContentVersionSimilarityAsync(
                    message.BaseVersionId,
                    message.CompareVersionId,
                    ct);
                break;
            case ResourceContentVersionSimilarityComparisonTypes.MachineTranslationToSnapshot:
                await ScoreMachineTranslationToSnapshotSimilarityAsync(
                    message.BaseVersionId,
                    message.CompareVersionId,
                    ct);
                break;
            case ResourceContentVersionSimilarityComparisonTypes.ResourceContentVersionToSnapshot:
                await ScoreResourceContentVersionToSnapshotSimilarityAsync(
                    message.BaseVersionId,
                    message.CompareVersionId,
                    ct);
                break;
            case ResourceContentVersionSimilarityComparisonTypes.SnapshotToSnapshot:
                await ScoreSnapshotToSnapshotSimilarityAsync(
                    message.BaseVersionId,
                    message.CompareVersionId,
                    ct);
                break;
            default:
                throw new InvalidOperationException($"Unknown similarity comparison type: {message.ComparisonType}");
        }
    }

    private async Task ScoreMachineTranslationToResourceContentVersionSimilarityAsync(
        int machineId, 
        int contentVersionId,
        CancellationToken ct)
    {
        var machineContentResource = await _dbContext
            .ResourceContentVersionMachineTranslations
            .AsTracking()
            .SingleOrDefaultAsync(x => x.Id == machineId, ct) 
                ?? throw new InvalidOperationException($"Machine translation with id {machineId} not found");
        
        var machineText = HtmlUtilities.GetPlainText(machineContentResource.Content);
        
        
        var compResource = await _dbContext
            .ResourceContentVersions
            .AsTracking()
            .SingleOrDefaultAsync(x => x.Id == contentVersionId, ct)
                ?? throw new InvalidOperationException($"Content version with id {contentVersionId} not found");
        
        var compHtmlItems = TiptapConverter.ConvertJsonToHtmlItems(compResource.Content);
        var compText = HtmlUtilities.GetPlainText(
            string.Join(string.Empty, compHtmlItems)
        );
        
        var (similarity, executionTime) = StringSimilarityUtilities
            .ComputeLevenshteinSimilarity(
                machineText,
                compText
            );
        _logger.LogInformation($"Similarity: {similarity}, execution time: {executionTime}");
        
        var similarityScore = new ResourceContentVersionSimilarityScore()
        {
            SimilarityScore = similarity,
            BaseVersionId = machineId,
            BaseVersionType = ResourceContentVersionTypes.MachineTranslation,
            ComparedVersionId = contentVersionId,
            ComparedVersionType = ResourceContentVersionTypes.Base,
        };
        await _dbContext.ResourceContentVersionSimilarityScores.AddAsync(similarityScore, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    private async Task ScoreMachineTranslationToSnapshotSimilarityAsync(
        int machineId, 
        int compareSnapshotId,
        CancellationToken ct)
    {
        var machineContentResource = await _dbContext
                                         .ResourceContentVersionMachineTranslations
                                         .AsTracking()
                                         .SingleOrDefaultAsync(x => x.Id == machineId, ct) 
                                     ?? throw new InvalidOperationException($"Machine translation with id {machineId} not found");
        
        var machineText = HtmlUtilities.GetPlainText(machineContentResource.Content);
        
        
        var compSnapshotResource = await _dbContext
                                       .ResourceContentVersionSnapshots
                                       .AsTracking()
                                       .SingleOrDefaultAsync(x => x.Id == compareSnapshotId, ct)
                                   ?? throw new InvalidOperationException($"Snapshot version with id {compareSnapshotId} not found");
        
        var compHtmlItems = TiptapConverter.ConvertJsonToHtmlItems(compSnapshotResource.Content);
        var compText = HtmlUtilities.GetPlainText(
            string.Join(string.Empty, compHtmlItems)
        );
        
        var (similarity, executionTime) = StringSimilarityUtilities
            .ComputeLevenshteinSimilarity(
                machineText,
                compText
            );
        _logger.LogInformation($"Similarity: {similarity}, execution time: {executionTime}");
        
        var similarityScore = new ResourceContentVersionSimilarityScore()
        {
            SimilarityScore = similarity,
            BaseVersionId = machineId,
            BaseVersionType = ResourceContentVersionTypes.MachineTranslation,
            ComparedVersionId = compareSnapshotId,
            ComparedVersionType = ResourceContentVersionTypes.Snapshot,
        };
        await _dbContext.ResourceContentVersionSimilarityScores.AddAsync(similarityScore, ct);
        await _dbContext.SaveChangesAsync(ct);
    }
    
    private async Task ScoreResourceContentVersionToSnapshotSimilarityAsync(
        int baseContentVersionId, 
        int compareSnapshotId,
        CancellationToken ct)
    {
        var baseContentResource = await _dbContext
                                       .ResourceContentVersions
                                       .AsTracking()
                                       .SingleOrDefaultAsync(x => x.Id == baseContentVersionId, ct) 
                                  ?? throw new InvalidOperationException($"Resource Content Version with id {baseContentVersionId} not found");
        
        var baseContentHtmlItems = TiptapConverter.ConvertJsonToHtmlItems(baseContentResource.Content);
        var baseText = HtmlUtilities.GetPlainText(
            string.Join(string.Empty, baseContentHtmlItems)    
        );
        
        var compSnapshotResource = await _dbContext
                                       .ResourceContentVersionSnapshots
                                       .AsTracking()
                                       .SingleOrDefaultAsync(x => x.Id == compareSnapshotId, ct)
                                   ?? throw new InvalidOperationException($"Snapshot version with id {compareSnapshotId} not found");
        
        var compHtmlItems = TiptapConverter.ConvertJsonToHtmlItems(compSnapshotResource.Content);
        var compText = HtmlUtilities.GetPlainText(
            string.Join(string.Empty, compHtmlItems)
        );
        
        var (similarity, executionTime) = StringSimilarityUtilities
            .ComputeLevenshteinSimilarity(
                baseText,
                compText
            );
        _logger.LogInformation($"Similarity: {similarity}, execution time: {executionTime}");
        
        var similarityScore = new ResourceContentVersionSimilarityScore()
        {
            SimilarityScore = similarity,
            BaseVersionId = baseContentVersionId,
            BaseVersionType = ResourceContentVersionTypes.Base,
            ComparedVersionId = compareSnapshotId,
            ComparedVersionType = ResourceContentVersionTypes.Snapshot,
        };
        await _dbContext.ResourceContentVersionSimilarityScores.AddAsync(similarityScore, ct);
        await _dbContext.SaveChangesAsync(ct);
    }
    
    private async Task ScoreSnapshotToSnapshotSimilarityAsync(
        int baseSnapshotId, 
        int compareSnapshotId,
        CancellationToken ct)
    {
        var baseSnapshotResource = await _dbContext
                                         .ResourceContentVersionSnapshots
                                         .AsTracking()
                                         .SingleOrDefaultAsync(x => x.Id == baseSnapshotId, ct) 
                                     ?? throw new InvalidOperationException($"Snapshot with id {baseSnapshotId} not found");
        
        var baseSnapshotHtmlItems = TiptapConverter.ConvertJsonToHtmlItems(baseSnapshotResource.Content);
        var baseText = HtmlUtilities.GetPlainText(
            string.Join(string.Empty, baseSnapshotHtmlItems)    
        );
        
        var compSnapshotResource = await _dbContext
                                       .ResourceContentVersionSnapshots
                                       .AsTracking()
                                       .SingleOrDefaultAsync(x => x.Id == compareSnapshotId, ct)
                                   ?? throw new InvalidOperationException($"Snapshot version with id {compareSnapshotId} not found");
        
        var compHtmlItems = TiptapConverter.ConvertJsonToHtmlItems(compSnapshotResource.Content);
        var compText = HtmlUtilities.GetPlainText(
            string.Join(string.Empty, compHtmlItems)
        );
        
        var (similarity, executionTime) = StringSimilarityUtilities
            .ComputeLevenshteinSimilarity(
                baseText,
                compText
            );
        _logger.LogInformation($"Similarity: {similarity}, execution time: {executionTime}");
        
        var similarityScore = new ResourceContentVersionSimilarityScore()
        {
            SimilarityScore = similarity,
            BaseVersionId = baseSnapshotId,
            BaseVersionType = ResourceContentVersionTypes.Snapshot,
            ComparedVersionId = compareSnapshotId,
            ComparedVersionType = ResourceContentVersionTypes.Snapshot,
        };
        await _dbContext.ResourceContentVersionSimilarityScores.AddAsync(similarityScore, ct);
        await _dbContext.SaveChangesAsync(ct);
    }
}

