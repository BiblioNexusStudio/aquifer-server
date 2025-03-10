using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Projects;

public class ProjectResourceStatusCounts
{
    internal static readonly List<ResourceContentStatus> InCompanyReviewStatuses =
    [
        ResourceContentStatus.AquiferizeCompanyReview,
        ResourceContentStatus.TranslationCompanyReview
    ];

    internal static readonly List<ResourceContentStatus> EditorReviewStatuses =
    [
        ResourceContentStatus.AquiferizeEditorReview,
        ResourceContentStatus.TranslationEditorReview
    ];

    internal static readonly List<ResourceContentStatus> InPublisherReviewStatuses =
    [
        ResourceContentStatus.AquiferizePublisherReview,
        ResourceContentStatus.AquiferizeReviewPending,
        ResourceContentStatus.TranslationPublisherReview,
        ResourceContentStatus.TranslationReviewPending,
        ResourceContentStatus.TranslationNotApplicable
    ];

    internal static readonly List<ResourceContentStatus> NotStartedStatuses =
    [
        ResourceContentStatus.New,
        ResourceContentStatus.TranslationAwaitingAiDraft,
        ResourceContentStatus.TranslationAiDraftComplete,
        ResourceContentStatus.AquiferizeAwaitingAiDraft,
        ResourceContentStatus.AquiferizeAiDraftComplete
    ];

    internal static readonly List<ResourceContentStatus> CompletedStatuses =
    [
        ResourceContentStatus.Complete, ResourceContentStatus.CompleteNotApplicable
    ];

    internal static readonly List<ResourceContentStatus> RemainingStatuses =
    [
        ResourceContentStatus.New,
        ResourceContentStatus.TranslationAwaitingAiDraft,
        ResourceContentStatus.AquiferizeAwaitingAiDraft,
        ResourceContentStatus.AquiferizeAiDraftComplete,
        ResourceContentStatus.AquiferizeEditorReview,
        ResourceContentStatus.TranslationEditorReview,
        ResourceContentStatus.AquiferizeCompanyReview,
        ResourceContentStatus.TranslationCompanyReview
    ];

    public ProjectResourceStatusCounts() { }

    public ProjectResourceStatusCounts(IReadOnlyList<(ResourceContentStatus Status, int? WordCount)> resourceData)
    {
        NotStarted = resourceData.Where(x => NotStartedStatuses.Contains(x.Status)).Sum(x => x.WordCount ?? 0);
        EditorReview = resourceData.Where(x => EditorReviewStatuses.Contains(x.Status)).Sum(x => x.WordCount ?? 0);
        InCompanyReview = resourceData.Where(x => InCompanyReviewStatuses.Contains(x.Status)).Sum(x => x.WordCount ?? 0);
        InPublisherReview = resourceData.Where(x => InPublisherReviewStatuses.Contains(x.Status)).Sum(x => x.WordCount ?? 0);
        Completed = resourceData.Where(x => CompletedStatuses.Contains(x.Status)).Sum(x => x.WordCount ?? 0);
        Remaining = resourceData.Where(x => RemainingStatuses.Contains(x.Status)).Sum(x => x.WordCount ?? 0);
    }
    public int NotStarted { get; init; }
    public int EditorReview { get; init; }
    public int InCompanyReview { get; init; }
    public int InPublisherReview { get; init; }
    public int Completed { get; init; }
    public int Remaining { get; init; }
}