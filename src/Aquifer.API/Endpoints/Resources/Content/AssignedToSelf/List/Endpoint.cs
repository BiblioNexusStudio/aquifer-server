using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Common.Extensions;
using Aquifer.Common.Services.Caching;
using Aquifer.Common.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Aquifer.API.Common.Dtos;

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
                IsDraft = true,
                AssignedUserId = user.Id,
                ExcludeContentStatuses = [ResourceContentStatus.TranslationAiDraftComplete, ResourceContentStatus.AquiferizeAiDraftComplete],
            },
            offset: 0,
            limit: null,
            shouldSortByName: false,
            ct);

        var languageEntityByIdMap = await _cachingLanguageService.GetLanguageEntityByIdMapAsync(ct);

        var projectByIdMap = await _dbContext.Projects
            .Where(p => resourceContentSummaries.Select(rcs => rcs.ProjectId).Distinct().Contains(p.Id))
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.ProjectedDeliveryDate,
            })
            .ToDictionaryAsync(p => p.Id, ct);

        var sourceWordCountByResourceContentVersionIdMap = await _dbContext.ResourceContentVersions
            .Where(rcv => resourceContentSummaries.Select(rcs => rcs.LatestResourceContentVersionId).Contains(rcv.Id))
            .Select(rcv => new { rcv.Id, rcv.SourceWordCount })
            .ToDictionaryAsync(x => x.Id, x => x.SourceWordCount, ct);

        var lastUserAssignmentsByResourceContentVersionIdMap =
            await Helpers.GetLastUserAssignmentsByResourceContentVersionIdMapAsync(
                sourceWordCountByResourceContentVersionIdMap.Keys,
                numberOfAssignments: 2,
                _dbContext,
                ct);

        // get previously assigned user IDs.
        var userIdsToFetch = lastUserAssignmentsByResourceContentVersionIdMap.Values
            .Select(x => x.Count > 1 ? x[1].UserId : null)
            .OfType<int>()
            .Distinct()
            .ToList();

        var userDtoByIdMap = await _dbContext.Users
            .Where(u => userIdsToFetch.Contains(u.Id))
            .Select(u => new UserDto(u.Id, u.FirstName, u.LastName))
            .ToDictionaryAsync(u => u.Id, ct);

        Response = resourceContentSummaries
            .Select(rcs =>
            {
                // ProjectId may be null
                var project = rcs.ProjectId.HasValue ? projectByIdMap[rcs.ProjectId.Value] : null;

                // Because we filtered by AssignedUserId we can assume that the LatestResourceContentVersionId is the Draft version
                // (the only version that allows user assignments).
                var sourceWordCount = sourceWordCountByResourceContentVersionIdMap[rcs.LatestResourceContentVersionId];

                // The most recent user assignment history should be to assign to the current user.
                var lastTwoUserAssignments =
                    lastUserAssignmentsByResourceContentVersionIdMap[rcs.LatestResourceContentVersionId];
                var currentUserAssignment = lastTwoUserAssignments[0];
                if (currentUserAssignment.UserId != user.Id)
                {
                    throw new InvalidOperationException(
                        $"Expected the currently assigned user to be the last assigned user, but the current user ID is \"{user.Id}\" and the last assigned user is \"{currentUserAssignment.UserId}\".");
                }

                var previouslyAssignedUserId = lastTwoUserAssignments.Count > 1 ? lastTwoUserAssignments[1].UserId : null;

                return new Response
                {
                    Id = rcs.Id,
                    EnglishLabel = rcs.ResourceEnglishLabel,
                    ParentResourceName = rcs.ParentResourceEnglishDisplayName,
                    LanguageEnglishDisplay = languageEntityByIdMap[rcs.LanguageId].EnglishDisplay,
                    WordCount = sourceWordCount,
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
                    LastAssignedUser = previouslyAssignedUserId.HasValue
                        ? userDtoByIdMap[previouslyAssignedUserId.Value]
                        : null,
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