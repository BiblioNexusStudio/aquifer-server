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

public sealed class ResourceContentVersionSimilarityMessageSubscriber(
    AquiferDbContext _dbContext,
    ILogger<ResourceContentVersionSimilarityMessageSubscriber> _logger)
{
    [Function(nameof(ScoreResourceContentVersionSimilarityMessageSubscriber))]
    public async Task ScoreResourceContentVersionSimilarityMessageSubscriber(
        [QueueTrigger(Queues.GenerateResourceContentSimilarityScore)]
        QueueMessage queueMessage,
        CancellationToken ct)
    {
        await queueMessage.ProcessAsync<ScoreResourceContentVersionSimilarityMessage, ResourceContentVersionSimilarityMessageSubscriber>(
            _logger,
            nameof(ScoreResourceContentVersionSimilarityMessageSubscriber),
            ProcessAsync,
            ct);
    }

    private async Task ProcessAsync(QueueMessage queueMessage, ScoreResourceContentVersionSimilarityMessage message, CancellationToken ct)
    {
        switch (message.ComparisonType)
        {
            case ResourceContentVersionSimilarityComparisonTypes.MachineTranslationToResourceContentVersion:
                _logger.LogInformation(
                    "Scoring machine translation {BaseVersionId} vs published resource content version {CompareVersionId}...",
                    message.BaseVersionId,
                    message.CompareVersionId);

                await ScoreMachineTranslationToResourceContentVersionSimilarityAsync(
                    message.BaseVersionId,
                    message.CompareVersionId,
                    ct);
                break;
            case ResourceContentVersionSimilarityComparisonTypes.MachineTranslationToSnapshot:
                _logger.LogInformation(
                    "Scoring resource content version {BaseVersionId} vs snapshot version {CompareVersionId}...",
                    message.BaseVersionId,
                    message.CompareVersionId);

                await ScoreMachineTranslationToSnapshotSimilarityAsync(
                    message.BaseVersionId,
                    message.CompareVersionId,
                    ct);
                break;
            case ResourceContentVersionSimilarityComparisonTypes.ResourceContentVersionToSnapshot:
                _logger.LogInformation(
                    "Scoring resource content version {BaseVersionId} vs snapshot version {CompareVersionId}...",
                    message.BaseVersionId,
                    message.CompareVersionId);

                await ScoreResourceContentVersionToSnapshotSimilarityAsync(
                    message.BaseVersionId,
                    message.CompareVersionId,
                    ct);
                break;
            case ResourceContentVersionSimilarityComparisonTypes.SnapshotToSnapshot:
                _logger.LogInformation(
                    "Scoring snapshot version {BaseVersionId} vs snapshot version {CompareVersionId}...",
                    message.BaseVersionId,
                    message.CompareVersionId);

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
        
        var similarity = StringSimilarityUtilities
            .ComputeLevenshteinSimilarity(
                machineText,
                compText
            );
        
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
        
        var similarity = StringSimilarityUtilities
            .ComputeLevenshteinSimilarity(
                machineText,
                compText
            );
        
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
        
        var similarity = StringSimilarityUtilities
            .ComputeLevenshteinSimilarity(
                baseText,
                compText
            );
        
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
        
        var similarity = StringSimilarityUtilities
            .ComputeLevenshteinSimilarity(
                baseText,
                compText
            );
        
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

