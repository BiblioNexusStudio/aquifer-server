using Aquifer.Data.EventHandlers;

namespace Aquifer.Data.Entities;

public class VersificationMappingEntity : IHasUpdatedTimestamp
{
    public int Id { get; private set; }
    public int BibleId { get; set; }
    public int BibleVerseId { get; set; }
    public int BaseVerseId { get; set; }
    public BibleEntity Bible { get; set; } = null!;
    public VerseEntity BibleVerse { get; set; } = null!;
    public VerseEntity BaseVerse { get; set; } = null!;
    public char? VerseIdPart { get; set; }
    public char? BaseVerseIdPart { get; set; }
    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}