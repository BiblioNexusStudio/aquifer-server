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
    None = 0,
    Basic = 1,
    Advanced = 2
}

public enum ResourceType
{
    None = 0,
    Guide = 1,
    Dictionary = 2,
    StudyNotes = 3,
    Images = 4,
    Videos = 5
}