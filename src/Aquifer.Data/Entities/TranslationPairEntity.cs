using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aquifer.Data.Entities;

[EntityTypeConfiguration(typeof(TranslationPairEntityConfiguration))]
public class TranslationPairEntity
{
    public int Id { get; set; }
    public required LanguageEntity Language { get; set; }
    public required int LanguageId { get; set; }
    public required string Key { get; set; }
    public required string Value { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public required DateTime Created { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public required DateTime Updated { get; set; }
}

public class TranslationPairEntityConfiguration : IEntityTypeConfiguration<TranslationPairEntity>
{
    public void Configure(EntityTypeBuilder<TranslationPairEntity> builder)
    {
        builder.HasOne(x => x.Language).WithMany(l => l.TranslationPairs).HasForeignKey(tp => tp.LanguageId);
    }
}