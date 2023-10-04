using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aquifer.Data.Entities;

[Index(nameof(Type),
    nameof(EnglishLabel),
    IsUnique = true)]
[EntityTypeConfiguration(typeof(ResourceEntityConfiguration))]
public class ResourceEntity
{
    public int Id { get; set; }
    public ResourceEntityType Type { get; set; }
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

    public ICollection<ResourceEntity> AssociatedResourceChildren { get; set; } =
        new List<ResourceEntity>();

    public ICollection<ResourceEntity> AssociatedResourceParents { get; set; } =
        new List<ResourceEntity>();

    public ICollection<ResourceEntity> SupportingResources { get; set; } =
        new List<ResourceEntity>();

    public ICollection<ResourceEntity> ResourcesSupported { get; set; } =
        new List<ResourceEntity>();
}

public class ResourceEntityConfiguration : IEntityTypeConfiguration<ResourceEntity>
{
    public void Configure(EntityTypeBuilder<ResourceEntity> builder)
    {
        builder.HasMany(e => e.SupportingResources)
            .WithMany(j => j.ResourcesSupported)
            .UsingEntity("SupportingResources",
                j => j
                    .HasOne(typeof(ResourceEntity))
                    .WithMany()
                    .HasForeignKey("SupportingResourceId"),
                j => j
                    .HasOne(typeof(ResourceEntity))
                    .WithMany()
                    .HasForeignKey("ParentResourceId"));

        builder.HasMany(e => e.AssociatedResourceChildren)
            .WithMany(j => j.AssociatedResourceParents)
            .UsingEntity("AssociatedResources",
                j => j
                    .HasOne(typeof(ResourceEntity))
                    .WithMany()
                    .HasForeignKey("AssociatedResourceId"),
                j => j
                    .HasOne(typeof(ResourceEntity))
                    .WithMany()
                    .HasForeignKey("ResourceId"));
    }
}

public enum ResourceEntityType
{
    None = 0,
    CBBTER = 1,
    TyndaleBibleDictionary = 2,
    UbsImage = 3,
}