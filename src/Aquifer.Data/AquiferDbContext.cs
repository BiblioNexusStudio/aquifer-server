using Aquifer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data;

public class AquiferDbContext : DbContext
{
    public AquiferDbContext(DbContextOptions<AquiferDbContext> options)
        : base(options)
    {
    }

    public DbSet<BibleEntity> Bibles { get; set; }
    public DbSet<BibleBookContentEntity> BibleBookContents { get; set; }
    public DbSet<VerseEntity> Verses { get; set; }
    public DbSet<PassageEntity> Passages { get; set; }
    public DbSet<ResourceEntity> Resources { get; set; }
    public DbSet<VerseResourceEntity> VerseResources { get; set; }
    public DbSet<PassageResourceEntity> PassageResources { get; set; }
    public DbSet<LanguageEntity> Languages { get; set; }
    public DbSet<ResourceContentEntity> ResourceContents { get; set; }
    public DbSet<ParentResourceEntity> ParentResources { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        SqlDefaultValueAttributeConvention.Apply(builder);
    }
}
