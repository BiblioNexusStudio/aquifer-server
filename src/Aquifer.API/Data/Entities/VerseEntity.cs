using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aquifer.API.Data.Entities;

[PrimaryKey(nameof(Id), nameof(BibleId))]
public class VerseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public int BibleId { get; set; }
    public string Content { get; set; } = null!;
    public float? AudioStartTime { get; set; }
    public float? AudioEndTime { get; set; }
    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;
    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;

    public ICollection<VerseResourceEntity> VerseResources { get; set; } = new List<VerseResourceEntity>();
    public BibleEntity Bible { get; set; } = null!;
}
