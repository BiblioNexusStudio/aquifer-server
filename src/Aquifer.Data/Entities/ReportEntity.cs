using System.ComponentModel.DataAnnotations;
using Aquifer.Data.EventHandlers;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[Index(nameof(Slug), IsUnique = true)]
public class ReportEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    /// <summary>
    /// SQL query that runs to pull the report's data.
    /// For Table reports: Data is displayed as-is from the query results.
    /// For Bar Chart reports: First column represents X-axis values, second column represents Y-axis values.
    /// For Line Chart reports: First column contains X-axis values, subsequent columns represent individual lines with Y-axis values.
    /// Supports dynamic parameter substitution (e.g., @StartDate, @EndDate, @LanguageId) based on configured report acceptance settings.
    /// </summary>
    public string SqlStatement { get; set; } = null!;

    public ReportType Type { get; set; }
    public bool Enabled { get; set; } = true;
    public string Slug { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public bool AcceptsDateRange { get; set; }
    public bool AcceptsLanguage { get; set; }
    public bool AcceptsParentResource { get; set; }
    public bool AcceptsCompany { get; set; }

    public int? DefaultDateRangeMonths { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;

    [MaxLength(32)]
    public string? AllowedRoles { get; set; }
}

public enum ReportType
{
    None = 0,
    Table = 1,
    BarChart = 2,
    LineChart = 3
}