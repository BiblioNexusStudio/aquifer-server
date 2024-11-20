using Microsoft.EntityFrameworkCore;
using Aquifer.Data.EventHandlers;

namespace Aquifer.Data.Entities;

[Index(nameof(LanguageId), nameof(Key), IsUnique = true)]
public class TranslationPairEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }
    public int LanguageId { get; set; }
    public string Key { get; set; } = null!;
    public string Value { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;

    public LanguageEntity Language { get; set; } = null!;
}