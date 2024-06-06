using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[PrimaryKey(nameof(GreekNewTestamentWordId), nameof(GreekSenseId))]
public class GreekNewTestamentWordSenseEntity
{
    public int GreekNewTestamentWordId { get; set; }
    public int GreekSenseId { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public GreekNewTestamentWordEntity GreekNewTestamentWord { get; set; } = null!;
    public GreekSenseEntity GreekSense { get; set; } = null!;
}
