namespace Aquifer.Data.Entities;

public class VersificationExclusionEntity
{
    public int Id { get; set; }
    public int BibleId { get; set; }
    public int BibleVerseId { get; set; }
    public BibleEntity Bible { get; set; } = null!;
    public VerseEntity Verse { get; set; } = null!;
    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;
}