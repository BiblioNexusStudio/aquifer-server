using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[PrimaryKey(nameof(BookChapterId), nameof(ResourceId))]
[Index(nameof(ResourceId))]
public class BookChapterResourceEntity
{
    public int BookChapterId { get; set; }
    public int ResourceId { get; set; }

    public BookChapterEntity BookChapter { get; set; } = null!;
    public ResourceEntity Resource { get; set; } = null!;
}