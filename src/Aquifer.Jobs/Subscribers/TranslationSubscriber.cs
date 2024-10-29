using Aquifer.Common.Jobs;
using Aquifer.Common.Jobs.Messages;
using Aquifer.Common.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Aquifer.Data.Enums;
using Aquifer.Data.Services;
using Aquifer.Jobs.Services;
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
    INotificationService _notificationService)
{
    [Function(nameof(TranslateResource))]
    public async Task TranslateResource(
        [QueueTrigger(Queues.TranslateResource)] QueueMessage queueMessage,
        CancellationToken ct)
    {
        var message = queueMessage.Deserialize<TranslateResourceMessage, TranslationSubscriber>(_logger);

        await TranslateResourceCoreAsync(message.ResourceContentId, message.StartedByUserId, message.TranslationOrigin, ct);
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

        // kick off the durable function orchestration
        await durableTaskClient.ScheduleNewOrchestrationInstanceAsync(
            nameof(OrchestrateProjectResourcesTranslation),
            new OrchestrateProjectResourcesTranslationDto(message.ProjectId, message.StartedByUserId, projectResourceContentIds),
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
        var translateResourceTasks = dto.ProjectResourceContentIds
            .Select(resourceContentId => context.CallActivityAsync(
                nameof(TranslateProjectResourceActivity),
                new TranslateProjectResourceActivityDto(resourceContentId, dto.StartedByUserId)))
            .ToList();

        await Task.WhenAll(translateResourceTasks);

        await context.CallActivityAsync(
            nameof(UpdateProjectPostTranslationActivity),
            new UpdateProjectPostTranslationActivityDto(dto.ProjectId, dto.StartedByUserId));
    }

    [Function(nameof(TranslateProjectResourceActivity))]
    public async Task TranslateProjectResourceActivity(
        [ActivityTrigger] TranslateProjectResourceActivityDto dto,
        FunctionContext activityContext)
    {
        await TranslateResourceCoreAsync(dto.ResourceContentId, dto.StartedByUserId, TranslationOrigin.Project, activityContext.CancellationToken);
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

        if (project.CompanyLeadUserId is not null)
        {
            var resourceContentVersions = await _dbContext.ResourceContentVersions
                .AsTracking()
                .Where(rcv => rcv.ResourceContent.ProjectResourceContents.Any(prc => prc.Project.Id == project.Id) && rcv.IsDraft)
                .Include(rcv => rcv.ResourceContent)
                .ToListAsync(activityContext.CancellationToken);

            foreach (var resourceContentVersion in resourceContentVersions)
            {
                resourceContentVersion.AssignedUserId = project.CompanyLeadUserId;
                await _resourceHistoryService.AddAssignedUserHistoryAsync(
                    resourceContentVersion,
                    project.CompanyLeadUserId,
                    dto.StartedByUserId,
                    activityContext.CancellationToken);
                await _resourceHistoryService.AddSnapshotHistoryAsync(
                    resourceContentVersion,
                    dto.StartedByUserId,
                    ResourceContentStatus.New,
                    activityContext.CancellationToken);
            }

            await _dbContext.SaveChangesAsync(activityContext.CancellationToken);
        }

        await _notificationService.SendProjectStartedNotificationAsync(dto.ProjectId, activityContext.CancellationToken);
    }

    private async Task TranslateResourceCoreAsync(
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
            .SingleOrDefaultAsync(rcv => rcv.IsDraft && rcv.ResourceContent.Id == resourceContentId, ct);

        if (resourceContentVersion == null)
        {
            _logger.LogInformation(
                "Skipping translation for Resource Content ID {ResourceContentId} because it has no ResourceContentVersion in the Draft status.",
                resourceContentId);
            return;
        }

        if (resourceContentVersion.ResourceContent.Status != ResourceContentStatus.TranslationAwaitingAiDraft)
        {
            _logger.LogInformation(
                "Skipping translation for Resource Content ID {ResourceContentId} because the content is not in the {ExpectedStatus} status.",
                resourceContentId,
                ResourceContentStatus.TranslationAwaitingAiDraft.ToString());
            return;
        }

        string translatedContent;
        try
        {
            translatedContent = await _translationService.TranslateAsync(resourceContentVersion.Content, ct);
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

        _dbContext.ResourceContentVersionMachineTranslations
            .Add(new ResourceContentVersionMachineTranslationEntity
            {
                ResourceContentVersionId = resourceContentVersion.Id,
                DisplayName = resourceContentVersion.DisplayName, // TODO is this correct?
                Content = translatedContent,
                ContentIndex = 0, // TODO is this correct?
                UserId = startedByUserId, // TODO is this correct for projects to use the user who started the project?
                SourceId = MachineTranslationSourceId.OpenAi,
                RetranslationReason = null,
            });

        // TODO convert to TipTap
        resourceContentVersion.Content = translatedContent;
        resourceContentVersion.ResourceContent.Status = ResourceContentStatus.TranslationAiDraftComplete;

        //await _dbContext.SaveChangesAsync(ct);
    }

    public sealed record OrchestrateProjectResourcesTranslationDto(int ProjectId, int StartedByUserId, IReadOnlyList<int> ProjectResourceContentIds);
    public sealed record TranslateProjectResourceActivityDto(int ResourceContentId, int StartedByUserId);
    public sealed record UpdateProjectPostTranslationActivityDto(int ProjectId, int StartedByUserId);
}