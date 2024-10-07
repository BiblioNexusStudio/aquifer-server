using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Reports.Dynamic.Get;

public record Response
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required ReportType Type { get; set; }
    public required bool AcceptsDateRange { get; set; }
    public required bool AcceptsLanguage { get; set; }
    public required bool AcceptsParentResource { get; set; }
    public required bool AcceptsCompany { get; set; }
    public required DateOnly? StartDate { get; set; }
    public required DateOnly? EndDate { get; set; }
    public required IEnumerable<string> Columns { get; set; }
    public required IEnumerable<object> Results { get; set; }
}