namespace Aquifer.API.Endpoints.Reports.BarCharts.DailyResourceDownloads;

public record Request
{
    public DateOnly? StartDate { get; init; }
    public DateOnly? EndDate { get; init; }
}