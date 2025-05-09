using Aquifer.Common.Services;
using Aquifer.Common.Utilities;
using Aquifer.Data.Entities;
using FastEndpoints;

namespace Aquifer.API.Endpoints.Resources.Untranslated.List;

public class Endpoint(IResourceContentSearchService _resourceContentSearchService)
    : Endpoint<Request, IReadOnlyList<Response>>
{
    private static readonly IReadOnlySet<ResourceContentStatus> s_aquiferizationResourceContentStatuses = new HashSet<ResourceContentStatus>
    {
        ResourceContentStatus.AquiferizeEditorReview,
        ResourceContentStatus.AquiferizePublisherReview,
        ResourceContentStatus.AquiferizeReviewPending,
    };

    public override void Configure()
    {
        Get("/resources/untranslated");
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var verseIdRanges = BibleUtilities.VerseRangesForBookAndChapters(request.BookCode, request.Chapters);

        // search for untranslated resources
        var (_, resourceContentSummaries) = await _resourceContentSearchService.SearchAsync(
            ResourceContentSearchIncludeFlags.None,
            new ResourceContentSearchFilter
            {
                IncludeContentMediaTypes = [ResourceContentMediaType.Text],
                IsPublished = true,
                IsTranslated = false,
                TranslationSourceLanguageId = request.SourceLanguageId,
                LanguageId = request.TargetLanguageId,
                ParentResourceId = request.ParentResourceId,
                ResourceEnglishLabelQuery = request.SearchQuery,
                VerseIdRanges = verseIdRanges.Select(vir => new VerseIdRange(vir)).ToList(),
            },
            ResourceContentSearchSortOrder.ParentResourceAndResourceName,
            0,
            null,
            ct);

        var response = resourceContentSummaries
            .Select(rcs => new Response
            {
                ResourceId = rcs.Resource.Id,
                Title = rcs.Resource.EnglishLabel,
                SortOrder = rcs.Resource.SortOrder,
                WordCount = rcs.ResourceContentVersion!.WordCount ?? 0,
                IsBeingAquiferized = s_aquiferizationResourceContentStatuses.Contains(rcs.ResourceContent.Status),
            })
            .ToList();

        await SendOkAsync(response, ct);
    }
}