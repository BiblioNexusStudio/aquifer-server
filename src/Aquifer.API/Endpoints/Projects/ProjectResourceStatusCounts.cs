﻿using Aquifer.Data.Entities;

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

    public int NotStarted { get; init; }
    public int EditorReview { get; init; }
    public int InCompanyReview { get; init; }
    public int InPublisherReview { get; init; }
    public int Completed { get; init; }
}