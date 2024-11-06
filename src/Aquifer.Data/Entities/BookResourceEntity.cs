using Aquifer.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[PrimaryKey(nameof(BookId), nameof(ResourceId))]
[Index(nameof(ResourceId))]
public class BookResourceEntity
{
    public BookId BookId { get; set; }
    public int ResourceId { get; set; }

    public BookEntity Book { get; set; } = null!;
    public ResourceEntity Resource { get; set; } = null!;
}