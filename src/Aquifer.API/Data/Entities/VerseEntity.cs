using System.ComponentModel.DataAnnotations.Schema;

namespace Aquifer.API.Data.Entities;

public class VerseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public int BibleId { get; set; }
    public string Content { get; set; } = null!;
    public float? AudioStartTime { get; set; }
    public float? AudioEndTime { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}
