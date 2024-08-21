using System.ComponentModel.DataAnnotations.Schema;
using Aquifer.Data.EventHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aquifer.Data.Entities;

[Index(nameof(ProviderId), IsUnique = true), Index(nameof(Email), IsUnique = true),
 EntityTypeConfiguration(typeof(UserEntityConfiguration))]
public class UserEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }
    public string ProviderId { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public bool EmailVerified { get; set; }
    public UserRole Role { get; set; }
    public bool Enabled { get; set; }

    public int CompanyId { get; set; }
    public CompanyEntity Company { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    // this is not used but is needed for EF to understand the relationship from the CompanyEntity side
    [InverseProperty(nameof(CompanyEntity.DefaultReviewerUser))]
    public ICollection<CompanyEntity> CompaniesAsDefaultReviewer { get; set; } = [];
    
    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.Property(u => u.Enabled).HasDefaultValue(true);
    }
}

public enum UserRole
{
    None = 0,
    Editor = 1,
    Manager = 2,
    Publisher = 3,
    Admin = 4,
    ReportViewer = 5
}