using Aquifer.Data.Entities;
using Aquifer.Data.EventHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Aquifer.Data;

public class AquiferDbContext : DbContext
{
    private readonly DbContextOptions _options;

    public AquiferDbContext(DbContextOptions<AquiferDbContext> options) : this(options, false)
    {
    }

    public AquiferDbContext(DbContextOptions options, bool isReadOnly) : base(options)
    {
        _options = options;

        IsReadOnly = isReadOnly;

        if (!isReadOnly)
        {
            // Note the issue here. I don't want to seal the class because there are situations where we want to put
            // a wrapper around it. https://www.jetbrains.com/help/resharper/VirtualMemberCallInConstructor.html
            // It's likely irrelevant anyway, because the events are additive.
            // ReSharper disable once VirtualMemberCallInConstructor
            ChangeTracker.StateChanged += OnStateChange;

            // Using an async lambda for a void returning method could crash the process if there are any exceptions in this event.
            // If a crash occurs then we should look into changing this based on https://stackoverflow.com/a/43187621.
#pragma warning disable VSTHRD101
            SavedChanges += async (s, e) => await OnSavedChangesAsync(s, e);
#pragma warning restore VSTHRD101

            SavingChanges += OnSavingChanges;
        }
    }

    public bool IsReadOnly { get; }

    public DbSet<ApiKeyEntity> ApiKeys { get; set; }
    public DbSet<AssociatedResourceEntity> AssociatedResources { get; set; }
    public DbSet<BibleBookContentEntity> BibleBookContents { get; set; }
    public DbSet<BibleEntity> Bibles { get; set; }
    public DbSet<BibleTextEntity> BibleTexts { get; set; }
    public DbSet<BibleVersionWordEntity> BibleVersionWords { get; set; }
    public DbSet<BibleVersionWordGroupEntity> BibleVersionWordGroups { get; set; }
    public DbSet<BibleVersionWordGroupWordEntity> BibleVersionWordGroupWords { get; set; }
    public DbSet<BookChapterEntity> BookChapters { get; set; }
    public DbSet<BookChapterResourceEntity> BookChapterResources { get; set; }
    public DbSet<BookEntity> Books { get; set; }
    public DbSet<BookResourceEntity> BookResources { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<CommentMentionEntity> CommentMentions { get; set; }
    public DbSet<CommentThreadEntity> CommentThreads { get; set; }
    public DbSet<CompanyEntity> Companies { get; set; }
    public DbSet<CompanyReviewerEntity> CompanyReviewers { get; set; }
    public DbSet<ContentSubscriberEntity> ContentSubscribers { get; set; }
    public DbSet<ContentSubscriberLanguageEntity> ContentSubscriberLanguages { get; set; }
    public DbSet<ContentSubscriberParentResourceEntity> ContentSubscriberParentResources { get; set; }
    public DbSet<EmailTemplateEntity> EmailTemplates { get; set; }
    public DbSet<FeedbackEntity> Feedback { get; set; }
    public DbSet<GreekLemmaEntity> GreekLemmas { get; set; }
    public DbSet<GreekNewTestamentEntity> GreekNewTestaments { get; set; }
    public DbSet<GreekNewTestamentWordEntity> GreekNewTestamentWords { get; set; }
    public DbSet<GreekNewTestamentWordGroupEntity> GreekNewTestamentWordGroups { get; set; }
    public DbSet<GreekNewTestamentWordGroupWordEntity> GreekNewTestamentWordGroupWords { get; set; }
    public DbSet<GreekNewTestamentWordSenseEntity> GreekNewTestamentWordSenses { get; set; }
    public DbSet<GreekSenseEntity> GreekSenses { get; set; }
    public DbSet<GreekSenseGlossEntity> GreekSenseGlosses { get; set; }
    public DbSet<GreekWordEntity> GreekWords { get; set; }
    public DbSet<HelpDocumentEntity> HelpDocuments { get; set; }
    public DbSet<IpAddressDataEntity> IpAddressData { get; set; }
    public DbSet<JobHistoryEntity> JobHistory { get; set; }
    public DbSet<LanguageEntity> Languages { get; set; }
    public DbSet<NewTestamentAlignmentEntity> NewTestamentAlignments { get; set; }
    public DbSet<NotificationEntity> Notifications { get; set; }
    public DbSet<ParentResourceEntity> ParentResources { get; set; }
    public DbSet<ParentResourceLocalizationEntity> ParentResourceLocalizations { get; set; }
    public DbSet<PassageEntity> Passages { get; set; }
    public DbSet<PassageResourceEntity> PassageResources { get; set; }
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<ProjectPlatformEntity> ProjectPlatforms { get; set; }
    public DbSet<ProjectResourceContentEntity> ProjectResourceContents { get; set; }
    public DbSet<ReportEntity> Reports { get; set; }
    public DbSet<ResourceContentEntity> ResourceContents { get; set; }
    public DbSet<ResourceContentRequestEntity> ResourceContentRequests { get; set; }
    public DbSet<ResourceContentVersionAssignedUserHistoryEntity> ResourceContentVersionAssignedUserHistory { get; set; }
    public DbSet<ResourceContentVersionCommentThreadEntity> ResourceContentVersionCommentThreads { get; set; }
    public DbSet<ResourceContentVersionEditTimeEntity> ResourceContentVersionEditTimes { get; set; }
    public DbSet<ResourceContentVersionEntity> ResourceContentVersions { get; set; }
    public DbSet<ResourceContentVersionFeedbackEntity> ResourceContentVersionFeedback { get; set; }
    public DbSet<ResourceContentVersionMachineTranslationEntity> ResourceContentVersionMachineTranslations { get; set; }
    public DbSet<ResourceContentVersionSimilarityScoreEntity> ResourceContentVersionSimilarityScores { get; set; }
    public DbSet<ResourceContentVersionSnapshotEntity> ResourceContentVersionSnapshots { get; set; }
    public DbSet<ResourceContentVersionStatusHistoryEntity> ResourceContentVersionStatusHistory { get; set; }
    public DbSet<ResourceEntity> Resources { get; set; }
    public DbSet<StrongNumberEntity> StrongNumbers { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<VerseEntity> Verses { get; set; }
    public DbSet<VerseResourceEntity> VerseResources { get; set; }
    public DbSet<VersificationMappingEntity> VersificationMappings { get; set; }
    public DbSet<VersificationExclusionEntity> VersificationExclusions { get; set; }
    public DbSet<TranslationPairEntity> TranslationPairs { get; set; }
    public DbSet<UploadEntity> Uploads { get; set; }
    public DbSet<CompanyLanguageEntity> CompanyLanguages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        SqlDefaultValueAttributeConvention.Apply(builder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove(typeof(ForeignKeyIndexConvention));
    }

    private static void OnStateChange(object? sender, EntityEntryEventArgs e)
    {
        UpdatedTimestampHandler.Handle(e.Entry);
    }

    private async Task OnSavedChangesAsync(object? sender, SavedChangesEventArgs e)
    {
        var entries = ChangeTracker.Entries();
        await ResourceStatusChangeHandler.HandleAsync(_options, entries);
    }

    private void OnSavingChanges(object? sender, SavingChangesEventArgs e)
    {
        var entries = ChangeTracker.Entries();
        BadTranslationPairHandler.Handle(entries);
    }
}