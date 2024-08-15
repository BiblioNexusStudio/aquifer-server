namespace Aquifer.API.Endpoints.Reports.Monthly;

public record Response(
    IEnumerable<StatusCountPerMonth> Starts,
    IEnumerable<StatusCountPerMonth> Completions
);

public record StatusCountPerMonth(
    DateTime Date,
    int StatusCount
);