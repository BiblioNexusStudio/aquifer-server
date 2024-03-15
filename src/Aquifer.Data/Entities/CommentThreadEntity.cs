using Aquifer.Data.EventHandlers;

namespace Aquifer.Data.Entities;

public class CommentThreadEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }
    public bool Resolved { get; set; }
    public int ResolvedByUserId { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; }

    public ICollection<CommentEntity> Comments { get; set; } = [];
    public UserEntity? ResolvedByUser { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; }
}