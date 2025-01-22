using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Common.Extensions;
using Aquifer.Common.Services.Caching;
using Aquifer.Common.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.AssignedToSelf.List;

public class Endpoint(
    AquiferDbContext _dbContext,
    IUserService _userService,
    IResourceContentSearchService _resourceContentSearchService,
    ICachingLanguageService _cachingLanguageService)
    : EndpointWithoutRequest<IReadOnlyList<Response>>
{
    public override void Configure()
    {
        Get("/resources/content/assigned-to-self");
        Permissions(PermissionName.ReadResources);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var user = await _userService.GetUserFromJwtAsync(ct);

        var (_, resourceContentSummaries) = await _resourceContentSearchService.SearchAsync(
            new ResourceContentSearchFilter
            {
                AssignedUserId = user.Id,
                ExcludeContentStatuses = [ResourceContentStatus.TranslationAiDraftComplete, ResourceContentStatus.AquiferizeAiDraftComplete],
            },
            offset: 0,
            limit: int.MaxValue,
            ct);

        var languageEntityByIdMap = await _cachingLanguageService.GetLanguageEntityByIdMapAsync(ct);

        var projectByIdMap = await _dbContext.Projects
            .Where(p => resourceContentSummaries.Select(rcs => rcs.ProjectId).Distinct().Contains(p.Id))
            .ToDictionaryAsync(p => p.Id, ct);

        var latestResourceContentVersionByIdMap = await _dbContext.ResourceContentVersions
            .Where(rcv => resourceContentSummaries.Select(rcs => rcs.LatestResourceContentVersionId).Contains(rcv.Id))
            .ToDictionaryAsync(rcv => rcv.Id, ct);

        var lastUserAssignmentsByResourceContentVersionIdMap =
            await Helpers.GetLastUserAssignmentsByResourceContentVersionIdMapAsync(
                latestResourceContentVersionByIdMap.Keys,
                numberOfAssignments: 2,
                _dbContext,
                ct);

        Response = resourceContentSummaries
            .Select(rcs =>
            {
                // ProjectId may be null
                var project = rcs.ProjectId.HasValue ? projectByIdMap[rcs.ProjectId.Value] : null;

                // Because we filtered by AssignedUserId we can assume that the LatestResourceContentVersionId is the Draft version
                // (the only version that allows user assignments).
                var draftResourceContentVersion = latestResourceContentVersionByIdMap[rcs.LatestResourceContentVersionId];

                // The most recent user assignment history should be to assign to the current user.
                var lastTwoUserAssignments = lastUserAssignmentsByResourceContentVersionIdMap[draftResourceContentVersion.Id];
                var currentUserAssignment = lastTwoUserAssignments[0];
                if (currentUserAssignment.User?.Id != user.Id)
                {
                    throw new InvalidOperationException(
                        $"Expected the currently assigned user to be the last assigned user, but the current user ID is \"{user.Id}\" and the last assigned user is \"{currentUserAssignment.User?.Id}\".");
                }

                var previouslyAssignedUser = lastTwoUserAssignments.Count > 1 ? lastTwoUserAssignments[1].User : null;

                return new Response
                {
                    Id = rcs.Id,
                    EnglishLabel = rcs.ResourceEnglishLabel,
                    ParentResourceName = rcs.ParentResourceEnglishDisplayName,
                    LanguageEnglishDisplay = languageEntityByIdMap[rcs.LanguageId].EnglishDisplay,
                    WordCount = draftResourceContentVersion.SourceWordCount,
                    StatusValue = rcs.Status,
                    SortOrder = rcs.ResourceSortOrder,
                    ProjectName = project?.Name,
                    Status = rcs.Status.GetDisplayName(),
                    StatusDisplayName = rcs.Status.GetDisplayName(),
                    DaysSinceAssignment = (DateTime.UtcNow - currentUserAssignment.Created).Days,
                    DaysUntilProjectDeadline = project?.ProjectedDeliveryDate == null
                        ? null
                        : (project.ProjectedDeliveryDate.Value.ToDateTime(new TimeOnly(23, 59)) - DateTime.UtcNow).Days,
                    DaysSinceContentUpdated = rcs.ContentUpdated == null ? null : (DateTime.UtcNow - (DateTime)rcs.ContentUpdated).Days,
                    LastAssignedUser = previouslyAssignedUser,
                    HasAudio = rcs.HasAudio,
                    HasUnresolvedCommentThreads = rcs.HasUnresolvedCommentThreads,
                };
            })
            .OrderByDescending(x => x.DaysSinceAssignment)
            .ThenBy(x => x.ProjectName)
            .ThenBy(x => x.EnglishLabel)
            .ToList();
    }
}