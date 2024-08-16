namespace Aquifer.API.Endpoints.Reports.BarCharts.DailyResourceDownloads;

public record Response
{
    public int Amount { get; set; }
    public DateTime Date { get; init; }
}