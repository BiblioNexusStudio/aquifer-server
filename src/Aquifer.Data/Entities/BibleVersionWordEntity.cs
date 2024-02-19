using Aquifer.Data.EventHandlers;

namespace Aquifer.Data.Entities;
public class BibleVersionWordEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }
    public int BibleId { get; set; }
    public int WordIdentifier { get; set; }
    public int BibleWordId { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;

    public BibleEntity Bible { get; set; } = null!;

    public ICollection<NewTestamentAlignmentEntity> NewTestamentAlignments { get; set; } = new List<NewTestamentAlignmentEntity>();
}
