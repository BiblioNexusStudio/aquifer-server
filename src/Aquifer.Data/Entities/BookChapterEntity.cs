using Aquifer.Data.Enums;

namespace Aquifer.Data.Entities;

public class BookChapterEntity
{
    public int Id { get; set; }
    public BookId BookId { get; set; }
    public int Number { get; set; }
    public int MaxVerseNumber { get; set; }

    public BookEntity Book { get; set; } = null!;
}