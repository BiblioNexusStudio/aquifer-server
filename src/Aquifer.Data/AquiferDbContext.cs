using Aquifer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data;

public class AquiferDbContext(DbContextOptions<AquiferDbContext> options) : DbContext(options)
{
    public DbSet<BibleBookContentEntity> BibleBookContents { get; set; }
    public DbSet<BibleEntity> Bibles { get; set; }
    public DbSet<CompanyEntity> Companies { get; set; }
    public DbSet<LanguageEntity> Languages { get; set; }
    public DbSet<ParentResourceEntity> ParentResources { get; set; }
    public DbSet<PassageEntity> Passages { get; set; }
    public DbSet<PassageResourceEntity> PassageResources { get; set; }
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<ProjectPlatformEntity> ProjectPlatforms { get; set; }
    public DbSet<ResourceContentEntity> ResourceContents { get; set; }
    public DbSet<ResourceContentRequestEntity> ResourceContentRequests { get; set; }
    public DbSet<ResourceContentVersionAssignedUserHistoryEntity> ResourceContentVersionAssignedUserHistory { get; set; }
    public DbSet<ResourceContentVersionEntity> ResourceContentVersions { get; set; }
    public DbSet<ResourceContentVersionStatusHistoryEntity> ResourceContentVersionStatusHistory { get; set; }
    public DbSet<ResourceEntity> Resources { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<VerseEntity> Verses { get; set; }
    public DbSet<VerseResourceEntity> VerseResources { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        SqlDefaultValueAttributeConvention.Apply(builder);
    }
}