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
    : Endpoint<Request, IReadOnlyList<Response>>
{
    public override void Configure()
    {
        Get("/resources/content/assigned-to-own-company");
        Permissions(PermissionName.ReadCompanyContentAssignments);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var user = await _userService.GetUserFromJwtAsync(ct);

        var (_, resourceContentSummaries) = await _resourceContentSearchService.SearchAsync(
            ResourceContentSearchIncludeFlags.Project |
                ResourceContentSearchIncludeFlags.HasAudioForLanguage |
                ResourceContentSearchIncludeFlags.HasUnresolvedCommentThreads,
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
                HasAudio = req.HasAudio,
                HasUnresolvedCommentThreads = req.HasUnresolvedCommentThreads,
            },
            ResourceContentSearchSortOrder.ProjectProjectedDeliveryDate,
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

        // AssignedUserId must be populated because we filtered on AssignedUserCompanyId.
        // Also get previously assigned user IDs.
        var userIdsToFetch = resourceContentSummaries
            .Select(rcs => rcs.ResourceContentVersion!.AssignedUserId!.Value)
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
                // Project should never be null because we filtered to IsInProject = true
                var project = rcs.Project!;
                var resourceContentVersion = rcs.ResourceContentVersion!;

                var lastTwoUserAssignments = lastUserAssignmentsByResourceContentVersionIdMap[resourceContentVersion.Id];
                var previouslyAssignedUserId = lastTwoUserAssignments.Count > 1 ? lastTwoUserAssignments[1].UserId : null;

                return new Response
                {
                    Id = rcs.ResourceContent.Id,
                    EnglishLabel = rcs.Resource.EnglishLabel,
                    ParentResourceName = rcs.ParentResource.DisplayName,
                    LanguageEnglishDisplay = languageEntityByIdMap[rcs.ResourceContent.LanguageId].EnglishDisplay,
                    WordCount = resourceContentVersion.SourceWordCount,
                    StatusValue = rcs.ResourceContent.Status,
                    StatusDisplayName = rcs.ResourceContent.Status.GetDisplayName(),
                    SortOrder = rcs.Resource.SortOrder,
                    ProjectName = project.Name,
                    AssignedUser = userDtoByIdMap[resourceContentVersion.AssignedUserId!.Value],
                    DaysSinceContentUpdated = rcs.ResourceContent.ContentUpdated == null
                        ? null
                        : (DateTime.UtcNow - (DateTime)rcs.ResourceContent.ContentUpdated).Days,
                    DaysUntilProjectDeadline =
                        project.ProjectedDeliveryDate == null
                            ? null
                            : (project.ProjectedDeliveryDate.Value.ToDateTime(new TimeOnly(23, 59)) - DateTime.UtcNow).Days,
                    LastAssignedUser = previouslyAssignedUserId != null
                        ? userDtoByIdMap[previouslyAssignedUserId.Value]
                        : null,
                    HasAudio = rcs.Resource.HasAudioForLanguage!.Value,
                    HasUnresolvedCommentThreads = resourceContentVersion.HasUnresolvedCommentThreads!.Value,
                };
            })
            .ToList();
    }
}