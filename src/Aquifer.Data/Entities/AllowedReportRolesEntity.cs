using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[Index(
    nameof(ReportId),
    nameof(Role),
    IsUnique = true)]
public class AllowedReportRolesEntity
{
    public int Id { get; set; }
    public int ReportId { get; set; }
    public ReportEntity Report { get; set; } = null!;
    public UserRole Role { get; set; }
}