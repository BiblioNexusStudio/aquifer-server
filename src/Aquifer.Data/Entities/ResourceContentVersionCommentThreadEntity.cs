using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[Index(nameof(ResourceContentVersionId))]
[PrimaryKey(nameof(CommentThreadId), nameof(ResourceContentVersionId))]
public class ResourceContentVersionCommentThreadEntity
{
    public int CommentThreadId { get; set; }
    public int ResourceContentVersionId { get; set; }

    public CommentThreadEntity CommentThread { get; set; } = null!;
    public ResourceContentVersionEntity ResourceContentVersion { get; set; } = null!;
}