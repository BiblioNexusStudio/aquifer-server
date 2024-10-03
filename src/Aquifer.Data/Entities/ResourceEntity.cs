using Aquifer.Data.EventHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aquifer.Data.Entities;

[Index(nameof(ExternalId))]
[Index(nameof(ParentResourceId), nameof(ExternalId), IsUnique = true)]
[EntityTypeConfiguration(typeof(ResourceEntityConfiguration))]
public class ResourceEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }
    public int ParentResourceId { get; set; }
    public ParentResourceEntity ParentResource { get; set; } = null!;
    public string EnglishLabel { get; set; } = null!;
    public string? ExternalId { get; set; }
    public int SortOrder { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public ICollection<VerseResourceEntity> VerseResources { get; set; } =
        new List<VerseResourceEntity>();

    public ICollection<PassageResourceEntity> PassageResources { get; set; } =
        new List<PassageResourceEntity>();

    public ICollection<ResourceContentEntity> ResourceContents { get; set; } =
        new List<ResourceContentEntity>();

    public ICollection<AssociatedResourceEntity> AssociatedResources { get; set; } =
        new List<AssociatedResourceEntity>();

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}

public class ResourceEntityConfiguration : IEntityTypeConfiguration<ResourceEntity>
{
    public void Configure(EntityTypeBuilder<ResourceEntity> builder)
    {
        builder.HasMany(r => r.AssociatedResources)
            .WithOne(ar => ar.Resource)
            .HasForeignKey(ar => ar.ResourceId);

        builder.Property(r => r.SortOrder).HasDefaultValue(0);
    }
}