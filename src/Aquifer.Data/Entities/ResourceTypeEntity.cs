namespace Aquifer.Data.Entities;

public class ResourceTypeEntity
{
    public int Id { get; set; }
    public string ShortName { get; set; } = null!;
    public string DisplayName { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}