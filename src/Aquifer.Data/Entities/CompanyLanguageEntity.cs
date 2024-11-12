using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[PrimaryKey(nameof(CompanyId), nameof(LanguageId))]
public class CompanyLanguageEntity
{
    public int CompanyId { get; set; }
    public int LanguageId { get; set; }
    public CompanyEntity Company { get; set; } = null!;
    public LanguageEntity Language { get; set; } = null!;
}