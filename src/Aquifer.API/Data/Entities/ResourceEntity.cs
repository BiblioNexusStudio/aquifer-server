using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aquifer.API.Data.Entities;

[Index(nameof(Type), nameof(MediaType), nameof(EnglishLabel), IsUnique = true)]
[EntityTypeConfiguration(typeof(ResourceEntityConfiguration))]
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

    public ICollection<ResourceEntity> SupportingResources { get; set; } =
        new List<ResourceEntity>();

    public ICollection<ResourceEntity> SupportsResources { get; set; } =
        new List<ResourceEntity>();
}

public class ResourceEntityConfiguration : IEntityTypeConfiguration<ResourceEntity>
{
    public void Configure(EntityTypeBuilder<ResourceEntity> builder)
    {
        builder.HasMany(e => e.SupportingResources)
            .WithMany(j => j.SupportsResources)
            .UsingEntity<Dictionary<string, object>>(
                "SupportingResources",
                j => j
                    .HasOne<ResourceEntity>()
                    .WithMany()
                    .HasForeignKey("SupportingResourceId"),
                j => j
                    .HasOne<ResourceEntity>()
                    .WithMany()
                    .HasForeignKey("ParentResourceId"));
    }
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