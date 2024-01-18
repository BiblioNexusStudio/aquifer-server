
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Identity.Client;

public record MonthlyAquiferiationStartsAndCompletionsResponse(
    List<StatusCountPerMonth> Starts,
    List<StatusCountPerMonth> Completions
);
public record StatusCountPerMonth
    {
        public DateTime? date { get; set; }
        public int? StatusCount { get; set; }
    }

