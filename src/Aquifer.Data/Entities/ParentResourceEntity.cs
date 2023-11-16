namespace Aquifer.Data.Entities;

public class ParentResourceEntity
{
    public int Id { get; set; }
    public string ShortName { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public string? LicenseInfo { get; set; }
    public ResourceTypeComplexityLevel ComplexityLevel { get; set; }
    public ResourceType ResourceType { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}

public enum ResourceTypeComplexityLevel
{
    None = 0
}

public enum ResourceType
{
    None = 0,
    Dictionary = 1,
    StudyNotes = 2,
    Images = 3,
    Videos = 4
}
