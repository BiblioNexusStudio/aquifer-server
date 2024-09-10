using System.ComponentModel.DataAnnotations.Schema;

namespace Aquifer.Data.Entities;

public class EmailTemplateEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public int LanguageId { get; set; }
    public LanguageEntity Language { get; set; } = null!;
    public string Template { get; set; } = null!;
    public string Subject { get; set; } = null!;
    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;
    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}