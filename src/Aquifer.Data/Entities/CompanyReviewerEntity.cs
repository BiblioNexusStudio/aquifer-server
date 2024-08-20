using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[Index(nameof(UserId), nameof(CompanyId), nameof(LanguageId), IsUnique = true)]
public class CompanyReviewerEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CompanyId { get; set; }
    public int LanguageId { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public UserEntity User { get; set; } = null!;
    public CompanyEntity Company { get; set; } = null!;
    public LanguageEntity Language { get; set; } = null!;
}