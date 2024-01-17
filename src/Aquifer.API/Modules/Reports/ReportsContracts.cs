
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Identity.Client;

public record MonthlyAquiferiationStartsAndCompletionsResponse(
    List<StatusCountPerMonth> Starts,
    List<StatusCountPerMonth> Completions
);
public record MonthlyAquiferizationTotalsByMonthResponse(
    DateOnly Date,
    string MonthAbbreviation,
    int ResourceCount);

public record StatusCountPerMonth
    {
        public string? Month { get; set; }
        public int? StatusCount { get; set; }
    }

