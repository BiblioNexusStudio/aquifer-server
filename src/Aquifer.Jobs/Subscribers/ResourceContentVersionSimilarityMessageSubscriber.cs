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
        var similarityScoreEntity = await GenerateResourceContentVersionSimilarityScoreAsync(message, ct);
        
        await _dbContext.ResourceContentVersionSimilarityScores.AddAsync(similarityScoreEntity, ct);
        await _dbContext.SaveChangesAsync(ct);
    }
    
    private async Task<ResourceContentVersionSimilarityScoreEntity> GenerateResourceContentVersionSimilarityScoreAsync(
        ScoreResourceContentVersionSimilarityMessage message,
        CancellationToken ct)
    {
        ResourceContentVersionMachineTranslationEntity machineTranslationEntity;
        var similarityScoreEntity = new ResourceContentVersionSimilarityScoreEntity { BaseVersionId = message.BaseVersionId };
        string baseVersionText;
        string compareVersionText;
        
        switch (message.ComparisonType)
        {
            case ResourceContentVersionSimilarityComparisonType.MachineTranslationToResourceContentVersion:
                similarityScoreEntity.BaseVersionType = ResourceContentVersionTypes.Base;
                similarityScoreEntity.ComparedVersionType = ResourceContentVersionTypes.MachineTranslation;

                if (message.CompareVersionId is null)
                {
                    machineTranslationEntity = await GetMachineTranslationForResourceContentVersion(message.BaseVersionId, ct);
                    similarityScoreEntity.ComparedVersionId = machineTranslationEntity.Id;
                }
                else
                {
                    similarityScoreEntity.ComparedVersionId = message.CompareVersionId.Value;
                    machineTranslationEntity = await GetMachineTranslationById(similarityScoreEntity.ComparedVersionId, ct);
                }
                
                compareVersionText = GetMachineTranslationVersionText(machineTranslationEntity.Content);
                baseVersionText = await GetBaseVersionContentText(similarityScoreEntity.BaseVersionId, ct);

                _logger.LogInformation(
                    "Scoring machine translation {MachineTranslationId} vs published resource content version {ResourceContentVersionId}...",
                    similarityScoreEntity.ComparedVersionId,
                    similarityScoreEntity.BaseVersionId);
                
                break;
            case ResourceContentVersionSimilarityComparisonType.MachineTranslationToSnapshot:
                similarityScoreEntity.BaseVersionType = ResourceContentVersionTypes.Snapshot;
                similarityScoreEntity.ComparedVersionType = ResourceContentVersionTypes.MachineTranslation;

                if (message.CompareVersionId is null)
                {
                    machineTranslationEntity = await GetMachineTranslationForSnapshot(message.BaseVersionId, ct);
                    similarityScoreEntity.ComparedVersionId = machineTranslationEntity.Id;
                }
                else
                {
                    similarityScoreEntity.ComparedVersionId = message.CompareVersionId.Value;
                    machineTranslationEntity = await GetMachineTranslationById(similarityScoreEntity.ComparedVersionId, ct);
                }

                compareVersionText = GetMachineTranslationVersionText(machineTranslationEntity.Content);
                baseVersionText = await GetSnapshotVersionText(similarityScoreEntity.BaseVersionId, ct);
                
                _logger.LogInformation(
                    "Scoring machine translation {MachineTranslationId} vs snapshot version {SnapShotVersionId}...",
                    similarityScoreEntity.ComparedVersionId,
                    similarityScoreEntity.BaseVersionId);

                break;
            case ResourceContentVersionSimilarityComparisonType.ResourceContentVersionToSnapshot:
                similarityScoreEntity.BaseVersionType = ResourceContentVersionTypes.Base;
                similarityScoreEntity.ComparedVersionType = ResourceContentVersionTypes.Snapshot;

                similarityScoreEntity.ComparedVersionId = message.CompareVersionId 
                                                          ?? throw new InvalidOperationException(
                                                              $"Snapshot version {message.BaseVersionId} not found.");
                
                baseVersionText = await GetBaseVersionContentText(similarityScoreEntity.BaseVersionId, ct);
                compareVersionText = await GetSnapshotVersionText(similarityScoreEntity.ComparedVersionId, ct);
                
                _logger.LogInformation(
                    "Scoring resource content version {ResourceContentVersionId} vs snapshot version {SnapShotVersionId}...",
                    similarityScoreEntity.BaseVersionId,
                    similarityScoreEntity.ComparedVersionId);

                break;
            case ResourceContentVersionSimilarityComparisonType.SnapshotToSnapshot:
                similarityScoreEntity.BaseVersionType = ResourceContentVersionTypes.Snapshot;
                similarityScoreEntity.ComparedVersionType = ResourceContentVersionTypes.Snapshot;

                similarityScoreEntity.ComparedVersionId = message.CompareVersionId 
                                                            ?? throw new InvalidOperationException(
                                                                $"{nameof(message.CompareVersionId)} (snapshot ID) is required for {nameof(
                                                                    ResourceContentVersionSimilarityComparisonType.SnapshotToSnapshot
                                                                    )} similarity scoring.");
                
                baseVersionText = await GetSnapshotVersionText(similarityScoreEntity.BaseVersionId, ct);
                compareVersionText = await GetSnapshotVersionText(similarityScoreEntity.ComparedVersionId, ct);
                
                _logger.LogInformation(
                    "Scoring snapshot version {BaseSnapShotVersionId} vs snapshot version {ComparedSnapShotVersionId}...",
                    similarityScoreEntity.BaseVersionId,
                    similarityScoreEntity.ComparedVersionId);

                break;
            default:
                throw new InvalidOperationException($"Unknown similarity comparison type: {message.ComparisonType}");
        }
        
        similarityScoreEntity.SimilarityScore = StringSimilarityUtilities.ComputeLevenshteinSimilarity(baseVersionText, compareVersionText);

        return similarityScoreEntity;
    }
    
    private async Task<ResourceContentVersionMachineTranslationEntity> GetMachineTranslationById(
        int machineTranslationId,
        CancellationToken ct)
    {
        return await _dbContext
                .ResourceContentVersionMachineTranslations
                .SingleOrDefaultAsync(m => m.Id == machineTranslationId, ct)
            ?? throw new InvalidOperationException($"Machine translation with id {machineTranslationId} not found");
    }
    
    private async Task<ResourceContentVersionMachineTranslationEntity> GetMachineTranslationForResourceContentVersion(
        int resourceContentVersionId, 
        CancellationToken ct)
    {
        return await _dbContext
                   .ResourceContentVersionMachineTranslations
                   .Where(x => x.ResourceContentVersionId == resourceContentVersionId)
                   .OrderBy(x => x.Id)
                   .LastOrDefaultAsync(ct) 
               ?? throw new InvalidOperationException($"No Machine translation for ResourceContentVersion with id {resourceContentVersionId} found");
    }
    
    private async Task<ResourceContentVersionMachineTranslationEntity> GetMachineTranslationForSnapshot(
        int resourceContentVersionId, 
        CancellationToken ct)
    {
        return await _dbContext
                       .ResourceContentVersionMachineTranslations
     
                       .OrderBy(x => x.Id)  
                       .LastOrDefaultAsync(ct) 
                   ?? throw new InvalidOperationException(
                       $"No Machine translation for ResourceContentVersionSnapshot with id {resourceContentVersionId} found");
    }
    
    private async Task<string> GetBaseVersionContentText(int resourceContentVersionId, CancellationToken ct)
    {
        var resource = await _dbContext
                           .ResourceContentVersions
                           .SingleOrDefaultAsync(x => x.Id == resourceContentVersionId, ct)
                       ?? throw new InvalidOperationException($"Content version with id {resourceContentVersionId} not found");
        
        var resourceContentHtmlItems = TiptapConverter.ConvertJsonToHtmlItems(resource.Content);
        
        return HtmlUtilities.GetPlainText(
            string.Join(string.Empty, resourceContentHtmlItems)
        );
    }

    private async Task<string> GetSnapshotVersionText(int snapshotVersionId, CancellationToken ct)
    {
        var resource = await _dbContext
                   .ResourceContentVersionSnapshots
                   .SingleOrDefaultAsync(x => x.Id == snapshotVersionId, ct)
               ?? throw new InvalidOperationException($"Snapshot version with id {snapshotVersionId} not found");
        
        var contentHtmlItems = TiptapConverter.ConvertJsonToHtmlItems(resource.Content);
        
        return HtmlUtilities.GetPlainText(
            string.Join(string.Empty, contentHtmlItems)
        );
    }
    
    private static string GetMachineTranslationVersionText(string resourceHtmlStringContent)
    {
        return HtmlUtilities.GetPlainText(resourceHtmlStringContent);
    }
}