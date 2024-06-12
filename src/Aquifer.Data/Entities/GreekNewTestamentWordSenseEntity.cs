using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aquifer.Data.Entities;

[PrimaryKey(nameof(GreekNewTestamentWordId), nameof(GreekSenseId))]
[EntityTypeConfiguration(typeof(GreekNewTestamentWordSenseEntityConfiguration))]
public class GreekNewTestamentWordSenseEntity
{
    public int GreekNewTestamentWordId { get; set; }
    public int GreekSenseId { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public GreekNewTestamentWordEntity GreekNewTestamentWord { get; set; } = null!;
    public GreekSenseEntity GreekSense { get; set; } = null!;
}

public class GreekNewTestamentWordSenseEntityConfiguration : IEntityTypeConfiguration<GreekNewTestamentWordSenseEntity>
{
    public void Configure(EntityTypeBuilder<GreekNewTestamentWordSenseEntity> builder)
    {
        builder.HasOne(p => p.GreekSense).WithMany(p => p.GreekNewTestamentWordSenses).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(p => p.GreekNewTestamentWord).WithMany(p => p.GreekNewTestamentWordSenses).OnDelete(DeleteBehavior.NoAction);
    }
}