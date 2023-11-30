using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[Index(nameof(ProviderId), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
public class UserEntity
{
    public int Id { get; set; }
    public string ProviderId { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public ICollection<ResourceContentUserAssignmentEntity> ResourceContentAssignments { get; set; } =
        new List<ResourceContentUserAssignmentEntity>();

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}
