
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;
[PrimaryKey(nameof(BibleVersionWordGroupId), nameof(BibleVersionWordId))]
public class BibleVersionWordGroupWordEntity
{
    public int BibleVersionWordGroupId { get; set; }
    public int BibleVersionWordId { get; set; }

    public BibleVersionWordGroupEntity BibleVersionWordGroup { get; set; } = null!;
    public BibleVersionWordEntity BibleVersionWord { get; set; } = null!;
}
