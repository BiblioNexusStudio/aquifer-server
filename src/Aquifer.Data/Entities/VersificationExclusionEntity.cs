using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[PrimaryKey(nameof(BibleId), nameof(BibleVerseId))]
public class VersificationExclusionEntity
{
    public int BibleId { get; set; }
    public int BibleVerseId { get; set; }
    public BibleEntity Bible { get; set; } = null!;
    public VerseEntity BibleVerse { get; set; } = null!;
    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;
}