using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Reports.Dynamic.List;

public record Response
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ReportType Type { get; set; }
}