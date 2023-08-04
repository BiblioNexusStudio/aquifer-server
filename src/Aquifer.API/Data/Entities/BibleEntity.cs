namespace Aquifer.API.Data.Entities;

public class BibleEntity
{
    public int Id { get; set; }
    public int LanguageId { get; set; }
    public BibleType Type { get; set; }
    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;
    [SqlDefaultValue("getutcdate()")] 
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}

public enum BibleType
{
    BSB = 1,
}
