namespace Aquifer.Common.Messages.Models;

public enum ScoreResourceContentVersionSimilarityStatus
{
    None = 0,
    Publish = 1,
    EditorReview = 2,
    PublisherReview = 3,
    CompanyReview = 4, //?
}

public enum ResourceContentVersionSimilarityComparisonTypes
{
    MachineTranslationToResourceContentVersion,
    MachineTranslationToSnapshot,
    ResourceContentVersionToSnapshot,
    SnapshotToSnapshot,
}