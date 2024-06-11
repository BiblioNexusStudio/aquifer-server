namespace Aquifer.Data.Entities;
public class GreekSenseGlossEntity
{
    public int Id { get; set; }
    public int GreekSenseId { get; set; }
    public string Text { get; set; } = null!;
    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public GreekSenseEntity GreekSense { get; set; } = null!;
}
