using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Aquifer.API.Data.Entities;

[Index(nameof(ISO6393Code), IsUnique = true)]
public class LanguageEntity
{
    public int Id { get; set; }

    [MaxLength(3)]
    public string ISO6393Code { get; set; } = null!;

    [MaxLength(255)]
    public string EnglishDisplay { get; set; } = null!;
}
