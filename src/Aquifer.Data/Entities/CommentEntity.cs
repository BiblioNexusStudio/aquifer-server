using Aquifer.Data.EventHandlers;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[Index(nameof(ThreadId))]
public class CommentEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }
    public int ThreadId { get; set; }
    public int UserId { get; set; }
    public string Comment { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; }

    public CommentThreadEntity Thread { get; set; } = null!;
    public UserEntity User { get; set; } = null!;
    public ICollection<CommentMentionEntity> Mentions { get; set; } = [];

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; }
}