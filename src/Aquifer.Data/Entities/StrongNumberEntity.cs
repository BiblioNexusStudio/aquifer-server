using Aquifer.Data.EventHandlers;

namespace Aquifer.Data.Entities;
public class StrongNumberEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }
    public string Value { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;

    public ICollection<GreekLemmaEntity> GreekLemmas { get; set; } = [];
}
