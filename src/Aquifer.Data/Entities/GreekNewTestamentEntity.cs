using Aquifer.Data.EventHandlers;

namespace Aquifer.Data.Entities;

public class GreekNewTestamentEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public ICollection<GreekNewTestamentWordEntity> GreekNewTestamentWords { get; set; } = [];

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}