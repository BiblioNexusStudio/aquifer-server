using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aquifer.Data.Entities;

[PrimaryKey(nameof(Id), nameof(BibleVersionWordId))]
//[EntityTypeConfiguration(typeof(BibleVersionWordGroupConfiguration))]
public class BibleVersionWordGroupEntity
{
    public int Id { get; set; }
    public int BibleVersionWordId { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    //public BibleVersionWordEntity BibleVersionWord { get; set; } = null!;
    //public NewTestamentAlignmentEntity NewTestamentAlignment { get; set; } = null!;

}
//public class BibleVersionWordGroupConfiguration : IEntityTypeConfiguration<BibleVersionWordGroupEntity>
//{
//    public void Configure(EntityTypeBuilder<BibleVersionWordGroupEntity> builder)
//    {
//        builder.HasOne(p => p.BibleVersionWord).WithOne().OnDelete(DeleteBehavior.NoAction);
//    }
//}
