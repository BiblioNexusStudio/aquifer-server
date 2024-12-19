using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[PrimaryKey(nameof(BaseVersionId),
        nameof(ComparedVersionId))]
public class ResourceContentVersionSimilarityScore
{
    public int BaseVersionId { get; set; }
    public ResourceContentVersionTypes BaseVersionType { get; set; }
    public int ComparedVersionId { get; set; }
    public ResourceContentVersionTypes ComparedVersionType { get; set; }
    public ResourceContentStatus Status { get; set; }
    public double SimilarityScore { get; set; }
    
    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;
}

public enum ResourceContentVersionTypes
{
    Base,
    MachineTranslation,
    Snapshot,
}