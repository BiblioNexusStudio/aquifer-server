using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aquifer.Data.Entities;

[Index(nameof(ResourceId))]
[PrimaryKey(nameof(AssociatedResourceId), nameof(ResourceId))]
[EntityTypeConfiguration(typeof(AssociatedResourceEntityConfiguration))]
public class AssociatedResourceEntity
{
    public int ResourceId { get; set; }
    public ResourceEntity Resource { get; set; } = null!;

    public int AssociatedResourceId { get; set; }
    public ResourceEntity AssociatedResource { get; set; } = null!;
}

public class AssociatedResourceEntityConfiguration : IEntityTypeConfiguration<AssociatedResourceEntity>
{
    public void Configure(EntityTypeBuilder<AssociatedResourceEntity> builder)
    {
        builder.HasOne(ar => ar.Resource)
            .WithMany()
            .HasForeignKey(ar => ar.ResourceId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}