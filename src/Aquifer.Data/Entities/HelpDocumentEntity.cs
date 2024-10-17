namespace Aquifer.Data.Entities;

public class HelpDocumentEntity
{
    public int Id { get; set; }
    public HelpDocumentType Type { get; set; }
    public string Title { get; set; } = null!;
    public string Url { get; set; } = null!;
    public bool Enabled { get; set; }
    
    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}

public enum HelpDocumentType
{
    None = 0,
    Release = 1,
    HowTo = 2
}