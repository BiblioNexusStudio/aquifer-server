using Aquifer.Data.EventHandlers;

namespace Aquifer.Data.Entities;
public class GreekNewTestamentWordEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }
    public int GreekSourceId { get; set; }
    public int GreekWordId { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;

    public GreekWordEntity GreekWord { get; set; } = null!;
    public GreekNewTestamentEntity GreekNewTestament { get; set; } = null!;

    public ICollection<NewTestamentAlignmentEntity> NewTestamentAlignments { get; set; } = new List<NewTestamentAlignmentEntity>();
}
