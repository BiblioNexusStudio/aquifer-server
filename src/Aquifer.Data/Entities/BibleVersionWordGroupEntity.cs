namespace Aquifer.Data.Entities;

public class BibleVersionWordGroupEntity
{
    public int Id { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public ICollection<NewTestamentAlignmentEntity> NewTestamentAlignments { get; set; } = [];
    public ICollection<BibleVersionWordGroupWordEntity> BibleVersionWordGroupWords { get; set; } = [];
}