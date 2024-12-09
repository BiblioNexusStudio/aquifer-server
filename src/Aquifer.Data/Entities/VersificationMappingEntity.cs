using Aquifer.Data.EventHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aquifer.Data.Entities;

[EntityTypeConfiguration(typeof(VersificationMappingEntityConfiguration))]
public class VersificationMappingEntity : IHasUpdatedTimestamp
{
    public int Id { get; private set; }
    public int BibleId { get; set; }
    public int BibleVerseId { get; set; }
    public int BaseVerseId { get; set; }
    public BibleEntity Bible { get; set; } = null!;
    public VerseEntity BibleVerse { get; set; } = null!;
    public VerseEntity BaseVerse { get; set; } = null!;
    public char? VerseIdPart { get; set; }
    public char? BaseVerseIdPart { get; set; }
    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}

public class VersificationMappingEntityConfiguration : IEntityTypeConfiguration<VersificationMappingEntity>
{
    public void Configure(EntityTypeBuilder<VersificationMappingEntity> builder)
    {
        builder.HasOne(p => p.BibleVerse).WithMany().OnDelete(DeleteBehavior.NoAction);
    }
}