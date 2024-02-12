using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aquifer.Data.Entities;

[Index(nameof(ParentResourceId),
     nameof(ExternalId),
     IsUnique = true), EntityTypeConfiguration(typeof(ResourceEntityConfiguration))]
public class ResourceEntity
{
    public int Id { get; set; }
    public int ParentResourceId { get; set; }
    public ParentResourceEntity ParentResource { get; set; } = null!;
    public string EnglishLabel { get; set; } = null!;
    public string? ExternalId { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

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

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
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