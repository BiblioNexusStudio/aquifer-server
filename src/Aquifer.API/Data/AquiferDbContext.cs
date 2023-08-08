using Aquifer.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Data;

public class AquiferDbContext : DbContext
{
    public DbSet<BibleEntity> Bibles { get; set; }
    public DbSet<VerseEntity> Verses { get; set; }
    public DbSet<PassageEntity> Passages { get; set; }
    public DbSet<ResourceEntity> Resources { get; set; }
    public DbSet<VerseResourceEntity> VerseResources { get; set; }
    public DbSet<PassageResourceEntity> PassageResources { get; set; }
    public DbSet<LanguageEntity> Languages { get; set; }
    public DbSet<ResourceContentEntity> ResourceContents { get; set; }
    public DbSet<SupportingResourceEntity> SupportingResources { get; set; }

    public AquiferDbContext(DbContextOptions<AquiferDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        SqlDefaultValueAttributeConvention.Apply(builder);
    }
}
