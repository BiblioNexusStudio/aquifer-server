using System.ComponentModel.DataAnnotations.Schema;

namespace Aquifer.Data.Entities;

public class ResourceTypeEntity
{
    public int Id { get; set; }
    public string ShortName { get; set; }
    public string DisplayName { get; set; } = null!;
    public ResourceTypeComplexityLevel ComplexityLevel { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}

public enum ResourceTypeComplexityLevel
{
    None = 0
}