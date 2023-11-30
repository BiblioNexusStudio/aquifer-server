using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aquifer.Data.Entities;

[EntityTypeConfiguration(typeof(ResourceContentUserAssignmentEntityConfiguration))]
public class ResourceContentUserAssignmentEntity
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

public class ResourceContentUserAssignmentEntityConfiguration
    : IEntityTypeConfiguration<ResourceContentUserAssignmentEntity>
{
    public void Configure(EntityTypeBuilder<ResourceContentUserAssignmentEntity> builder)
    {
        builder
            .HasIndex(x => x.ResourceContentId)
            .HasFilter($"{nameof(ResourceContentUserAssignmentEntity.Completed)} IS NOT NULL")
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
