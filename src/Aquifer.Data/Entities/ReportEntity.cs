using Aquifer.Data.EventHandlers;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[Index(nameof(Slug), IsUnique = true)]
public class ReportEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string SqlStatement { get; set; } = null!;
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

public enum ReportType
{
    None = 0,
    Table = 1,
    BarChart = 2,
    LineChart = 3
}