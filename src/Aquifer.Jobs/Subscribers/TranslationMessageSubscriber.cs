using System.Text;
using System.Text.RegularExpressions;
using Aquifer.AI;
using Aquifer.Common.Messages;
using Aquifer.Common.Messages.Models;
using Aquifer.Common.Messages.Publishers;
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

public sealed class TranslationMessageSubscriber(
    AquiferDbContext _dbContext,
    ILogger<TranslationMessageSubscriber> _logger,
    ITranslationService _translationService,
    IResourceHistoryService _resourceHistoryService,
    INotificationMessagePublisher _notificationMessagePublisher,
    IQueueClientFactory _queueClientFactory)
{
    private const int _englishLanguageId = 1;

    private static readonly IReadOnlySet<TranslationOrigin> s_allowedTranslationOriginsForIndividualResourceContentTranslation =
        new HashSet<TranslationOrigin>
        {
            TranslationOrigin.CreateTranslation,
            TranslationOrigin.CommunityReviewer,
            TranslationOrigin.BasicTranslationOnly
        };

    private static readonly TaskOptions s_durableFunctionTaskOptions =
        TaskOptions.FromRetryPolicy(new RetryPolicy(5, TimeSpan.FromSeconds(1)));

    /// <summary>
    ///     Translates a single resource content item.
    ///     Only used by individual resource translation flows. Not used by project translation.
    /// </summary>
    [Function(nameof(TranslateResource))]
    public async Task TranslateResource([QueueTrigger(Queues.TranslateResource)] QueueMessage queueMessage, CancellationToken ct)
    {
        var message = queueMessage.Deserialize<TranslateResourceMessage, TranslationMessageSubscriber>(_logger);

        if (!s_allowedTranslationOriginsForIndividualResourceContentTranslation.Contains(message.TranslationOrigin))
        {
            throw new InvalidOperationException(
                $"Invalid {nameof(TranslationOrigin)} for the {nameof(TranslateResource)} flow: \"{message.TranslationOrigin}\".");
        }

        await TranslateResourceCoreAsync(message.ResourceContentId, message.StartedByUserId, message.TranslationOrigin, ct);
    }

    /// <summary>
    ///     Translates a project's resource content items and then takes a final action on the project itself.
    ///     This is done using the fan out/fan in pattern
    ///     (see https://learn.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-overview#fan-in-out)
    ///     which requires using durable functions to maintain state.
    ///     (see https://learn.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-bindings#durableTaskClient-usage).
    /// </summary>
    [Function(nameof(TranslateProjectResources))]
    public async Task TranslateProjectResources([QueueTrigger(Queues.TranslateProjectResources)] QueueMessage queueMessage,
        [DurableClient] DurableTaskClient durableTaskClient,
        CancellationToken ct)
    {
        var message = queueMessage.Deserialize<TranslateProjectResourcesMessage, TranslationMessageSubscriber>(_logger);

        var projectResourceContentIds = await _dbContext.ProjectResourceContents.Where(prc => prc.ProjectId == message.ProjectId)
            .Select(prc => prc.ResourceContentId)
            .ToListAsync(ct);

        if (projectResourceContentIds.Count == 0)
        {
            throw new InvalidOperationException($"Project ID {message.ProjectId} does not have any resource contents.");
        }

        // Kick off the durable function orchestration.
        // If it fails it will put a message on the poison queue.
        await durableTaskClient.ScheduleNewOrchestrationInstanceAsync(nameof(OrchestrateProjectResourcesTranslation),
            new OrchestrateProjectResourcesTranslationDto(message.ProjectId,
                message.StartedByUserId,
                projectResourceContentIds,
                Queues.GetPoisonQueueName(Queues.TranslateProjectResources),
                queueMessage.MessageText),
            ct);

        _logger.LogInformation("Kicked off {FunctionName} for Project ID {ProjectId}.",
            nameof(OrchestrateProjectResourcesTranslation),
            message.ProjectId);
    }

    /// <summary>
    ///     Durable orchestration function.
    ///     This function is single-threaded and thus should not do any DB operations or any substantial processing.
    ///     It should only orchestrate activities.
    /// </summary>
    [Function(nameof(OrchestrateProjectResourcesTranslation))]
    public async Task OrchestrateProjectResourcesTranslation([OrchestrationTrigger] TaskOrchestrationContext context,
        OrchestrateProjectResourcesTranslationDto dto)
    {
        try
        {
            var translateResourceTasks = dto.ProjectResourceContentIds.Select(resourceContentId =>
                    context.CallActivityAsync<int>(nameof(TranslateProjectResourceActivity),
                        new TranslateProjectResourceActivityDto(resourceContentId, dto.StartedByUserId),
                        s_durableFunctionTaskOptions))
                .ToList();

            await Task.WhenAll(translateResourceTasks);

            var translatedResourceContentIds = translateResourceTasks.Select(trt => trt.Result).ToList();

            await context.CallActivityAsync(nameof(UpdateProjectPostTranslationActivity),
                new UpdateProjectPostTranslationActivityDto(dto.ProjectId, dto.StartedByUserId, translatedResourceContentIds),
                s_durableFunctionTaskOptions);
        }
        catch (Exception orchestrationException)
        {
            // All the Activities orchestrated here have a retry policy.  If things are still failing after multiple retries
            // then something is very wrong. If this happens then swallow errors here to allow successful orchestration function
            // completion but publish a poison queue item for manual retry later.
            var ct = CancellationToken.None;
            try
            {
                _logger.LogError(orchestrationException,
                    "Error translating resource content for Project ID {ProjectId}. A poison message will be published to \"{PoisonQueueName}\" to enable manual retry. Manual dev intervention is required.",
                    dto.ProjectId,
                    dto.PoisonQueueName);

                var translateProjectResourcesPoisonQueueClient = await _queueClientFactory.GetQueueClientAsync(dto.PoisonQueueName, ct);
                await translateProjectResourcesPoisonQueueClient.SendMessageAsync(dto.OriginalQueueMessageText, ct);
            }
            catch (Exception queuePublishingException)
            {
                // don't allow errors during poison queue publishing to fail the durable function
                _logger.LogError(queuePublishingException,
                    "After an error translating resource content for Project ID {ProjectId} another error occurred when attempting to publish a poison message to \"{PoisonQueueName}\". Manual dev intervention is required to replay the message. Message text: {MessageText}",
                    dto.ProjectId,
                    dto.PoisonQueueName,
                    dto.OriginalQueueMessageText);
            }
        }
    }

    [Function(nameof(TranslateProjectResourceActivity))]
    public async Task<int> TranslateProjectResourceActivity([ActivityTrigger] TranslateProjectResourceActivityDto dto,
        FunctionContext activityContext)
    {
        await TranslateResourceCoreAsync(dto.ResourceContentId,
            dto.StartedByUserId,
            TranslationOrigin.Project,
            activityContext.CancellationToken);

        return dto.ResourceContentId;
    }

    [Function(nameof(UpdateProjectPostTranslationActivity))]
    public async Task UpdateProjectPostTranslationActivity([ActivityTrigger] UpdateProjectPostTranslationActivityDto dto,
        FunctionContext activityContext)
    {
        _logger.LogInformation("Beginning post-translation project updates for Project ID {ProjectId}.", dto.ProjectId);

        var project = await _dbContext.Projects.SingleAsync(p => p.Id == dto.ProjectId);

        var companyLeadUserId = project.CompanyLeadUserId ??
            throw new InvalidOperationException($"Company Lead User ID is null for Project ID {project.Id}. This should never happen.");

        // Assign all Draft Resource Content Versions in the project to the Company Lead of the Project.
        //
        // Edge case handling:
        // 1) It's possible that something else acted on the resource content version status
        // and moved it out of the TranslationAiDraftComplete status between the resource finishing translation
        // and this project post-processing. If so, ignore those resources when performing assignments
        // because there might already have been a user assignment.
        // 2) Skip assignment if the resource is already assigned (probably by a previous run of this function).
        //
        // Note: Resource contents with the New status are also included as these were not translated but still
        // need to be assigned (for Aquiferization of English resources).
        var resourceContentVersionsToAssign = await _dbContext.ResourceContentVersions.AsTracking()
            .Include(rcv => rcv.ResourceContent)
            .Where(rcv => rcv.ResourceContent.ProjectResourceContents.Any(prc => prc.Project.Id == project.Id) &&
                rcv.IsDraft &&
                (rcv.ResourceContent.Status == ResourceContentStatus.TranslationAiDraftComplete ||
                    rcv.ResourceContent.Status == ResourceContentStatus.New) &&
                rcv.AssignedUserId == null)
            .ToListAsync(activityContext.CancellationToken);

        // Edge case handling: Only update resource content versions that were queued for translation (even if gracefully skipped)
        // by this fan out -> fan in orchestration process.
        // Due to an unknown amount of time passing between translation and this post-processing it's unlikely but still possible
        // that the currently list of Draft Resource Content Versions is different from the list upon which the function originally acted.
        foreach (var resourceContentVersionToAssign in resourceContentVersionsToAssign.Where(rcv =>
            dto.TranslatedProjectResourceContentIds.Contains(rcv.ResourceContentId)))
        {
            // now that ALL translations are complete for the project, assign all resources in the project to the company lead
            resourceContentVersionToAssign.AssignedUserId = companyLeadUserId;
            await _resourceHistoryService.AddAssignedUserHistoryAsync(resourceContentVersionToAssign,
                resourceContentVersionToAssign.AssignedUserId,
                dto.StartedByUserId,
                activityContext.CancellationToken);
        }

        if (resourceContentVersionsToAssign.Count > 0)
        {
            await _dbContext.SaveChangesAsync(activityContext.CancellationToken);
        }

        await _notificationMessagePublisher.PublishSendProjectStartedNotificationMessageAsync(
            new SendProjectStartedNotificationMessage(dto.ProjectId),
            activityContext.CancellationToken);
    }

    /// <summary>
    ///     Note: Callers are responsible for performing resource assignments and creating original resource content snapshots.
    ///     This method will perform the actual translation, create machine translations,
    ///     update the existing draft resource version with translated content, create a new snapshot for the translation,
    ///     and change the resource content status.
    /// </summary>
    private async Task TranslateResourceCoreAsync(int resourceContentId,
        int startedByUserId,
        TranslationOrigin translationOrigin,
        CancellationToken ct)
    {
        _logger.LogInformation(
            "Beginning translation for Resource Content ID {ResourceContentId} (started by User ID {StartedByUserId} via Translation Origin: {TranslationOrigin}).",
            resourceContentId,
            startedByUserId,
            translationOrigin);

        if (translationOrigin == TranslationOrigin.None)
        {
            throw new InvalidOperationException($"Translation Origin should never be \"{translationOrigin}\".");
        }

        var resourceContentVersion = await _dbContext.ResourceContentVersions
                .AsTracking()
                .Include(rcv => rcv.ResourceContent)
                .Include(rcv => rcv.ResourceContent.Language)
                .Include(rcv => rcv.ResourceContentVersionSnapshots)
                .SingleOrDefaultAsync(rcv =>
                        rcv.IsDraft &&
                        rcv.ResourceContent.MediaType == ResourceContentMediaType.Text &&
                        rcv.ResourceContent.Id == resourceContentId,
                    ct) ??
            throw new InvalidOperationException(
                $"Aborting translation for Resource Content ID {resourceContentId} because it has no Resource Content Version with Text Media Type in the Draft status.");

        if (resourceContentVersion.ResourceContentVersionSnapshots.Count == 0)
        {
            throw new InvalidOperationException(
                $"Aborting translation for Resource Content ID {resourceContentId} because there are no pre-existing Snapshots for Resource Content Version ID {resourceContentVersion.Id}.");
        }

        if (translationOrigin != TranslationOrigin.BasicTranslationOnly &&
            resourceContentVersion.ResourceContent.Status != ResourceContentStatus.TranslationAwaitingAiDraft)
        {
            _logger.LogInformation(
                "Gracefully skipping translation for Resource Content ID {ResourceContentId} because it is not in the {ExpectedStatus} status.",
                resourceContentId,
                ResourceContentStatus.TranslationAwaitingAiDraft.ToString());

            return;
        }

        if (resourceContentVersion.ResourceContent.ContentUpdated != null)
        {
            _logger.LogInformation(
                "Gracefully skipping translation for Resource Content ID {ResourceContentId} because it already has updated content.",
                resourceContentId);

            return;
        }

        var resourceContentLanguage = resourceContentVersion.ResourceContent.Language;
        if (resourceContentLanguage.Id == _englishLanguageId)
        {
            _logger.LogInformation(
                "Gracefully skipping translation for Resource Content ID {ResourceContentId} because it is does not have a non-English target language.",
                resourceContentId);

            return;
        }

        var destinationLanguage = (Iso6393Code: resourceContentLanguage.ISO6393Code, EnglishName: resourceContentLanguage.EnglishDisplay);

        var translationPairs = await _dbContext.TranslationPairs.Where(x => x.LanguageId == resourceContentLanguage.Id)
            .Select(x => new
            {
                x.Key,
                x.Value
            })
            .ToDictionaryAsync(x => x.Key, x => x.Value, ct);

        // translate the display name
        string translatedDisplayName;
        try
        {
            var (preprocessedDisplayName, isExactMatch) = ReplaceTranslationPairs(resourceContentVersion.DisplayName, translationPairs);
            translatedDisplayName = isExactMatch
                ? preprocessedDisplayName
                : await _translationService.TranslateTextAsync(preprocessedDisplayName, destinationLanguage, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An error occurred during translation of the display name. Resource Content ID: {ResourceContentId}; Resource Content Version ID: {ResourceContentVersionId}.",
                resourceContentId,
                resourceContentVersion.Id);
            throw;
        }

        // Translate the resource content.
        // Note: Resource content is saved as Tiptap JSON in the DB.
        // It must be converted to HTML in order to be translated and then converted back to JSON after translation.
        // Some Resource Content, such as FIA, also has multiple steps.
        string translatedContentJson;
        var wordCount = 0;
        try
        {
            var contentHtmlItems = TiptapConverter.ConvertJsonToHtmlItems(resourceContentVersion.Content);

            // translate each Tiptap step independently
            var translatedContentHtmlItems = new List<string>();
            for (var contentIndex = 0; contentIndex < contentHtmlItems.Count; contentIndex++)
            {
                var (contentHtmlItem, _) = ReplaceTranslationPairs(contentHtmlItems[contentIndex], translationPairs);

                var translatedContentHtmlItem = await _translationService.TranslateHtmlAsync(contentHtmlItem, destinationLanguage, ct);
                translatedContentHtmlItems.Add(translatedContentHtmlItem);

                wordCount += HtmlUtilities.GetWordCount(translatedContentHtmlItem);

                // each step gets its own machine translation record
                _dbContext.ResourceContentVersionMachineTranslations.Add(new ResourceContentVersionMachineTranslationEntity
                {
                    ResourceContentVersionId = resourceContentVersion.Id,
                    DisplayName = translatedDisplayName,
                    Content = translatedContentHtmlItem,
                    ContentIndex = contentIndex,
                    UserId = null,
                    SourceId = MachineTranslationSourceId.OpenAi,
                    RetranslationReason = null
                });
            }

            translatedContentJson = TiptapConverter.ConvertHtmlItemsToJson(translatedContentHtmlItems);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An error occurred during translation. Resource Content ID: {ResourceContentId}; Resource Content Version ID: {ResourceContentVersionId}.",
                resourceContentId,
                resourceContentVersion.Id);
            throw;
        }

        resourceContentVersion.DisplayName = translatedDisplayName;
        resourceContentVersion.Content = translatedContentJson;
        resourceContentVersion.ContentSize = Encoding.UTF8.GetByteCount(translatedContentJson);
        resourceContentVersion.WordCount = wordCount;

        // save a snapshot of the translation
        await _resourceHistoryService.AddSnapshotHistoryAsync(resourceContentVersion,
            startedByUserId,
            resourceContentVersion.ResourceContent.Status,
            ct);

        // basic translations should not change the status unless transitioning from AI draft to complete
        if (translationOrigin != TranslationOrigin.BasicTranslationOnly ||
            resourceContentVersion.ResourceContent.Status == ResourceContentStatus.TranslationAwaitingAiDraft)
        {
            resourceContentVersion.ResourceContent.Status = ResourceContentStatus.TranslationAiDraftComplete;
            await _resourceHistoryService.AddStatusHistoryAsync(resourceContentVersion,
                resourceContentVersion.ResourceContent.Status,
                startedByUserId,
                ct);
        }

        // if an individual translation was requested then the status needs to be updated (again) now that translation has completed
        if (translationOrigin is TranslationOrigin.CreateTranslation or TranslationOrigin.CommunityReviewer)
        {
            resourceContentVersion.ResourceContent.Status = ResourceContentStatus.TranslationEditorReview;
            await _resourceHistoryService.AddStatusHistoryAsync(resourceContentVersion,
                resourceContentVersion.ResourceContent.Status,
                startedByUserId,
                ct);

            // The resource content version should already be assigned but if it isn't then assign it to
            // the user who requested the translation.
            if (resourceContentVersion.AssignedUserId == null)
            {
                resourceContentVersion.AssignedUserId = startedByUserId;

                await _resourceHistoryService.AddAssignedUserHistoryAsync(resourceContentVersion,
                    resourceContentVersion.AssignedUserId,
                    startedByUserId,
                    ct);
            }
        }

        await _dbContext.SaveChangesAsync(ct);
    }

    private static (string text, bool isFullReplace) ReplaceTranslationPairs(string text, Dictionary<string, string> translationPairs)
    {
        foreach (var pair in translationPairs.OrderByDescending(x => x.Key.Length))
        {
            if (string.Equals(pair.Key, text, StringComparison.InvariantCultureIgnoreCase))
            {
                return (pair.Value, true);
            }

            text = Regex.Replace(text, $"""\b(?:{pair.Key})\b""", pair.Value, RegexOptions.IgnoreCase);
        }

        return (text, false);
    }

    public sealed record OrchestrateProjectResourcesTranslationDto(
        int ProjectId,
        int StartedByUserId,
        IReadOnlyList<int> ProjectResourceContentIds,
        string PoisonQueueName,
        string OriginalQueueMessageText);

    public sealed record TranslateProjectResourceActivityDto(int ResourceContentId, int StartedByUserId);

    public sealed record UpdateProjectPostTranslationActivityDto(
        int ProjectId,
        int StartedByUserId,
        IReadOnlyList<int> TranslatedProjectResourceContentIds);
}