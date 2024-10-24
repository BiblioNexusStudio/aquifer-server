using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Projects;

public class ProjectResourceStatusCounts
{
    internal static readonly List<ResourceContentStatus> InManagerReviewStatuses =
    [
        ResourceContentStatus.AquiferizeManagerReview,
        ResourceContentStatus.TranslationManagerReview
    ];

    internal static readonly List<ResourceContentStatus> InProgressStatuses =
    [
        ResourceContentStatus.AquiferizeInProgress,
        ResourceContentStatus.TranslationInProgress
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
        ResourceContentStatus.TranslationNotStarted
    ];

    internal static readonly List<ResourceContentStatus> CompletedStatuses =
    [
        ResourceContentStatus.Complete, ResourceContentStatus.CompleteNotApplicable
    ];

    public int NotStarted { get; init; }
    public int InProgress { get; init; }
    public int InManagerReview { get; init; }
    public int InPublisherReview { get; init; }
    public int Completed { get; init; }
}