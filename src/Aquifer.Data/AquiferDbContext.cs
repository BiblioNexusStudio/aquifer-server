using Aquifer.Data.Entities;
using Aquifer.Data.EventHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Aquifer.Data;

public class AquiferDbContext : DbContext
{
    private readonly DbContextOptions<AquiferDbContext> _options;

    public AquiferDbContext(DbContextOptions<AquiferDbContext> options) : base(options)
    {
        _options = options;

        // Note the issue here. I don't want to seal the class because there are situations where we want to put
        // a wrapper around it. https://www.jetbrains.com/help/resharper/VirtualMemberCallInConstructor.html
        // It's likely irrelevant anyway, because the events are additive.
        ChangeTracker.StateChanged += OnStateChange;
        SavedChanges += async (s, e) => await OnSavingChanges(s, e);
    }

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

    private static void OnStateChange(object? sender, EntityEntryEventArgs e)
    {
        UpdatedTimestampHandler.Handle(e.Entry);
    }

    private async Task OnSavingChanges(object? sender, SavedChangesEventArgs e)
    {
        var entries = ChangeTracker.Entries();
        await ProjectCompletionHandler.HandleAsync(_options, entries);
    }
}