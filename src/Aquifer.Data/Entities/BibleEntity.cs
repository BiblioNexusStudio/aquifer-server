namespace Aquifer.Data.Entities;

public class BibleEntity
{
    public int Id { get; set; }
    public int LanguageId { get; set; }
    public string Name { get; set; } = null!;
    public string Abbreviation { get; set; } = null!;
    public bool Enabled { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;

    public ICollection<BibleBookContentEntity> BibleBookContents { get; set; } = new List<BibleBookContentEntity>();
}