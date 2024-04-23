namespace Aquifer.Data.Entities;

public class BibleBookChapterEntity
{
    public int Id { get; set; }
    public int BibleBookId { get; set; }
    public int Number { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public BibleBookEntity BibleBook { get; set; } = null!;
    public ICollection<BibleBookChapterVerseEntity> Verses { get; set; } = [];
}