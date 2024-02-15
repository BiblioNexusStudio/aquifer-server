using Aquifer.Data.EventHandlers;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[Index(nameof(ProviderId), IsUnique = true), Index(nameof(Email), IsUnique = true)]
public class UserEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }
    public string ProviderId { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public bool EmailVerified { get; set; }
    public UserRole Role { get; set; }

    public int CompanyId { get; set; }
    public CompanyEntity Company { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}

public enum UserRole
{
    None = 0,
    Editor = 1,
    Manager = 2,
    Publisher = 3,
    Admin = 4
}