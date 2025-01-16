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
    private const string ScoreResourceContentVersionSimilarityMessageSubscriberFunctionName = "ScoreResourceContentVersionSimilarityMessageSubscriber";
    
    [Function(ScoreResourceContentVersionSimilarityMessageSubscriberFunctionName)]
    public async Task RunAsync(
        [QueueTrigger(Queues.GenerateResourceContentSimilarityScore)]
        QueueMessage queueMessage,
        CancellationToken ct)
    {
        await queueMessage.ProcessAsync<ScoreResourceContentVersionSimilarityMessage, ResourceContentVersionSimilarityMessageSubscriber>(
            _logger,
            ScoreResourceContentVersionSimilarityMessageSubscriberFunctionName,
            ProcessAsync,
            ct);
    }

    private async Task ProcessAsync(QueueMessage queueMessage, ScoreResourceContentVersionSimilarityMessage message, CancellationToken ct)
    {
        var similarityScoreEntity = await GenerateResourceContentVersionSimilarityScoreAsync(message, ct);

        if (similarityScoreEntity is null)
        {
            return;
        }

        await _dbContext.ResourceContentVersionSimilarityScores.AddAsync(similarityScoreEntity, ct);
        await _dbContext.SaveChangesAsync(ct);
    }
    
    private async Task<ResourceContentVersionSimilarityScoreEntity?> GenerateResourceContentVersionSimilarityScoreAsync(
        ScoreResourceContentVersionSimilarityMessage message,
        CancellationToken ct)
    {
        var similarityScoreEntity = new ResourceContentVersionSimilarityScoreEntity { BaseVersionId = message.BaseVersionId };
        string baseVersionText;
        string compareVersionText;
        IReadOnlyList<string> resourceContentHtmlItems;
        IReadOnlyList<string> rcvSnapshotHtmlItems;
        ResourceContentVersionEntity resourceContentVersion;
        ResourceContentVersionSnapshotEntity rcvSnapshot;
        
        switch (message.ComparisonType)
        {
            case ResourceContentVersionSimilarityComparisonType.MachineTranslationToResourceContentVersion:
                similarityScoreEntity.BaseVersionId = message.BaseVersionId;
                similarityScoreEntity.BaseVersionType = ResourceContentVersionTypes.Base;
                similarityScoreEntity.ComparedVersionType = ResourceContentVersionTypes.MachineTranslation;
                
                resourceContentVersion = await GetBaseResourceContentVersionAsync(similarityScoreEntity.BaseVersionId, ct);
                resourceContentHtmlItems = TiptapConverter.ConvertJsonToHtmlItems(resourceContentVersion.Content);
        
                baseVersionText = HtmlUtilities.GetPlainText(string.Join(string.Empty, resourceContentHtmlItems));
                
                (similarityScoreEntity.ComparedVersionId, compareVersionText) = await GetMachineTranslationIdAndTextForResourceContentVersionAsync(
                    similarityScoreEntity.BaseVersionId,
                    message.CompareVersionId,
                    ct);

                if (similarityScoreEntity.ComparedVersionId == -1)
                {
                    return null;
                }

                _logger.LogInformation(
                    "Scoring machine translation {MachineTranslationId} vs published resource content version {ResourceContentVersionId}...",
                    similarityScoreEntity.ComparedVersionId,
                    similarityScoreEntity.BaseVersionId);
                
                break;
            case ResourceContentVersionSimilarityComparisonType.MachineTranslationToSnapshot:
                similarityScoreEntity.BaseVersionId = message.BaseVersionId;
                similarityScoreEntity.BaseVersionType = ResourceContentVersionTypes.Snapshot;
                similarityScoreEntity.ComparedVersionType = ResourceContentVersionTypes.MachineTranslation;
                
                rcvSnapshot = await GetSnapshotVersionAsync(similarityScoreEntity.BaseVersionId, ct);
                rcvSnapshotHtmlItems = TiptapConverter.ConvertJsonToHtmlItems(rcvSnapshot.Content);
                
                baseVersionText = HtmlUtilities.GetPlainText(string.Join(string.Empty, rcvSnapshotHtmlItems));

                (similarityScoreEntity.ComparedVersionId, compareVersionText) = await GetMachineTranslationTextAndIdForSnapshotAsync(
                    similarityScoreEntity.BaseVersionId,
                    message.CompareVersionId,
                    ct);

                if (similarityScoreEntity.ComparedVersionId == -1)
                {
                    return null;
                }
                
                _logger.LogInformation(
                    "Scoring machine translation {MachineTranslationId} vs snapshot version {SnapShotVersionId}...",
                    similarityScoreEntity.ComparedVersionId,
                    similarityScoreEntity.BaseVersionId);

                break;
            case ResourceContentVersionSimilarityComparisonType.ResourceContentVersionToSnapshot:
                similarityScoreEntity.BaseVersionId = message.BaseVersionId;
                similarityScoreEntity.BaseVersionType = ResourceContentVersionTypes.Base;
                similarityScoreEntity.ComparedVersionType = ResourceContentVersionTypes.Snapshot;

                similarityScoreEntity.ComparedVersionId = message.CompareVersionId 
                                                          ?? throw new InvalidOperationException(
                                                              $"{nameof(message.CompareVersionId)} (snapshot ID) is required for {nameof(
                                                                  ResourceContentVersionSimilarityComparisonType.ResourceContentVersionToSnapshot
                                                              )} similarity scoring.");
                
                resourceContentVersion = await GetBaseResourceContentVersionAsync(similarityScoreEntity.BaseVersionId, ct);
                resourceContentHtmlItems = TiptapConverter.ConvertJsonToHtmlItems(resourceContentVersion.Content);
        
                baseVersionText = HtmlUtilities.GetPlainText(string.Join(string.Empty, resourceContentHtmlItems));
                
                rcvSnapshot = await GetSnapshotVersionAsync(similarityScoreEntity.ComparedVersionId, ct);
                rcvSnapshotHtmlItems = TiptapConverter.ConvertJsonToHtmlItems(rcvSnapshot.Content);
        
                compareVersionText = HtmlUtilities.GetPlainText(string.Join(string.Empty, rcvSnapshotHtmlItems));
                
                _logger.LogInformation(
                    "Scoring resource content version {ResourceContentVersionId} vs snapshot version {SnapShotVersionId}...",
                    similarityScoreEntity.BaseVersionId,
                    similarityScoreEntity.ComparedVersionId);

                break;
            case ResourceContentVersionSimilarityComparisonType.SnapshotToSnapshot:
                similarityScoreEntity.BaseVersionId = message.BaseVersionId;
                similarityScoreEntity.BaseVersionType = ResourceContentVersionTypes.Snapshot;
                similarityScoreEntity.ComparedVersionType = ResourceContentVersionTypes.Snapshot;

                similarityScoreEntity.ComparedVersionId = message.CompareVersionId 
                                                            ?? throw new InvalidOperationException(
                                                                $"{nameof(message.CompareVersionId)} (snapshot ID) is required for {nameof(
                                                                    ResourceContentVersionSimilarityComparisonType.SnapshotToSnapshot
                                                                    )} similarity scoring.");
                
                rcvSnapshot = await GetSnapshotVersionAsync(similarityScoreEntity.BaseVersionId, ct);
                rcvSnapshotHtmlItems = TiptapConverter.ConvertJsonToHtmlItems(rcvSnapshot.Content);
                
                baseVersionText = HtmlUtilities.GetPlainText(string.Join(string.Empty, rcvSnapshotHtmlItems));
                
                var compareSnapshot = await GetSnapshotVersionAsync(similarityScoreEntity.ComparedVersionId, ct);
                var compareSnapshotHtmlItems = TiptapConverter.ConvertJsonToHtmlItems(compareSnapshot.Content);
                
                compareVersionText = HtmlUtilities.GetPlainText(string.Join(string.Empty, compareSnapshotHtmlItems));
                
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
    
    private async Task<ResourceContentVersionEntity> GetBaseResourceContentVersionAsync(int resourceContentVersionId, CancellationToken ct)
    {
        return await _dbContext
                   .ResourceContentVersions
                   .SingleOrDefaultAsync(x => x.Id == resourceContentVersionId, ct)
               ?? throw new InvalidOperationException($"Resource Content version with id {resourceContentVersionId} not found");
    }

    private async Task<ResourceContentVersionSnapshotEntity> GetSnapshotVersionAsync(int snapshotVersionId, CancellationToken ct)
    {
        return await _dbContext
                   .ResourceContentVersionSnapshots
                   .SingleOrDefaultAsync(x => x.Id == snapshotVersionId, ct)
               ?? throw new InvalidOperationException($"Snapshot version with id {snapshotVersionId} not found");
    }
    
    private async Task<(int, string)> GetMachineTranslationIdAndTextForResourceContentVersionAsync(
        int resourceContentVersionId,
        int? machineTranslationId,
        CancellationToken ct)
    {
        var machineTranslations = await _dbContext
                                        .ResourceContentVersionMachineTranslations
                                        .Where(x => x.ResourceContentVersionId == resourceContentVersionId)
                                        .OrderBy(x => x.Id)
                                        .ToListAsync(ct);

        if (machineTranslations.Count == 0)
        {
            _logger.LogWarning(
                "No Machine translation for ResourceContentVersion with id {ResourceContentVersionId} found. Skipping similarity score generation.",
                resourceContentVersionId);
            
            return (-1, string.Empty);
        }

        if (machineTranslationId is not null && !machineTranslations.Any(m => m.Id != machineTranslationId))
        {
            throw new InvalidOperationException(
                $"Machine translation with id {machineTranslationId} not found for resource content version {resourceContentVersionId}");
        }

        return GetMachineTranslationIdAndText(machineTranslations, machineTranslationId);
    }
    
    private async Task<(int, string)> GetMachineTranslationTextAndIdForSnapshotAsync(
        int resourceContentVersionSnapshotId,
        int? machineTranslationId,
        CancellationToken ct)
    {
        var machineTranslations = await _dbContext
                                            .ResourceContentVersionMachineTranslations
                                            .Where(x => x.ResourceContentVersion.ResourceContentVersionSnapshots
                                                .Any(s => s.Id == resourceContentVersionSnapshotId))
                                            .OrderBy(x => x.Id)  
                                            .ToListAsync(ct);

        if (machineTranslations.Count == 0)
        {
            _logger.LogWarning(
                "No Machine translation for Snapshot with id {ResourceContentVersionSnapshotId} found. Skipping similarity score generation.",
                resourceContentVersionSnapshotId);
            
            return (-1, string.Empty);
        }

        if (machineTranslationId is not null && !machineTranslations.Any(m => m.Id != machineTranslationId))
        {
            throw new InvalidOperationException(
                $"Machine translation with id {machineTranslationId} not found for snapshot version {resourceContentVersionSnapshotId}");
        }
        
        return GetMachineTranslationIdAndText(machineTranslations, machineTranslationId);
    }

    private (int, string) GetMachineTranslationIdAndText(
        List<ResourceContentVersionMachineTranslationEntity> machineTranslations,
        int? machineTranslationId)
    {
        List<string> contentList;
        string machineTranslationText;
        
        if (machineTranslations.Count == 1)
        {
            return (
                machineTranslations.First().Id, 
                HtmlUtilities.GetPlainText(machineTranslations.First().Content));
        }

        // We want to ensure that we get the correct set of MTs, whether it is 1 or many ( as in the case of FIA content )
        // So, we determine the reference MT. This can be the specified MT or the latest MT with a ContentIndex of 0.
        var referenceMachineTranslation = machineTranslations
            .Where(x => x.ContentIndex == 0 && (machineTranslationId is null || x.Id == machineTranslationId))
            .OrderByDescending(x => x.Id)
            .FirstOrDefault()
            ?? throw new InvalidOperationException("No Machine translation with content index 0 found");

        if (machineTranslationId is null)
        {
            // Assume we are using the latest MT
            contentList = machineTranslations
                .Where(x => x.Id >= referenceMachineTranslation.Id)
                .Select(x => x.Content)
                .ToList();
            
            machineTranslationText = HtmlUtilities.GetPlainText(string.Join(string.Empty, contentList));

            return (referenceMachineTranslation.Id, machineTranslationText);
        }
        
        // Otherwise, we are getting a specific MT - or set of MTs for FIA
        // So, we need to determine if the reference MT is the latest or not and build our content list accordingly.
        var nextMachineTranslation = machineTranslations
            .Where(x => x.ContentIndex == 0 && x.Id > referenceMachineTranslation.Id )
            .OrderBy(x => x.Id)
            .FirstOrDefault();

        contentList = machineTranslations
            .Where(x => x.Id >= referenceMachineTranslation.Id && (nextMachineTranslation is null || x.Id < nextMachineTranslation.Id))
            .Select(x => x.Content)
            .ToList();

        machineTranslationText = HtmlUtilities.GetPlainText( string.Join(string.Empty, contentList) );
        
        return (referenceMachineTranslation.Id, machineTranslationText);
    }
}