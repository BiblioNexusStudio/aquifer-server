namespace Aquifer.API.Modules.Reports;

public record MonthlyAquiferiationStartsAndCompletionsResponse(
    IEnumerable<StatusCountPerMonth> Starts,
    IEnumerable<StatusCountPerMonth> Completions
);

public record StatusCountPerMonth(
    DateTime Date,
    int StatusCount
);