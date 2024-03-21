using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[PrimaryKey(nameof(BibleVersionWordGroupId), nameof(GreekNewTestamentWordGroupId))]
public class NewTestamentAlignmentEntity
{
    public int BibleVersionWordGroupId { get; set; }
    public int GreekNewTestamentWordGroupId { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public BibleVersionWordGroupEntity BibleVersionWordGroup { get; set; } = null!;
    public GreekNewTestamentWordGroupEntity GreekNewTestamentWordGroup { get; set; } = null!;
}