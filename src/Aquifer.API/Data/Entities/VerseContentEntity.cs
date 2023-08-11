using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Data.Entities;

[PrimaryKey(nameof(VerseId), nameof(BibleId))]
public class VerseContentEntity
{
    public int VerseId { get; set; }
    public int BibleId { get; set; }
    public string Content { get; set; } = null!;
    public float? AudioStartTime { get; set; }
    public float? AudioEndTime { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;

    public BibleEntity Bible { get; set; } = null!;
    public VerseEntity Verse { get; set; } = null!;
}