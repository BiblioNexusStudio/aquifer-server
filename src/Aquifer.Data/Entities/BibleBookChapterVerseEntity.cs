namespace Aquifer.Data.Entities;

public class BibleBookChapterVerseEntity
{
    public int Id { get; set; }
    public int BibleBookChapterId { get; set; }
    public int Number { get; set; }
    public string Text { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public BibleBookChapterEntity BibleBookChapter { get; set; } = null!;
}