using Aquifer.Data.EventHandlers;

namespace Aquifer.Data.Entities;

public class GreekSenseEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }
    public string ExternalId { get; set; } = null!;
    public string DefinitionShort { get; set; } = null!;
    public string Comments { get; set; } = null!;
    public string Entrycode { get; set; } = null!;
    public string Domain { get; set; } = null!;
    public string SubDomain { get; set; } = null!;
    public int GreekLemmaId { get; set; }
    public int? StrongNumberId { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public GreekLemmaEntity GreekLemma { get; set; } = null!;
    public StrongNumberEntity StrongNumber { get; set; } = null!;
    public ICollection<GreekSenseGlossEntity> GreekSenseGlosses { get; set; } = [];

    public ICollection<GreekNewTestamentWordSenseEntity> GreekNewTestamentWordSenses { get; set; } =
        new List<GreekNewTestamentWordSenseEntity>();

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}