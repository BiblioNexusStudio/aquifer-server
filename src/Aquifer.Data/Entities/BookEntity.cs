using System.ComponentModel.DataAnnotations;
using Aquifer.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[PrimaryKey(nameof(Id))]
public class BookEntity
{
    public BookId Id { get; set; }

    [MaxLength(5)]
    public string Code { get; set; } = null!;

    public ICollection<BookChapterEntity> Chapters { get; set; } = null!;
}