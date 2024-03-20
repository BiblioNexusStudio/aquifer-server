
namespace Aquifer.Data.Entities;
public class GreekNewTestamentWordGroupEntity
{
    public int Id { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public ICollection<NewTestamentAlignmentEntity> NewTestamentAlignments { get; set; } = new List<NewTestamentAlignmentEntity>();
    public ICollection<GreekNewTestamentWordGroupWordEntity> GreekNewTestamentWordGroupWords { get; set; } = new List<GreekNewTestamentWordGroupWordEntity>();

}
