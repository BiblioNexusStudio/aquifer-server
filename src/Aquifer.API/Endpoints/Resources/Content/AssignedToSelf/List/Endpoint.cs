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

    /// ---------------------------------------------------------------------------------------------------
    /// ---------------------------------------------------------------------------------------------------
    /// IMPORTANT: WHEN THIS SEARCH FILTER IS UPDATED THE /next-up ENDPOINT NEEDS TO BE UPDATED ACCORDINGLY
    /// ---------------------------------------------------------------------------------------------------
    /// ---------------------------------------------------------------------------------------------------
    public override async Task HandleAsync(CancellationToken ct)
    {
        var user = await _userService.GetUserFromJwtAsync(ct);

        var (_, resourceContentSummaries) = await _resourceContentSearchService.SearchAsync(
            ResourceContentSearchIncludeFlags.Project |
                ResourceContentSearchIncludeFlags.HasAudioForLanguage |
                ResourceContentSearchIncludeFlags.HasUnresolvedCommentThreads,
            new ResourceContentSearchFilter
            {
                IsDraft = true,
                AssignedUserId = user.Id,
                ExcludeContentStatuses =
                [
                    ResourceContentStatus.TranslationAiDraftComplete,
                    ResourceContentStatus.AquiferizeAiDraftComplete,
                ],
            },
            ResourceContentSearchSortOrder.ResourceContentId,
            offset: 0,
            limit: null,
            ct);

        var languageEntityByIdMap = await _cachingLanguageService.GetLanguageEntityByIdMapAsync(ct);

        // ResourceContentVersion is populated by default, and we didn't filter it out.
        var lastUserAssignmentsByResourceContentVersionIdMap =
            await Helpers.GetLastUserAssignmentsByResourceContentVersionIdMapAsync(
                resourceContentSummaries.Select(rcs => rcs.ResourceContentVersion!.Id),
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
                // Project may be null
                var project = rcs.Project;
                var resourceContentVersion = rcs.ResourceContentVersion!;

                // The most recent user assignment history should be to assign to the current user.
                var lastTwoUserAssignments =
                    lastUserAssignmentsByResourceContentVersionIdMap[resourceContentVersion.Id];
                var currentUserAssignment = lastTwoUserAssignments[0];
                if (currentUserAssignment.UserId != user.Id)
                {
                    throw new InvalidOperationException(
                        $"Expected the currently assigned user to be the last assigned user, but the current user ID is \"{user.Id}\" and the last assigned user is \"{currentUserAssignment.UserId}\".");
                }

                var previouslyAssignedUserId = lastTwoUserAssignments.Count > 1 ? lastTwoUserAssignments[1].UserId : null;

                return new Response
                {
                    Id = rcs.ResourceContent.Id,
                    EnglishLabel = rcs.Resource.EnglishLabel,
                    ParentResourceName = rcs.ParentResource.DisplayName,
                    LanguageEnglishDisplay = languageEntityByIdMap[rcs.ResourceContent.LanguageId].EnglishDisplay,
                    WordCount = resourceContentVersion.SourceWordCount,
                    StatusValue = rcs.ResourceContent.Status,
                    Status = rcs.ResourceContent.Status.GetDisplayName(),
                    StatusDisplayName = rcs.ResourceContent.Status.GetDisplayName(),
                    SortOrder = rcs.Resource.SortOrder,
                    ProjectName = project?.Name,
                    DaysSinceAssignment = (DateTime.UtcNow - currentUserAssignment.Created).Days,
                    DaysUntilProjectDeadline = project?.ProjectedDeliveryDate == null
                        ? null
                        : (project.ProjectedDeliveryDate.Value.ToDateTime(new TimeOnly(23, 59)) - DateTime.UtcNow).Days,
                    DaysSinceContentUpdated = rcs.ResourceContent.ContentUpdated == null
                        ? null
                        : (DateTime.UtcNow - (DateTime)rcs.ResourceContent.ContentUpdated).Days,
                    LastAssignedUser = previouslyAssignedUserId.HasValue
                        ? userDtoByIdMap[previouslyAssignedUserId.Value]
                        : null,
                    HasAudio = rcs.Resource.HasAudioForLanguage!.Value,
                    HasUnresolvedCommentThreads = resourceContentVersion.HasUnresolvedCommentThreads!.Value,
                };
            })
            // There's no support yet for sorting by days since assignment in the search service so we have to fetch everything and
            // post-sort. Probably the closest thing would be to sort on the ResourceContent's Updated date.
            .OrderByDescending(x => x.DaysSinceAssignment)
            .ThenBy(x => x.ProjectName)
            .ThenBy(x => x.ParentResourceName)
            .ThenBy(x => x.SortOrder)
            .ThenBy(x => x.EnglishLabel)
            .ToList();
    }
}