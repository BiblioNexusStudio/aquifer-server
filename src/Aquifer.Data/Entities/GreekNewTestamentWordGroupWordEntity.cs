
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;
[PrimaryKey(nameof(GreekNewTestamentWordGroupId), nameof(GreekNewTestamentWordId))]
public class GreekNewTestamentWordGroupWordEntity
{
    public int GreekNewTestamentWordGroupId { get; set; }
    public int GreekNewTestamentWordId { get; set; }

    public GreekNewTestamentWordGroupEntity GreekNewTestamentWordGroup { get; set; } = null!;
    public GreekNewTestamentWordEntity GreekNewTestamentWord { get; set; } = null!;
}
