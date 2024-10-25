using Aquifer.Data.EventHandlers;

namespace Aquifer.Data.Entities;
public class GreekLemmaEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public int? StrongNumberId { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;

    public ICollection<GreekWordEntity> GreekWords { get; set; } = [];
    public StrongNumberEntity StrongNumber { get; set; } = null!;
}
