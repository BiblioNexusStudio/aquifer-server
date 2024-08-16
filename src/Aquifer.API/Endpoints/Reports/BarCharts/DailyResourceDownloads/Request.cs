namespace Aquifer.API.Endpoints.Reports.BarCharts.DailyResourceDownloads;

public record Request
{
    public DateOnly StartDate { get; init; } = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-3));
    public DateOnly EndDate { get; init; } = DateOnly.FromDateTime(DateTime.UtcNow);
}