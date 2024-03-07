using Aquifer.Data.EventHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aquifer.Data.Entities;

[EntityTypeConfiguration(typeof(ResourceContentVersionSnapshotsEntityConfiguration))]
public class ResourceContentVersionSnapshotsEntity
{
    public int Id { get; set; }
    public int ResourceContentVersionId { get; set; }
    public ResourceContentVersionEntity ResourceContentVersion { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int? WordCount { get; set; }
    public int? UserId { get; set; }
    public UserEntity? User { get; set; }
    public int Status { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
}

public class ResourceContentVersionSnapshotsEntityConfiguration : IEntityTypeConfiguration<ResourceContentVersionSnapshotsEntity>
{
    public void Configure(EntityTypeBuilder<ResourceContentVersionSnapshotsEntity> builder)
    {
        builder
            .HasKey(e => e.Id);

        builder
            .Property(e => e.DisplayName)
            .HasColumnType("nvarchar(max)");

        builder
            .Property(e => e.Content)
            .HasColumnType("nvarchar(max)");

        builder
            .HasOne(e => e.ResourceContentVersion)
            .WithMany()
            .HasForeignKey(e => e.ResourceContentVersionId);

        builder
            .HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .IsRequired(false);

        builder
            .Property(e => e.Created)
            .HasColumnType("datetime2");
    }
}