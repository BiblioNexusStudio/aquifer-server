using Aquifer.Data.EventHandlers;

namespace Aquifer.Data.Entities;

public class CommentThreadEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }
    public bool Resolved { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; }

    public ICollection<CommentEntity> ResourceComments { get; set; } = [];

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; }
}