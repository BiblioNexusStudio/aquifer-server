using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aquifer.Data.Entities;

//[PrimaryKey(nameof(Id), nameof(GreekNewTestamentWordId))]
//[EntityTypeConfiguration(typeof(GreekNewTestamentWordGroupConfiguration))]
public class GreekNewTestamentWordGroupEntity
{
    public int Id { get; set; }
    public int GreekNewTestamentWordId { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    //public GreekNewTestamentWordEntity GreekNewTestamentWord { get; set; } = null!;
    //public NewTestamentAlignmentEntity NewTestamentAlignment { get; set; } = null!;
}

//public class GreekNewTestamentWordGroupConfiguration : IEntityTypeConfiguration<GreekNewTestamentWordGroupEntity>
//{
//    public void Configure(EntityTypeBuilder<GreekNewTestamentWordGroupEntity> builder)
//    {
//        builder.HasOne(p => p.GreekNewTestamentWord).WithOne().OnDelete(DeleteBehavior.NoAction);
//    }
//}