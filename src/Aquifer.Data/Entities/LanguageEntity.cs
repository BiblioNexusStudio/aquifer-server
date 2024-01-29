using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[Index(nameof(ISO6393Code), IsUnique = true)]
public class LanguageEntity
{
    public int Id { get; set; }

    [MaxLength(3)]
    public string ISO6393Code { get; set; } = null!;

    [MaxLength(255)]
    public string EnglishDisplay { get; set; } = null!;

    [MaxLength(255)]
    public string DisplayName { get; set; } = null!;

    public bool Enabled { get; set; }

    public ScriptDirection ScriptDirection { get; set; }
}

public enum ScriptDirection
{
    None = 0,
    LTR = 1,
    RTL = 2
}