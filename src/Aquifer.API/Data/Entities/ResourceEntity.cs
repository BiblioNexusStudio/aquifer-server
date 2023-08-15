using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Data.Entities;

[Index(nameof(Type), nameof(MediaType), nameof(EnglishLabel), IsUnique = true)]
public class ResourceEntity
{
    public int Id { get; set; }
    public ResourceEntityType Type { get; set; }
    public ResourceEntityMediaType MediaType { get; set; }
    public string EnglishLabel { get; set; } = null!;
    public string? Tag { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;

    public ICollection<VerseResourceEntity> VerseResources { get; set; } =
        new List<VerseResourceEntity>();

    public ICollection<PassageResourceEntity> PassageResources { get; set; } =
        new List<PassageResourceEntity>();

    public ICollection<ResourceContentEntity> ResourceContents { get; set; } =
        new List<ResourceContentEntity>();

    public ICollection<SupportingResourceEntity> SupportingResources { get; set; } =
        new List<SupportingResourceEntity>();

    public SupportingResourceEntity? SupportingResource { get; set; }
}

public enum ResourceEntityType
{
    None = 0,
    CBBTER = 1
}

public enum ResourceEntityMediaType
{
    None = 0,
    Text = 1,
    Audio = 2,
    Video = 3,
    Image = 4
}