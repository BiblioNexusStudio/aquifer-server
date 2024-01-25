namespace Aquifer.API.Modules.Reports;

public record MonthlyStartsAndCompletionsResponse(
    IEnumerable<StatusCountPerMonth> Starts,
    IEnumerable<StatusCountPerMonth> Completions
);

public record StatusCountPerMonth(
    DateTime Date,
    int StatusCount
);