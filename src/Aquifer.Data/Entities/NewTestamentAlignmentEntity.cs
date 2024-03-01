using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aquifer.Data.Entities;

[PrimaryKey(nameof(BibleVersionWordGroupId), nameof(GreekNewTestamentWordGroupId))]
[EntityTypeConfiguration(typeof(NewTestamentAlignmentConfiguration))]
public class NewTestamentAlignmentEntity
{
    public int BibleVersionWordGroupId { get; set; }
    public int GreekNewTestamentWordGroupId { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public ICollection<GreekNewTestamentWordEntity> GreekNewTestamentWords { get; set; } = new List<GreekNewTestamentWordEntity>();
    public ICollection<BibleVersionWordEntity> BibleVersionWords { get; set; } = new List<BibleVersionWordEntity>();
}

public class NewTestamentAlignmentConfiguration : IEntityTypeConfiguration<NewTestamentAlignmentEntity>
{
    public void Configure(EntityTypeBuilder<NewTestamentAlignmentEntity> builder)
    {
        builder
            .HasMany(p => p.BibleVersionWords)
            .WithOne(c => c.NewTestamentAlignment)
            .HasForeignKey(k => k.GroupId)
            .HasPrincipalKey(p => p.BibleVersionWordGroupId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasMany(p => p.GreekNewTestamentWords)
            .WithOne(c => c.NewTestamentAlignment)
            .HasForeignKey(k => k.GroupId)
            .HasPrincipalKey(p => p.GreekNewTestamentWordGroupId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}