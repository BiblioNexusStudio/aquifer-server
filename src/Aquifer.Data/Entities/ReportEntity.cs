using Aquifer.Data.EventHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aquifer.Data.Entities;

[Index(nameof(Slug), IsUnique = true)]
[EntityTypeConfiguration(typeof(ReportEntityConfiguration))]
public class ReportEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string StoredProcedureName { get; set; } = null!; // must begin with DynamicReport
    public ReportType Type { get; set; }
    public bool Enabled { get; set; } = true;
    public string Slug { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public bool AcceptsDateRange { get; set; }
    public bool AcceptsLanguage { get; set; }
    public bool AcceptsParentResource { get; set; }

    public int? DefaultDateRangeMonths { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}

public class ReportEntityConfiguration : IEntityTypeConfiguration<ReportEntity>
{
    public void Configure(EntityTypeBuilder<ReportEntity> builder)
    {
        builder.ToTable(t => t.HasCheckConstraint("CK_StoredProcedureExists", "dbo.StoredProcedureExists(StoredProcedureName) = 1"));
        builder.ToTable(t => t.HasCheckConstraint("CK_StoredProcedureNamingConvention",
            "dbo.StoredProcedureMatchesNamingConvention(StoredProcedureName) = 1"));
    }
}

public enum ReportType
{
    None = 0,
    Table = 1,
    BarChart = 2,
    LineChart = 3
}