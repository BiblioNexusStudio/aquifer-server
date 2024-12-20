namespace Aquifer.Common.Messages.Models;

public sealed record ScoreResourceContentVersionSimilarityMessage(
    int BaseVersionId,
    int CompareVersionId,
    ResourceContentVersionSimilarityComparisonType ComparisonType
);