namespace Aquifer.Data.Entities;

public class HelpDocumentEntity
{
    public int Id { get; set; }
    public required HelpDocumentType Type { get; set; }
    public required string Title { get; set; }
    public required string Url { get; set; }
    
    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; }
}

public enum HelpDocumentType
{
    None = 0,
    Release = 1,
    HowTo = 2
}