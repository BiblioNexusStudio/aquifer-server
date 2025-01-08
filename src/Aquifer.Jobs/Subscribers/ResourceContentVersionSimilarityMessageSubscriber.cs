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
        var similarityScoreEntity = await GenerateResourceContentVersionSimilarityScoreEntity(message, _dbContext, ct);
        
        await _dbContext.ResourceContentVersionSimilarityScores.AddAsync(similarityScoreEntity, ct);
        await _dbContext.SaveChangesAsync(ct);
    }
    
    private async Task<ResourceContentVersionSimilarityScoreEntity> GenerateResourceContentVersionSimilarityScoreEntity(
        ScoreResourceContentVersionSimilarityMessage message,
        AquiferDbContext dbContext,
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
                    machineTranslationEntity = await GetMachineTranslationForResourceContentVersion(
                        message.BaseVersionId,
                        dbContext,
                        ct);
                    
                    similarityScoreEntity.ComparedVersionId = machineTranslationEntity.Id;
                }
                else
                {
                    similarityScoreEntity.ComparedVersionId = (int)message.CompareVersionId;
                    machineTranslationEntity = await GetMachineTranslationById(similarityScoreEntity.ComparedVersionId, dbContext, ct);
                }
                
                compareVersionText = GetMachineTranslationVersionText(machineTranslationEntity.Content);
                baseVersionText = await GetBaseVersionContentText(similarityScoreEntity.BaseVersionId, dbContext, ct);

                _logger.LogInformation(
                    "Scoring machine translation {CompareVersionId} vs published resource content version {BaseVersionId}...",
                    similarityScoreEntity.ComparedVersionId,
                    similarityScoreEntity.BaseVersionId);
                
                break;
            case ResourceContentVersionSimilarityComparisonType.MachineTranslationToSnapshot:
                similarityScoreEntity.BaseVersionType = ResourceContentVersionTypes.Snapshot;
                similarityScoreEntity.ComparedVersionType = ResourceContentVersionTypes.MachineTranslation;

                if (message.CompareVersionId is null)
                {
                    machineTranslationEntity = await GetMachineTranslationForSnapshot(
                        message.BaseVersionId,
                        dbContext,
                        ct);
                    
                    similarityScoreEntity.ComparedVersionId = machineTranslationEntity.Id;
                }
                else
                {
                    similarityScoreEntity.ComparedVersionId = (int)message.CompareVersionId;
                    machineTranslationEntity = await GetMachineTranslationById(similarityScoreEntity.ComparedVersionId, dbContext, ct);
                }

                compareVersionText = GetMachineTranslationVersionText(machineTranslationEntity.Content);
                baseVersionText = await GetSnapshotVersionText(similarityScoreEntity.BaseVersionId, dbContext, ct);
                
                _logger.LogInformation(
                    "Scoring machine translation {CompareVersionId} vs snapshot version {BaseVersionId}...",
                    similarityScoreEntity.ComparedVersionId,
                    similarityScoreEntity.BaseVersionId);

                break;
            case ResourceContentVersionSimilarityComparisonType.ResourceContentVersionToSnapshot:
                similarityScoreEntity.BaseVersionType = ResourceContentVersionTypes.Base;
                similarityScoreEntity.ComparedVersionType = ResourceContentVersionTypes.Snapshot;

                similarityScoreEntity.ComparedVersionId = message.CompareVersionId 
                                                          ?? throw new InvalidOperationException(
                                                              $"Snapshot version {message.BaseVersionId} not found.");
                
                baseVersionText = await GetBaseVersionContentText(similarityScoreEntity.BaseVersionId, dbContext, ct);
                compareVersionText = await GetSnapshotVersionText(similarityScoreEntity.ComparedVersionId, dbContext, ct);
                
                _logger.LogInformation(
                    "Scoring resource content version {BaseVersionId} vs snapshot version {CompareVersionId}...",
                    similarityScoreEntity.BaseVersionId,
                    similarityScoreEntity.ComparedVersionId);

                break;
            case ResourceContentVersionSimilarityComparisonType.SnapshotToSnapshot:
                similarityScoreEntity.BaseVersionType = ResourceContentVersionTypes.Snapshot;
                similarityScoreEntity.ComparedVersionType = ResourceContentVersionTypes.Snapshot;

                similarityScoreEntity.ComparedVersionId = message.CompareVersionId 
                                                          ?? throw new InvalidOperationException(
                                                              $"Snapshot version {message.CompareVersionId} not found.");
                
                baseVersionText = await GetSnapshotVersionText(similarityScoreEntity.BaseVersionId, dbContext, ct);
                compareVersionText = await GetSnapshotVersionText(similarityScoreEntity.ComparedVersionId, dbContext, ct);
                
                _logger.LogInformation(
                    "Scoring snapshot version {BaseVersionId} vs snapshot version {CompareVersionId}...",
                    similarityScoreEntity.BaseVersionId,
                    similarityScoreEntity.ComparedVersionId);

                break;
            default:
                throw new InvalidOperationException($"Unknown similarity comparison type: {message.ComparisonType}");
        }
        
        similarityScoreEntity.SimilarityScore = StringSimilarityUtilities.ComputeLevenshteinSimilarity(baseVersionText, compareVersionText);

        return similarityScoreEntity;
    }
    
    private static async Task<ResourceContentVersionMachineTranslationEntity> GetMachineTranslationById(
        int machineTranslationId,
        AquiferDbContext dbContext,
        CancellationToken ct)
    {
        return await dbContext
                .ResourceContentVersionMachineTranslations
                .AsTracking()
                .FirstOrDefaultAsync(m => m.Id == machineTranslationId, ct)
            ?? throw new InvalidOperationException($"Machine translation with id {machineTranslationId} not found");
    }
    
    private static async Task<ResourceContentVersionMachineTranslationEntity> GetMachineTranslationForResourceContentVersion(
        int versionId, 
        AquiferDbContext dbContext,
        CancellationToken ct)
    {
        return await dbContext
                   .ResourceContentVersionMachineTranslations
                   .AsTracking()
                   .Where(x => x.ResourceContentVersionId == versionId)
                   .OrderBy(x => x.Id)
                   .LastOrDefaultAsync(ct) 
               ?? throw new InvalidOperationException( $"No Machine translation for ResourceContentVersion with id {versionId} found");
    }
    
    private static async Task<ResourceContentVersionMachineTranslationEntity> GetMachineTranslationForSnapshot(
        int versionId, 
        AquiferDbContext dbContext,
        CancellationToken ct)
    {
        return await dbContext
                       .ResourceContentVersionMachineTranslations
                       .AsTracking()
                       .Join(
                           dbContext.ResourceContentVersionSnapshots,
                           rcvmt => rcvmt.ResourceContentVersionId,
                           rcvs => rcvs.ResourceContentVersionId,
                           (rcvmt, rcvs) => new {rcvmt, rcvs}
                       )
                       .Where(joined => joined.rcvs.Id == versionId)
                       .Select(x => x.rcvmt)
                       .OrderBy(x => x.Id)  
                       .LastOrDefaultAsync(ct) 
                   ?? throw new InvalidOperationException(
                       $"No Machine translation for ResourceContentVersionSnapshot with id {versionId} found"
                   );
    }
    
    private static async Task<string> GetBaseVersionContentText(
        int resourceContentVersionId,
        AquiferDbContext dbContext,
        CancellationToken ct)
    {
        var resource = await dbContext
                           .ResourceContentVersions
                           .AsTracking()
                           .SingleOrDefaultAsync(x => x.Id == resourceContentVersionId, ct)
                       ?? throw new InvalidOperationException($"Content version with id {resourceContentVersionId} not found");
        
        var resourceContentHtmlItems = TiptapConverter.ConvertJsonToHtmlItems(resource.Content);
        
        return HtmlUtilities.GetPlainText(
            string.Join(string.Empty, resourceContentHtmlItems)
        );
    }

    private static async Task<string> GetSnapshotVersionText(
        int snapshotVersionId,
        AquiferDbContext dbContext,
        CancellationToken ct)
    {
        var resource = await dbContext
                   .ResourceContentVersionSnapshots
                   .AsTracking()
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