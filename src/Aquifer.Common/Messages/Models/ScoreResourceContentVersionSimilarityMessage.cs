namespace Aquifer.Common.Messages.Models;

public sealed record ScoreResourceContentVersionSimilarityMessage(
    ResourceContentVersionSimilarityComparisonType ComparisonType,
    int BaseVersionId,
    int? CompareVersionId = null);