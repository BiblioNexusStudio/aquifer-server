using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Common.Services;
using Aquifer.Common.Services.Caching;
using Aquifer.Data.Entities;
using FastEndpoints;

namespace Aquifer.API.Endpoints.Resources.Content.ToAssign.List;

public class Endpoint(
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
             ResourceContentSearchIncludeFlags.Project |
                 ResourceContentSearchIncludeFlags.HasAudioForLanguage |
                 ResourceContentSearchIncludeFlags.HasUnresolvedCommentThreads,
            new ResourceContentSearchFilter
            {
                IsDraft = true,
                IsInProject = true,
                AssignedUserId = user.Id,
                IncludeContentStatuses =
                [
                    ResourceContentStatus.New,
                    ResourceContentStatus.TranslationAiDraftComplete,
                    ResourceContentStatus.AquiferizeAiDraftComplete,
                ],
            },
            ResourceContentSearchSortOrder.ResourceContentId,
            offset: 0,
            limit: null,
            ct);

        var languageEntityByIdMap = await _cachingLanguageService.GetLanguageEntityByIdMapAsync(ct);

        var resources = resourceContentSummaries
            .Select(rcs =>
            {
                // Project should never be null because we filtered to IsInProject = true
                var project = rcs.Project!;
                var resourceContentVersion = rcs.ResourceContentVersion!;

                return new Response
                {
                    Id = rcs.ResourceContent.Id,
                    EnglishLabel = rcs.Resource.EnglishLabel,
                    ParentResourceName = rcs.ParentResource.DisplayName,
                    LanguageEnglishDisplay = languageEntityByIdMap[rcs.ResourceContent.LanguageId].EnglishDisplay,
                    WordCount = resourceContentVersion.SourceWordCount,
                    ProjectName = project.Name,
                    ProjectProjectedDeliveryDate = project.ProjectedDeliveryDate,
                    SortOrder = rcs.Resource.SortOrder,
                    HasAudio = rcs.Resource.HasAudioForLanguage!.Value,
                    HasUnresolvedCommentThreads = resourceContentVersion.HasUnresolvedCommentThreads!.Value,
                };
            })
            .ToList();

        await SendOkAsync(resources, ct);
    }
}