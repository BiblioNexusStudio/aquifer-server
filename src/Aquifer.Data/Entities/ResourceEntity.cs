using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aquifer.Data.Entities;

[Index(nameof(TypeId),
    nameof(EnglishLabel),
    IsUnique = true)]
[EntityTypeConfiguration(typeof(ResourceEntityConfiguration))]
public class ResourceEntity
{
    public int Id { get; set; }
    public int TypeId { get; set; }
    public ResourceTypeEntity Type { get; set; } = null!;
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
}

public class ResourceEntityConfiguration : IEntityTypeConfiguration<ResourceEntity>
{
    public void Configure(EntityTypeBuilder<ResourceEntity> builder)
    {
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