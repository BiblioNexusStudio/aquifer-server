namespace Aquifer.API.Modules.Reports.DailyDownloadTotals;

public record DailyDownloadTotalsResponse(
    IEnumerable<AmountPerDay> Downloads
);

public class AmountPerDay
{
    public int Amount { get; set; }
    public DateTime DateValue { get; init; }
    public DateOnly Date => DateOnly.FromDateTime(DateValue);
}