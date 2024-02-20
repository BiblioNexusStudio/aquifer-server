using Aquifer.Data.EventHandlers;

namespace Aquifer.Data.Entities;

public class GreekWordEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }
    public string Word { get; set; } = null!;
    public int? StrongNumber { get; set; }
    public string? Lemma { get; set; } = null!;
    public string? GrammarType { get; set; } = null!;
    public string? Sense { get; set; } = null!;
    public string? UsageCode { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;

    public ICollection<GreekNewTestamentWordEntity> GreekNewTestamentWords { get; set; } = new List<GreekNewTestamentWordEntity>();
}
