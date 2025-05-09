using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[Index(nameof(GreekSenseId))]
public class GreekSenseGlossEntity
{
    public int Id { get; set; }
    public int GreekSenseId { get; set; }
    public string Text { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public GreekSenseEntity GreekSense { get; set; } = null!;
}