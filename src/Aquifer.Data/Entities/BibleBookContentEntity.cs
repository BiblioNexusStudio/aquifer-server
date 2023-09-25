using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[PrimaryKey(nameof(BibleId), nameof(BookId))]
public class BibleBookContentEntity
{
    public int BibleId { get; set; }
    public int BookId { get; set; }
    public string DisplayName { get; set; } = null!;
    public string TextUrl { get; set; } = null!;
    public string AudioUrls { get; set; } = null!; // JSON
    public int TextSize { get; set; }
    public int AudioSize { get; set; }
    public int ChapterCount { get; set; }

    [SqlDefaultValue("getutcdate()")] public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")] public DateTime Updated { get; set; } = DateTime.UtcNow;

    public BibleEntity Bible { get; set; } = null!;
}