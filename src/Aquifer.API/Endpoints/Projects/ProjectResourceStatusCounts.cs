using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Projects;

public class ProjectResourceStatusCounts
{
    private readonly List<ResourceContentStatus> _inManagerReviewStatuses =
    [
        ResourceContentStatus.AquiferizeManagerReview,
        ResourceContentStatus.TranslationManagerReview
    ];

    private readonly List<ResourceContentStatus> _inProgressStatuses =
    [
        ResourceContentStatus.AquiferizeInProgress,
        ResourceContentStatus.TranslationInProgress
    ];

    private readonly List<ResourceContentStatus> _inPublisherReviewStatuses =
    [
        ResourceContentStatus.AquiferizePublisherReview,
        ResourceContentStatus.AquiferizeReviewPending,
        ResourceContentStatus.TranslationPublisherReview,
        ResourceContentStatus.TranslationReviewPending
    ];

    private readonly List<ResourceContentStatus> _notStartedStatuses =
    [
        ResourceContentStatus.New,
        ResourceContentStatus.TranslationNotStarted
    ];

    public ProjectResourceStatusCounts(ICollection<ResourceContentEntity> resourceContents)
    {
        NotStarted = resourceContents.Count(rc => _notStartedStatuses.Contains(rc.Status));
        InProgress = resourceContents.Count(rc => _inProgressStatuses.Contains(rc.Status));
        InManagerReview = resourceContents.Count(rc => _inManagerReviewStatuses.Contains(rc.Status));
        InPublisherReview = resourceContents.Count(rc => _inPublisherReviewStatuses.Contains(rc.Status));
        Completed = resourceContents.Count(rc => rc.Status == ResourceContentStatus.Complete);
    }

    public int NotStarted { get; private init; }
    public int InProgress { get; private init; }
    public int InManagerReview { get; private init; }
    public int InPublisherReview { get; private init; }
    public int Completed { get; private init; }
}