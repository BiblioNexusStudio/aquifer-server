﻿using System.Text;
using Aquifer.AI;
using Aquifer.Common.Jobs;
using Aquifer.Common.Jobs.Messages;
using Aquifer.Common.Services;
using Aquifer.Common.Tiptap;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Aquifer.Data.Enums;
using Aquifer.Data.Services;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Aquifer.Jobs.Subscribers;

public sealed class TranslationSubscriber(
    AquiferDbContext _dbContext,
    ILogger<TranslationSubscriber> _logger,
    ITranslationService _translationService,
    IResourceHistoryService _resourceHistoryService,
    INotificationService _notificationService,
    IQueueClientFactory _queueClientFactory)
{
    private static readonly TaskOptions s_durableFunctionTaskOptions = TaskOptions.FromRetryPolicy(
        new RetryPolicy(
            maxNumberOfAttempts: 5,
            firstRetryInterval: TimeSpan.FromSeconds(1)));

    [Function(nameof(TranslateResource))]
    public async Task TranslateResource(
        [QueueTrigger(Queues.TranslateResource)] QueueMessage queueMessage,
        CancellationToken ct)
    {
        var message = queueMessage.Deserialize<TranslateResourceMessage, TranslationSubscriber>(_logger);

        var translatedResourceContentVersion = await TranslateResourceCoreAsync(
            message.ResourceContentId,
            message.StartedByUserId,
            message.TranslationOrigin,
            ct);

        // if a Community Reviewer requested the translation then the status needs to be updated (again) now that translation has completed
        if (message.TranslationOrigin == TranslationOrigin.CommunityReviewer)
        {
            translatedResourceContentVersion.ResourceContent.Status = ResourceContentStatus.TranslationEditorReview;
            await _resourceHistoryService.AddStatusHistoryAsync(
                translatedResourceContentVersion,
                ResourceContentStatus.TranslationEditorReview,
                changedByUserId: message.StartedByUserId,
                ct
            );

            await _dbContext.SaveChangesAsync(ct);
        }
    }

    /// <summary>
    /// Translates a project's resources and then takes a final action on the project itself.
    /// This is done using the fan out/fan in pattern
    /// (see https://learn.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-overview#fan-in-out)
    /// which requires using durable functions to maintain state.
    /// (see https://learn.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-bindings#durableTaskClient-usage).
    /// </summary>
    [Function(nameof(TranslateProjectResources))]
    public async Task TranslateProjectResources(
        [QueueTrigger(Queues.TranslateProjectResources)] QueueMessage queueMessage,
        [DurableClient] DurableTaskClient durableTaskClient,
        CancellationToken ct)
    {
        var message = queueMessage.Deserialize<TranslateProjectResourcesMessage, TranslationSubscriber>(_logger);

        var projectResourceContentIds = await _dbContext.Projects
            .Where(p => p.Id == message.ProjectId)
            .SelectMany(p => p.ProjectResourceContents.Select(prc => prc.ResourceContentId))
            .ToListAsync(ct);

        if (projectResourceContentIds.Count == 0)
        {
            throw new InvalidOperationException($"Project ID {message.ProjectId} does not have any resource contents.");
        }

        // Kick off the durable function orchestration.
        // If it fails it will put a message on the poison queue.
        await durableTaskClient.ScheduleNewOrchestrationInstanceAsync(
            nameof(OrchestrateProjectResourcesTranslation),
            new OrchestrateProjectResourcesTranslationDto(
                message.ProjectId,
                message.StartedByUserId,
                projectResourceContentIds,
                queueMessage.MessageText),
            ct);

        _logger.LogInformation(
            "Kicked off {FunctionName} for Project ID {ProjectId}.",
            nameof(OrchestrateProjectResourcesTranslation),
            message.ProjectId);
    }

    /// <summary>
    /// Durable orchestration function.
    /// This function is single-threaded and thus should not do any DB operations or any substantial processing.
    /// It should only orchestrate activities.
    /// </summary>
    [Function(nameof(OrchestrateProjectResourcesTranslation))]
    public async Task OrchestrateProjectResourcesTranslation(
        [OrchestrationTrigger] TaskOrchestrationContext context,
        OrchestrateProjectResourcesTranslationDto dto)
    {
        try
        {
            var translateResourceTasks = dto.ProjectResourceContentIds
                .Select(resourceContentId => context.CallActivityAsync<int>(
                    nameof(TranslateProjectResourceActivity),
                    new TranslateProjectResourceActivityDto(resourceContentId, dto.StartedByUserId),
                    s_durableFunctionTaskOptions))
                .ToList();

            await Task.WhenAll(translateResourceTasks);

            var translatedResourceContentIds = translateResourceTasks
                .Select(trt => trt.Result)
                .ToList();

            await context.CallActivityAsync(
                nameof(UpdateProjectPostTranslationActivity),
                new UpdateProjectPostTranslationActivityDto(dto.ProjectId, dto.StartedByUserId, translatedResourceContentIds),
                s_durableFunctionTaskOptions);
        }
        catch (Exception orchestrationException)
        {
            // All the Activities orchestrated here have a retry policy.  If things are still failing after multiple retries
            // then something is very wrong. If this happens then swallow errors here to allow successful orchestration function
            // completion but publish a poison queue item for manual retry later.
            var ct = CancellationToken.None;
            var poisonQueueName = Queues.GetPoisonQueueName(Queues.TranslateProjectResources);

            try
            {
                _logger.LogError(
                    orchestrationException,
                    "Error translating resource content for Project ID {ProjectId}. A poison message will be published to \"{PoisonQueueName}\" to enable manual retry. Manual dev intervention is required.",
                    dto.ProjectId,
                    poisonQueueName);

                var translateProjectResourcesPoisonQueueClient = await _queueClientFactory.GetQueueClientAsync(
                    poisonQueueName,
                    ct);
                await translateProjectResourcesPoisonQueueClient.SendMessageAsync(dto.OriginalQueueMessageText, ct);
            }
            catch (Exception queuePublishingException)
            {
                // don't allow errors during poison queue publishing to fail the durable function
                _logger.LogError(
                    queuePublishingException,
                    "After an error translating resource content for Project ID {ProjectId} another error occurred when attempting to publish a poison message to \"{PoisonQueueName}\". Manual dev intervention is required to replay the message. Message text: {MessageText}",
                    dto.ProjectId,
                    poisonQueueName,
                    dto.OriginalQueueMessageText);
            }
        }
    }

    [Function(nameof(TranslateProjectResourceActivity))]
    public async Task<int> TranslateProjectResourceActivity(
        [ActivityTrigger] TranslateProjectResourceActivityDto dto,
        FunctionContext activityContext)
    {
        var translatedResourceContentVersion = await TranslateResourceCoreAsync(
            dto.ResourceContentId,
            dto.StartedByUserId,
            TranslationOrigin.Project,
            activityContext.CancellationToken);

        return translatedResourceContentVersion.ResourceContentId;
    }

    [Function(nameof(UpdateProjectPostTranslationActivity))]
    public async Task UpdateProjectPostTranslationActivity(
        [ActivityTrigger] UpdateProjectPostTranslationActivityDto dto,
        FunctionContext activityContext)
    {
        _logger.LogInformation(
            "Beginning post-translation project updates for Project ID {ProjectId}.",
            dto.ProjectId);

        var project = await _dbContext.Projects
            .SingleAsync(p => p.Id == dto.ProjectId);

        var companyLeadUserId = project.CompanyLeadUserId
            ?? throw new InvalidOperationException($"Company Lead User ID is null for Project ID {project.Id}. This should never happen.");

        // Edge case handling: It's possible that something else acted on the resource content version status
        // and moved it out of the TranslationAiDraftComplete status between the resource finishing translation
        // and this project post-processing. If so, ignore those resources when performing assignments
        // because there might already have been a user assignment.
        var resourceContentVersions = await _dbContext.ResourceContentVersions
            .AsTracking()
            .Include(rcv => rcv.ResourceContent)
            .Where(rcv => rcv.ResourceContent.ProjectResourceContents.Any(prc => prc.Project.Id == project.Id) &&
                rcv.IsDraft &&
                rcv.ResourceContent.Status == ResourceContentStatus.TranslationAiDraftComplete)
            .ToListAsync(activityContext.CancellationToken);

        // Edge case handling: Only update resource content versions that were queued for translation (even if gracefully skipped).
        // Due to an unknown amount of time passing between translation and this post-processing it's unlikely
        // but still possible that the list is different.
        foreach (var resourceContentVersion in resourceContentVersions
            .Where(rcv => dto.TranslatedProjectResourceContentIds.Contains(rcv.ResourceContentId)))
        {
            // now that ALL translations are complete for the project, assign all resources in the project to the company lead
            resourceContentVersion.AssignedUserId = project.CompanyLeadUserId;
            await _resourceHistoryService.AddAssignedUserHistoryAsync(
                resourceContentVersion,
                assignedUserId: project.CompanyLeadUserId,
                changedByUserId: dto.StartedByUserId,
                activityContext.CancellationToken);
        }

        await _dbContext.SaveChangesAsync(activityContext.CancellationToken);

        await _notificationService.SendProjectStartedNotificationAsync(dto.ProjectId, activityContext.CancellationToken);
    }

    /// <summary>
    /// Note: Callers are responsible for performing resource assignments and creating original resource content snapshots.
    /// This method will perform the actual translation, create machine translations,
    /// update the existing draft resource version with translated content, create a new snapshot for the translation,
    /// and change the resource content status.
    /// </summary>
    private async Task<ResourceContentVersionEntity> TranslateResourceCoreAsync(
        int resourceContentId,
        int startedByUserId,
        TranslationOrigin translationOrigin,
        CancellationToken ct)
    {
        _logger.LogInformation(
            "Beginning translation for Resource Content ID {ResourceContentId} (started by User ID {StartedByUserId} via Translation Origin: {TranslationOrigin}).",
            resourceContentId,
            startedByUserId,
            translationOrigin);

        var resourceContentVersion = await _dbContext.ResourceContentVersions
            .AsTracking()
            .Include(rcv => rcv.ResourceContent)
            .Include(rcv => rcv.ResourceContent.Language)
            .Include(rcv => rcv.ResourceContentVersionSnapshots)
            .SingleOrDefaultAsync(
                rcv =>
                    rcv.IsDraft &&
                    rcv.ResourceContent.MediaType == ResourceContentMediaType.Text &&
                    rcv.ResourceContent.Id == resourceContentId,
                ct)
            ?? throw new InvalidOperationException(
                $"Aborting translation for Resource Content ID {resourceContentId} because it has no Resource Content Version with Text Media Type in the Draft status.");

        if (resourceContentVersion.ResourceContentVersionSnapshots.Count == 0)
        {
            throw new InvalidOperationException(
                $"Aborting translation for Resource Content ID {resourceContentId} because there are no pre-existing Snapshots for Resource Content Version ID {resourceContentVersion.Id}.");
        }

        if (resourceContentVersion.ResourceContent.Status != ResourceContentStatus.TranslationAwaitingAiDraft)
        {
            _logger.LogInformation(
                "Gracefully skipping translation for Resource Content ID {ResourceContentId} because it is not in the {ExpectedStatus} status.",
                resourceContentId,
                ResourceContentStatus.TranslationAwaitingAiDraft.ToString());
            return resourceContentVersion;
        }

        var resourceContentLanguage = resourceContentVersion.ResourceContent.Language;
        var destinationLanguage =
            (Iso6393Code: resourceContentLanguage.ISO6393Code, EnglishName: resourceContentLanguage.EnglishDisplay);

        // translate the display name
        string translatedDisplayName;
        try
        {
            translatedDisplayName =
                await _translationService.TranslateTextAsync(resourceContentVersion.DisplayName, destinationLanguage, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "An error occurred during translation of the display name. Resource Content ID: {ResourceContentId}; Resource Content Version ID: {ResourceContentVersionId}.",
                resourceContentId,
                resourceContentVersion.Id);
            throw;
        }

        // Translate the resource content.
        // Note: Resource content is saved as Tiptap JSON in the DB.
        // It must be converted to HTML in order to be translated and then converted back to JSON after translation.
        string translatedContentJson;
        var wordCount = 0;
        try
        {
            var contentHtmlItems = TiptapConverter.ConvertJsonToHtmlItems(resourceContentVersion.Content);

            // translate each Tiptap step independently
            var translatedContentHtmlItems = new List<string>();
            for (var index = 0; index < contentHtmlItems.Count; index++)
            {
                var translatedContentHtmlItem = await _translationService.TranslateHtmlAsync(contentHtmlItems[index], destinationLanguage, ct);
                translatedContentHtmlItems.Add(translatedContentHtmlItem);
                wordCount += HtmlUtilities.GetWordCount(translatedContentHtmlItem);

                // create machine translation
                _dbContext.ResourceContentVersionMachineTranslations
                    .Add(new ResourceContentVersionMachineTranslationEntity
                    {
                        ResourceContentVersionId = resourceContentVersion.Id,
                        DisplayName = translatedDisplayName,
                        Content = translatedContentHtmlItem,
                        ContentIndex = index,
                        UserId = null,
                        SourceId = MachineTranslationSourceId.OpenAi,
                        RetranslationReason = null,
                    });
            }

            translatedContentJson = TiptapConverter.ConvertHtmlItemsToJson(translatedContentHtmlItems);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "An error occurred during translation. Resource Content ID: {ResourceContentId}; Resource Content Version ID: {ResourceContentVersionId}.",
                resourceContentId,
                resourceContentVersion.Id);
            throw;
        }

        resourceContentVersion.Content = translatedContentJson;
        resourceContentVersion.ContentSize = Encoding.UTF8.GetByteCount(translatedContentJson);
        resourceContentVersion.WordCount = wordCount;

        // save a snapshot of the translation
        await _resourceHistoryService.AddSnapshotHistoryAsync(
            resourceContentVersion,
            oldUserId: startedByUserId,
            oldStatus: ResourceContentStatus.TranslationAwaitingAiDraft,
            ct);

        // move to correct completed status
        resourceContentVersion.ResourceContent.Status = ResourceContentStatus.TranslationAiDraftComplete;
        await _resourceHistoryService.AddStatusHistoryAsync(
            resourceContentVersion,
            ResourceContentStatus.TranslationAiDraftComplete,
            changedByUserId: startedByUserId,
            ct
        );

        await _dbContext.SaveChangesAsync(ct);

        return resourceContentVersion;
    }

    public sealed record OrchestrateProjectResourcesTranslationDto(
        int ProjectId,
        int StartedByUserId,
        IReadOnlyList<int> ProjectResourceContentIds,
        string OriginalQueueMessageText);

    public sealed record TranslateProjectResourceActivityDto(
        int ResourceContentId,
        int StartedByUserId);

    public sealed record UpdateProjectPostTranslationActivityDto(
        int ProjectId,
        int StartedByUserId,
        IReadOnlyList<int> TranslatedProjectResourceContentIds);
}