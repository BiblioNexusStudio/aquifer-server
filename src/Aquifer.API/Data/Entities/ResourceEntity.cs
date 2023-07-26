namespace Aquifer.API.Data.Entities;

public class ResourceEntity
{
    public int Id { get; set; }
    public ResourceEntityType Type { get; set; }
    public required string EnglishLabel { get; set; }
    public string? Tag { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime Updated { get; set; } = DateTime.UtcNow;

    public PassageResourceEntity? PassageResource { get; set; }
    public ResourceContentEntity ResourceContent { get; set; } = null!;

    public ICollection<SupportingResourceEntity> SupportingResources { get; set; } = new List<SupportingResourceEntity>();
    public SupportingResourceEntity? SupportingResource { get; set; }
}

public enum ResourceEntityType
{
    None = 0,
    CBBTER = 1,
}
