using Aquifer.Data.EventHandlers;

namespace Aquifer.Data.Entities;

public class NewTestamentAlignmentEntity : IHasUpdatedTimestamp
{
    public int BibleVersionWordId { get; set; }
    public int GreekSourceWordId { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;

    public BibleVersionWordEntity BibleVersionWord { get; set; } = null!;
    public GreekNewTestamentWordEntity GreekNewTestamentWord { get; set; } = null!;
}
