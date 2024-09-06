using Aquifer.Data.Enums;
using Aquifer.Data.EventHandlers;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[PrimaryKey(nameof(BibleId), nameof(BookId))]
public class BibleBookContentEntity : IHasUpdatedTimestamp
{
    public int BibleId { get; set; }
    public BookId BookId { get; set; }
    public string DisplayName { get; set; } = null!;
    public string? AudioUrls { get; set; } // JSON
    public int TextSize { get; set; }
    public int AudioSize { get; set; }
    public int ChapterCount { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public BibleEntity Bible { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}