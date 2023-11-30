using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aquifer.Data.Entities;

[EntityTypeConfiguration(typeof(UserResourceContentAssignmentEntityConfiguration))]
public class UserResourceContentAssignmentEntity
{
    public int Id { get; set; }

    public int ResourceContentId { get; set; }
    public ResourceContentEntity ResourceContent { get; set; } = null!;

    public int AssignedUserId { get; set; }
    public UserEntity AssignedUser { get; set; } = null!;

    public int AssignedByUserId { get; set; }
    public UserEntity AssignedByUser { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public DateTime? Completed { get; set; }
}

public class UserResourceContentAssignmentEntityConfiguration
    : IEntityTypeConfiguration<UserResourceContentAssignmentEntity>
{
    public void Configure(EntityTypeBuilder<UserResourceContentAssignmentEntity> builder)
    {
        builder
            .HasIndex(x => x.ResourceContentId)
            .HasFilter($"{nameof(UserResourceContentAssignmentEntity.Completed)} IS NOT NULL")
            .IsUnique();

        builder
            .HasOne(x => x.AssignedUser)
            .WithMany(x => x.ResourceContentAssignments)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.AssignedByUser)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
    }
}
