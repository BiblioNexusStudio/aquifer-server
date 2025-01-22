using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Common.Services;
using Aquifer.Common.Services.Caching;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.ToAssign.List;

public class Endpoint(
    AquiferDbContext _dbContext,
    IUserService _userService,
    IResourceContentSearchService _resourceContentSearchService,
    ICachingLanguageService _cachingLanguageService)
    : EndpointWithoutRequest<IReadOnlyList<Response>>
{
    public override void Configure()
    {
        Get("/resources/content/to-assign");
        Permissions(PermissionName.AssignOverride);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var user = await _userService.GetUserFromJwtAsync(ct);

        var (_, resourceContentSummaries) = await _resourceContentSearchService.SearchAsync(
            new ResourceContentSearchFilter
            {
                IsDraft = true,
                IsInProject = true,
                AssignedUserId = user.Id,
                IncludeContentStatuses = [ResourceContentStatus.New, ResourceContentStatus.TranslationAiDraftComplete, ResourceContentStatus.AquiferizeAiDraftComplete],
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

        var resources = resourceContentSummaries
            .Select(rcs =>
            {
                // ProjectId should never be null because we filtered to IsInProject = true
                var project = projectByIdMap[rcs.ProjectId!.Value];

                // Because we filtered to InDraft = true, we can assume that the LatestResourceContentVersionId is the Draft version.
                var draftResourceContentVersion = latestResourceContentVersionByIdMap[rcs.LatestResourceContentVersionId];

                return new Response
                {
                    Id = rcs.Id,
                    EnglishLabel = rcs.ResourceEnglishLabel,
                    ParentResourceName = rcs.ParentResourceEnglishDisplayName,
                    LanguageEnglishDisplay = languageEntityByIdMap[rcs.LanguageId].EnglishDisplay,
                    WordCount = draftResourceContentVersion.SourceWordCount,
                    ProjectName = project.Name,
                    ProjectProjectedDeliveryDate = project.ProjectedDeliveryDate,
                    SortOrder = rcs.ResourceSortOrder,
                    HasAudio = rcs.HasAudio,
                    HasUnresolvedCommentThreads = rcs.HasUnresolvedCommentThreads,
                };
            })
            .ToList();

        await SendOkAsync(resources, ct);
    }
}