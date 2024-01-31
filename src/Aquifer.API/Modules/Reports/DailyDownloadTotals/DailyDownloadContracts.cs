namespace Aquifer.API.Modules.Reports.DailyDownloadTotals;

public record DailyDownloadTotalsResponse(
    IEnumerable<AmountPerDay> Downloads
);

public record AmountPerDay(
    DateTime Date,
    int Amount
);