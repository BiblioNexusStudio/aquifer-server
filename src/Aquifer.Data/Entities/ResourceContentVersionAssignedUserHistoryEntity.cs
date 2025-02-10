using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aquifer.Data.Entities;

[EntityTypeConfiguration(typeof(ResourceContentVersionAssignedUserHistoryEntityConfiguration))]
[Index(nameof(AssignedUserId))]
[Index(nameof(ResourceContentVersionId), nameof(AssignedUserId))]
public class ResourceContentVersionAssignedUserHistoryEntity
{
    public int Id { get; set; }

    public int ResourceContentVersionId { get; set; }
    public ResourceContentVersionEntity ResourceContentVersion { get; set; } = null!;

    public int? AssignedUserId { get; set; }
    public UserEntity? AssignedUser { get; set; }

    public int ChangedByUserId { get; set; }
    public UserEntity ChangedByUser { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;
}

public class ResourceContentVersionAssignedUserHistoryEntityConfiguration
    : IEntityTypeConfiguration<ResourceContentVersionAssignedUserHistoryEntity>
{
    public void Configure(EntityTypeBuilder<ResourceContentVersionAssignedUserHistoryEntity> builder)
    {
        builder
            .HasOne(x => x.AssignedUser)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.ChangedByUser)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(e => new { e.ChangedByUserId, e.Created })
            .IncludeProperties(e => e.ResourceContentVersionId);

        builder.HasIndex(e => e.Created)
            .IncludeProperties(e => new { e.AssignedUserId, e.ResourceContentVersionId });
    }
}