namespace Aquifer.API.Modules.Reports.DailyDownloadTotals;

public record DailyDownloadTotalsResponse(
    IEnumerable<AmountPerDay> Downloads
);

public record AmountPerDay(
    DateOnly Date,
    int Amount
);

public record AmountPerDayResult(
    DateTime Date,
    int Amount
);