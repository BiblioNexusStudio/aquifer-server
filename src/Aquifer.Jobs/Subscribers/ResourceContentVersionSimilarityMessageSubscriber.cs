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
        var similarityScore = new ResourceContentVersionSimilarityScore()
        {
            BaseVersionId = message.BaseVersionId,
            ComparedVersionId = message.CompareVersionId,
        };
        
        (similarityScore.BaseVersionType, similarityScore.ComparedVersionType) = GetComparisonTypes(message);
        
        var baseVersionText = await GetResourceContentVersionText(
            message.BaseVersionId,
            similarityScore.BaseVersionType,
            _dbContext,
            ct);
        
        var compareVersionText = await GetResourceContentVersionText(
            message.CompareVersionId, 
            similarityScore.ComparedVersionType,
            _dbContext,
            ct);
        
        similarityScore.SimilarityScore = StringSimilarityUtilities.ComputeLevenshteinSimilarity(baseVersionText, compareVersionText);
        
        await _dbContext.ResourceContentVersionSimilarityScores.AddAsync(similarityScore, ct);
        await _dbContext.SaveChangesAsync(ct);
    }
    
    private (
        ResourceContentVersionTypes baseVersionType, 
        ResourceContentVersionTypes comparedVersionType
        ) GetComparisonTypes(ScoreResourceContentVersionSimilarityMessage message)
    {
        switch (message.ComparisonType)
        {
            case ResourceContentVersionSimilarityComparisonType.MachineTranslationToResourceContentVersion:
                _logger.LogInformation(
                    "Scoring machine translation {BaseVersionId} vs published resource content version {CompareVersionId}...",
                    message.BaseVersionId,
                    message.CompareVersionId);

                return (ResourceContentVersionTypes.MachineTranslation, ResourceContentVersionTypes.Base);
            case ResourceContentVersionSimilarityComparisonType.MachineTranslationToSnapshot:
                _logger.LogInformation(
                    "Scoring machine translation {BaseVersionId} vs snapshot version {CompareVersionId}...",
                    message.BaseVersionId,
                    message.CompareVersionId);

                return (ResourceContentVersionTypes.MachineTranslation, ResourceContentVersionTypes.Snapshot);
            case ResourceContentVersionSimilarityComparisonType.ResourceContentVersionToSnapshot:
                _logger.LogInformation(
                    "Scoring resource content version {BaseVersionId} vs snapshot version {CompareVersionId}...",
                    message.BaseVersionId,
                    message.CompareVersionId);

                return (ResourceContentVersionTypes.Base, ResourceContentVersionTypes.Snapshot);
            case ResourceContentVersionSimilarityComparisonType.SnapshotToSnapshot:
                _logger.LogInformation(
                    "Scoring snapshot version {BaseVersionId} vs snapshot version {CompareVersionId}...",
                    message.BaseVersionId,
                    message.CompareVersionId);

                return (ResourceContentVersionTypes.Snapshot, ResourceContentVersionTypes.Snapshot);
            default:
                throw new InvalidOperationException($"Unknown similarity comparison type: {message.ComparisonType}");
        }
    }
    
    private static async Task<string> GetResourceContentVersionText(
        int versionId, 
        ResourceContentVersionTypes resourceType,
        AquiferDbContext dbContext,
        CancellationToken ct)
    {
        return resourceType switch
        {
            ResourceContentVersionTypes.Base => await GetBaseVersionContentText(versionId, dbContext, ct),
            ResourceContentVersionTypes.Snapshot => await GetSnapshotVersionText(versionId, dbContext, ct),
            ResourceContentVersionTypes.MachineTranslation => await GetMachineTranslationVersionText(versionId, dbContext, ct),
            ResourceContentVersionTypes.None => throw new InvalidOperationException($"Invalid similarity comparison type: {resourceType}"),
            _ => throw new InvalidOperationException($"Invalid similarity comparison type: {resourceType}"),
        };
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

    private static async Task<string> GetMachineTranslationVersionText(
        int machineTranslationId,
        AquiferDbContext dbContext,
        CancellationToken ct)
    {
        var machineTranslationResource = await dbContext
                                         .ResourceContentVersionMachineTranslations
                                         .AsTracking()
                                         .SingleOrDefaultAsync(x => x.Id == machineTranslationId, ct) 
                                     ?? throw new InvalidOperationException($"Machine translation with id {machineTranslationId} not found");
        
        return HtmlUtilities.GetPlainText(machineTranslationResource.Content);
    }

    private static async Task<string> GetSnapshotVersionText(
        int snapshotVersionId,
        AquiferDbContext dbContext,
        CancellationToken ct)
    {
        var snapshotResource = await dbContext
                                   .ResourceContentVersionSnapshots
                                   .AsTracking()
                                   .SingleOrDefaultAsync(x => x.Id == snapshotVersionId, ct)
                               ?? throw new InvalidOperationException($"Snapshot version with id {snapshotVersionId} not found");
        
        var snapshotContentHtmlItems = TiptapConverter.ConvertJsonToHtmlItems(snapshotResource.Content);
        
        return HtmlUtilities.GetPlainText(
            string.Join(string.Empty, snapshotContentHtmlItems)
        );
    }
}