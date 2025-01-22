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
                IsInProject = true,
                AssignedUserCompanyId = user.CompanyId,
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
            limit: int.MaxValue,
            ct);

        var languageEntityByIdMap = await _cachingLanguageService.GetLanguageEntityByIdMapAsync(ct);

        var projectByIdMap = await _dbContext.Projects
            .Where(p => resourceContentSummaries.Select(rcs => rcs.ProjectId).Distinct().Contains(p.Id))
            .ToDictionaryAsync(p => p.Id, ct);

        var latestResourceContentVersionByIdMap = await _dbContext.ResourceContentVersions
            .Where(rcv => resourceContentSummaries.Select(rcs => rcs.LatestResourceContentVersionId).Contains(rcv.Id))
            .ToDictionaryAsync(rcv => rcv.Id, ct);

        // AssignedUserId must be populated because we filtered on AssignedUserCompanyId.
        var userByIdMap = await _dbContext.Users
            .Where(u => resourceContentSummaries.Select(rcs => rcs.AssignedUserId!.Value).Distinct().Contains(u.Id))
            .ToDictionaryAsync(u => u.Id, ct);

        var lastUserAssignmentsByResourceContentVersionIdMap =
            await Helpers.GetLastUserAssignmentsByResourceContentVersionIdMapAsync(
                latestResourceContentVersionByIdMap.Keys,
                numberOfAssignments: 2,
                _dbContext,
                ct);

        Response = resourceContentSummaries
            .Select(rcs =>
            {
                // ProjectId should never be null because we filtered to IsInProject = true
                var project = projectByIdMap[rcs.ProjectId!.Value];

                // Because we filtered by IsDraft we know that the LatestResourceContentVersionId is the Draft version.
                var draftResourceContentVersion = latestResourceContentVersionByIdMap[rcs.LatestResourceContentVersionId];

                var lastTwoUserAssignments = lastUserAssignmentsByResourceContentVersionIdMap[draftResourceContentVersion.Id];
                var previouslyAssignedUser = lastTwoUserAssignments.Count > 1 ? lastTwoUserAssignments[1].User : null;

                return new Response
                {
                    Id = rcs.Id,
                    EnglishLabel = rcs.ResourceEnglishLabel,
                    ParentResourceName = rcs.ParentResourceEnglishDisplayName,
                    LanguageEnglishDisplay = languageEntityByIdMap[rcs.LanguageId].EnglishDisplay,
                    WordCount = draftResourceContentVersion.SourceWordCount,
                    StatusValue = rcs.Status,
                    StatusDisplayName = rcs.Status.GetDisplayName(),
                    SortOrder = rcs.ResourceSortOrder,
                    ProjectName = project.Name,
                    AssignedUser = UserDto.FromUserEntity(userByIdMap[rcs.AssignedUserId!.Value])!,
                    DaysSinceContentUpdated = rcs.ContentUpdated == null ? null : (DateTime.UtcNow - (DateTime)rcs.ContentUpdated).Days,
                    DaysUntilProjectDeadline =
                        project.ProjectedDeliveryDate == null
                            ? null
                            : (project.ProjectedDeliveryDate.Value.ToDateTime(new TimeOnly(23, 59)) - DateTime.UtcNow).Days,
                    LastAssignedUser = previouslyAssignedUser,
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