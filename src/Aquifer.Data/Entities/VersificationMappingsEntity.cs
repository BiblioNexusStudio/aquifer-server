namespace Aquifer.Data.Entities;

public class VersificationMappingsEntity
{
    public int VerseId {get; set;}
    public int BibleId { get; set; }
    public int MappedVerseId { get; set; }
    public VerseEntity Verse {get; set;} = null!;
    public BibleEntity Bible { get; set; } = null!;
    public VerseEntity MappedVerse { get; set; } = null!;
}