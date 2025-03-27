using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aquifer.Data.Entities;

[EntityTypeConfiguration(typeof(CommentMentionEntityConfiguration))]
public sealed class CommentMentionEntity
{
    public int Id { get; set; }

    public int CommentId { get; set; }
    public CommentEntity Comment { get; set; } = null!;

    public int UserId { get; set; }
    public UserEntity User { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}

public sealed class CommentMentionEntityConfiguration : IEntityTypeConfiguration<CommentMentionEntity>
{
    public void Configure(EntityTypeBuilder<CommentMentionEntity> builder)
    {
        // Don't delete user mentions when a user is deleted.
        // This necessary in order to prevent cascading delete cycles.
        builder
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}