namespace Aquifer.Common.Messages.Models;

public sealed record ScoreResourceContentVersionSimilarityMessage(
    int BaseVersionId,
    int CompareVersionId,
    ScoreResourceContentVersionSimilarityStatus Status,
    ResourceContentVersionSimilarityComparisonTypes ComparisonType
);