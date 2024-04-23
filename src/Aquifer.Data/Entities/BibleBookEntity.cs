using System.ComponentModel.DataAnnotations;
using Aquifer.Data.Enums;

namespace Aquifer.Data.Entities;

public class BibleBookEntity
{
    public int Id { get; set; }
    public int BibleId { get; set; }
    public BookId Number { get; set; }

    [MaxLength(5)]
    public string Code { get; set; } = null!;

    [MaxLength(64)]
    public string LocalizedName { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public BibleEntity Bible { get; set; } = null!;
    public ICollection<BibleBookChapterEntity> Chapters { get; set; } = [];
}