using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aquifer.Data.Entities;

[Index(nameof(StartVerseId), nameof(EndVerseId), IsUnique = true)]
[EntityTypeConfiguration(typeof(PassageEntityConfiguration))]
public class PassageEntity
{
    public int Id { get; set; }
    public int StartVerseId { get; set; }
    public int EndVerseId { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; }

    public VerseEntity StartVerse { get; set; } = null!;
    public VerseEntity EndVerse { get; set; } = null!;
    public ICollection<PassageResourceEntity> PassageResources { get; set; } = new List<PassageResourceEntity>();
}

public class PassageEntityConfiguration : IEntityTypeConfiguration<PassageEntity>
{
    public void Configure(EntityTypeBuilder<PassageEntity> builder)
    {
        builder.HasOne(p => p.StartVerse).WithMany().OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(p => p.EndVerse).WithMany().OnDelete(DeleteBehavior.NoAction);
    }
}