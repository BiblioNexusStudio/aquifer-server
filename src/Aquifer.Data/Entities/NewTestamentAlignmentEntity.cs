using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[PrimaryKey(nameof(BibleVersionWordId), nameof(GreekNewTestamentWordId))]
public class NewTestamentAlignmentEntity
{
    public int BibleVersionWordId { get; set; }
    public int GreekNewTestamentWordId { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public BibleVersionWordEntity BibleVersionWord { get; set; } = null!;
    public GreekNewTestamentWordEntity GreekNewTestamentWord { get; set; } = null!;
}
