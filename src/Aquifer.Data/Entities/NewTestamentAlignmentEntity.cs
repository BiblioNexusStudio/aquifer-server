using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Aquifer.Data.Entities;

[PrimaryKey(nameof(BibleVersionWordGroupId), nameof(GreekNewTestamentWordGroupId))]
[EntityTypeConfiguration(typeof(NewTestamentAlignmentConfiguration))]
public class NewTestamentAlignmentEntity
{
    public int BibleVersionWordGroupId { get; set; }
    public int GreekNewTestamentWordGroupId { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    //public ICollection<BibleVersionWordGroupEntity> BibleVersionWordGroups { get; set; } = new List<BibleVersionWordGroupEntity>();
    //public ICollection<GreekNewTestamentWordGroupEntity> GreekNewTestamentWordGroups { get; set; } = new List<GreekNewTestamentWordGroupEntity>();

    public ICollection<GreekNewTestamentWordEntity> GreekNewTestamentWords { get; set; } = new List<GreekNewTestamentWordEntity>();
    public ICollection<BibleVersionWordEntity> BibleVersionWords { get; set; } = new List<BibleVersionWordEntity>();

}

public class NewTestamentAlignmentConfiguration : IEntityTypeConfiguration<NewTestamentAlignmentEntity>
{
    public void Configure(EntityTypeBuilder<NewTestamentAlignmentEntity> builder)
    {
        //builder.HasOne(p => p.GreekNewTestamentWord).WithOne().OnDelete(DeleteBehavior.NoAction);

        //builder
        //    .HasMany(x => x.GreekNewTestamentWords)
        //    .WithOne(y => y.NewTestamentAlignment)
        //    .UsingEntity(
        //        "ProjectResourceContents",
        //        l => l.HasOne(typeof(ResourceContentEntity)).WithMany().HasForeignKey("ResourceContentId")
        //            .HasPrincipalKey(nameof(ResourceContentEntity.Id)).OnDelete(DeleteBehavior.Restrict),
        //        r => r.HasOne(typeof(ProjectEntity)).WithMany().HasForeignKey("ProjectId").HasPrincipalKey(nameof(ProjectEntity.Id))
        //            .OnDelete(DeleteBehavior.Restrict),
        //        j =>
        //        {
        //            j.HasKey("ProjectId", "ResourceContentId");
        //            j.HasIndex("ResourceContentId").IsUnique();
        //        });

        //builder.Entity<NewTestamentAlignmentEntity>()
        //.HasKey(p => p.Id);

        //builder.Entity<Child>()
        //    .HasKey(c => c.Id);

        // Foreign key relationship
        builder
            .HasMany(p => p.GreekNewTestamentWords)
            .WithOne(c => c.NewTestamentAlignment)
            .HasForeignKey(c => c.GroupId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasMany(p => p.BibleVersionWords)
            .WithOne(c => c.NewTestamentAlignment)
            .HasForeignKey(c => c.GroupId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}