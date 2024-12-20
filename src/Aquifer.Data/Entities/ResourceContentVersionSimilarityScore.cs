namespace Aquifer.Data.Entities;

public class ResourceContentVersionSimilarityScore
{
    public int Id { get; set; }
    public int BaseVersionId { get; set; }
    public ResourceContentVersionTypes BaseVersionType { get; set; }
    public int ComparedVersionId { get; set; }
    public ResourceContentVersionTypes ComparedVersionType { get; set; }
    public double SimilarityScore { get; set; }
    
    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;
}

public enum ResourceContentVersionTypes
{
    None = 0,
    Base = 1,
    MachineTranslation = 2,
    Snapshot = 3,
}