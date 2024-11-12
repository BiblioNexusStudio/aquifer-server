namespace Aquifer.Data.Entities;

public class TranslationPairEntity
{
    public int Id { get; set; }
    public LanguageEntity Language { get; set; } = null!;
    public int LanguageId { get; set; }
    public string Key { get; set; } = null!;
    public string Value { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; }
}