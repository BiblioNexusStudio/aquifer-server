using Aquifer.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[Index(nameof(BibleId), nameof(BookId), nameof(ChapterNumber), nameof(VerseNumber))]
public class BibleTextEntity
{
    public int Id { get; set; }
    public int BibleId { get; set; }
    public BookId BookId { get; set; }
    public int ChapterNumber { get; set; }
    public int VerseNumber { get; set; }
    public string Text { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public BookEntity Book { get; set; } = null!;
}