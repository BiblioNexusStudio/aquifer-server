using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[Index(nameof(ResourceId), nameof(LanguageId), IsUnique = true)]
public class ResourceContentEntity
{
    public int Id { get; set; }
    public int ResourceId { get; set; }
    public int LanguageId { get; set; }
    public string DisplayName { get; set; } = null!;
    public string? Summary { get; set; }
    public int Version { get; set; }
    public bool Completed { get; set; }
    public bool Trusted { get; set; }
    public string Content { get; set; } = null!; // JSON
    public int ContentSize { get; set; }
    public ResourceContentMediaType MediaType { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;

    public LanguageEntity Language { get; set; } = null!;
    public ResourceEntity Resource { get; set; } = null!;
}

public enum ResourceContentMediaType
{
    None = 0,
    Text = 1,
    Audio = 2,
    Video = 3,
    Image = 4
}