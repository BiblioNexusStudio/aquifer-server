using Aquifer.API.Common;
using Aquifer.API.Common.Dtos;
using Aquifer.API.Services;
using Aquifer.Common.Extensions;
using Aquifer.Common.Services.Caching;
using Aquifer.Common.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.AssignedToOwnCompany.List;

public class Endpoint(
    AquiferDbContext _dbContext,
    IUserService _userService,
    IResourceContentSearchService _resourceContentSearchService,
    ICachingLanguageService _cachingLanguageService)
    : EndpointWithoutRequest<IReadOnlyList<Response>>
{
    public override void Configure()
    {
        Get("/resources/content/assigned-to-own-company");
        Permissions(PermissionName.ReadCompanyContentAssignments);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var user = await _userService.GetUserFromJwtAsync(ct);

        var (_, resourceContentSummaries) = await _resourceContentSearchService.SearchAsync(
            new ResourceContentSearchFilter
            {
                IsDraft = true,
                AssignedUserCompanyId = user.CompanyId,
                IsInProject = true,
                IncludeContentStatuses =
                [
                    ResourceContentStatus.New,
                    ResourceContentStatus.AquiferizeAiDraftComplete,
                    ResourceContentStatus.AquiferizeCompanyReview,
                    ResourceContentStatus.AquiferizeEditorReview,
                    ResourceContentStatus.TranslationAiDraftComplete,
                    ResourceContentStatus.TranslationCompanyReview,
                    ResourceContentStatus.TranslationEditorReview,
                ],
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

        // AssignedUserId must be populated because we filtered on AssignedUserCompanyId.
        // Also get previously assigned user IDs.
        var userIdsToFetch = resourceContentSummaries
            .Select(rcs => rcs.AssignedUserId!.Value)
            .Concat(lastUserAssignmentsByResourceContentVersionIdMap.Values
                .Select(x => x.Count > 1 ? x[1].UserId : null)
                .OfType<int>())
            .Distinct()
            .ToList();

        var userDtoByIdMap = await _dbContext.Users
            .Where(u => userIdsToFetch.Contains(u.Id))
            .Select(u => new UserDto(u.Id, u.FirstName, u.LastName))
            .ToDictionaryAsync(u => u.Id, ct);

        Response = resourceContentSummaries
            .Select(rcs =>
            {
                // ProjectId should never be null because we filtered to IsInProject = true
                var project = projectByIdMap[rcs.ProjectId!.Value];

                // Because we filtered by IsDraft we know that the LatestResourceContentVersionId is the Draft version.
                var sourceWordCount = sourceWordCountByResourceContentVersionIdMap[rcs.LatestResourceContentVersionId];

                var lastTwoUserAssignments = lastUserAssignmentsByResourceContentVersionIdMap[rcs.LatestResourceContentVersionId];
                var previouslyAssignedUserId = lastTwoUserAssignments.Count > 1 ? lastTwoUserAssignments[1].UserId : null;

                return new Response
                {
                    Id = rcs.Id,
                    EnglishLabel = rcs.ResourceEnglishLabel,
                    ParentResourceName = rcs.ParentResourceEnglishDisplayName,
                    LanguageEnglishDisplay = languageEntityByIdMap[rcs.LanguageId].EnglishDisplay,
                    WordCount = sourceWordCount,
                    StatusValue = rcs.Status,
                    StatusDisplayName = rcs.Status.GetDisplayName(),
                    SortOrder = rcs.ResourceSortOrder,
                    ProjectName = project.Name,
                    AssignedUser = userDtoByIdMap[rcs.AssignedUserId!.Value],
                    DaysSinceContentUpdated = rcs.ContentUpdated == null ? null : (DateTime.UtcNow - (DateTime)rcs.ContentUpdated).Days,
                    DaysUntilProjectDeadline =
                        project.ProjectedDeliveryDate == null
                            ? null
                            : (project.ProjectedDeliveryDate.Value.ToDateTime(new TimeOnly(23, 59)) - DateTime.UtcNow).Days,
                    LastAssignedUser = previouslyAssignedUserId != null
                        ? userDtoByIdMap[previouslyAssignedUserId.Value]
                        : null,
                    HasAudio = rcs.HasAudio,
                    HasUnresolvedCommentThreads = rcs.HasUnresolvedCommentThreads,
                };
            })
            .OrderBy(x => x.DaysUntilProjectDeadline)
            .ThenBy(x => x.ProjectName)
            .ThenBy(x => x.ParentResourceName)
            .ThenBy(x => x.SortOrder)
            .ThenBy(x => x.EnglishLabel)
            .ToList();
    }
}