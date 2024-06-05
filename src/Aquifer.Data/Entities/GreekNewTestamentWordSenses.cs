namespace Aquifer.Data.Entities;
public class GreekNewTestamentWordSenseEntity
{
    public int GreekNewTestamentWordId { get; set; }
    public int GreekSenseId { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public ICollection<GreekNewTestamentWordEntity> GreekNewTestamentWords { get; set; } = new List<GreekNewTestamentWordEntity>();  //  Ask Jesse about this
    public ICollection<GreekSenseGlossEntity> GreekSenses { get; set; } = new List<GreekSenseGlossEntity>();
}
