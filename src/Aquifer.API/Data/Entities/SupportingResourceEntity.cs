using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aquifer.API.Data.Entities;

[PrimaryKey(nameof(ParentResourceId), nameof(SupportingResourceId))]
[EntityTypeConfiguration(typeof(SupportingResourceEntityConfiguration))]
public class SupportingResourceEntity
{
    public int ParentResourceId { get; set; }
    public int SupportingResourceId { get; set; }

    public ResourceEntity ParentResource { get; set; } = null!;
    public ResourceEntity SupportingResource { get; set; } = null!;
}

public class SupportingResourceEntityConfiguration : IEntityTypeConfiguration<SupportingResourceEntity>
{
    public void Configure(EntityTypeBuilder<SupportingResourceEntity> builder)
    {
        builder.HasOne(x => x.ParentResource).WithMany(x => x.SupportingResources).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.SupportingResource).WithOne(x => x.SupportingResource).OnDelete(DeleteBehavior.NoAction);
    }
}
